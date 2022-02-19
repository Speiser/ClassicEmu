using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Classic.Shared;

public class ErrorHandler
{
    public ErrorHandler(ILogger<ErrorHandler> logger)
    {
        AppDomain.CurrentDomain.UnhandledException += (s, e) => logger.LogError($"UnhandledException: {JsonConvert.SerializeObject(e)}");
        TaskScheduler.UnobservedTaskException += (s, e) => logger.LogError($"UnobservedTaskException: {JsonConvert.SerializeObject(e)}");
    }
}
