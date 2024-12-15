using Autofac;
using AutoMapper;
using CustomerMan.API.Infrastructure.AutoMapperProfiles.CustomerMap;
namespace CustomerMan.API.Infrastructure.Mappings;
public class AutoMapperLoad : Autofac.Module
{
    public void AutoMapperLoads(ContainerBuilder containerBuilder)
    {
        containerBuilder.Register(context => MapperConfiguration())
                        .AsSelf()
                        .SingleInstance();

        containerBuilder.Register(context =>
        {
            context.Resolve<IComponentContext>();
            return context.Resolve<MapperConfiguration>().CreateMapper(context.Resolve);

        }).As<IMapper>().InstancePerLifetimeScope();
    }

    private static MapperConfiguration MapperConfiguration() => new(mapper =>
    {
        mapper.AddProfile<CreateCustomerProfile>();
    });
}