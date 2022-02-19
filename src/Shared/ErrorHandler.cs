using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Classic.Shared;

public class ErrorHandler
{
    public ErrorHandler(ILogger<ErrorHandler> logger)
    {
        AppDomain.CurrentDomain.UnhandledException += (s, e) => logger.LogError($"UnhandledException: {JsonSerializer.Serialize(e)}");
        TaskScheduler.UnobservedTaskException += (s, e) => logger.LogError($"UnobservedTaskException: {JsonSerializer.Serialize(e)}");
    }
}
