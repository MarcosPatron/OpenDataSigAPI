using Microsoft.AspNetCore.Mvc;
using OpenDataSigAPI.Services.OpenDataSig;
using Services.OpenAi;
using Shared.OpenDataSig;


namespace OpenDataSigAPI.Controllers
{
    [ApiController]
    [Route("/api/OpenDataSig")]
    public class OpenDataSigController : Controller
    {
        private readonly IOpenAiService _openAiService;
        private readonly IOpenDataSigService _openDataSigService;
        private readonly IConfiguration _configuration;

        public OpenDataSigController(IOpenAiService openAiService, IConfiguration configuration, IOpenDataSigService openDataSigService)
        {
            _openAiService = openAiService;
            _openDataSigService = openDataSigService;
            _configuration = configuration;
        }

        /*
        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] string body)
        {
            try
            {
                if (body == null)
                {
                    return BadRequest("No se han enviado datos");
                }

                var respuestaMensajeOpenDataSig = await _openDataSigService.ManageMessageUi(body);

                var result = Result<RespuestaMensajeOpenDataSig>.Success(respuestaMensajeOpenDataSig);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("createThreadAndRun")]
        public async Task<IActionResult> CreateThreadAndRun(string OpenAI_ApiKey, [FromBody] CreateThreadAndRun request)
        {
            if (request == null)
            {
                return BadRequest("No se han enviado datos");
            }

            var threadAndRun = await _openAiService.CreateThreadAndRunAsync(request);
            return Ok(threadAndRun);
        }

        [HttpPost("createThread")]
        public async Task<IActionResult> CreateThread(string OpenAI_ApiKey, [FromBody] CreateThread request)
        {
            if (request == null)
            {
                return BadRequest("No se han enviado datos");
            }

            var thread = await _openAiService.CreateThreadAsync(request);
            return Ok(thread);
        }
        
        [HttpGet("retrieveThread/{threadId}")]
        public async Task<IActionResult> RetrieveThread(string OpenAI_ApiKey, string threadId)
        {
            if (string.IsNullOrWhiteSpace(OpenAI_ApiKey))
            {
                return BadRequest("No se ha enviado ninguna API Key.");
            }
            try
            {
                var thread = await _openAiService.RetrieveTheadAsync(threadId);

                if (thread != null)
                {
                    return Ok(thread);
                }
                else
                {
                    return NotFound(new { error = "Hilo no encontrado" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al recuperar el hilo: {e.Message}");
            }
        }


        [HttpPost("createRun/{threadId}")]
        public async Task<IActionResult> CreateRun(string OpenAI_ApiKey, [FromBody] CreateRun request, string threadId)
        {
            if (request == null)
            {
                return BadRequest("No se han enviado datos");
            }

            var run = await _openAiService.CreateRunAsync(request, threadId);
            return Ok(run);
        }


        [HttpGet("retrieveRun/{threadId}/{runId}")]
        public async Task<IActionResult> RetrieveRun(string OpenAI_ApiKey, string threadId, string runId)
        {
            var run = await _openAiService.RetrieveRunAsync(threadId, runId);
            if(run != null)
            {
                return Ok(run);
            }
            else
            {
                return NotFound(new { error = "Run no encontrado" });
            }
        }
        */

        [HttpPost("sendMessage")]
        public async Task<IActionResult> CreateMessage([FromBody] RequestMessageOpenDataSig request)
        {
            if (request == null)
            {
                return BadRequest("No se han enviado datos");
            }

            try
            {
                var message = await _openDataSigService.ManageMessageUi(request);

                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el mensaje: {ex.Message}");
            }
        }

        /*
        [HttpGet("retrieveMessage/{threadId}/{messageId}")]
        public async Task<IActionResult> RetrieveMessage(string OpenAI_ApiKey, string threadId, string messageId)
        {
            var message = await _openAiService.RetrieveMessageAsync(threadId, messageId);

            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NotFound(new { error = "Mensaje no encontrado" });
            }
        }
        */

        [HttpGet("listMessage/{threadId}")]
        public async Task<IActionResult> ListMessage(string OpenAI_ApiKey, string threadId, int? limit, string? order, string? after, string? before)
        {
            var messages = await _openAiService.ListMessageAsync(threadId, limit, order, after, before);

            if (messages != null)
            {
                return Ok(messages);
            }
            else
            {
                return NotFound(new { error = "No hay mensajes en el hilo." });
            }
            
        }

        /*
         [HttpPost("SubmitToolOutputs/{threadId}/{runId}")]
        public async Task<IActionResult> SubmitToolOutputs(
            [FromRoute] string threadId,                                   
            [FromRoute] string runId,                                       
            [FromHeader(Name = "OpenAi-ApiKey")] string openAiApiKey,        
            [FromBody] SubmitToolOutputs request)                            
        {
            if (request == null || request.ToolOutputs == null || !request.ToolOutputs.Any())
            {
                return BadRequest("No se han enviado datos o la lista de tool_outputs está vacía.");
            }

        
            var result = await _openAiService.SubmitToolOutputsAsync(request, threadId, runId);

            return Ok(result);
        }
        */
    }

}



