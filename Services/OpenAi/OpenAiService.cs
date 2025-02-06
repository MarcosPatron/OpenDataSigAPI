using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request;
using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response;
using Shared.Models.OpenAi.Assistant.Request;
using Shared.Models.OpenAi.Assistant.Response;
using Thread = OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response.Thread;
using OpenDataSigAPI.Shared.Models.OpenAi.Chat.Response;
using OpenDataSigAPI.Shared.Models.OpenAi.Chat.Request;
using File = OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response.File;
using OpenDataSigAPI.Shared.Models.OpenAi.Files.Request;

namespace OpenDataSigAPI.Services.OpenAi
{
    public class OpenAiService : IOpenAiService
    {
        private readonly IConfiguration _configuration;

        public OpenAiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //MÉTODO AUXILIARES
        private async Task<T> ProccessResponse<T>(HttpResponseMessage response)
        {
            // Verifica la respuesta
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(jsonResponse);
            }
            else
            {
                var statusCode = response.StatusCode;

                if (response.Content != null)
                {
                    // Lee el contenido de la respuesta como string.
                    var contentError = await response.Content.ReadAsStringAsync();

                    // Deserializo el contenido JSON a un objeto para acceder a los detalles del error.
                    var errorDetails = JsonSerializer.Deserialize<object>(contentError);

                    throw new Exception($"Code:{statusCode}-{errorDetails}");
                }

                throw new Exception($"Code:{statusCode}");
            }
        }

        private async Task ProccessResponse(HttpResponseMessage response)
        {
            // Verifica la respuesta
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                var statusCode = response.StatusCode;

                if (response.Content != null)
                {
                    // Lee el contenido de la respuesta como string.
                    var contentError = await response.Content.ReadAsStringAsync();

                    // Deserializo el contenido JSON a un objeto para acceder a los detalles del error.
                    var errorDetails = JsonSerializer.Deserialize<object>(contentError);

                    throw new Exception($"Code:{statusCode}-{errorDetails}");
                }

                throw new Exception($"Code:{statusCode}");
            }
        }

        private HttpClient GetOpenAiHttpClient(bool isAssistantApi = false)
        {
            var httpClient = new HttpClient();

            //Configuro httpClient
            httpClient.BaseAddress = new Uri("https://api.openai.com/v2/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if(isAssistantApi) httpClient.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration["API-KEY_OpenAI"]}");

            return httpClient;
        }

        #region Assitant Api
        //THREADS
        public async Task<Run> CreateThreadAndRunAsync(CreateThreadAndRun request)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi:true))
                {
                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:Threads"]}/{_configuration["EndPointsOpenAi:Runs"]}";

                    var jsonRequest = JsonSerializer.Serialize(request);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(apiEndpoint, content);

                    return await ProccessResponse<Run>(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Thread> CreateThreadAsync(CreateThread request)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi: true))
                {
                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:Threads"]}";

                    var jsonRequest = JsonSerializer.Serialize(request);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(apiEndpoint, content);

                    return await ProccessResponse<Thread>(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Thread> RetrieveTheadAsync(string threadId)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi: true))
                {
                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:Threads"]}/{threadId}";

                    var response = await httpClient.GetAsync(apiEndpoint);

                    return await ProccessResponse<Thread>(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        //RUNS
        public async Task<Run> CreateRunAsync(CreateRun request, string threadId)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi: true))
                {
                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:Threads"]}/{threadId}/{_configuration["EndPointsOpenAi:Runs"]}";

                    var jsonRequest = JsonSerializer.Serialize(request);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(apiEndpoint, content);

                    return await ProccessResponse<Run>(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Run> RetrieveRunAsync(string threadId, string runId)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi: true))
                {
                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:Threads"]}/{threadId}/{_configuration["EndPointsOpenAi:Runs"]}/{runId}";

                    var response = await httpClient.GetAsync(apiEndpoint);

                    return await ProccessResponse<Run>(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Run> SubmitToolOutputsAsync(OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request.SubmitToolOutputs request, string threadId, string runId)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi: true))
                {
                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:Threads"]}/{threadId}/{_configuration["EndPointsOpenAi:Runs"]}/{runId}/{_configuration["EndPointsOpenAi:SubmitToolOutputs"]}";

                    var jsonRequest = JsonSerializer.Serialize(request);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(apiEndpoint, content);

                    return await ProccessResponse<Run>(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //MESSAGES
        public async Task<Message> CreateMessageAsync(CreateMessage request, string threadId)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi: true))
                {
                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:Threads"]}/{threadId}/{_configuration["EndPointsOpenAi:Messages"]}";

                    var jsonRequest = JsonSerializer.Serialize(request);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(apiEndpoint, content);

                    return await ProccessResponse<Message>(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Message> RetrieveMessageAsync(string threadId, string messageId)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi: true))
                {
                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:Threads"]}/{threadId}/{_configuration["EndPointsOpenAi:Messages"]}/{messageId}";

                    var response = await httpClient.GetAsync(apiEndpoint);

                    return await ProccessResponse<Message>(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ListMessage> ListMessageAsync(string threadId, int? limit = null, string? order = null, string? after = null, string? before = null)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi: true))
                {
                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:Threads"]}/{threadId}/{_configuration["EndPointsOpenAi:Messages"]}";

                    // Agrega parámetros de consulta si están presentes
                    var queryParameters = new List<string>();
                    if (limit.HasValue)
                    {
                        queryParameters.Add($"limit={limit}");
                    }
                    if (!string.IsNullOrEmpty(order))
                    {
                        queryParameters.Add($"order={order}");
                    }
                    if (!string.IsNullOrEmpty(after))
                    {
                        queryParameters.Add($"after={after}");
                    }
                    if (!string.IsNullOrEmpty(before))
                    {
                        queryParameters.Add($"before={before}");
                    }
                    string queryString = string.Join("&", queryParameters);
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        apiEndpoint += $"?{queryString}";
                    }

                    var response = await httpClient.GetAsync(apiEndpoint);

                    return await ProccessResponse<ListMessage>(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //VECTOR STORES
        public async Task<VectorStore> CreateVectorStore(CreateVectorStore request)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi: true))
                {
                    var jsonRequest = JsonSerializer.Serialize(request);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync($"{_configuration["EndPointsOpenAi:VectorStores"]}", content);

                    return await ProccessResponse<VectorStore>(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteVectorStore(string vectorStoreId)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi: true))
                {
                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:VectorStores"]}/{vectorStoreId}";

                    var response = await httpClient.DeleteAsync(apiEndpoint);

                    await ProccessResponse(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //VECTOR STORES FILES
        public async Task<VectorStoreFile> CreateVectorStoreFile(CreateVectorStoreFile request, string vectorStoreId)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi: true))
                {
                    var jsonRequest = JsonSerializer.Serialize(request);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:VectorStores"]}/{vectorStoreId}/{_configuration["EndPointsOpenAi:Files"]}";

                    var response = await httpClient.PostAsync(apiEndpoint, content);

                    return await ProccessResponse<VectorStoreFile>(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteVectorStoreFile(string vectorStoreId, string fileId)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient(isAssistantApi: true))
                {
                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:VectorStores"]}/{vectorStoreId}/{_configuration["EndPointsOpenAi:Files"]}/{fileId}";

                    var response = await httpClient.DeleteAsync(apiEndpoint);

                    await ProccessResponse(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        //CHAT
        public async Task<ChatCompletionResponse> ChatCompletion(ChatCompletionRequest request)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient())
                {

                    var jsonRequest = JsonSerializer.Serialize(request);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:ChatCompletions"]}";

                    var response = await httpClient.PostAsync(apiEndpoint, content);

                    return await ProccessResponse<ChatCompletionResponse>(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //FILES
        public async Task<File> UploadFile(UploadFile request)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        // Agrega el archivo al contenido del formulario
                        var fileContent = new ByteArrayContent(request.File);
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                        form.Add(fileContent, "file", request.Filename);

                        // Agrega el campo "purpose" al formulario
                        form.Add(new StringContent(request.Purpose), "purpose");

                        var apiEndpoint = $"{_configuration["EndPointsOpenAi:Files"]}";

                        var response = await httpClient.PostAsync(apiEndpoint, form);

                        return await ProccessResponse<File>(response);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteFile(string fileId)
        {
            try
            {
                using (var httpClient = GetOpenAiHttpClient())
                {
                    var apiEndpoint = $"{_configuration["EndPointsOpenAi:Files"]}/{fileId}";

                    var response = await httpClient.DeleteAsync(apiEndpoint);

                    await ProccessResponse(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        Task<Shared.Models.OpenAi.Files.Response.File> IOpenAiService.UploadFile(UploadFile request)
        {
            throw new NotImplementedException();
        }
    }
}
