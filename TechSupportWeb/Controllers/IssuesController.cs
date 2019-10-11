using System.Web.Mvc;
using TechSupportWeb.Services.Interfaces;

namespace TechSupportWeb.Controllers
{
    public class IssuesController : BaseController
    {
        private readonly IIssuesService service;

        public IssuesController(IIssuesService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult Queve()
        {
            var issues = service.GetIssuesQueve();
            return View(issues);
        }

        [HttpGet]
        public ActionResult History()
        {
            var issues = service.GetIssuesHistory();
            return View(issues);
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var details = service.GetDetails(id);
            return View(details);
        }

        [HttpPost]
        public ActionResult Create(string text)
        {
            var issueId = service.CreateIssue(text);
            return Json(issueId);
        }

        [HttpPost]
        public ActionResult GetState(long issueId)
        {
            var state = service.GetIssueState(issueId);
            return Json(state);
        }

        [HttpPost]
        public ActionResult Cancel(long issueId)
        {
            service.CancelIssue(issueId);
            return new EmptyResult();
        }

        protected override void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
            {
                return;
            }

            var action = context.RouteData.Values["action"];
            switch (action)
            {
                case nameof(Create):
                case nameof(GetState):
                case nameof(Cancel):
                    context.Result = Json(context.Exception.Message);
                    context.ExceptionHandled = true;
                    break;

                default:
                    base.OnException(context);
                    break;
            }
        }
    }
}