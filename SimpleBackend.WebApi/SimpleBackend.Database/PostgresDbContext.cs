using System;
using Microsoft.EntityFrameworkCore;
using SimpleBackend.Database.Entities;

namespace SimpleBackend.Database
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class PostgresDbContext : DbContext, ICloneable
    {
        private readonly PostgresConnection _connection;
        
        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="connection">Данные для подключения</param>
        public PostgresDbContext(PostgresConnection connection)
            => _connection = connection ?? throw new ArgumentNullException(nameof(connection), "Отсутствуют данные для подключения к БД");


        /// <summary>
        /// Копирование контекста
        /// </summary>
        /// <returns>Копия текущего контекста</returns>
        public object Clone()=>new PostgresDbContext(_connection);

        /// <summary>
        /// Сет основных задач
        /// </summary>
        public DbSet<Todo> Todos { get; set; }

        /// <summary>
        /// Сет позадач
        /// </summary>
        public DbSet<SubTodo> SubTodos { get; set; }

        /// <summary>
        /// Конфигурация подключения
        /// </summary>
        /// <param name="optionsBuilder">Конфигуратор опций</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_connection.GetConnectionString());

        /// <summary>
        /// Конфигурация контекста БД
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .HasMany(t => t.SubTodos)
                .WithOne(s => s.Todo)
                .HasForeignKey(s => s.TodoId);
        }
    }
}