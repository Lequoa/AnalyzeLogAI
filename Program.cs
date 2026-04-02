using AnalyzeLogAI.Models;
using AnalyzeLogAI.Services;
using AnalyzeLogAI.Services.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyzeLogAI
{
    internal class Program
    {
        private static readonly ILogAnaylzer _logAnaylzer;
        private static readonly IConfiguration _configuration;

        static Program()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                       .Build();

            var provider = new ServiceCollection().AddSingleton(_configuration)
                                                  .Configure<OllamaSettings>(_configuration.GetSection("Ollama"))
                                                  .AddHttpClient("ollama", client =>
                                                  {
                                                      var settings = _configuration.GetSection("Ollama").Get<OllamaSettings>()!;
                                                      client.BaseAddress = new Uri(settings.BaseUrl);
                                                      client.Timeout = TimeSpan.FromMinutes(settings.TimeoutMinutes);
                                                  }).Services.AddTransient<ILogAnaylzer, LogAnaylzer>()
                                                             .BuildServiceProvider();

            _logAnaylzer = provider.GetRequiredService<ILogAnaylzer>();
        }

        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║           ERROR LOG ANALYZER  v1.0                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            while (true)
            {
                Console.Write("Enter log to analyze:");

                string? logInput = Console.ReadLine();

                if (string.IsNullOrEmpty(logInput))
                {
                    Console.WriteLine("Promt is empty please fill in the prompt.");
                    continue;
                }

                var response = await _logAnaylzer.AnalyzeLog(logInput);

                Console.WriteLine();

                Console.WriteLine("Done!!!");

                Console.WriteLine(response);

                Console.WriteLine();
            }
        }
    }
}
