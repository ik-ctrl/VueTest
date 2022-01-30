using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SimpleBackend.Database;
using SimpleBackend.Database.Entities;
using SimpleBackend.WebApi.Helpers;
using SimpleBackend.WebApi.Models.Jobs.Worker;

namespace SimpleBackend.Tests.ApiTests
{
    [TestFixture]
    public class TodoWorkerServiceTest
    {
        private TodoWorkerService _service;

        private readonly PostgresConnection _connection = new PostgresConnection()
        {
            Username = "postgres",
            Password = "postgres",
            DatabaseName = "TestDb1",
            Host = "127.0.0.1",
            Port = 5434
        };

        [OneTimeSetUp]
        public void Init()
        {
            using (var ctx = new PostgresDbContext(_connection))
            {
                ctx.Database.Migrate();
                var todos = ctx.Todos.ToList();
                ctx.Todos.RemoveRange(todos);
                ctx.SaveChanges();
                ctx.Dispose();
            }

            _service = new TodoWorkerService(new PostgresDbContextFactory(_connection));
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            using (var ctx = new PostgresDbContext(_connection))
            {
                var todos = ctx.Todos.ToList();
                ctx.Todos.RemoveRange(todos);
                ctx.SaveChanges();
            }
        }

        [SetUp]
        public void TestSetup()
        {
            var todos = new List<Todo>();

            for (var i = 1; i <= 5; i++)
            {
                //todo: как сделать  разные контролируемые UiKey?
                var todo = new Todo()
                {
                    Confirm = false,
                    Title = $"Test todo{i + 1}",
                    UiKey = i + 1,
                    SubTodos = Enumerable.Range(0, i).Select(
                        index => new SubTodo()
                        {
                            Confirm = false,
                            Description = $"Test SubTodo {i + 1}",
                            UiKey = i + 11,
                        }).ToList(),
                };
            }
        }

        [TearDown]
        public void TestCleanup()
        {
        }

        [Test]
        public void AddTodos_Test()
        {
        }
    }
}