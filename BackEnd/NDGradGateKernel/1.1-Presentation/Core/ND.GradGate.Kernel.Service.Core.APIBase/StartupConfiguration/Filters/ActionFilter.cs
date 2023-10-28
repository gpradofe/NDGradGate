using Microsoft.AspNetCore.Mvc.Filters;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration.Filters
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            LogContext.PushProperty("EventId", Guid.NewGuid());
        }
    }
}
