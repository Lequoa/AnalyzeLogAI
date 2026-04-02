using AnalyzeLogAI.Services;
using AnalyzeLogAI.Services.IService;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyzeLogAI
{
    internal class Program
    {
        private static ILogAnaylzer _logAnaylzer;

        static Program()
        {
            var provider = new ServiceCollection().AddHttpClient("ollama", client =>
            {
                client.BaseAddress = new Uri("http://localhost:11434");
                client.Timeout = TimeSpan.FromMinutes(10);
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
                Console.WriteLine("Enter log to analyze:");

                string? logInput = Console.ReadLine();

                var response = await _logAnaylzer.AnalyzeLog(logInput);

                Console.WriteLine();

                Console.WriteLine("Done!!!");

                Console.WriteLine(response);

                Console.WriteLine();
            }
        }
    }
}
