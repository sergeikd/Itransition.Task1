using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using Castle.Windsor;

namespace Itransition.Task1.Web.Infrastructure.CastleWindsor
{
    public class WindsorModelValidator : DataAnnotationsModelValidator
    {
        public WindsorModelValidator(IWindsorContainer container, ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute)
            : base(metadata, context, attribute)
        {
            Type type = Attribute.GetType();

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanWrite && container.Kernel.HasComponent(property.PropertyType))
                {
                    object value = container.Resolve(property.PropertyType);
                    property.SetValue(Attribute, value, null);
                }
            }
        }
    }
}