using Microsoft.Extensions.Configuration;
using OpenDataSigAPI.Data.Repositories;
using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request;
using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response;
using Services.Functions.ContenedoresBasura;
using Services.Functions.Desfibriladores;
using Services.Functions.Farmacias;
using Services.Functions.PlazasMovilidadReducida;
using Services.OpenAi;
using Shared.OpenDataSig;
using static Shared.Constants;
using SubmitToolOutputs = OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request.SubmitToolOutputs;

namespace OpenDataSigAPI.Services.OpenDataSig
{
    public class OpenDataSigService : IOpenDataSigService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOpenAiService _openAiService;
        private readonly IFarmaciasService _farmaciasService;
        private readonly IDesfibriladoresService _desfibriladoresService;
        private readonly IPlazasMovilidadReducidaService _plazasMovilidadReducidaService;
        private readonly IContenedoresBasuraService _contenedoresBasuraService;

        public OpenDataSigService(IConfiguration configuration, IOpenAiService openAiService, IFarmaciasService farmaciasService, IDesfibriladoresService desfibriladoresService, IPlazasMovilidadReducidaService plazasMovilidadReducidaService, IContenedoresBasuraService contenedoresBasuraService, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _openAiService = openAiService;
            _farmaciasService = farmaciasService;
            _desfibriladoresService = desfibriladoresService;
            _plazasMovilidadReducidaService = plazasMovilidadReducidaService;
            _contenedoresBasuraService = contenedoresBasuraService;
            _unitOfWork = unitOfWork;
        }

        public async Task<RespuestaMensajeOpenDataSig> ManageMessageUi(string message, string? threadId)
        {
            // COMPROBACIONES
            if (string.IsNullOrWhiteSpace(message))
            {
                await _unitOfWork.Logs.LogError(this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, TiposErrores.MENSAJE_CLIENTE_VACIO, string.Empty, "usertest3"); // Usuario de pruebas Marcos
                return new RespuestaMensajeOpenDataSig { Mensaje = "El mensaje llega vacío. Consulte con su administrador." };
            }

            // PROCESAMIENTO
            var toolOutput = string.Empty;
            Shared.Models.OpenAi.Assistant.Response.Run runResponse;
            bool isNewThread = string.IsNullOrWhiteSpace(threadId);
            decimal userId = 42;  // Usuario de pruebas Marcos
            decimal agentId = 11; // OpenDataSig 
            decimal threadIdDB = 0;

            if (isNewThread)
            {
                runResponse = await CreateThreadAndRun(message, _configuration["ModelosOpenAi:gpt-4o-mini"], _configuration["IdAssistant"]);
                threadId = runResponse.ThreadId;
            }
            else
            {
                threadIdDB = await _unitOfWork.Threads.GetThreadIdByIdThread(threadId);
                runResponse = await CreateMessageAndRun(message, _configuration["ModelosOpenAi:gpt-4o-mini"], _configuration["IdAssistant"], threadId);

            }

            // Espero a que se procese la consulta
            await checkResponseStatus(runResponse, threadId, userId, agentId, threadIdDB);


            var mensajes = await _openAiService.ListMessageAsync(threadId, null, null, null, null);

            return new RespuestaMensajeOpenDataSig { Mensaje = mensajes.Messages[0].Content[0].Text.Value, ThreadId = runResponse.ThreadId };
        }

        private async Task checkResponseStatus(Shared.Models.OpenAi.Assistant.Response.Run runResponse, string threadId, decimal userId, decimal agentId, decimal threadIdDB)
        {
            int reintentos = 30;
            int delay = 1000;

            while (!runResponse.Status.Equals("completed") && reintentos > 0)
            {
                Console.WriteLine(runResponse.Status);
                await Task.Delay(delay);
                reintentos--;

                if (reintentos % 10 == 0)
                    delay = Math.Min(delay * 2, 5000); // Maximo 5 segundos

                runResponse = await _openAiService.RetrieveRunAsync(runResponse.ThreadId, runResponse.Id);

                if (runResponse.Status.Equals("requires_action"))
                {
                    var toolResponses = new List<ToolOutputs>();

                    foreach (var toolCall in runResponse.RequiredAction.SubmitToolOutputs.ToolCalls)
                    {
                        string toolResponse = string.Empty;

                        switch (toolCall.Function.Name)
                        {
                            case "Farmacias":
                                var farmaciasResponse = await _farmaciasService.GetFarmaciasAsync();
                                toolResponse = _farmaciasService.ParseData(farmaciasResponse);
                                break;

                            case "Desfibriladores":
                                var desfibriladoresResponse = await _desfibriladoresService.GetDesfibriladoresAsync();
                                toolResponse = _desfibriladoresService.ParseData(desfibriladoresResponse);
                                break;

                            case "Plazas_Movilidad_Reducida":
                                var plazasMovilidadReducidaResponse = await _plazasMovilidadReducidaService.GetPlazasMovilidadReducidaAsync();
                                toolResponse = _plazasMovilidadReducidaService.ParseData(plazasMovilidadReducidaResponse);
                                break;

                            case "Contenedores_Basura":
                                var contenedoresBasuraResponse = await _contenedoresBasuraService.GetContenedoresBasuraAsync();
                                toolResponse = _contenedoresBasuraService.ParseData(contenedoresBasuraResponse);
                                break;
                        }

                        // Agrega la respuesta si se proceso correctamente
                        if (!string.IsNullOrEmpty(toolResponse))
                        {
                            toolResponses.Add(new ToolOutputs()
                            {
                                ToolCallId = toolCall.Id,
                                Output = toolResponse
                            });
                        }
                    }

                    // Enviar todas las respuestas
                    if (toolResponses.Any())
                    {
                        var submitToolsOutputRequest = new SubmitToolOutputs()
                        {
                            ToolOutputs = toolResponses
                        };

                        runResponse = await _openAiService.SubmitToolOutputsAsync(submitToolsOutputRequest, runResponse.ThreadId, runResponse.Id);
                    }
                }
                //El run no se ha podido completar
                else if (runResponse.Status.Equals("expired") || runResponse.Status.Equals("cancelled") ||
                    runResponse.Status.Equals("failed") || runResponse.Status.Equals("incomplete") || reintentos == 0)
                {
                    //Devuelvo el mensaje con el error
                    throw new Exception("En estos momentos el asistente no está disponible. Inténtalo de nuevo más adelante.");
                }
            }

            // Llamo a listMessage y recupero los dos últimos mensajes
            var listaMessageResponse = await _openAiService.ListMessageAsync(runResponse.ThreadId, order: "desc", limit: 2);
            var lastMessage = listaMessageResponse.Messages.First().Content[0].Text.Value;

            // Guardo el hilo en la base de datos
            await SaveOrUpdateThreadDB(threadIdDB, threadId, _configuration["Provider"], runResponse.Status, _configuration["Description"], userId,
                                      CargarListaMensajes(listaMessageResponse), agentId, _configuration["ModelosOpenAi:gpt-4o-mini"],
                                      runResponse.Usage.PromptTokens, runResponse.Usage.CompletionTokens, runResponse.Usage.TotalTokens,
                                      runResponse.Status, runResponse.Id);
        }

        private async Task<Shared.Models.OpenAi.Assistant.Response.Run> CreateThreadAndRun(string message, string model, string assistantId)
        {
            var createMessageAndRun = new CreateThreadAndRun();
            createMessageAndRun.AssistantId = assistantId;
            createMessageAndRun.Model = model;
            createMessageAndRun.Thread = new CreateThread()
            {
                Messages = new List<CreateMessage>() { new CreateMessage() { Role = "user", Content = message } }
            };

            return await _openAiService.CreateThreadAndRunAsync(createMessageAndRun);
        }

        private async Task<Shared.Models.OpenAi.Assistant.Response.Run> CreateMessageAndRun(string message, string model, string assistantId, string threadId)
        {
            var createMessageRequest = new CreateMessage() { Role = "user", Content = message };
            await _openAiService.CreateMessageAsync(createMessageRequest, threadId);

            var createRun = new CreateRun() { AssistantId = assistantId, Model = model };
            return await _openAiService.CreateRunAsync(createRun, threadId);
        }

        private async Task<Data.Entities.Thread> SaveOrUpdateThreadDB(
            decimal threadId, string idThreadProvider, string iaProvider, string threadStatus, string threadDescription, decimal userId,
            List<Data.Entities.Message> listMessage, decimal agentId, string model, int promptTokens, int completionTokens, int totalTokens,
            string runStatus, string runId)
        {
            var nuevoRun = new Data.Entities.Run()
            {
                AgentId = agentId,
                PromptTokens = promptTokens,
                CompletionTokens = completionTokens,
                TotalTokens = totalTokens,
                Status = runStatus,
                IdRun = runId,
                Model = model
            };

            if (threadId <= 0)
            {
                var nuevoThread = new Data.Entities.Thread
                {
                    UserId = userId,
                    Provider = iaProvider,
                    Status = threadStatus,
                    IdThread = idThreadProvider,
                    Description = threadDescription,
                    CompletionTokens = completionTokens,
                    PromptTokens = promptTokens,
                    TotalTokens = totalTokens
                };

                listMessage.ForEach(m => nuevoThread.Messages.Add(m));
                nuevoThread.Runs.Add(nuevoRun);

                var agent = await _unitOfWork.Agents.GetById(agentId);

                await _unitOfWork.Threads.Create(nuevoThread, agent.Nombre);
                return nuevoThread;
            }
            else
            {
                return await _unitOfWork.Threads.UpdateThreadAndCreateMessageAndCreateRun(threadId, threadStatus, listMessage, nuevoRun);
            }
        }

        private List<Data.Entities.Message> CargarListaMensajes(ListMessage listaMessageResponse)
        {
            var listaMensajes = new List<Data.Entities.Message>();

            foreach (var message in listaMessageResponse.Messages.OrderBy(m => m.CreatedAt))
            {
                var nuevoMensaje = new Data.Entities.Message();
                nuevoMensaje.Content = message.Content[0].Text.Value;
                nuevoMensaje.Type = message.Role;
                nuevoMensaje.IdMessage = message.Id;

                listaMensajes.Add(nuevoMensaje);
            }

            return listaMensajes;
        }
    }
}
