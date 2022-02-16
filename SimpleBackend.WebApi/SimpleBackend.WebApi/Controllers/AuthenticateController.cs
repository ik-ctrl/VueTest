using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBackend.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для аутентификации пользователей
    /// </summary>
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}/[action]")]
    public class AuthenticateController:ControllerBase
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <returns>Данные пользователя с токеном</returns>
        public async Task<IActionResult> Registration() => await Task.Run(() => Ok());

        /// <summary>
        /// Аутентификация пользователя 
        /// </summary>
        /// <returns>Данные пользователя с токеном</returns>
        public async Task<IActionResult> Authenticate() => await Task.Run(() => Ok());


    }
}