using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AnalyzeLogAI.Constants;
using AnalyzeLogAI.Services.IService;
using Newtonsoft.Json;

namespace AnalyzeLogAI.Services
{
    internal class LogAnaylzer(IHttpClientFactory factory) : ILogAnaylzer
    {
        public async Task<string> AnalyzeLog(string logInput)
        {
            var client = factory.CreateClient("ollama");

            var requestBody = new
            {
                model = "my-csharp-assistant",
                prompt = $"{LogAnalyzerConstants.propmtation}:\n{logInput}",
                stream = false
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/generate", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            dynamic? result = string.IsNullOrEmpty(responseString) ? null : JsonConvert.DeserializeObject(responseString);

            return result is not null ? result.response : string.Empty;
        }
    }
}
