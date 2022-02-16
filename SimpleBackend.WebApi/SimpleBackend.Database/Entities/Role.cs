using System.Collections.Generic;

namespace SimpleBackend.Database.Entities
{
    /// <summary>
    /// Роли пользователей
    /// </summary>
    public class Role
    {

        /// <summary>
        /// Инициализация
        /// </summary>
        public Role() => Users = new HashSet<User>();
        
        /// <summary>
        /// Пользователь
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Роль пользователя
        /// </summary>
        public UserRole RoleType { get; set; }
        
        /// <summary>
        /// Коллекция пользователей
        /// </summary>
        public ICollection<User> Users { get; set; }
    }
}