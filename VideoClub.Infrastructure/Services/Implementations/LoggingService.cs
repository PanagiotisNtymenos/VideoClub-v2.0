using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Diagnostics;
using VideoClub.Infrastructure.Services.Interfaces;

namespace VideoClub.Infrastructure.Services.Implementations
{
    public class LoggingService : ILoggingService
    {
        public ILogger Writer => Log.Logger;

        public LoggingService()
        {
            Debug.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "Log/logs.txt");
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File(
                path: AppDomain.CurrentDomain.BaseDirectory + "Log/logs.txt",
                rollingInterval: RollingInterval.Day,
                shared: true,
                retainedFileCountLimit: 100,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .WriteTo.File(
                formatter: new CompactJsonFormatter(),
                path: AppDomain.CurrentDomain.BaseDirectory + "Log/logs.json",
                rollingInterval: RollingInterval.Day,
                shared: true,
                retainedFileCountLimit: 100
            )
            .WriteTo.ApplicationInsights(
                TelemetryConfiguration.Active,
                TelemetryConverter.Events,
                LogEventLevel.Verbose
            )
            .WriteTo.ApplicationInsights(
                TelemetryConfiguration.Active,
                TelemetryConverter.Traces,
                LogEventLevel.Verbose
             )
            .CreateLogger();
            /*        .WriteTo.MicrosoftTeams(
                        ConfigurationManager.AppSettings["Serilog:MicrosoftTeams.webHookUri.Alerts"],
                        title: "eCoupon Alerts Sink",
                        restrictedToMinimumLevel: LogEventLevel.Error
                    )
                    )
                    .WriteTo.MicrosoftTeams(
                        ConfigurationManager.AppSettings["Serilog:MicrosoftTeams.webHookUri.Information"],
                        title: "eCoupon Information Sink",
                        restrictedToMinimumLevel: LogEventLevel.Verbose
                    )
                    .CreateLogger();
            */
        }
    }
}
