using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SimpleBackend.Database;
using SimpleBackend.Database.Entities;
using SimpleBackend.WebApi.DTO;
using SimpleBackend.WebApi.Helpers;
using SimpleBackend.WebApi.Models.Enums;
using SimpleBackend.WebApi.Models.Jobs;
using SimpleBackend.WebApi.Models.Jobs.Worker;

namespace SimpleBackend.Tests.ApiTests
{
    [TestFixture]
    public class TodoWorkerServiceTest
    {
        private TodoWorkerService _service;
        private Randomizer _randomizerSetup;
        private readonly PostgresConnection _connection = new PostgresConnection()
        {
            Username = "postgres",
            Password = "postgres",
            DatabaseName = "TestDb1",
            Host = "127.0.0.1",
            Port = 5434
        };

        private Todo GetFirstFullTodo()
        {
            Todo todo;
            using (var db = new PostgresDbContext(_connection))
            {
                todo = db.Todos.Include(i=>i.SubTodos).First();
            }
            return todo;
        }

        private Todo GetFirstFullTodoBuUiKey(int uiKey)
        {
            Todo todo;
            using (var db = new PostgresDbContext(_connection))
            {
                todo = db.Todos.Include(item => item.SubTodos).First(item => item.UiKey == uiKey);
            }
            return todo;
        }
        
        private Job GenerateJob(string message, JobType type, Guid id, object obj) => new Job() { Message = message, Type = type, JobId = id, JobObject = obj };
        
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
            _randomizerSetup = new Randomizer(100);
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
                var todoUiKey = _randomizerSetup.Next(1,100);
                var subTodoUiKey = _randomizerSetup.Next(1,100);
                var todo = new Todo()
                {
                    Confirm = false,
                    Title = $"Test todo{i + 1}",
                    UiKey =todoUiKey,
                    SubTodos = Enumerable.Range(0, i).Select(
                        (item) => new SubTodo()
                        {
                            Confirm = false,
                            Description = $"Test SubTodo {item + 1}",
                            UiKey =subTodoUiKey,
                        }).ToList(),
                };
                todos.Add(todo);
            }

            using (var db= new PostgresDbContext(_connection))
            {
                var todosDb = db.Todos.ToList();
                db.RemoveRange(todosDb);
                db.SaveChanges();
                db.Todos.AddRange(todos);
                db.SaveChanges();
            }
            
        }

        [TearDown]
        public void TestCleanup()
        {
            using (var db = new PostgresDbContext(_connection))
            {
                var todos = db.Todos.ToList();
                db.Todos.RemoveRange(todos);
                db.SaveChanges();
            }
        }

        [TestCase(TestName = "Запрос всех задач")]
        public void GetAllTodos_Test()
        {
            var id = Guid.NewGuid();
            var job = new Job()
            {
                JobId = id,
                Message = string.Empty,
                Type = JobType.GetAllTodos,
                JobObject = null,
            };
            var jobResult = _service.GetAllTodos(job);
            Assert.AreEqual(true,jobResult.IsSuccess);
            Assert.AreEqual(id,jobResult.JobId);
            var results = (IEnumerable<TodoDTO>)jobResult.ResultObject;
            Assert.AreEqual(5,results.Count());
        }
        
        [TestCase(TestName = "Добавление новых задачек")]
        public void AddTodos_Test()
        {
            var id = Guid.NewGuid();
            var job = new Job()
            {
                JobId = id,
                Message = string.Empty,
                Type = JobType.AddTodos,
                JobObject = new List<TodoDTO>()
                {
                    new TodoDTO()
                    {
                        Confirm = false,
                        Title = "Test Addtodos",
                        SubTodos = new List<SubTodoDTO>(),
                        UiId = 1000
                    }
                },
            };
            var result = _service.AddTodos(job);
            Assert.AreEqual(true,result.IsSuccess);
            Assert.AreEqual(id,result.JobId);
            Assert.AreEqual(null,result.ResultObject);

            List<Todo> dbTodos;
            using (var db = new PostgresDbContext(_connection))
            {
                dbTodos = db.Todos.ToList();
            }

            foreach (var item in dbTodos)
            {
                if (item.UiKey != 1000) continue;
                Assert.AreEqual(false,item.Confirm);
                Assert.AreEqual(1000,item.UiKey);
                Assert.AreEqual("Test Addtodos",item.Title);
                Assert.AreEqual(0,item.SubTodos.Count());
                return;
            }
            Assert.Fail();
        }

        [TestCase(TestName = "Удаление задачек")]
        public void RemoveTodos_Test()
        {
            List<Todo> todos;
            using(var db = new PostgresDbContext(_connection))
            {
                todos = db.Todos.ToList();
            }

            var keysDTO = new List<UiKeyDTO>();
            todos.ForEach(item =>
            {
                keysDTO.Add(new UiKeyDTO()
                {
                    UiKeyItem = item.UiKey
                });
            });

            var id = Guid.NewGuid();
            var job = GenerateJob(string.Empty, JobType.RemoveTodos, id, keysDTO);
            var result = _service.RemoveTodos(job);
            Assert.AreEqual(true,result.IsSuccess);
            Assert.AreEqual(id,result.JobId);
            Assert.AreEqual(null,result.ResultObject);

            using (var db = new PostgresDbContext(_connection))
            {
                var testTodos = db.Todos.ToList();
                Assert.AreEqual(0,testTodos.Count());
            }

        }

        [TestCase(TestName = "Удаление задачек")]
        public void UpdateTodos_Test()
        {
            var todo = GetFirstFullTodo();
            var updateTitle = "Its title was update";
            var updatedTodos = new List<TodoDTO>()
            {
                new TodoDTO(){Confirm = false,Title =updateTitle,SubTodos = new List<SubTodoDTO>(),UiId = todo.UiKey}
            };
            var id = Guid.NewGuid();
            var job = GenerateJob(string.Empty, JobType.UpdateTodos, id, updatedTodos);
            var result = _service.UpdateTodos(job);
            Assert.AreEqual(true,result.IsSuccess);
            Assert.AreEqual(id,result.JobId);
            Assert.AreEqual(null,result.ResultObject);

            var testTodo = GetFirstFullTodoBuUiKey(todo.UiKey);
            Assert.AreEqual(false,testTodo.Confirm);
            Assert.AreEqual(updateTitle,testTodo.Title);
            Assert.AreEqual(0,testTodo.SubTodos.Count);
        }

        [TestCase(TestName = "Добавление подзадач")]
        public void AddSubTodos_Test()
        {
            var todo = GetFirstFullTodo();
            var newSubTodoDescription = "new SubTodo";
            var subTodos = new List<SubTodoDTO>()
            {
                new SubTodoDTO() { Confirm = false, Description = "new SubTodo", UiId = 1000, TodoUiId = todo.UiKey },
            };
            var request = new SubTodoRequestDTO()
            {
                TodoUiKey = todo.UiKey,
                SubTodos = subTodos,
            };
            var id = Guid.NewGuid();
            var job = new Job()
            {
                Message = string.Empty,
                Type = JobType.UpdateSubTodos,
                JobId = id,
                JobObject = request,
            };
            var result = _service.UpdateSubTodos(job);
            Assert.AreEqual(true,result.IsSuccess);
            Assert.AreEqual(id,result.JobId);
            Assert.AreEqual(null,result.ResultObject);

            Todo testTodo;
            using (var db = new PostgresDbContext(_connection))
            {
                testTodo = db.Todos.Include(i=>i.SubTodos).First(item => item.UiKey == todo.UiKey);
            }
            Assert.AreEqual(1,testTodo.SubTodos.Count());

            foreach (var subTodo in testTodo.SubTodos)
            {
                if(subTodo.UiKey!=1000) 
                    continue;
                Assert.AreEqual(newSubTodoDescription,subTodo.Description);
                return;
            }
            Assert.Fail();
        }

        [TestCase(TestName = "Удаление подзадач")]
        public void RemoveSubTodos_Test()
        {
            var todo = GetFirstFullTodo();
            var removableUiKeyDto = todo.SubTodos
                .Select(todoSubTodo => new UiKeyDTO() { UiKeyItem = todoSubTodo.UiKey, })
                .ToList();
            var id = Guid.NewGuid();
            var job = GenerateJob(string.Empty, JobType.RemoveSubTodos, id, removableUiKeyDto);
            var result = _service.RemoveSubTodos(job);
            Assert.AreEqual(true,result.IsSuccess);
            Assert.AreEqual(id,result.JobId);
            Assert.AreEqual(null,result.ResultObject);
            var testTodo = GetFirstFullTodoBuUiKey(todo.UiKey);
            Assert.AreEqual(0,testTodo.SubTodos.Count);
        }
 
        [TestCase(TestName = "Обновление подзадач")]
        public void UpdateSubTodos_Test()
        {
            var todo = GetFirstFullTodo();
            var subTodosCount = todo.SubTodos.Count();
            var updatedDescription = "New Description";
            var request = new SubTodoRequestDTO()
            {
                TodoUiKey = todo.UiKey,
                SubTodos = new List<SubTodoDTO>()
                {
                    new SubTodoDTO(){Confirm = false,Description =updatedDescription,UiId = 1000,TodoUiId = todo.UiKey},
                    new SubTodoDTO(){Confirm = false,Description =updatedDescription,UiId = 2000,TodoUiId = todo.UiKey},
                    new SubTodoDTO(){Confirm = false,Description =updatedDescription,UiId = 3000,TodoUiId = todo.UiKey},
                    new SubTodoDTO(){Confirm = false,Description =updatedDescription,UiId = 4000,TodoUiId = todo.UiKey},
                    new SubTodoDTO(){Confirm = false,Description =updatedDescription,UiId = 4000,TodoUiId = todo.UiKey},
                }
            };
            var id = Guid.NewGuid();
            var job = GenerateJob(string.Empty, JobType.UpdateSubTodos, id, request);
            var result = _service.UpdateSubTodos(job);
            Assert.AreEqual(true,result.IsSuccess);
            Assert.AreEqual(id,result.JobId);
            Assert.AreEqual(null,result.ResultObject);
            var testTodo = GetFirstFullTodoBuUiKey(todo.UiKey);
            Assert.AreEqual(5,testTodo.SubTodos.Count());
            Assert.AreNotEqual(subTodosCount,testTodo.SubTodos.Count());
            foreach (var subTodo in testTodo.SubTodos)
            {
                if (!subTodo.Description.Equals(updatedDescription))
                    Assert.Fail();
            }
            Assert.Pass();
        }
    }
}