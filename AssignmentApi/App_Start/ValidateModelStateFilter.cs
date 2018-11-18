using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AssignmentApi.App_Start
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                if (actionContext.ModelState.ContainsKey(string.Empty))
                {
                    actionContext.ModelState.Add("DateValidation", actionContext.ModelState[string.Empty]);
                    actionContext.ModelState.Remove(string.Empty);
                }

                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}