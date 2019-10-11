using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

using TechSupportWeb.Services.Interfaces;
using TechSupportWeb.Services.Implementation;
using TechSupportWeb.Domain;

namespace TechSupportWeb
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));

            // данные хран€тс€ не в Ѕƒ, поэтому регистрируем экземпл€ры, чтобы они не создавались каждый раз
            container.RegisterInstance(container.Resolve<IRepository<IssueInfo>>());

            var workersRepository = container.Resolve<IRepository<WorkerInfo>>();
            container.RegisterInstance(workersRepository);

            // директор добавл€етс€ изначально и не может быть удален
            workersRepository.Add(new WorkerInfo { Level = WorkerLevels.Director });

            container.RegisterType<IIssuesService, IssuesService>();
            container.RegisterType<IWorkersService, WorkersService>();
            container.RegisterType<ISettingsService, SettingsService>();

            container.RegisterType<IScheduler, Scheduler>();

            // данные хран€тс€ не в Ѕƒ, поэтому регистрируем экземпл€р сервиса
            var settingsService = container.Resolve<ISettingsService>();
            container.RegisterInstance(settingsService);

            // так же нужен экземпл€р дл€ фонового сервиса
            var scheduler = container.Resolve<IScheduler>();
            container.RegisterInstance(scheduler);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}