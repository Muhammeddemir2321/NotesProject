using NotesProject.Core.Services;
using NotesProject.Core.UnitOfWorks;
using NotesProject.Repository.UnitOfWorks;
using NotesProject.Repository;
using NotesProject.Service.Mapping;
using NotesProject.Service.Services;
using System.Reflection;
using Autofac;
using NotesProject.Repository.Repositories;
using NotesProject.Core.Repositories;

namespace NotesProject.API.Modules
{
    public class RepoServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<,>)).As(typeof(IService<,>))
                .InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As(typeof(IUnitOfWork));


            var ApiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes( repoAssembly, serviceAssembly)
                    .Where(x => x.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(repoAssembly, serviceAssembly)
                        .Where(x => x.Name.EndsWith("Service"))
                        .AsImplementedInterfaces()
                        .InstancePerLifetimeScope();
        }
    }
}