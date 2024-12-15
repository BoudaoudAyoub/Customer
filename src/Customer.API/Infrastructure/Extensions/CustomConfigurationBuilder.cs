namespace CustomerMan.API.Infrastructure.Extensions;
public static class CustomConfigurationBuilder
{
    public static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                         .AddEnvironmentVariables()
                                         .Build();
    }
}