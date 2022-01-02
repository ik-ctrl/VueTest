using System;
using Microsoft.Extensions.Logging;

namespace SimpleBackend.WebApi.Models.Jobs
{
    /// <summary>
    /// Диспетчер задач
    /// </summary>
    public sealed class JobDispatcherService
    {
        private readonly AcceptedJobQueue _acceptingQueue;
        private readonly ResultJobQueue _resultQueue;
        private readonly Worker _worker;
        private readonly ILogger _logger;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="acceptingQueue">Очередь принятых задач</param>
        /// <param name="resultQueue">Очередь выполненых задач</param>
        /// <param name="jobWorker">Обработчки задач</param>
        /// <param name="logger">Журнал логирования</param>
        /// <exception cref="ArgumentException">Если одна из очередей, либо обработчик задач будут null</exception>
        public JobDispatcherService(AcceptedJobQueue acceptingQueue, ResultJobQueue resultQueue, Worker jobWorker, ILogger logger = null)
        {
            _acceptingQueue = acceptingQueue ?? throw new ArgumentException(nameof(acceptingQueue));
            _resultQueue = resultQueue ?? throw new ArgumentException(nameof(resultQueue));
            _worker = jobWorker ?? throw new ArgumentException(nameof(jobWorker));
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
                _acceptingQueue.EnqueueJob(newJob);
            }
            catch (Exception e)
            {
                _logger?.LogError($"Не удалось добавить задачу в список задач. Причина:{e.Message}");
                throw;
            }
        }
        
        public bool CheckStatusJob() => true;

        
        public object GetResultJob() => true;
        
        //
        // /// <summary>
        // /// Запрос записанных задач
        // /// </summary>
        // /// <returns> Список записанных задач</returns>
        // [HttpGet]
        // public async Task<IActionResult> GetTodos()
        // {
        //     return await Task.Run( () => Ok());
        // }
        //
        // /// <summary>
        // /// Создание новой задачи
        // /// </summary>
        // /// <returns>Результат выполнения операции</returns>
        // [HttpPost]
        // public async Task<IActionResult> AddTodo()
        // {
        //     return await Task.Run( () => Ok());
        // }
        //
        // /// <summary>
        // /// Обновление определенной задачи
        // /// </summary>
        // /// <returns></returns>
        // [HttpPost]
        // public async Task<IActionResult> UpdateTodo()
        // {
        //     return await Task.Run( () => Ok());
        // }
        
        
        

       

    }
}