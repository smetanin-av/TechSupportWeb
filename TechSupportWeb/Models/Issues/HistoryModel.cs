using System;
using System.ComponentModel.DataAnnotations;

namespace TechSupportWeb.Models.Issues
{
    public class HistoryModel
    {
        [Display(Name = "Номер запроса")]
        public long ID { get; set; }

        [Display(Name = "Запрос")]
        public string Text { get; set; }

        [Display(Name = "Время создания")]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Время отмены")]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}")]
        public DateTime? CanceledAt { get; set; }

        [Display(Name = "Начало обработки")]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}")]
        public DateTime? AssignedAt { get; set; }

        [Display(Name = "Время ожидания, сек")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double TimeOfWaiting { get; set; }

        [Display(Name = "ID сотрудника")]
        public long? WorkerId { get; set; }

        [Display(Name = "Имя сотрудника")]
        public string WorkerName { get; set; }

        [Display(Name = "Завершение обработки")]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}")]
        public DateTime? CompletedAt { get; set; }

        [Display(Name = "Время обработки, сек")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double? TimeOfProcess { get; set; }
    }
}