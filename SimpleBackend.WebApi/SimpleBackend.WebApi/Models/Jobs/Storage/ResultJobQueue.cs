using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace SimpleBackend.WebApi.Models.Jobs.Storage
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ResultJobQueue
    {
        private readonly ConcurrentDictionary<Guid, JobResult> _resultDictionary;
        private readonly ILogger _logger;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="logger">Журнал логирования</param>
        public ResultJobQueue(ILogger logger)
        {
            _logger = logger;
            _resultDictionary = new ConcurrentDictionary<Guid, JobResult>();
        }

        /// <summary>
        /// Проверка наличия результата работы
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <returns>Результат проверки наличия результата выполненой работы</returns>
        public bool CheckJobResult(Guid jobId)
        {
            var keys = _resultDictionary.Keys;
            if (keys.Any())
                return keys.Contains(jobId);
            return false;
        }

        /// <summary>
        /// Добавление нового результата 
        /// </summary>
        /// <param name="result">Результат выполненой работы</param>
        /// <exception cref="ArgumentException">result==null</exception>
        /// <exception cref="Exception">Не удалось добавить результат работы в очередь</exception>
        public void AddResult(JobResult result)
        {
            if (result == null)
                throw new ArgumentException(nameof(result));

            if (!_resultDictionary.TryAdd(result.Id, result))
                throw new Exception($"Не удалось добавить работу в список результатов:{result.Id}");
        }

        /// <summary>
        /// Запрос результат выполненой работы с последующим удалением
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <returns>Результат выполнения работы</returns>
        public JobResult GetResult(Guid jobId)
        {
            if (isEmpty)
                return null;
            if (_resultDictionary.TryGetValue(jobId, out var result))
                return result;
            throw new Exception("Не удалось изъять результат работы из очереди");
        }

        /// <summary>
        /// Флаг проверки на пустую очередь результатов
        /// </summary>
        public bool isEmpty => !_resultDictionary.Any();
    }
}