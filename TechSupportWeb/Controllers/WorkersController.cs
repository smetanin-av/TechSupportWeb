using System.Web.Mvc;
using TechSupportWeb.Services.Interfaces;

namespace TechSupportWeb.Controllers
{
    public class WorkersController : BaseController
    {
        private readonly IWorkersService service;

        public WorkersController(IWorkersService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var workers = service.GetWorkers();
            return View(workers);
        }

        [HttpGet]
        public ActionResult AddOperator()
        {
            service.AddWorker(Domain.WorkerLevels.Operator);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddManager()
        {
            service.AddWorker(Domain.WorkerLevels.Manager);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var details = service.GetDetails(id);
            return View(details);
        }
    }
}