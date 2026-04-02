using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeLogAI.Services.IService
{
    internal interface ILogAnaylzer
    {
        Task<string> AnalyzeLog(string logInput);
    }
}
