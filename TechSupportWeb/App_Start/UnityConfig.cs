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

            // ������ �������� �� � ��, ������� ������������ ����������, ����� ��� �� ����������� ������ ���
            container.RegisterInstance(container.Resolve<IRepository<IssueInfo>>());

            var workersRepository = container.Resolve<IRepository<WorkerInfo>>();
            container.RegisterInstance(workersRepository);

            // �������� ����������� ���������� � �� ����� ���� ������
            workersRepository.Add(new WorkerInfo { Level = WorkerLevels.Director });

            container.RegisterType<IIssuesService, IssuesService>();
            container.RegisterType<IWorkersService, WorkersService>();
            container.RegisterType<ISettingsService, SettingsService>();

            container.RegisterType<IScheduler, Scheduler>();

            // ������ �������� �� � ��, ������� ������������ ��������� �������
            var settingsService = container.Resolve<ISettingsService>();
            container.RegisterInstance(settingsService);

            // ��� �� ����� ��������� ��� �������� �������
            var scheduler = container.Resolve<IScheduler>();
            container.RegisterInstance(scheduler);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}