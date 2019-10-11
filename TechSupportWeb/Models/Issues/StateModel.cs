using System;

namespace TechSupportWeb.Models.Issues
{
    /// <summary>
    /// Состояние запроса сводится к датам изменения состояний. Если все поля пусты значит запрос в очереди.
    /// </summary>
    public class StateModel
    {
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
        /// Время завершения обработки
        /// </summary>
        public DateTime? CompletedAt { get; set; }
    }
}