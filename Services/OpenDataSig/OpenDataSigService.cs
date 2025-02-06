using Microsoft.Extensions.Configuration;
using OpenDataSigAPI.Services.OpenAi;
using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request;
using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response;
using Shared.OpenDataSig;
using Shared.Result;

namespace Services.OpenDataSig
{
    public class OpenDataSigService
    {
        private readonly IConfiguration _configuration;
        private readonly IOpenAiService _openAiService;

        public OpenDataSigService(IConfiguration configuration, IOpenAiService openAiService)
        {
            _configuration = configuration;
            _openAiService = openAiService;
        }

        public async Task<Result<RespuestaMensajeOpenDataSig>> ManageMessageUi(string message, string? threadId)
        {
            //Mensaje no es vacío
            if (string.IsNullOrWhiteSpace(message))
            {
                return Result<RespuestaMensajeOpenDataSig>.Failure("El mensaje llega vacío. Consulte con su administrador.");
            }

            var runResponse = await _openAiService.CreateRunAsync(new CreateRun {
                Model = "gpt-4o-mini",
                Temperature = 0.7, 
                TopP = 1,
                MaxCompletionTokens = 1000,
                MaxPromptTokens = 1000,
                Message = message }, "threadId");

            int reintentos = 30;
            int delay = 1000;

            while (!runResponse.Status.Equals("completed") && reintentos > 0)
            {
                await Task.Delay(delay);
                reintentos--;

                if (reintentos % 10 == 0) // Aumenta el delay cada 10 intentos
                    delay = Math.Min(delay * 2, 5000); // Máximo 5 segundos

                runResponse = await _openAiService.RetrieveRunAsync(runResponse.ThreadId, runResponse.Id);
            }

            // Return a successful result
            var respuesta = new RespuestaMensajeOpenDataSig { Mensaje = "Mensaje procesado correctamente." };
            return Result<RespuestaMensajeOpenDataSig>.Success(respuesta);
        }
    }
}
