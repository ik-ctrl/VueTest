using System;
using Microsoft.EntityFrameworkCore;
using SimpleBackend.Database.Entities;

namespace SimpleBackend.Database
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class PostgresDbContext : DbContext
    {
        private readonly PostgresConnection _connection;
        private readonly DbContextOptions<PostgresDbContext> _options;

        /// <summary>
        /// Инициализация с конфигурацией
        /// </summary>
        /// <param name="options">Конфигурация подключения</param>
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
        {
            _options = options;
            _connection = null;
        }

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="connection">Данные для подключения</param>
        public PostgresDbContext(PostgresConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection), "Отсутствуют данные для подключения к БД");
            _options = null;
        }


        /// <summary>
        /// Копирование контекста
        /// </summary>
        /// <returns>Копия текущего контекста</returns>
        public PostgresDbContext Clone() => _options == null
            ? new PostgresDbContext(_connection)
            : new PostgresDbContext(_options);

        /// <summary>
        /// Сет основных задач
        /// </summary>
        public DbSet<Todo> Todos { get; set; }

        /// <summary>
        /// Сет позадач
        /// </summary>
        public DbSet<SubTodo> SubTodos { get; set; }

        /// <summary>
        /// Сет пользователей приложения
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Сет доступных ролей
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Конфигурация подключения
        /// </summary>
        /// <param name="optionsBuilder">Конфигуратор опций</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql(_connection.GetConnectionString());
        }

        /// <summary>
        /// Конфигурация контекста БД
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .Property(item => item.RoleType)
                .HasConversion(
                    value => value.ToString(),
                    value =>Enum.Parse<UserRole>(value)
                );
            
            modelBuilder.Entity<Role>()
                .HasData(
                    new Role() { Id = 1,RoleType = UserRole.User },
                    new Role() { Id = 2,RoleType = UserRole.Admin }
                );

            modelBuilder.Entity<User>()
                .Property(item => item.Email)
                .IsRequired();
            
            modelBuilder.Entity<User>()
                .Property(item => item.Password)
                .IsRequired();
            
            modelBuilder.Entity<User>()
                .HasOne(item => item.Role)
                .WithMany(item => item.Users)
                .HasForeignKey(item => item.RoleId);
            
            modelBuilder.Entity<Todo>().HasKey(t => t.TodoId);
            modelBuilder.Entity<SubTodo>().HasKey(s => s.SubTodoId);
            modelBuilder.Entity<Todo>()
                .HasMany(t => t.SubTodos)
                .WithOne(s => s.Todo)
                .HasForeignKey(s => s.TodoId);
        }
    }
}