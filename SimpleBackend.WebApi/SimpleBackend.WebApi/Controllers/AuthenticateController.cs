using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleBackend.WebApi.DTO;
using SimpleBackend.WebApi.Models;

namespace SimpleBackend.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для аутентификации пользователей
    /// </summary>
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}/[action]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthenticateController> _logger;

        /// <summary>
        /// Инициализация 
        /// </summary>
        /// <param name="authService">Сервис аутентификации пользователя</param>
        /// <param name="logger">Журнал логирования</param>
        /// <exception cref="ArgumentNullException">Сервис аутентификации равен null</exception>

        public AuthenticateController(AuthService authService, ILogger<AuthenticateController> logger = null)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService), "Не удалось инициализировать контроллер аутентификации");
            _logger = logger;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <returns>Данные пользователя с токеном</returns>
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationDTO request)
        {
            try
            {
                var result =await _authService.RegistrationAsync(request);
                return Ok(result);
            }
            catch (Exception e)
            {
               _logger?.LogError(e,"Не удалось зарегистрировать пользователя");
               return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Аутентификация пользователя 
        /// </summary>
        /// <returns>Данные пользователя с токеном</returns>
        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthDTO request)
        {
            try
            {
                var result =await _authService.Authentification(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger?.LogError(e,"Не удалось аутентифицировать пользователя");
                return BadRequest(e.Message);
            }
        }
    }
}