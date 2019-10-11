using System.ComponentModel.DataAnnotations;

namespace TechSupportWeb.Models.Settings
{
    public class SettingsModel
    {
        [Display(Name = "Время получения запроса менеджером (Tm), сек")]
        [Required(ErrorMessage = "Не указано время через которое запрос может обработать менеджер")]
        [Range(15, 60)]
        public int TimeTm { get; set; }

        [Display(Name = "Время получения запроса директором Td, сек")]
        [Required(ErrorMessage = "Не указано время через которое запрос может обработать директор")]
        [Range(60, 120)]
        public int TimeTd { get; set; }

        [Display(Name = "Минимальное время обработки, сек")]
        [Required(ErrorMessage = "Не указано минимальное время обработки")]
        [Range(15, 60)]
        public int SpanMin { get; set; }

        [Display(Name = "Максимальное время обработки, сек")]
        [Required(ErrorMessage = "Не указано максимальное время обработки")]
        [Range(60, 120)]
        public int SpanMax { get; set; }
    }
}