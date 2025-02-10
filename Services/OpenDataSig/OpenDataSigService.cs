using Microsoft.Extensions.Configuration;
using OpenDataSigAPI.Services.OpenAi;
using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request;
using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response;
using Shared.OpenDataSig;
using Shared.Result;
using System.Reflection;

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
            
            var msgResponse = await _openAiService.CreateMessageAsync(new CreateMessage()
            { 
                Role = "user",
                Content = message 
            },
            threadId);

            var runResponse = await _openAiService.CreateRunAsync(new CreateRun
            {
                AssistantId = _configuration["IdAssistantFarmacias"],
                Model = "gpt-4o-mini",
                Temperature = 0.7,
                TopP = 1,
                MaxCompletionTokens = 1000,
                MaxPromptTokens = 1000
            },
            threadId);

            // Para saber si se ha creado el RUN
            Console.WriteLine("Run creado correctamente, ID: " + runResponse.Id);


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

            return new RespuestaMensajeOpenDataSig { Mensaje = "Mensaje procesado correctamente." };
        }
    }
}
