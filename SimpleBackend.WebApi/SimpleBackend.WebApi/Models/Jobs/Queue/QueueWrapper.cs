using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Extensions.Logging;
using SimpleBackend.WebApi.Models.Exceptions;

namespace SimpleBackend.WebApi.Models.Jobs.Queue
{
    /// <summary>
    /// Очередь
    /// </summary>
    public abstract class QueueWrapper<T> where T : class, IGuided
    {
        private readonly ILogger _logger;
        private readonly ConcurrentQueue<T> _queue;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="logger">Журнал логирования</param>
        public QueueWrapper(ILogger logger = null)
        {
            _logger = logger;
            _queue = new ConcurrentQueue<T>();
        }

        /// <summary>
        /// Добавление новой работы в список
        /// </summary>
        /// <param name="item">Новая задача</param>
        /// <returns></returns>
        public void Enqueue(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            _queue.Enqueue(item);
            _logger?.LogTrace($"AcceptedJobQueue::EnqueueJob::Работа добавлена в очередь:{item}");
        }

        /// <summary>
        /// Запрос первой в очереди задачи на обработку
        /// </summary>
        /// <returns>Первая задач из очереди</returns>
        public T Dequeue()
        {
            if (IsEmpty)
            {
                _logger?.LogTrace($"AcceptedJobQueue::DequeueJob::Очередь пуста");
                return null;
            }

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
                var result = _queue.IsEmpty;
                _logger?.LogTrace($"Результат проверки на пустоту={result}");
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
                _logger?.LogTrace("Очередь пуста (результат false)");
                return false;
            }

            var result = _queue.FirstOrDefault(item => item.CompareId(id)) != null;
            _logger?.LogTrace($"AcceptedJobQueue::CheckJob::Результат проверки = {result}");
            return result;
        }

        /// <summary>
        /// Очистка всей очереди
        /// </summary>
        /// <returns></returns>
        public void ClearQueue()
        {
            try
            {
                if (!IsEmpty)
                    _queue.Clear();
                _logger?.LogTrace("Очередь очищена успешно");
            }
            catch (Exception e)
            {
                var message = $"Не удалось очистить очередь.Причина: {e.Message}";
                _logger?.LogTrace($"{message}");
                throw new AcceptingJobException(message, e);
            }
        }
    }
}