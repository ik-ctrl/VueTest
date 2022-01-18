using System;
using Microsoft.Extensions.Logging;
using SimpleBackend.WebApi.Models.Jobs.Storage;
using SimpleBackend.WebApi.Models.Jobs.Worker;

namespace SimpleBackend.WebApi.Models.Jobs
{
    /// <summary>
    /// Диспетчер задач
    /// </summary>
    public sealed class JobDispatcherService
    {
        private readonly AcceptedJobQueue _acceptingQueue;
        private readonly ResultJobQueue _resultQueue;
        private readonly ILogger _logger;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="acceptingQueue">Очередь принятых задач</param>
        /// <param name="resultQueue">Очередь выполненых задач</param>
        /// <param name="logger">Журнал логирования</param>
        /// <exception cref="ArgumentException">Если одна из очередей, либо обработчик задач будет null</exception>
        public JobDispatcherService(AcceptedJobQueue acceptingQueue, ResultJobQueue resultQueue, ILogger logger = null)
        {
            _acceptingQueue = acceptingQueue ?? throw new ArgumentException(nameof(acceptingQueue));
            _resultQueue = resultQueue ?? throw new ArgumentException(nameof(resultQueue));
            _logger = logger;
        }

        /// <summary>
        /// Добавление задачи в список задач принятых в обработку
        /// </summary>
        /// <param name="newJob">Новая задача</param>
        public void AddJob(Job newJob)
        {
            try
            {
                _acceptingQueue.Enqueue(newJob);
            }
            catch (Exception e)
            {
                _logger?.LogError($"Не удалось добавить задачу в список задач. Причина:{e.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// Проверка статуса выполнения задачи
        /// </summary>
        /// <returns>Статус выполнения задачи</returns>
        public bool CheckStatusJob()
        {
            return true;
        }
        
        /// <summary>
        /// Результат выполнения задачи
        /// </summary>
        /// <returns>Результат выполнения Задачи</returns>
        public object GetResultJob() => true;
    }
}