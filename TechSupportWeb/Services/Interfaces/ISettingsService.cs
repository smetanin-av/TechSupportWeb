using TechSupportWeb.Models.Settings;

namespace TechSupportWeb.Services.Interfaces
{
    public interface ISettingsService
    {
        /// <summary>
        /// Получение текущих настроек (Tm, Td и т.д.)
        /// </summary>
        SettingsModel GetSettings();

        /// <summary>
        /// Изменение настроек
        /// </summary>
        void SaveSettings(SettingsModel model);
    }
}