using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SimpleBackend.WebApi.Models.Enums;
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
        /// Асинхронное добавление задачи в список задач принятых в обработку
        /// </summary>
        /// <param name="newJob">Новая задача</param>
        public async Task AddJobAsync(Job newJob) =>  await Task.Run(() => AddJob(newJob));
        
        /// <summary>
        /// Проверка статус выполнения задачи
        /// </summary>
        /// <returns>Статус выполнения задачи</returns>
        public JobStatusType CheckStatusJob(Guid jobId)
        {
            try
            {
                if (_acceptingQueue.CheckJob(jobId))
                    return JobStatusType.Accepted;
                else if (_resultQueue.CheckJobResult(jobId))
                    return JobStatusType.Finished;
                return JobStatusType.NotFound;
            }
            catch (Exception e)
            {
                _logger?.LogError($"Не удалось проверить состояние выполняемой работы. Причина:{e.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// Асинхронное проверка статус выполнения задачи
        /// </summary>
        /// <returns>Статус выполнения задачи</returns>
        public async Task<JobStatusType> CheckStatusJobAsync(Guid jobId) => await Task.Run(() => CheckStatusJob(jobId));

        /// <summary>
        /// Результат выполнения задачи
        /// </summary>
        /// <returns>Результат выполнения Задачи</returns>
        public object GetResultJob(Guid jobId)
        {
            try
            {
                return !_resultQueue.CheckJobResult(jobId) ? null : _resultQueue.GetResult(jobId);
            }
            catch (Exception e)
            {
                _logger?.LogError($"Не удалось проверить состояние выполняемой работы. Причина:{e.Message}");
                throw;
            }
        }

        /// <summary>
        /// Асинхронный запрос результат  выполнения задачи
        /// </summary>
        /// <returns>Результат выполнения задачи</returns>
        public async Task<object> GetResultJobAsync(Guid jobId) => await Task.Run(() => GetResultJob(jobId));
 
    }
}