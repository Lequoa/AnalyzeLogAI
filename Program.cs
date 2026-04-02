using System.Text;
using AnalyzeLogAI.Services;
using AnalyzeLogAI.Services.IService;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║           ERROR LOG ANALYZER  v1.0                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("Enter log to analyze:");

            string? logInput = Console.ReadLine();

            var cts = new CancellationTokenSource();

            var spinner = ShowSpinner(cts.Token);

            await Task.Delay(3000); // simulate work

            var response = await _logAnaylzer.AnalyzeLog(logInput);

            cts.Cancel();

            await spinner;

            Console.WriteLine();

            Console.WriteLine("Done!!!");

            Console.WriteLine(response);
        }

        static async Task ShowSpinner(CancellationToken ctx)
        {
            var frames = new[] { '|', '/', '-', '\\' };
            int i = 0;
            while (!ctx.IsCancellationRequested)
            {
                Console.Write($"\r Loading... {frames[i++ % frames.Length]}");
                await Task.Delay(100, ctx).ContinueWith(_ => { }); // swallow cancellation
            }
        }
    }
}
