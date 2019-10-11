using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using TechSupportWeb.Services.Interfaces;

namespace TechSupportWeb.Services.Implementation
{
    public class Scheduler : IScheduler
    {
        private CancellationTokenSource cancel;
        private Task task;

        public void Start()
        {
            var service = DependencyResolver.Current.GetService<IIssuesService>();
            cancel = new CancellationTokenSource();
            task = Task.Run(() => IssuesJob(service, cancel.Token));
        }

        public void Stop()
        {
            cancel?.Cancel();
            task?.Wait();
            cancel?.Dispose();
        }

        private void IssuesJob(IIssuesService service, CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                // проверка выполняющихся запросов
                service.ReviseActiveIssues();
                Thread.Sleep(100);

                // попытка распределения запросов из очереди
                while (service.TryAssignIssue())
                {
                    Thread.Sleep(100);
                }

                Thread.Sleep(1000);
            }
        }
    }
}