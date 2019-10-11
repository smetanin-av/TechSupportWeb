using System.Web.Mvc;

namespace TechSupportWeb.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
            {
                return;
            }

            context.Result = View("Error", context.Exception);
            context.ExceptionHandled = true;
        }
    }
}