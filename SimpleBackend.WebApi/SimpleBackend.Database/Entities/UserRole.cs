using System.ComponentModel;

namespace SimpleBackend.Database.Entities
{
    /// <summary>
    /// Роли пользователя
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Обычный пользователь
        /// </summary>
        [Description("Обычный пользователь")] 
        User = 0,
        
        /// <summary>
        /// Администратор
        /// </summary>
        [Description("Администратор")]
        Admin= 1,
    }
}