using TechSupportWeb.Models.Settings;
using TechSupportWeb.Services.Interfaces;

namespace TechSupportWeb.Services.Implementation
{
    internal class SettingsService : ISettingsService
    {
        private readonly object locker = new object();

        private int timeTm;
        private int timeTd;

        private int spanMin;
        private int spanMax;

        public SettingsService()
        {
            timeTm = 30;
            timeTd = 60;

            spanMin = 30;
            spanMax = 60;
        }

        public SettingsModel GetSettings()
        {
            lock (locker)
            {
                return new SettingsModel
                {
                    TimeTm = timeTm,
                    TimeTd = timeTd,

                    SpanMin = spanMin,
                    SpanMax = spanMax
                };
            }
        }

        public void SaveSettings(SettingsModel model)
        {
            lock (locker)
            {
                timeTm = model.TimeTm;
                timeTd = model.TimeTd;

                spanMin = model.SpanMin;
                spanMax = model.SpanMax;
            }
        }
    }
}