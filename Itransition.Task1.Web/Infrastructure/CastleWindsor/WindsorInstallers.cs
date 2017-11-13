using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Itransition.Task1.DAL;
using Itransition.Task1.BL.Services;
using Itransition.Task1.DAL.Repositories;

namespace Itransition.Task1.Web.Infrastructure.CastleWindsor
{
    public class WindsorInstallers : IWindsorInstaller
    {
        
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Controllers insataller
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestyleTransient());

            //Servces installer
            container.Register(Classes.FromAssemblyContaining(typeof(BankAccountService))
                .Where(x => x.Name.EndsWith("Service"))
                .WithServiceAllInterfaces()
                .LifestylePerWebRequest());

            //Repositories insataller
            container.Register(Classes.FromAssemblyContaining(typeof (BaseRepository<>))
            .Where(x => x.Name.EndsWith("Repository"))
            .WithServiceAllInterfaces()
                .LifestylePerWebRequest());

            //DbContext installer
            container.Register(Component.For<AppDbContext>().LifeStyle.PerWebRequest);
        }

    }
}