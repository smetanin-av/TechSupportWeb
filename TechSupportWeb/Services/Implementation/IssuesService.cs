using System;
using System.Collections.Generic;
using System.Linq;
using TechSupportWeb.Domain;
using TechSupportWeb.Models.Issues;
using TechSupportWeb.Services.Interfaces;

namespace TechSupportWeb.Services.Implementation
{
    internal class IssuesService : IIssuesService
    {
        private readonly IRepository<IssueInfo> issuesRepository;
        private readonly IRepository<WorkerInfo> workersRepository;

        private readonly ISettingsService settingsService;

        public IssuesService(
            IRepository<IssueInfo> issuesRepository,
            IRepository<WorkerInfo> workersRepository,
            ISettingsService settingsService)
        {
            this.issuesRepository = issuesRepository;
            this.workersRepository = workersRepository;

            this.settingsService = settingsService;
        }

        public long CreateIssue(string text)
        {
            var issue = new IssueInfo { Text = text, Duration = GenerateDuration() };
            issuesRepository.Add(issue);
            return issue.ID;
        }

        public StateModel GetIssueState(long issueId)
        {
            return issuesRepository.DoFunc(
                issueId,
                issue => new StateModel
                {
                    CreatedAt = issue.CreatedAt,
                    CanceledAt = issue.CanceledAt,
                    AssignedAt = issue.AssignedAt,
                    CompletedAt = issue.CompletedAt
                });
        }

        public void CancelIssue(long issueId)
        {
            var workerId = issuesRepository.DoFunc(
                issueId,
                issue =>
                {
                    if (issue.CompletedAt != null)
                    {
                        throw new Exception($"Запрос #{issueId} уже выполнен.");
                    }

                    issue.CanceledAt = DateTime.Now;
                    return issue.WorkerId;
                });

            if (workerId != null)
            {
                SetWorkerFree((long)workerId);
            }
        }

        private void SetWorkerFree(long workerId)
        {
            workersRepository.DoAction(workerId, worker => { worker.IssueId = null; });
        }

        public IList<QueveModel> GetIssuesQueve()
        {
            return issuesRepository.DoFunc(
                issues => issues
                    .Values
                    .Where(x => x.AssignedAt == null && x.CanceledAt == null)
                    .OrderBy(x => x.CreatedAt)
                    .Select(x => new QueveModel
                    {
                        ID = x.ID,
                        Text = x.Text,
                        CreatedAt = x.CreatedAt,
                        Duration = x.Duration,
                        TimeOfWaiting = GetTimeOfWaiting(x)
                    })
                    .ToList());
        }

        public IList<HistoryModel> GetIssuesHistory()
        {
            var models = issuesRepository.DoFunc(
                issues => issues
                    .Values
                    .Where(x => (x.AssignedAt != null && x.CompletedAt != null) || x.CanceledAt != null)
                    .OrderBy(x => x.CreatedAt)
                    .Select(x => new HistoryModel
                    {
                        ID = x.ID,
                        Text = x.Text,
                        CreatedAt = x.CreatedAt,
                        CanceledAt = x.CanceledAt,
                        AssignedAt = x.AssignedAt,
                        TimeOfWaiting = GetTimeOfWaiting(x),
                        WorkerId = x.WorkerId,
                        CompletedAt = x.CompletedAt,
                        TimeOfProcess = GetTimeOfProcess(x)
                    })
                    .ToList());

            workersRepository.DoAction(workers =>
            {
                foreach (var model in models)
                {
                    model.WorkerName = model.WorkerId != null
                        ? workers[(long)model.WorkerId].Name
                        : null;
                }
            });

            return models;
        }

        public void ReviseActiveIssues()
        {
            var workersIds = issuesRepository.DoFunc(
                issues =>
                {
                    var freed = new List<long>();

                    foreach (var issue in issues.Values.Where(IsIssueInProgress))
                    {
                        var elapsed = GetTimeOfProcess(issue);
                        if (elapsed >= issue.Duration)
                        {
                            issue.CompletedAt = DateTime.Now;
                            if (issue.WorkerId != null)
                            {
                                freed.Add((long)issue.WorkerId);
                            }
                        }
                    }

                    return freed;
                });

            foreach (var workerId in workersIds)
            {
                SetWorkerFree(workerId);
            }
        }

        public bool TryAssignIssue()
        {
            long? issueId;
            double? elapsed;

            (issueId, elapsed) = issuesRepository.DoFunc(
                issues =>
                {
                    var issue = issues
                        .Values
                        .OrderBy(x => x.CreatedAt)
                        .FirstOrDefault(x => x.AssignedAt == null && x.CanceledAt == null);

                    return issue != null
                        ? (issue.ID, GetTimeOfWaiting(issue))
                        : ((long?)null, (double?)null);
                });

            if (issueId == null)
            {
                return false;
            }

            var settings = settingsService.GetSettings();
            var level = elapsed < settings.TimeTm
                ? WorkerLevels.Operator
                : elapsed < settings.TimeTd
                    ? WorkerLevels.Manager
                    : WorkerLevels.Director;

            var workerId = workersRepository.DoFunc(
                workers =>
                {
                    var worker = workers
                        .Values
                        .FirstOrDefault(x => !x.IsDeleted && x.Level <= level && x.IssueId == null);

                    if (worker != null)
                    {
                        worker.IssueId = issueId;
                    }

                    return worker?.ID;
                });

            if (workerId != null)
            {
                issuesRepository.DoAction(
                    (long)issueId,
                    issue =>
                    {
                        issue.AssignedAt = DateTime.Now;
                        issue.WorkerId = workerId;
                    });
            }

            return workerId != null;
        }

        public DetailsModel GetDetails(long issueId)
        {
            var details = issuesRepository.DoFunc(
                issueId,
                issue => new DetailsModel
                {
                    ID = issue.ID,
                    Text = issue.Text,
                    CreatedAt = issue.CreatedAt,
                    Duration = issue.Duration,
                    CanceledAt = issue.CanceledAt,
                    AssignedAt = issue.AssignedAt,
                    TimeOfWaiting = GetTimeOfWaiting(issue),
                    WorkerId = issue.WorkerId,
                    CompletedAt = issue.CompletedAt,
                    TimeOfProcess = GetTimeOfProcess(issue)
                });

            if (details.WorkerId != null)
            {
                details.WorkerName = workersRepository.DoFunc((long)details.WorkerId, worker => worker.Name);
            }

            return details;
        }

        private int GenerateDuration()
        {
            var settings = settingsService.GetSettings();
            return new Random().Next(settings.SpanMin, settings.SpanMax);
        }

        private double GetTimeOfWaiting(IssueInfo issue)
        {
            var endedAt = issue.AssignedAt ?? issue.CanceledAt ?? DateTime.Now;
            return (endedAt - issue.CreatedAt).TotalSeconds;
        }

        private double? GetTimeOfProcess(IssueInfo issue)
        {
            if (issue.AssignedAt == null)
            {
                return null;
            }

            var endedAt = issue.CompletedAt ?? issue.CanceledAt ?? DateTime.Now;
            return (endedAt - (DateTime)issue.AssignedAt).TotalSeconds;
        }

        private bool IsIssueInProgress(IssueInfo issue)
        {
            return issue.AssignedAt != null && issue.CanceledAt == null && issue.CompletedAt == null;
        }
    }
}