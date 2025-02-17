using Microsoft.Extensions.Configuration;
using OpenDataSigAPI.Data.Repositories;
using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request;
using Services.Functions.Farmacias;
using Services.OpenAi;
using Shared.OpenDataSig;
using static Shared.Constants;

namespace OpenDataSigAPI.Services.OpenDataSig
{
    public class OpenDataSigService : IOpenDataSigService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOpenAiService _openAiService;
        private readonly IFarmaciasService _farmaciasService;
        private string farmaciasResponse;

        public OpenDataSigService(IConfiguration configuration, IOpenAiService openAiService, IFarmaciasService farmaciasService)
        {
            _configuration = configuration;
            _openAiService = openAiService;
            _farmaciasService = farmaciasService;
        }

        public async Task<RespuestaMensajeOpenDataSig> ManageMessageUi(string message, string? threadId)
        {
            //COMPROBACIONES
            //Mensaje no es vacío
            if (string.IsNullOrWhiteSpace(message))
            {
                await _unitOfWork.Logs.LogError(this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, TiposErrores.MENSAJE_CLIENTE_VACIO, string.Empty);
                return new RespuestaMensajeOpenDataSig { Mensaje = "El mensaje llega vacío. Consulte con su administrador." };
            }


            //PROCESAMIENTO
            var toolOutput = string.Empty;
            Shared.Models.OpenAi.Assistant.Response.Run runResponse;

            if (string.IsNullOrWhiteSpace(threadId))
            {
                runResponse = await CreateThreadAndRun(message, "gpt-4o-mini", _configuration["IdAssistantFarmacias"]);
                threadId = runResponse.ThreadId;
            }

            else
                runResponse = await CreateMessageAndRun(message, "gpt-4o-mini", _configuration["IdAssistantFarmacias"], threadId);

            // Espero a que se procese la consulta
            await checkResponseStatus(runResponse);

            var mensajes = await _openAiService.ListMessageAsync(threadId, null, null, null, null);

            return new RespuestaMensajeOpenDataSig { Mensaje = mensajes.Messages[0].Content[0].Text.Value, ThreadId = runResponse.ThreadId };
        }

        private async Task checkResponseStatus(Shared.Models.OpenAi.Assistant.Response.Run runResponse)
        {
            int reintentos = 30;
            int delay = 1000;

            while (!runResponse.Status.Equals("completed") && reintentos > 0)
            {
                Console.WriteLine(runResponse.Status);
                await Task.Delay(delay);
                reintentos--;

                if (reintentos % 10 == 0) // Aumenta el delay cada 10 intentos
                    delay = Math.Min(delay * 2, 5000); // Maximo 5 segundos

                runResponse = await _openAiService.RetrieveRunAsync(runResponse.ThreadId, runResponse.Id);


                if (runResponse.Status.Equals("requires_action"))
                {

                    var toolCall = runResponse.RequiredAction.SubmitToolOutputs.ToolCalls.FirstOrDefault();

                    var farmaciasResponse = await _farmaciasService.GetFarmaciasAsync();

                    string toolResponse = _farmaciasService.ParseData(farmaciasResponse);

                    var submitToolsOutputRequest = new SubmitToolOutputs()
                    {
                        ToolOutputs = new List<ToolOutputs>() { new ToolOutputs() { ToolCallId = toolCall.Id, Output = toolResponse } }
                    };

                    Console.WriteLine(toolResponse);

                    runResponse = await _openAiService.SubmitToolOutputsAsync(submitToolsOutputRequest, runResponse.ThreadId, runResponse.Id);
                }
            }
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

    }
}
