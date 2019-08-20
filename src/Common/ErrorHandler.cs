using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Classic.Common
{
    public class ErrorHandler
    {
        public ErrorHandler(ILogger<ErrorHandler> logger)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) => logger.LogError($"UnhandledException: {JsonConvert.SerializeObject(e)}");
            TaskScheduler.UnobservedTaskException += (s, e) => logger.LogError($"UnobservedTaskException: {JsonConvert.SerializeObject(e)}");
        }
    }
}
