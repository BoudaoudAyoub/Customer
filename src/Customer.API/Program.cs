SerilogConfiguration.CreateSerilogLogger();
var appName = Assembly.GetExecutingAssembly().GetName().Name;

try
{
    Log.Information("Configuring web host ({CustmerContext})...", appName);
    var configuration = CustomConfigurationBuilder.GetConfiguration();
    var host = WebHostConfiguration.BuildWebHost(configuration, args);

    Log.Information("Starting web host ({CustmerContext})...", appName);
    host.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({CustmerContext})!", appName);
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}