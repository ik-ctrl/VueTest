using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleBackend.WebApi.DTO;
using SimpleBackend.WebApi.Helpers;
using SimpleBackend.WebApi.Models.Enums;
using SimpleBackend.WebApi.Models.Jobs;

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
        private readonly ResponseGenerator _responseGenerator;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="dispatcherService">Диспетчер задач</param>
        /// <param name="jobGenerator">Генератор единиц работы</param>
        /// <param name="responseGenerator">Генератор ответов</param>
        public TodoController(JobDispatcherService dispatcherService, JobGenerator jobGenerator, ResponseGenerator responseGenerator)
        {
            _dispatcherService = dispatcherService ?? throw new ArgumentNullException(nameof(dispatcherService), "Отсутствует диспетчер обработки задач");
            _jobGenerator = jobGenerator ?? throw new ArgumentNullException(nameof(jobGenerator), "Отсутствует генератор работы");
            _responseGenerator= responseGenerator?? throw new ArgumentNullException(nameof(responseGenerator), "Отсутствует генератор ответов");
        }

        /// <summary>
        /// Запрос записанных задач
        /// </summary>
        /// <returns> Список записанных задач</returns>
        [HttpGet]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTodos()
        {
            try
            {
                var id = Guid.NewGuid();
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateGetAllTodosJob(id));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось запросить список всех задач. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }

        /// <summary>
        /// Создание новой задачи
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTodo(TodoDTO todo)
        {
            try
            {
                var id = Guid.NewGuid();
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateAddTodosJob(id, new List<TodoDTO>() { todo }));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось создать новую задачу. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }

        /// <summary>
        /// Создание новых задач по списку задач
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTodos(IEnumerable<TodoDTO> todos)
        {
            try
            {
                var id = Guid.NewGuid();
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateAddTodosJob(id, todos));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось запросить список всех задач. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }
        
        /// <summary>
        /// Обновление определенной задачи
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTodo(TodoDTO todo)
        {
            try
            {
                var id = Guid.NewGuid();
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateUpdateTodosJob(id, new List<TodoDTO>(){todo}));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось обновить задачу. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }
  
        /// <summary>
        /// Обновление определенной задачи
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTodos(IEnumerable<TodoDTO> todos)
        {
            try
            {
                var id = Guid.NewGuid();
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateUpdateTodosJob(id, todos));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось обновить список задач. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }
        
        /// <summary>
        /// Удаление определенной задачи по идентификатору выданной графической системой
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTodo(UiKeyDTO keyDTO)
        {
            try
            {
                var id = Guid.NewGuid();
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateRemoveTodosJob(id, new List<UiKeyDTO>{keyDTO}));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось удалить задачу по графическому ключу. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }
        
        /// <summary>
        /// Удаление списка задач по идентификаторам выданной графической системой
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTodos(IEnumerable<UiKeyDTO> keysDTO)
        {
            try
            {
                var id = Guid.NewGuid();
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateRemoveTodosJob(id, keysDTO));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось удалить несколько задач по графическим ключам. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }

        /// <summary>
        /// Создание новой подзадачи
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSubTodo(SubTodoDTO subTodo)
        {
            try
            {
                var id = Guid.NewGuid();
                var request = new SubTodoRequestDTO() { TodoUiKey = subTodo.TodoUiId, SubTodos = new List<SubTodoDTO>(){subTodo}};
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateAddSubTodosJob(id,request));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось создать новую подзадачу. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }

        /// <summary>
        /// Создание новых подзадач
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSubTodos(SubTodoRequestDTO subTodos)
        {
            try
            {
                var id = Guid.NewGuid();
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateAddSubTodosJob(id, subTodos));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось создать новые подзадачи по списку. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }
        
        /// <summary>
        /// Обновление определенной подзадачи
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSubTodo(SubTodoDTO subTodo)
        {
            try
            {
                var id = Guid.NewGuid();
                var request = new SubTodoRequestDTO() { TodoUiKey = subTodo.TodoUiId, SubTodos = new List<SubTodoDTO>(){subTodo}};
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateUpdateSubTodosJob(id, request));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось обновить подзадачу. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }
  
        /// <summary>
        /// Обновление списка подзадач задачи
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSubTodos(SubTodoRequestDTO subTodos)
        {
            try
            {
                var id = Guid.NewGuid();
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateUpdateSubTodosJob(id, subTodos));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось обновить список подзадач. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }
        
        /// <summary>
        /// Удаление определенной подзадачи по идентификатору выданной графической системой
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSubTodo(UiKeyDTO keyDTO)
        {
            try
            {
                var id = Guid.NewGuid();
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateRemoveSubTodosJob(id, new List<UiKeyDTO>{keyDTO}));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось удалить подзадачу по графическому ключу. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }
        
        /// <summary>
        /// Удаление списка подзадач по идентификаторам выданной графической системой
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JobInfoDTO),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSubTodos(IEnumerable<UiKeyDTO> keysDTO)
        {
            try
            {
                var id = Guid.NewGuid();
                await _dispatcherService.AddJobAsync(_jobGenerator.GenerateRemoveSubTodosJob(id, keysDTO));
                return Accepted(_responseGenerator.GenerateSuccessfulResponse("TEST Location", id));
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось удалить несколько задач по графическим ключам. Причина: {e.Message}";
                _logger?.LogError(errorMessage, e);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }
    }
}