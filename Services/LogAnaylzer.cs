using System.Text;
using AnalyzeLogAI.ConsoleUI.Utils;
using AnalyzeLogAI.Constants;
using AnalyzeLogAI.Models;
using AnalyzeLogAI.Services.IService;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AnalyzeLogAI.Services
{
    internal class LogAnaylzer(IHttpClientFactory factory, IOptions<OllamaSettings> ollamaSettings) : ILogAnaylzer
    {
        public async Task<string> AnalyzeLog(string logInput)
        {
            var client = factory.CreateClient("ollama");

            var requestBody = new
            {
                model = ollamaSettings.Value.Model,
                prompt = $"{LogAnalyzerConstants.propmt}:\n{logInput}",
                stream = false
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var cts = new CancellationTokenSource();

            var spinnerTask = UtilsUI.ShowSpinner(cts.Token);

            var ollamaTask = client.PostAsync("/api/generate", content);

            var response = await ollamaTask;

            cts.Cancel();

            await spinnerTask;

            Console.Write("\r                          \r");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            dynamic? result = string.IsNullOrEmpty(responseString) ? null : JsonConvert.DeserializeObject(responseString);

            return result is not null ? result.response : string.Empty;
        }
    }
}
