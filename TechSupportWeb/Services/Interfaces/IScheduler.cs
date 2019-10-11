namespace TechSupportWeb.Services.Interfaces
{
    public interface IScheduler
    {
        /// <summary>
        /// Старт фоновой задачи
        /// </summary>
        void Start();

        /// <summary>
        /// Остановка фоновой задачи
        /// </summary>
        void Stop();
    }
}