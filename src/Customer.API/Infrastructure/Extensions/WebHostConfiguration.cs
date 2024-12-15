using Autofac;
using Autofac.Extensions.DependencyInjection;
using CustomerMan.API.Infrastructure.AutofacModules;
using CustomerMan.API.Infrastructure.Mappings;
namespace CustomerMan.API.Infrastructure.Extensions;
public static class WebHostConfiguration
{
    public static IHost BuildWebHost(IConfiguration configuration, string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                   .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.CaptureStartupErrors(false)
                                 .ConfigureKestrel(ConfigureKestrelWebServer())
                                 .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                                 .ConfigureServices(ConfigureServices(configuration))
                                 .Configure(ConfigureApplication())
                                 .UseContentRoot(Directory.GetCurrentDirectory());
                   })
                   .ConfigureContainer(ConfigureContainers())
                   .UseSerilog()
                   .Build();
    }

    private static Action<KestrelServerOptions> ConfigureKestrelWebServer()
    {
        return options =>
        {
            options.Listen(IPAddress.Any, 80, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
            });
        };
    }

    private static Action<IServiceCollection> ConfigureServices(IConfiguration configuration)
    {
        return services =>
        {
            services.AddCustomDbContext(configuration);
            services.AddCustomSwagger(configuration);
            services.AddCustomConfiguration(configuration);
            services.AddAutofac();
        };
    }

    private static Action<WebHostBuilderContext, IApplicationBuilder> ConfigureApplication()
    {
        return (whbc, app) =>
        {
            app.AddCustomConfigure(whbc.HostingEnvironment);
        };
    }

    private static Action<ContainerBuilder> ConfigureContainers()
    {
        return container =>
        {
            container.RegisterModule<MediatorModule>();
            container.RegisterModule<ApplicationModule>();
            container.RegisterModule<AutoMapperLoad>();
        };
    }
}