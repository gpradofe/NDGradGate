using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        #region Attibutes
        private ILogger<ApiExceptionFilter> _logger;
        #endregion

        #region Constructors
        public ApiExceptionFilter() : base()
        {
        }
        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger) : base()
        {
            _logger = logger;
        }
        #endregion
        #region Methods
        public override void OnException(ExceptionContext context)
        {
            int statusCodeException = 500;
            _logger?.LogError(context.Exception, $"Exception: {context.Exception.Message}");
            _logger?.LogError(context.Exception.InnerException, $"InnerExeception: {context.Exception?.InnerException?.Message}");

            context.Exception = context.Exception;
            context.HttpContext.Response.StatusCode = statusCodeException;
            var errors = new Dictionary<string, string>
            {
                {"Type", context.Exception.GetType().ToString() },
                {"Message", context.Exception.Message },
                {"Source", context.Exception.Source },
                {"StackTrace", context.Exception.StackTrace },
                {"CodeMessage", "INTERNAL_SERVER_ERROR" }
            };
            foreach (DictionaryEntry data in context.Exception.Data)
            {
                errors.Add(data.Key.ToString(), data.Value.ToString());
            }
            context.Result = new JsonResult(errors);
            base.OnException(context);
        }

        #endregion
    }
}
