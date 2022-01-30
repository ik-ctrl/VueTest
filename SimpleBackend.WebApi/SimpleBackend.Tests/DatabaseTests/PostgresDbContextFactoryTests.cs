using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SimpleBackend.Database;
using SimpleBackend.Database.Entities;
using SimpleBackend.WebApi.Helpers;

namespace SimpleBackend.Tests.DatabaseTests
{
    [TestFixture]
    public class PostgresDbContextFactoryTests
    {
        private PostgresConnection _connection = new PostgresConnection()
        {
            Username = "postgres",
            Password = "postgres",
            DatabaseName = "SimpleDB",
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

        [Test]
        public void CreateDbContext_Test()
        {
            var factory = new PostgresDbContextFactory(_connection);
            var todoTitle = "test";
            var todoConfirm = true;
            var todoUiKey = 10;

            using (var db = factory.CreateDbContext())
            {
                var todo = new Todo
                {
                    Title = todoTitle,
                    Confirm = todoConfirm,
                    SubTodos = new List<SubTodo>(),
                    UiKey = todoUiKey
                };
                db.Todos.Add(todo);
                db.SaveChanges();
            }

            IEnumerable<Todo> testTodos;
            using (var db = factory.CreateDbContext())
            {
                testTodos = db.Todos.ToList();
            }

            Assert.AreEqual(1, testTodos.Count());

            var testTodo = testTodos.FirstOrDefault();
            Assert.AreEqual(todoTitle, testTodo.Title);
            Assert.AreEqual(todoConfirm, testTodo.Confirm);
            Assert.AreEqual(todoUiKey, testTodo.UiKey);
        }

        [Test]
        public void DisposeContext_Test()
        {
            var factory = new PostgresDbContextFactory(_connection);

            PostgresDbContext ctx;
            using (ctx = factory.CreateDbContext())
            {
                var todos = ctx.Todos.ToList();
            }

            Assert.Throws<ObjectDisposedException>(() => { var test = ctx.Todos.ToList(); });
        }
    }
}