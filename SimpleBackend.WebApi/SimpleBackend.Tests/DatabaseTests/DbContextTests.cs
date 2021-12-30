using System;
using System.Collections.Generic;
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
        private const uint Port = 5434;
        private PostgresConnection _connection;
        private PostgresDbContext _context;
        
        /// <summary>
        /// Загрузка тестовых данных пустых задач
        /// </summary>
        /// <param name="todosCount">Количество сгенерированных задач</param>
        /// <returns>Список загруженных задач</returns>
        private IEnumerable<Todo> InsertOnlyTodosTestData(uint todosCount)
        {
            var randomizer = new Random(DateTime.Now.Millisecond);
            var testSamples = new List<Todo>();
            for (var i = 0; i < todosCount; i++)
            {
                var todo = new Todo()
                {
                    Confirm = false,
                    Title = $"Todo {i}",
                    UiKey = randomizer.Next(100, 200),
                };
                testSamples.Add(todo);
            }

            using (var db = _context.Clone())
            {
                foreach (var sample in testSamples)
                {
                    db.Add(sample);
                }

                db.SaveChanges();
            }
            return testSamples;
        }
        
        /// <summary>
        /// Загрузка тестовых данных задач c подзадачами
        /// </summary>
        /// <param name="todosCount">Количество сгенерированных задач</param>
        /// <param name="subTodosCount">Количество подзадач в задаче</param>
        /// <returns>Список загруженных задач</returns>
        private IEnumerable<Todo> InsertTodosWithSubTodosTestData(uint todosCount, uint subTodosCount)
        {
            var randomizer = new Random(DateTime.Now.Millisecond);
            var testSamples = new List<Todo>();
            for (var i = 0; i < todosCount; i++)
            {
                var todo = new Todo()
                {
                    Confirm = false,
                    Title = $"Todo #{i}",
                    UiKey = randomizer.Next(100, 200),
                };

                var subTodos = new List<SubTodo>();
                for (int j = 0; j < subTodosCount; j++)
                {
                    subTodos.Add(new SubTodo()
                    {
                        Confirm = false,
                        Description =$"SubTodos #{j}",
                        Todo = todo,
                        UiKey = randomizer.Next(400,500),
                    });
                }
                todo.SubTodos = subTodos;
                testSamples.Add(todo);
            }

            using (var db = _context.Clone())
            {
                foreach (var sample in testSamples)
                {
                    db.Add(sample);
                }
                db.SaveChanges();
            }
            return testSamples;
        }
        
        [SetUp]
       
        public void Init()
        {
            _connection = new PostgresConnection()
            {
                Password = Password,
                Host = Host,
                Port = Port,
                Username = Username,
                DatabaseName = DatabaseName,
            };
            _context = new PostgresDbContext(_connection);
            _context.Database.Migrate();
            var todos = _context.Todos.ToList();
            _context.Todos.RemoveRange(todos);
            _context.SaveChanges();
        }

        [TearDown]
        [Description("Очистка бд после  каждого теста")]
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
            var expectedConnection = $"Host={Host};Port={Port};Username={Username};Password={Password};Database={DatabaseName};";
            var usedConnection = _connection.GetConnectionString();
            Assert.AreEqual(expectedConnection, usedConnection);
        }

        [Test]
        [Description("Тест на проверку добавление задач")]
        public void AddOnlyTodo_Test()
        {
            const uint expectedTodoCount = 5;
            InsertOnlyTodosTestData(expectedTodoCount);
            int currentTodoCount;
            using (var db = _context.Clone())
            {
                currentTodoCount = db.Todos.Count();
            }
            Assert.AreEqual(expectedTodoCount, currentTodoCount);
        }
        
        [Test]
        [Description("Тест на проверку добавление задач")]
        public void AddTodoWithSubTodos_Test()
        {
            const uint expectedTodoCount = 5;
            const uint expectedSubTodosCount = 5;
            InsertTodosWithSubTodosTestData(expectedTodoCount, expectedSubTodosCount);
            int currentTodoCount;
            int currentSubTodosCount;
            using (var db = _context.Clone())
            {
                currentTodoCount = db.Todos.Count();
                
                currentSubTodosCount = db.Todos.Include(i=>i.SubTodos).ToList().Sum(todo => todo.SubTodos.Count);
            }
            Assert.AreEqual(expectedTodoCount, currentTodoCount);
            Assert.AreEqual(expectedTodoCount*expectedSubTodosCount,currentSubTodosCount);
        }

        [Test]
        [Description("Тест на внедерние подзадачи к уже созданной задаче")]
        public void InjectSubTodoToEmptyTodo_Test()
        {
            int todoId;
            InsertOnlyTodosTestData(5);
            using (var db = _context.Clone())
            {
                var todo = db.Todos.First();
                todoId = todo.TodoId;
                var subTodos = new SubTodo()
                {
                    Confirm = false,
                    Description = "Insert subTodo #1",
                    TodoId = todo.TodoId,
                    Todo = todo,
                    UiKey = new Random(DateTime.Now.Millisecond).Next(500, 900),
                };
                db.SubTodos.Add(subTodos);
                db.SaveChanges();
            }

            int currentSubTodosCount;
            using (var db = _context.Clone())
            {
                var todo = db.Todos.Include(item=>item.SubTodos).First(item=>item.TodoId==todoId);
                currentSubTodosCount = todo.SubTodos.Count;
            }
            Assert.AreEqual(1,currentSubTodosCount);
        }

        [Test]
        [Description("Тест на удаление задач")]
        public void RemoveTodo_Test()
        {
            const uint expectedTodoCount = 5;
            const uint expectedSubTodoCount = 5;
            InsertTodosWithSubTodosTestData(expectedTodoCount, expectedSubTodoCount);
            
            using (var db = _context.Clone())
            {
                var todo = db.Todos.First();
                db.Todos.Remove(todo);
                db.SaveChanges();
            }

            int currentTodosCount;
            int currentSubTodosCount;
            using (var db = _context.Clone())
            {
                currentTodosCount = db.Todos.Count();
                currentSubTodosCount = db.SubTodos.Count();
            }
            Assert.AreEqual(expectedTodoCount-1,currentTodosCount);
            Assert.AreEqual((expectedTodoCount*expectedSubTodoCount)-expectedSubTodoCount,currentSubTodosCount);
        }
    }
}