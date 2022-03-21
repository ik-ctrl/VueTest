using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBackend.Database.Entities;
using SimpleBackend.WebApi.DTO;
using SimpleBackend.WebApi.Helpers;

namespace SimpleBackend.WebApi.Models
{
    /// <summary>
    /// Сервис для аутентификации 
    /// </summary>
    public class AuthService
    {
        private readonly PostgresDbContextFactory _factory;
        private readonly TokenGenerator _tokenGenerator;

        /// <summary>
        /// Сервис аутентификации пользователя
        /// </summary>
        /// <param name="factory">Фабрика создания контекста базы данных</param>
        /// <param name="tokenGenerator">Генератор токенов</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AuthService(PostgresDbContextFactory factory, TokenGenerator tokenGenerator)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory), "Не удалось инициализировать сервис аутентификации");
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator), "Не удалось инициализировать сервис аутентификации");
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="dto">Данные для регистрации пользователя</param>
        /// <returns>Данные авторизированного пользователя</returns>
        /// <exception cref="ArgumentNullException">DTO = null </exception>
        /// <exception cref="ArgumentException">Пользователь с данной почтой уже существует</exception>
        /// <exception cref="ArgumentNullException">Пользователь не существует</exception>
        public async Task<object> RegistrationAsync(RegistrationDTO dto)
        {
            var userEmailToLower = dto.Email.ToLowerInvariant();
            var userName = string.IsNullOrEmpty(dto.Name) ? "unknown" : dto.Name;
            var userSurname = string.IsNullOrEmpty(dto.Surname) ? "unknown" : dto.Surname;
            using (var context = _factory.CreateDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(user => user.Email.Equals(userEmailToLower));
                if (user != null)
                    throw new ArgumentException("Пользователь с такой почтой уже зарегистрирован");
                var userRole = await context.Roles.SingleAsync(role => role.RoleType.Equals(UserRole.User));
                user = new User()
                {
                    Email = userEmailToLower,
                    Password = dto.Password,
                    Name = userName,
                    Surname = userSurname,
                    Role = userRole,
                    RoleId = userRole.Id
                };
                context.Add(user);
                await context.SaveChangesAsync();
            }

            return await Authentification(new AuthDTO() { Email = userEmailToLower, Password = dto.Password });
        }


        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="dto">Данные для аутентификации пользователя</param>
        /// <returns>Данные авторизированного пользователя</returns>
        public async Task<UserDTO> Authentification(AuthDTO dto)
        {
            var lowerEmail = dto.Email.ToLowerInvariant();
            var userDto = new UserDTO();
            int userId;
            string userRole;
            using (var db = _factory.CreateDbContext())
            {
                var user = await db.Users.Include(item => item.Role)
                    .FirstOrDefaultAsync(user => user.Email.Equals(lowerEmail) && user.Password.Equals(dto.Password));
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "Пользователь не найден");
                userId = user.Id;
                userDto.Name = user.Name;
                userDto.Surname = user.Surname;
                userRole = user.Role.RoleType==UserRole.Admin?"admin":"user";
            }

            userDto.Email = dto.Email;
            userDto.AccessToken = _tokenGenerator.GenerateAccessToken(userId, userRole);
            userDto.RefreshToken = _tokenGenerator.GenerateRefreshToken(userId, userRole);
            return userDto;
        }
    }
}