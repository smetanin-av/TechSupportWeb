using System;

namespace TechSupportWeb.Domain
{
    /// <summary>
    /// Сведения о запросе
    /// </summary>
    public class IssueInfo : EntityBase
    {
        public IssueInfo()
        {
            CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// Текст запроса
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Длительность, сек
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Время создания запроса
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Время отмены запроса
        /// </summary>
        public DateTime? CanceledAt { get; set; }

        /// <summary>
        /// Время начала обработки
        /// </summary>
        public DateTime? AssignedAt { get; set; }

        /// <summary>
        /// ID сотрудника
        /// </summary>
        public long? WorkerId { get; set; }

        /// <summary>
        /// Время завершения обработки
        /// </summary>
        public DateTime? CompletedAt { get; set; }
    }
}