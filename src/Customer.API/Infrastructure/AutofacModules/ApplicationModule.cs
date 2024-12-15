using Autofac;
using CustomerMan.API.Application.Queries.CustomerQueries;
using CustomerMan.Domain.Seedwork;
using CustomerMan.Infrastructure.Repositories;
using CustomerMan.Infrastructure.Repositories.CustomerRepositories;
namespace CustomerMan.API.Infrastructure.AutofacModules;
public class ApplicationModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Register generic interface 'IRepository' and it's implemented service 'Repository'
        builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>));

        // Register all assembly types their names ends with 'Repository'
        builder.RegisterAssemblyTypes(typeof(CustomerRepository).Assembly)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

        // Register all assembly types their names ends with 'Queries'
        builder.RegisterAssemblyTypes(typeof(CustomerQueries).Assembly)
               .Where(t => t.Name.EndsWith("Queries"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
    }
}