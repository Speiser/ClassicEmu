﻿using Classic.Auth.Extensions;
using Classic.Shared;
using Classic.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

await Host.CreateDefaultBuilder()
    .ConfigureLogging(logging => logging
        .AddConsole()
        .SetMinimumLevel(LogLevel.Trace))
    .ConfigureServices(services => services
        .AddSingleton<ErrorHandler>()
        .AddSharedServices()
        .AddAuthenticationServer())
    .RunConsoleAsync();