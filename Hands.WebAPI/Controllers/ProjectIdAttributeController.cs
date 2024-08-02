using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

using System.Web.Http.Filters;
using Hands.WebAPI.Properties;

namespace Hands.WebAPI.Controllers
{
    public class ProjectIdAttributeController : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var currentprojectId = filterContext.Request.Headers.Contains("projectId") ? filterContext.Request.Headers.GetValues("projectId").First() : Settings.Default.ProjectId.ToString();

            string projectId = currentprojectId;

            filterContext.ActionArguments["projectId"] = int.Parse(projectId);

            base.OnActionExecuting(filterContext);
        }
    }
}