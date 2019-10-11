using System.Collections.Generic;
using TechSupportWeb.Domain;
using TechSupportWeb.Models.Workers;

namespace TechSupportWeb.Services.Interfaces
{
    public interface IWorkersService
    {
        /// <summary>
        /// Сведения о сотрудниках
        /// </summary>
        IList<WorkerModel> GetWorkers();

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        void AddWorker(WorkerLevels level);

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        void Delete(long workerId);

        /// <summary>
        /// Детальные сведения о сотруднике
        /// </summary>
        DetailsModel GetDetails(long workerId);
    }
}