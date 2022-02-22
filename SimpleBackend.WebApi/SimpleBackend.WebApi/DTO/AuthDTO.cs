namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Модель для аутентификации данных
    /// </summary>
    public class AuthDTO
    {
        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }
    }
}