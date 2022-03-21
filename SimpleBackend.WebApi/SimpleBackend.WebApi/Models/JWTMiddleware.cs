using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleBackend.WebApi.DTO;
using SimpleBackend.WebApi.Helpers;
using SimpleBackend.WebApi.Options;

namespace SimpleBackend.WebApi.Models
{
    /// <summary>
    /// Обработка токенов
    /// </summary>
    public  sealed  class JWTMiddleware
    {
        private  readonly RequestDelegate _next;
        private readonly PostgresDbContextFactory _factory;
        private readonly AuthOptions _options;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="next">Следующий этап обработки</param>
        /// <param name="appConfig">Конфигурация приложения</param>
        /// <param name="factory">Фабрика создания контекста БД</param>
        public JWTMiddleware(RequestDelegate next, IConfiguration appConfig, PostgresDbContextFactory factory)
        {
            _next = next;
            _options = new AuthOptions();
            appConfig.GetSection(nameof(AuthOptions)).Bind(_options);
        }
        
        /// <summary>
        /// Обработка запроса токенов
        /// </summary>
        /// <param name="context"></param>
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();

            if (token != null)
            {
                var (endTime, userId) = ValidateToken(token);
                if (userId == -1)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync( new ServerMessage() { Message= "Неизвестный пользователь"});
                    return;
                }

                if (endTime < DateTime.Now)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync( new ServerMessage() { Message= "Невалидный токен доступа.Необходимо обновить токены"});
                    return;
                }
                AttachUser(context,userId);
            }

            await _next(context);
        }

        
        /// <summary>
        /// Валидация токена доступа
        /// </summary>
        /// <param name="token">Токен доступа</param>
        /// <returns>Возвращается кортеж со временем окончания действия токена и идентификатором пользователя</returns>
        private (DateTime, int) ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_options.AccessSecretKey);
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    },
                    out SecurityToken validateToken
                );
                var decodeToken = (JwtSecurityToken)validateToken;
                var endTime = decodeToken.ValidFrom;
                var userId = int.Parse(decodeToken.Claims.First(x => x.Type == "id").Value);
                return (endTime,userId);
            }
            catch (Exception e)
            {
                return (DateTime.MinValue,-1);
            }
        }
        
        /// <summary>
        /// Прикрепление пользователя к запросу
        /// </summary>
        /// <param name="context">Контекст выполнения запроса</param>
        /// <param name="userId">Идентификатор пользователя</param>
        private void AttachUser(HttpContext context, int userId)
        {
            try
            {
                using (var db = _factory.CreateDbContext())
                {
                    context.Items["User"] = db.Users.First(user => user.Id == userId);
                }
            }
            catch (Exception e)
            {
                context.Items["User"] = null;
            }
        }
    }
}