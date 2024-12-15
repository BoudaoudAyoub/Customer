namespace CustomerMan.API.Infrastructure.Extensions;
public static class SerilogConfiguration
{
    /// <summary>
    /// Static method to create and configure a Serilog ILogger instance.
    /// This method accepts a string 'appName' which is used to identify the application in the logs
    /// </summary>
    /// <param name="appName">application name</param>
    /// <returns>ILogger instance</returns>
    public static Serilog.ILogger CreateSerilogLogger()
    {
        // Set up and return a new Serilog Logger configured as follows:
        return Log.Logger = new LoggerConfiguration()
                               .MinimumLevel.Verbose() // Set the minimum log level to Verbose to capture detailed log data
                               .Enrich.WithProperty("CustmerContext", "Program") // Add a fixed property "CustomerManContext" with the application name to all log entries
                               .Enrich.FromLogContext() // Enrich log events with additional contextual properties (like thread id, etc.)
                               .WriteTo.Console() // Write log events to the console
                               .WriteTo.File("Infrastructure/Loggers/.txt", rollingInterval: RollingInterval.Day) // Write log events to a text file, creating a new file every day
                               .CreateLogger(); // Build and return the configured logger
    }
}