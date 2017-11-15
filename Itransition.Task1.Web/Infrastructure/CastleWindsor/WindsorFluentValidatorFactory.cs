using System;
using Castle.Windsor;
using FluentValidation;

namespace Itransition.Task1.Web.Infrastructure.CastleWindsor
{
    public class WindsorFluentValidatorFactory : ValidatorFactoryBase
    {

        private readonly IWindsorContainer _container;

        public WindsorFluentValidatorFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            if (_container.Kernel.HasComponent(validatorType))
            {
                return _container.Kernel.Resolve(validatorType) as IValidator;
            }
            return null;
        }
    }
}