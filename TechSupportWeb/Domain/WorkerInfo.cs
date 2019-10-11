namespace TechSupportWeb.Domain
{
    /// <summary>
    /// Сведения о сотруднике
    /// </summary>
    public class WorkerInfo : EntityBase
    {
        public WorkerInfo()
        {
            IsDeleted = false;
        }

        /// <summary>
        /// Признак удаленного сотрудника
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Уровень / должность сотрудника
        /// </summary>
        public WorkerLevels Level { get; set; }

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name => Level == WorkerLevels.Director ? "Director" : $"{Level} #{ID}";

        /// <summary>
        /// ID текущего запроса
        /// </summary>
        public long? IssueId { get; set; }
    }
}