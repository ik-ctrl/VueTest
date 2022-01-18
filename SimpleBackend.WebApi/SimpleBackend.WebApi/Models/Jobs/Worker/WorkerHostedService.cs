using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleBackend.Database;
using SimpleBackend.WebApi.Models.Jobs.Storage;
using SimpleBackend.WebApi.Models.Worker;

namespace SimpleBackend.WebApi.Models.Jobs.Worker
{
    /// <summary>
    /// Сервис по обработки единиц работы 
    /// </summary>
    internal sealed class WorkerHostedService : IHostedService
    {
        private Timer _timer;
        private readonly ILogger _logger;
        private readonly AcceptedJobQueue _acceptedQueue;
        private readonly ResultJobQueue _resultQueue;
        private readonly TodoWorkerService _todoWorkerService;
        private readonly IServiceScopeFactory _scopeFactory;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="acceptedQueue">Очередь принятых задач</param>
        /// <param name="resultQueue">Очередь выполненых задач</param>
        /// <param name="todoService">Сервис обработки  тудушек</param>
        /// <param name="logger">Журнал логирования</param>
        /// <exception cref="ArgumentNullException">acceptedQueue==null</exception>
        /// <exception cref="ArgumentNullException">resultQueue==null</exception>
        public WorkerHostedService(TodoWorkerService todoService, AcceptedJobQueue acceptedQueue, ResultJobQueue resultQueue,ILogger logger = null)
        {
            _acceptedQueue = acceptedQueue ?? throw new ArgumentNullException(nameof(acceptedQueue));
            _resultQueue = resultQueue ?? throw new ArgumentNullException(nameof(resultQueue));
            _todoWorkerService = todoService;
            _logger = logger;
        }

        /// <summary>
        /// Запуск  обработчика задач 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Main, null, 1000, 1000);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Обработка задач
        /// </summary>
        /// <param name="instanse">Заглушка</param>
        private void Main(object instanse)
        {
            _timer?.Change(Timeout.Infinite, 0);
            Console.WriteLine("Worker is start");
            Console.WriteLine($"AcceptedQueue is empty?:{_acceptedQueue.IsEmpty}");
            Console.WriteLine($"ResultQueue is empty?:{_resultQueue.IsEmpty}");
            if (_acceptedQueue.IsEmpty)
            {
                _timer?.Change(1000, 1000);
                return;
            }
            var job = _acceptedQueue.Dequeue();
            Console.WriteLine($"Executed Job :{job}");
            try
            {
                var result = job.Type switch
                {
                    JobType.AddTodos => _todoWorkerService.AddTodos(job),
                    JobType.RemoveTodos => _todoWorkerService.RemoveTodos(job),
                    JobType.UpdateTodos => _todoWorkerService.UpdateTodos(job),
                    JobType.GetAllTodos => _todoWorkerService.GetAllTodos(job),
                    JobType.AddSubTodos => _todoWorkerService.AddSubTodos(job),
                    JobType.RemoveSubTodos => _todoWorkerService.RemoveSubTodos(job),
                    JobType.UpdateSubTodos => _todoWorkerService.UpdateSubTodos(job),
                    _ => throw new ArgumentOutOfRangeException(nameof(job.Type),"Неизвестный тип работы ")
                };
                _resultQueue.AddResult(result);
            }
            catch (Exception e)
            {
                var errorMessage = $"Возникла ошибка при выполнении единицы работы:{job.Id}.Причина:{e.Message}";
                _logger?.LogError(errorMessage, e);
                _resultQueue.AddResult(new JobResult
                {
                    Id = job.Id,
                    IsSuccess = false,
                    ResultObject = null,
                    Message = errorMessage
                });
            }
            Console.WriteLine("Worker is end");
            _timer?.Change(1000, 1000);
        }


        /// <summary>
        /// Окончание работы сервиса
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Main, null, 1000, 1000);
            return Task.CompletedTask;
        }
    }
}