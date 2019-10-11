using System;
using System.ComponentModel.DataAnnotations;

namespace TechSupportWeb.Models.Issues
{
    public class QueveModel
    {
        [Display(Name = "Номер запроса")]
        public long ID { get; set; }

        [Display(Name = "Содержание запроса")]
        public string Text { get; set; }

        [Display(Name = "Время создания запроса")]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Длительность, сек")]
        public int Duration { get; set; }

        [Display(Name = "Время ожидания, сек")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double TimeOfWaiting { get; set; }
    }
}