using System.Web.Mvc;
using TechSupportWeb.Models.Settings;
using TechSupportWeb.Services.Interfaces;

namespace TechSupportWeb.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly ISettingsService service;

        public SettingsController(ISettingsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var settings = service.GetSettings();
            return View(settings);
        }

        [HttpPost]
        public ActionResult Update([Bind] SettingsModel model)
        {
            if (ModelState.IsValid)
            {
                service.SaveSettings(model);
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
    }
}