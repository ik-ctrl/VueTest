using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBackend.WebApi.Controllers
{
    /// <summary>
    /// Контроллер обработки задач
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}/[action]")]
    public class TodoController:ControllerBase
    {
        
        /// <summary>
        /// Запрос записанных задач
        /// </summary>
        /// <returns> Список записанных задач</returns>
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return await Task.Run( () => Ok());
        }
        
        /// <summary>
        /// Создание новой задачи
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        [HttpPost]
        public async Task<IActionResult> AddTodo()
        {
            return await Task.Run( () => Ok());
        }
        
        /// <summary>
        /// Обновление определенной задачи
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateTodo()
        {
            return await Task.Run( () => Ok());
        }
        
        /// <summary>
        /// Удаление определенной задачи
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteTodo()
        {
            return await Task.Run( () => Ok());
        }

        public async Task<IActionResult> DeleteSubTodos() => await Task.Run(() => Ok());


    }
}