using System.ComponentModel.DataAnnotations;

namespace TechSupportWeb.Models.Workers
{
    public class WorkerModel
    {
        [Display(Name = "Номер сотрудника")]
        public long ID { get; set; }

        [Display(Name = "Можно удалить")]
        public bool IsNotFixed { get; set; }

        [Display(Name = "Имя сотрудника")]
        public string Name { get; set; }

        [Display(Name = "ID текущего запроса")]
        public long? IssueId { get; set; }

        [Display(Name = "Текущий запрос")]
        public string IssueText { get; set; }
    }
}