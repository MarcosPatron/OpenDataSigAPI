﻿using AtencionUsuarios.Services.OpenAi;
using AtencionUsuarios.Shared.Models.OpenAi.Assistant.Request;
using Microsoft.AspNetCore.Mvc;

namespace OpenDataSigAPI.Controllers
{
    [ApiController]
    [Route("/api/OpenDataSig")]
    public class OpenDataSigController : Controller
    {
        private readonly IOpenAiService _openAiService;

        public OpenDataSigController( IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }


        [HttpPost("createThreadAndRun")]
        public async Task<IActionResult> CreateThreadAndRun(String OpenAI_ApiKey, [FromBody] CreateThreadAndRun request)
        {
            if (request == null)
            {
                return BadRequest("No se han enviado datos");
            }

            var threadAndRun = await _openAiService.CreateThreadAndRunAsync(request);
            return Ok(threadAndRun);
        }


        [HttpPost("createThread")]
        public async Task<IActionResult> CreateThread(String OpenAI_ApiKey, [FromBody] CreateThread request)
        {
            if (request == null)
            {
                return BadRequest("No se han enviado datos");
            }

            var thread = await _openAiService.CreateThreadAsync(request);
            return Ok(thread);
        }

        [HttpGet("retrieveThread/{threadId}")]
        public async Task<IActionResult> RetrieveThread(String OpenAI_ApiKey, string threadId)
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

        [HttpPost("createRun/{threadId}")]
        public async Task<IActionResult> CreateRun(String OpenAI_ApiKey, [FromBody] CreateRun request, string threadId)
        {
            if (request == null)
            {
                return BadRequest("No se han enviado datos");
            }

            var run = await _openAiService.CreateRunAsync(request, threadId);
            return Ok(run);
        }

        [HttpGet("retrieveRun/{threadId}/{runId}")]
        public async Task<IActionResult> RetrieveRun(String OpenAI_ApiKey, string threadId, string runId)
        {
            var run = await _openAiService.RetrieveRunAsync(threadId, runId);
            return run != null ? Ok(run) : NotFound(new { error = "Run no encontrado" });
        }

        [HttpPost("createMessage/{threadId}")]
        public async Task<IActionResult> CreateMessage(String OpenAI_ApiKey, [FromBody] CreateMessage request, string threadId)
        {
            if (request == null)
            {
                return BadRequest("No se han enviado datos");
            }

            var message = await _openAiService.CreateMessageAsync(request, threadId);
            return Ok(message);
        }

        [HttpGet("retrieveMessage/{threadId}/{messageId}")]
        public async Task<IActionResult> RetrieveMessage(String OpenAI_ApiKey, string threadId, string messageId)
        {
            var message = await _openAiService.RetrieveMessageAsync(threadId, messageId);
            return message != null ? Ok(message) : NotFound(new { error = "Mensaje no encontrado" });
        }

        [HttpGet("listMessages/{threadId}")]
        public async Task<IActionResult> ListMessages(String OpenAI_ApiKey, string threadId, int limit, String order, String after, String before)
        {
            var messages = await _openAiService.ListMessageAsync(threadId, limit, order, after, before);
            return messages != null ? Ok(messages.Messages) : NotFound(new { error = "No hay mensajes en el hilo." });
        }
    }
}
