using System;
using System.Collections.Generic;
using System.Linq;
using TechSupportWeb.Domain;
using TechSupportWeb.Models.Workers;
using TechSupportWeb.Services.Interfaces;

namespace TechSupportWeb.Services.Implementation
{
    internal class WorkersService : IWorkersService
    {
        private readonly IRepository<IssueInfo> issuesRepository;
        private readonly IRepository<WorkerInfo> workersRepository;

        public WorkersService(
            IRepository<IssueInfo> issuesRepository,
            IRepository<WorkerInfo> workersRepository)
        {
            this.issuesRepository = issuesRepository;
            this.workersRepository = workersRepository;
        }

        public IList<WorkerModel> GetWorkers()
        {
            var models = workersRepository.DoFunc(
                workers => workers
                    .Values
                    .Where(x => !x.IsDeleted)
                    .OrderByDescending(x => x.Level)
                    .ThenBy(x => x.ID)
                    .Select(x => new WorkerModel
                    {
                        ID = x.ID,
                        IsNotFixed = x.Level != WorkerLevels.Director,
                        Name = x.Name,
                        IssueId = x.IssueId
                    })
                    .ToList());

            issuesRepository.DoAction(
                issues =>
                {
                    foreach (var model in models)
                    {
                        model.IssueText = model.IssueId != null
                            ? issues[(long)model.IssueId].Text
                            : null;
                    }
                });

            return models;
        }

        public void AddWorker(WorkerLevels level)
        {
            if (level == WorkerLevels.Director)
            {
                throw new Exception("Запись директора добавить нельзя.");
            }

            var worker = new WorkerInfo { Level = level };
            workersRepository.Add(worker);
        }

        public void Delete(long workerId)
        {
            workersRepository.DoAction(
                workerId,
                worker =>
                {
                    if (worker.Level == WorkerLevels.Director)
                    {
                        throw new Exception("Запись директора удалить нельзя.");
                    }

                    worker.IsDeleted = true;
                });
        }

        public DetailsModel GetDetails(long workerId)
        {
            var details = workersRepository.DoFunc(
                workerId,
                worker => new DetailsModel
                {
                    ID = worker.ID,
                    IsDeleted = worker.IsDeleted ? "Да" : "Нет",
                    Name = worker.Name,
                    IssueId = worker.IssueId
                });

            if (details.IssueId != null)
            {
                details.IssueText = issuesRepository.DoFunc((long)details.IssueId, issue => issue.Text);
            }

            return details;
        }
    }
}