using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SimpleBackend.WebApi.Options;

namespace SimpleBackend.WebApi.Helpers
{
    /// <summary>
    /// Генератор JWT  токенов
    /// </summary>
    public class TokenGenerator
    {
        private readonly AuthOptions _options;

        /// <summary>
        /// Инициализация генератора токенов 
        /// </summary>
        /// <param name="options">Ключ шифрования</param>
        /// <exception cref="ArgumentException">Некорректный ключ шифрования</exception>
        public TokenGenerator(AuthOptions options)
            => _options = options ?? throw new ArgumentException("Некорректный ключ щифрования");

        /// <summary>
        /// Генерация токена доступа
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="userRole">Роль пользователя</param>
        /// <returns>Токен доступа</returns>
        public string GenerateAccessToken(int userId, string userRole)
            => GenerateToken(userId, userRole, _options.Issuer, _options.Audience, _options.AccessTokenLifeTime, _options.AccessSecretKey);

        /// <summary>
        /// Генерация токена обновления
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="userRole">Роль пользователя</param>
        /// <returns>Токен обновления</returns>
        public string GenerateRefreshToken(int userId, string userRole)
            => GenerateToken(userId, userRole, _options.Issuer, _options.Audience, _options.RefreshTokenLifeTime, _options.RefreshSecretKey);

        /// <summary>
        /// Генерация JWToken
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="userRole">Роль пользователя</param>
        /// <param name="issuer">Имя издателя токена</param>
        /// <param name="audience">Имя клиента</param>
        /// <param name="lifetime">Время жизни токена</param>
        /// <param name="key">Ключ шифрования</param>
        /// <returns>Токен</returns>
        private string GenerateToken(int userId, string userRole, string issuer, string audience, uint lifetime, string key)
        {
            var identity = GenerateUserIdentityClaims(userId, userRole);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(lifetime)),
                signingCredentials: new SigningCredentials(GetSymmetricKey(key), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        
        /// <summary>
        /// Создание признаков пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="userRole">Роль роль пользователя</param>
        /// <returns>Набор признаков пользователя</returns>
        private ClaimsIdentity GenerateUserIdentityClaims(int userId, string userRole)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole),
                new Claim(ClaimTypes.Sid, userId.ToString())
            };
            return new ClaimsIdentity(claims, "Token");
        }

        /// <summary>
        /// Создания симметричный ключа
        /// </summary>
        /// <param name="key">Ключ шифрования</param>
        /// <returns>Симметричный ключ шифрования</returns>
        private SymmetricSecurityKey GetSymmetricKey(string key)
            => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    }
}