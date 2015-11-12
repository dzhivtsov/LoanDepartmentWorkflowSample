using System;

namespace GangsterBank.Web.Infrastructure.Helpers.Location
{
    using System.Diagnostics.Contracts;
    using System.Web.Mvc;

    using GangsterBank.Core.Extensions;

    public class LocationHelper
    {
        public static bool IsCurrentControllerAndAction(string actionName, string controllerName, ViewContext viewContext)
        {
            Contract.Requires<ArgumentException>(controllerName.IsNotNull());
            Contract.Requires<ArgumentException>(actionName.IsNotNull());

            bool result = false;
            string normalizedControllerName = controllerName.EndsWith("Controller") ? controllerName : String.Format("{0}Controller", controllerName);

            if (viewContext == null) return false;
            if (String.IsNullOrEmpty(actionName)) return false;

            if (viewContext.ParentActionViewContext.Controller.GetType().Name.Equals(normalizedControllerName, StringComparison.InvariantCultureIgnoreCase) &&
                viewContext.ParentActionViewContext.Controller.ValueProvider.GetValue("action").AttemptedValue.Equals(actionName, StringComparison.InvariantCultureIgnoreCase))
            {
                result = true;
            }

            return result;
        }
    }
}