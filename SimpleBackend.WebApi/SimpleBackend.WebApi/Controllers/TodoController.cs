using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleBackend.WebApi.Models.Enums;
using SimpleBackend.WebApi.Models.Jobs;
using SimpleBackend.WebApi.Models.Responses;
using SimpleBackend.WebApi.Models.Worker;

namespace SimpleBackend.WebApi.Controllers
{
    /// <summary>
    /// Контроллер обработки задач
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}/[action]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly JobDispatcherService _dispatcherService;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="dispatcherService">Диспетчер задач</param>
        /// <param name="logger">Журнал логирования</param>
        public TodoController(JobDispatcherService dispatcherService, ILogger<TodoController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Отсутвует журнал логирования ошибок");
            _dispatcherService = dispatcherService ?? throw new ArgumentNullException(nameof(dispatcherService), "Отсутвует диспетчер обработки задач");
            ;
        }

        /// <summary>
        /// Запрос записанных задач
        /// </summary>
        /// <returns> Список записанных задач</returns>
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return await Task.Run(async () =>
            {
                var id = Guid.NewGuid();
                var job = new Job()
                {
                    Id = id ,
                    Message = string.Empty,
                    Type = JobType.GetAllTodos,
                    JobObject = null
                };
                    //todo: проверка асинхронности
               //  await Task.Delay(100000000);
                _dispatcherService.AddJob(job);
                var result = new JobInfoResponse()
                {
                    Location = "localhost",
                    ErrorCode = ErrorCodeType.NoError,
                    ErrorMessage = "все хорошо",
                    JobId = id
                };
                return Accepted(result);
            });
        }

        /// <summary>
        /// Создание новой задачи
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpPost]
        public async Task<IActionResult> AddTodo()
        {
            return await Task.Run(() => Ok());
        }

        /// <summary>
        /// Обновление определенной задачи
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateTodo()
        {
            return await Task.Run(() => Ok());
        }

        /// <summary>
        /// Удаление определенной задачи
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteTodo()
        {
            return await Task.Run(() => Ok());
        }

        public async Task<IActionResult> DeleteSubTodos() => await Task.Run(() => Ok());
    }
}