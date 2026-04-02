namespace AnalyzeLogAI.Models
{
    internal class OllamaSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int TimeoutMinutes { get; set; } = 5;
        public string Prompt { get; set; } = string.Empty;
    }
}
