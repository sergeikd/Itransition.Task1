﻿using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FluentValidation.Mvc;
using Itransition.Task1.DALMongo;
//using Itransition.Task1.DAL;
using Itransition.Task1.Web.Infrastructure.CastleWindsor;

namespace Itransition.Task1.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            //Database.SetInitializer(new DbInitializer());
            new DbInitializer().Initialize();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CastleWindsorContainer();
        }

        private static void CastleWindsorContainer()
        {
            _container = new WindsorContainer()
                .Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            DataAnnotationsModelValidatorProvider.RegisterDefaultAdapterFactory(
                (metadata, context, attribute) => new WindsorModelValidator(_container, metadata, context, attribute));
            FluentValidationModelValidatorProvider.Configure(provider =>
            {
                provider.ValidatorFactory = new WindsorFluentValidatorFactory(_container);
            });
        }

        protected void Application_End()
        {
            _container.Dispose();
        }
    }
}
