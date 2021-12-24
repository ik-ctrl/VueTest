using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBackend.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для проверки статуса задания
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}/[action]")]
    public class JobStatusController:ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Check() =>  await Task.Run(() => Ok());
    }
}