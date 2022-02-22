namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Данные пользователя
    /// </summary>
    public sealed class UserDTO
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Токен аутентификации
        /// </summary>
        public string AccessToken { get; set; }
        
        /// <summary>
        /// Токен обновления
        /// </summary>
        public string RefreshToken { get; set; }

    }
}