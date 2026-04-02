namespace AnalyzeLogAI.Services.IService
{
    internal interface ILogAnaylzer
    {
        Task<string> AnalyzeLog(string logInput);
    }
}
