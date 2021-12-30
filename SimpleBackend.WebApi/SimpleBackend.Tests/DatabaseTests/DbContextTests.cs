using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SimpleBackend.Database;
using SimpleBackend.Database.Entities;

namespace SimpleBackend.Tests.DatabaseTests
{
    [TestFixture]
    [Description("Проверка работы  контекста базы данных")]
    public sealed class DbContextTests
    {
        private const string Username = "postgres";
        private const string Password = "postgres";
        private const string DatabaseName = "TestDb1";
        private const string Host = "127.0.0.1";
        private const string AppName = "DbContextTests";
        private const uint Port = 5434;
        private const uint ConnectionIdleLifetime = 300;
        private const uint ConnectionPruningInterval = 310;
        private PostgresConnection _connection;
        private PostgresDbContext _context;

        [OneTimeSetUp]
        [Description("Инициализация подключения перед каждым тестом")]
        public void Init()
        {
            _connection = new PostgresConnection()
            {
                Password = Password,
                Host = Host,
                Port = Port,
                Username = Username,
                DatabaseName = DatabaseName,
                ConnectionIdleLifetime = ConnectionIdleLifetime,
                ConnectionPruningInterval = ConnectionPruningInterval,
            };
            _context = new PostgresDbContext(_connection);
            _context.Database.Migrate();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _connection = null;
            var todos = _context.Todos.ToList();
            _context.Todos.RemoveRange(todos);
            _context.SaveChanges();
            _context.Dispose();
            _context = null;
        }


        [Test]
        [Description("Проверка формирования строки подключения")]
        public void CheckConnectionString_Test()
        {
            var expectedConnection = $"Host={Host};Port={Port};Username={Username};Password={Password};Database={DatabaseName};" +
                                     $"Connection Idle Lifetime={ConnectionIdleLifetime};ConnectionPruningInterval={ConnectionPruningInterval}";
            var usedConnection = _connection.GetConnectionString();
            Assert.AreEqual(expectedConnection, usedConnection);
        }


        [Test]
        [Description("Тест на проверку задач")]
        public void AddTodo_Test()
        {
            var randomizer = new Random(DateTime.Now.Millisecond);
            using (var db = _context.Clone())
            {
                for (var i = 0; i < 5; i++)
                {
                    var todo = new Todo()
                    {
                        Confirm = false,
                        Title = $"Todo {i}",
                        UiKey = randomizer.Next(100 , 200),
                    };
                    db.Add(todo);
                }
                db.SaveChanges();
            }

            var expectedTodoCount = 5;
            var currentTodoCount = 0;
            using (var db = _context.Clone())
            {
                currentTodoCount = db.Todos.Count();
            }

            Assert.GreaterOrEqual(expectedTodoCount, currentTodoCount);
        }
    }
}