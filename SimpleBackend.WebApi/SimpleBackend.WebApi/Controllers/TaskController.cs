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
    public class TaskController:ControllerBase
    {

        /// <summary>
        /// Тестовый метод
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetInfo()
        {
            return Ok();
        }
        
    }
}