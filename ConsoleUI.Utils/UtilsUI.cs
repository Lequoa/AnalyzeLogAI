namespace AnalyzeLogAI.ConsoleUI.Utils
{
    internal class UtilsUI
    {
        public static async Task ShowSpinner(CancellationToken ctx)
        {
            var frames = new[] { '|', '/', '-', '\\' };
            int i = 0;

            Console.ForegroundColor = ConsoleColor.Green;

            while (!ctx.IsCancellationRequested)
            {

                Console.Write($"\r Loading... {frames[i++ % frames.Length]}");
                await Task.Delay(100, ctx).ContinueWith(_ => { }); // swallow cancellation
            }

            Console.ResetColor();
        }
    }
}
