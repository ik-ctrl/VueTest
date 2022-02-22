using System.ComponentModel.DataAnnotations;

namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Модель данных для регистрации пользователя 
    /// </summary>
    public sealed class RegistrationDTO
    {
        /// <summary>
        /// Электронный адрес пользователя
        /// </summary>
        [Required]
        public string Email { get; set; }
        
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required]
        public string Password { get; set; }
        
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; }
        
    }
}