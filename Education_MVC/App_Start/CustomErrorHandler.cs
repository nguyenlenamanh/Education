using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Education_MVC.App_Start
{
    public class CustomErrorHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;

            filterContext.ExceptionHandled = true;

            var result = new ViewResult
            {
                ViewName = "~/Views/Error.cshtml"
            };

            filterContext.Result = result;
        }

    }
}