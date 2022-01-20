using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleBackend.WebApi.Helpers;
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
        private readonly JobGenerator _jobGenerator;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="dispatcherService">Диспетчер задач</param>
        /// <param name="logger">Журнал логирования</param>
        /// <param name="jobGenerator">Генератор единиц работы</param>
        public TodoController(JobDispatcherService dispatcherService, JobGenerator jobGenerator)
        {
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger), "Отсутствует журнал логирования ошибок");
            _dispatcherService = dispatcherService ?? throw new ArgumentNullException(nameof(dispatcherService), "Отсутствует диспетчер обработки задач");
            _jobGenerator = jobGenerator ?? throw new ArgumentNullException(nameof(jobGenerator), "Отсутствует генератор работы");
        }

        /// <summary>
        /// Запрос записанных задач
        /// </summary>
        /// <returns> Список записанных задач</returns>
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            try
            {
                var id = Guid.NewGuid();
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateGetAllTodosJob(id));
                var result = new JobInfoResponse()
                {
                    Location = "Test Location",
                    ErrorCode = ErrorCodeType.NoError,
                    ErrorMessage = "все хорошо",
                    JobId = id
                };
                return Accepted(result);
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось запросить список всех задач. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                var result = new JobInfoResponse()
                {
                    Location = "none",
                    ErrorCode = ErrorCodeType.UnknownError,
                    ErrorMessage = errorMessage,
                    JobId = Guid.Empty
                };
                return BadRequest(result);
            }
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

        /// <summary>
        /// Удаление под задач
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteSubTodos() => await Task.Run(() => Ok());
    }
}