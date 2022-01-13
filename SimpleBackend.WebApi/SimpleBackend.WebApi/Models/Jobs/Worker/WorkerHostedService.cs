using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleBackend.WebApi.Models.Jobs.Storage;

namespace SimpleBackend.WebApi.Models.Jobs.Worker
{
    /// <summary>
    /// Сервис по обработки заданной работы 
    /// </summary>
    internal sealed class WorkerHostedService:IHostedService
    {
        private Timer _timer;
        private readonly ILogger _logger;
        private readonly AcceptedJobQueue _acceptedQueue;
        private readonly ResultJobQueue _resultQueue;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="acceptedQueue">Очередь принятых задач</param>
        /// <param name="resultQueue">Очередь выполненых задач</param>
        /// <param name="logger">Журнал логирования</param>
        /// <exception cref="ArgumentNullException">acceptedQueue==null</exception>
        /// <exception cref="ArgumentNullException">resultQueue==null</exception>
        public WorkerHostedService(AcceptedJobQueue acceptedQueue,ResultJobQueue resultQueue,ILogger logger=null)
        {
            _acceptedQueue = acceptedQueue?? throw  new ArgumentNullException(nameof(acceptedQueue));
            _resultQueue = resultQueue?? throw  new ArgumentNullException(nameof(resultQueue));
            _logger = logger;
        }

        /// <summary>
        /// Обработка задач
        /// </summary>
        /// <param name="instanse">Заглушка</param>
        private void Main(object instanse)
        {
            _timer?.Change(Timeout.Infinite, 0);
            Console.WriteLine("Worker is start");
            if(_acceptedQueue.IsEmpty)
                return;

            var job = _acceptedQueue.Dequeue();
            Console.WriteLine($"AcceptedQueue is empty?:{_acceptedQueue.IsEmpty}");
            Console.WriteLine($"ResultQueue is empty?:{_resultQueue.IsEmpty}");
            Console.WriteLine("Worker is end");
            _timer?.Change(1000, 1000);
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
        /// Окончание работы обработчика
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