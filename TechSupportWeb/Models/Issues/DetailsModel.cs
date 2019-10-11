using System;
using System.ComponentModel.DataAnnotations;

namespace TechSupportWeb.Models.Issues
{
    public class DetailsModel
    {
        [Display(Name = "Номер запроса")]
        public long ID { get; set; }

        [Display(Name = "Содержание запроса")]
        public string Text { get; set; }

        [Display(Name = "Время создания")]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Длительность, сек")]
        public int Duration { get; set; }

        [Display(Name = "Время отмены")]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? CanceledAt { get; set; }

        [Display(Name = "Начало обработки")]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? AssignedAt { get; set; }

        [Display(Name = "Время ожидания, сек")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double TimeOfWaiting { get; set; }

        [Display(Name = "ID сотрудника")]
        public long? WorkerId { get; set; }

        [Display(Name = "Имя сотрудника")]
        public string WorkerName { get; set; }

        [Display(Name = "Завершение обработки")]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? CompletedAt { get; set; }

        [Display(Name = "Время обработки, сек")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double? TimeOfProcess { get; set; }
    }
}