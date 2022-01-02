using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Extensions.Logging;
using SimpleBackend.WebApi.Models.Exceptions;

namespace SimpleBackend.WebApi.Models.Jobs
{
    /// <summary>
    /// Очередь принятых в обработку задач
    /// </summary>
    public sealed class AcceptedJobQueue
    {
        private readonly ILogger _logger;
        private readonly ConcurrentQueue<Job> _queue;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="logger">Журнал логирования</param>
        public AcceptedJobQueue(ILogger logger = null)
        {
            _logger = logger;
            _queue = new ConcurrentQueue<Job>();
        }

        /// <summary>
        /// Добавление новой работы в список
        /// </summary>
        /// <param name="newJob">Новая задача</param>
        /// <returns></returns>
        public void EnqueueJob(Job newJob)
        {
            _queue.Enqueue(newJob);
            _logger?.LogTrace($"AcceptedJobQueue::EnqueueJob::Работа добавлена в очередь:{newJob}");
        }

        /// <summary>
        /// Запрос первой в очереди задачи на обработку
        /// </summary>
        /// <returns>Первая задач из очереди</returns>
        public Job DequeueJob()
        {
            if (_queue.TryDequeue(out var result))
            {
                _logger?.LogTrace($"AcceptedJobQueue::DequeueJob::Работа изъята успешно:{result}");
                return result;
            }
            _logger?.LogTrace($"AcceptedJobQueue::DequeueJob::Работа не изъята");
            return null;
        }

        /// <summary>
        /// Флаг пустоты очереди
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                var result =_queue.IsEmpty;
                _logger?.LogTrace($"AcceptedJobQueue::DequeueJob::Результат проверки на пустоту={result}");
                return result;
            }
        }

        /// <summary>
        /// Проверка присутствия задачи в очереди
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns>Результат проверки задачи в очереди</returns>
        public bool CheckJob(Guid id)
        {
            if (IsEmpty)
            {
                _logger?.LogTrace("AcceptedJobQueue::CheckJob::Очередь пуста (результат false)");
                return false;
            }
            var result=_queue.FirstOrDefault(job => job.JobId.Equals(id)) != null;
            _logger?.LogTrace($"AcceptedJobQueue::CheckJob::Результат проверки = {result}");
            return result;
        }
        
        /// <summary>
        /// Очистка всей очереди
        /// </summary>
        /// <returns></returns>
        public void  ClearQueue()
        {
            try
            {
                _queue.Clear();
                _logger?.LogTrace($"AcceptedJobQueue::ClearQueue::Очередь очищена успешно");
            }
            catch (Exception e)
            {
                var message = $"Не удалось очистить очередь.Причина: {e.Message}";
                _logger?.LogTrace($"AcceptedJobQueue::ClearQueue::{message}");
                throw new AcceptingJobException(message, e);
            }
        }
    }
}