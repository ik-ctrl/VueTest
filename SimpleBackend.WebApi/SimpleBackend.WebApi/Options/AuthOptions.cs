namespace SimpleBackend.WebApi.Options
{
    /// <summary>
    /// Данные для настройки аутентификации
    /// </summary>
    public sealed class AuthOptions
    {
        /// <summary>
        /// Издатель токена
        /// </summary>
        public string Issuer { get; init; }

        /// <summary>
        /// Потребитель токена
        /// </summary>
        public string Audience { get; init; }
        
        /// <summary>
        /// Ключ шифрования токена доступа
        /// </summary>
        public string AccessSecretKey { get; init; }

        /// <summary>
        /// Ключ шифрования токена обновления
        /// </summary>
        public string RefreshSecretKey { get; init; }
        
        /// <summary>
        /// Время жизни токена доступа
        /// </summary>
        public uint AccessTokenLifeTime { get; init; }

        /// <summary>
        /// Время жизни токена обновляющего токена 
        /// </summary>
        public uint RefreshTokenLifeTime { get; init; }
    }
}