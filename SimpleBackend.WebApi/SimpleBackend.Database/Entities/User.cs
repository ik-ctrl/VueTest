namespace SimpleBackend.Database.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор поля
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set;}
        
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int RoleId { get; set; }
        
        /// <summary>
        /// Навигационное свойство роли пользователя
        /// </summary>
        public Role Role { get; set; }
    }
}