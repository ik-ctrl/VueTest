using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBackend.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для возврата результата запроса
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}/[action]")]
    public class JobResultController:ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult>GetResult() =>  await Task.Run(() => Ok());
    }
}