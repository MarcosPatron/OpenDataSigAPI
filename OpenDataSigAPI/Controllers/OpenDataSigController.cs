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
    }

}



