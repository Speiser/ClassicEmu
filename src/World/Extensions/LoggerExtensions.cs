using Microsoft.Extensions.Logging;

namespace Classic.World.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogUnhandledOpcode<T>(this ILogger<T> logger, Opcode opcode)
        {
            logger.LogWarning($"Unhandled opcode {opcode}");
        }
    }
}
