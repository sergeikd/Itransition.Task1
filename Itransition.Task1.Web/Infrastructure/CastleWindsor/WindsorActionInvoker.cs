using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using Itransition.Task1.DAL;

namespace Itransition.Task1.Web.Infrastructure.CastleWindsor
{
    public class WindsorActionInvoker: AsyncControllerActionInvoker
    {
        private readonly AppDbContext _context;

        public WindsorActionInvoker(AppDbContext context)
        {
            _context = context;
        }

        protected override ActionResult InvokeActionMethod(ControllerContext controllerContext,
            ActionDescriptor actionDescriptor,
            IDictionary<string, object> parameters)
        {
            var actionResult = base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);
            _context.SaveChanges();
            return actionResult;
        }
    }
}