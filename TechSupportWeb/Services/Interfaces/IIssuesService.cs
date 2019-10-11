using System.Collections.Generic;
using TechSupportWeb.Models.Issues;

namespace TechSupportWeb.Services.Interfaces
{
    public interface IIssuesService
    {
        /// <summary>
        /// Добавление нового запроса
        /// </summary>
        long CreateIssue(string text);

        /// <summary>
        /// Получение состояния запроса
        /// </summary>
        StateModel GetIssueState(long issueId);

        /// <summary>
        /// Отмена запроса
        /// </summary>
        void CancelIssue(long issueId);

        /// <summary>
        /// Список ожидающих запросов
        /// </summary>
        IList<QueveModel> GetIssuesQueve();

        /// <summary>
        /// Список выполненных запросов
        /// </summary>
        IList<HistoryModel> GetIssuesHistory();

        /// <summary>
        /// Проверка не завершились ли выполняющиеся запросы
        /// </summary>
        void ReviseActiveIssues();

        /// <summary>
        /// Попытка распределения запросов из очереди
        /// </summary>
        bool TryAssignIssue();

        /// <summary>
        /// Детальные сведения о запросе
        /// </summary>
        DetailsModel GetDetails(long issueId);
    }
}