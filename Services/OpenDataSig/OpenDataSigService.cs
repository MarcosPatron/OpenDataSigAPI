using Microsoft.Extensions.Configuration;
using OpenDataSigAPI.Services.OpenAi;
using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request;
using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response;
using Shared.OpenDataSig;

namespace OpenDataSigAPI.Services.OpenDataSig
{
    public class OpenDataSigService : IOpenDataSigService
    {
        private readonly IConfiguration _configuration;
        private readonly IOpenAiService _openAiService;

        public OpenDataSigService(IConfiguration configuration, IOpenAiService openAiService)
        {
            _configuration = configuration;
            _openAiService = openAiService;
        }

        public async Task<RespuestaMensajeOpenDataSig> ManageMessageUi(string message, string? threadId)
        {
            //COMPROBACIONES
            //Mensaje no es vacío
            if (string.IsNullOrWhiteSpace(message))
            {
                return new RespuestaMensajeOpenDataSig { Mensaje = "El mensaje llega vacío. Consulte con su administrador." };
            }

            //PROCESAMIENTO
            var toolOutput = string.Empty;
            Run runResponse;

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

            return new RespuestaMensajeOpenDataSig { Mensaje = mensajes.Messages[0].Content[0].Text.Value };
        }

        private async Task checkResponseStatus(Run runResponse)
        {
            int reintentos = 30;
            int delay = 1000;

            while (!runResponse.Status.Equals("completed") && reintentos > 0)
            {
                await Task.Delay(delay);
                reintentos--;

                if (reintentos % 10 == 0) // Aumenta el delay cada 10 intentos
                    delay = Math.Min(delay * 2, 5000); // Maximo 5 segundos

                runResponse = await _openAiService.RetrieveRunAsync(runResponse.ThreadId, runResponse.Id);
            }
        }

        private async Task<Run> CreateThreadAndRun(string message, string model, string assistantId)
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

        private async Task<Run> CreateMessageAndRun(string message, string model, string assistantId, string threadId)
        {
            var createMessageRequest = new CreateMessage() { Role = "user", Content = message };
            await _openAiService.CreateMessageAsync(createMessageRequest, threadId);

            var createRun = new CreateRun() { AssistantId = assistantId, Model = model };
            return await _openAiService.CreateRunAsync(createRun, threadId);
        }

    }
}
