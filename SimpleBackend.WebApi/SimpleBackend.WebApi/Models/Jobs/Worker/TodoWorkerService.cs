using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleBackend.Database;
using SimpleBackend.Database.Entities;
using SimpleBackend.WebApi.DTO;
using SimpleBackend.WebApi.Models.Worker;

namespace SimpleBackend.WebApi.Models.Jobs.Worker
{
    /// <summary>
    /// Сервис  обработки с связанный задачами
    /// </summary>
    internal sealed class TodoWorkerService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        /// <summary>
        /// Инициализация сервиса
        /// </summary>
        /// <param name="scopeFactory"></param>
        public TodoWorkerService(IServiceScopeFactory scopeFactory)
            => _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory), "Удалось инициализировать фабрику создания сервисов");

        /// <summary>
        /// Обработка запроса на получения всех задач
        /// </summary>
        /// <param name="jobUnit">Единица работы</param>
        /// <returns>Результат выполнения операции</returns>
        /// <exception cref="ArgumentNullException">jobUnit==null</exception>
        /// <exception cref="Exception">jobUnit.Type != JobType.GetAllTodos</exception>
        public JobResult GetAllTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.GetAllTodos)
                throw new Exception($"Некорректный тип работы для данного метода(GetAllTodos):{jobUnit.Type}");

            IEnumerable<Todo> todos;

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<PostgresDbContext>())
                {
                    todos = dbContext.Todos.Include(t => t.SubTodos).ToList();
                }
            }

            return new JobResult()
            {
                Id = jobUnit.Id,
                IsSuccess = true,
                Message = string.Empty,
                ResultObject = todos
            };
        }

        /// <summary>
        /// Добавление новых задач
        /// </summary>
        /// <param name="jobUnit">Единица работы</param>
        /// <returns>Результат выполнения операции</returns>
        /// <exception cref="ArgumentNullException">jobUnit == null</exception>
        /// <exception cref="Exception">jobUnit.Type != JobType.AddTodo</exception>
        /// <exception cref="Exception">jobUnit.JobObject is not IEnumerable (Todo) todos</exception>
        public JobResult AddTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.AddTodos)
                throw new Exception($"Некорректный тип работы для данного метода(AddTodo):{jobUnit.Type}");

            if (jobUnit.JobObject is not IEnumerable<TodoDTO> todosDTO)
                throw new Exception("AddTodoAsync::Не удалось преобразовать jobUnit.JobObject");

            var todos = new List<Todo>();
            foreach (var todoDTO in todosDTO)
            {
                var todo = new Todo() { Confirm = todoDTO.Confirm, Title = todoDTO.Title, UiKey = todoDTO.UiId};
                ExtractSubTodos(todoDTO, todo);
                todos.Add(todo);
            }

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<PostgresDbContext>())
                {
                    db.AddRange(todos);
                    db.SaveChanges();
                    db.Dispose();
                }
            }

            return new JobResult()
            {
                Id = jobUnit.Id,
                IsSuccess = true,
                Message = string.Empty,
                ResultObject = null
            };
        }

       

        /// <summary>
        /// Удаление списка задач по идентификаторам выданных графическим интерфейсом 
        /// </summary>
        /// <param name="jobUnit">Единица работы</param>
        /// <returns>Результат выполнения операции</returns>
        /// <exception cref="ArgumentNullException">jobUnit == null</exception>
        /// <exception cref="Exception">jobUnit.Type != JobType.RemoveTodos</exception>
        /// <exception cref="Exception">jobUnit.JobObject is not IEnumerable(int) uiKeys</exception>
        public JobResult RemoveTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.RemoveTodos)
                throw new Exception($"Некорректный тип работы для данного метода(RemoveTodos):{jobUnit.Type}");

            if (jobUnit.JobObject is not IEnumerable<int> uiKeys)
                throw new Exception("RemoveTodos::Не удалось преобразовать jobUnit.JobObject");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<PostgresDbContext>())
                {
                    foreach (var key in uiKeys)
                    {
                        var todo = db.Todos.FirstOrDefault(t => t.UiKey.Equals(key));
                        if (todo == null)
                            continue;
                        db.Todos.Remove(todo);
                    }

                    db.SaveChanges();
                }
            }

            return new JobResult()
            {
                Id = jobUnit.Id,
                IsSuccess = true,
                Message = string.Empty,
                ResultObject = null,
            };
        }

        /// <summary>
        /// Обновление списка задач
        /// </summary>
        /// <param name="jobUnit">Единица работы</param>
        /// <returns>Результат выполнения операции</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public JobResult UpdateTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.AddTodos)
                throw new Exception($"Некорректный тип работы для данного метода(UpdateTodos):{jobUnit.Type}");

            if (jobUnit.JobObject is not IEnumerable<TodoDTO> todos)
                throw new Exception("UpdateTodos::Не удалось преобразовать jobUnit.JobObject");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<PostgresDbContext>())
                {
                    foreach (var item in todos)
                    {
                        var todo = db.Todos.FirstOrDefault(t => t.UiKey.Equals(item.UiId));
                        if (todo == null)
                            continue;
                        todo.Confirm = item.Confirm;
                        todo.Title = item.Title;
                        UpdateSubTodos(todo, item);
                        db.Todos.Update(todo);
                    }

                    db.SaveChanges();
                }
            }

            return new JobResult()
            {
                Id = jobUnit.Id,
                IsSuccess = true,
                Message = string.Empty,
                ResultObject = null,
            };
        }



        /// <summary>
        /// Добавления подзадач
        /// </summary>
        /// <param name="jobUnit">Единица работы</param>
        /// <returns>Результат выполнения операции</returns>
        /// <exception cref="ArgumentNullException">jobUnit == null</exception>
        /// <exception cref="Exception">jobUnit.Type != JobType.AddSubTodos</exception>
        /// <exception cref="Exception">jobUnit.JobObject is not IEnumerable(SubTodo) subTodos</exception>
        /// <exception cref="Exception">подзадача с таким же графическим идентификатором уже присутствует</exception>
        public JobResult AddSubTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.AddSubTodos)
                throw new Exception($"Некорректный тип работы для данного метода(AddSubTodos):{jobUnit.Type}");

            if (jobUnit.JobObject is not IEnumerable<SubTodo> subTodos)
                throw new Exception("AddSubTodos::Не удалось преобразовать jobUnit.JobObject");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<PostgresDbContext>())
                {
                    foreach (var item in subTodos)
                    {
                        var subTodo = db.SubTodos.FirstOrDefault(t => t.UiKey.Equals(item.UiKey));
                        if (subTodo != null)
                            throw new Exception("AddSubTodos::Не удалось добавить подзадачу,т.к. подзадача с таким же графическим идентификатором уже присутствует");
                        db.SubTodos.Add(item);
                    }

                    db.SaveChanges();
                }
            }

            return new JobResult()
            {
                Id = jobUnit.Id,
                IsSuccess = true,
                Message = string.Empty,
                ResultObject = null,
            };
        }

        /// <summary>
        /// Обновление списка подзадач
        /// </summary>
        /// <param name="jobUnit">Единица работы</param>
        /// <returns>Результат выполнения операции</returns>
        /// <exception cref="ArgumentNullException">jobUnit == null</exception>
        /// <exception cref="Exception">jobUnit.Type != JobType.UpdateSubTodos</exception>
        /// <exception cref="Exception">jobUnit.JobObject is not IEnumerable(SubTodo) subTodos</exception>
        public JobResult UpdateSubTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.UpdateSubTodos)
                throw new Exception($"Некорректный тип работы для данного метода(UpdateSubTodos):{jobUnit.Type}");

            if (jobUnit.JobObject is not IEnumerable<SubTodo> subTodos)
                throw new Exception("UpdateSubTodos::Не удалось преобразовать jobUnit.JobObject");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<PostgresDbContext>())
                {
                    foreach (var item in subTodos)
                    {
                        var subTodo = db.SubTodos.FirstOrDefault(t => t.UiKey.Equals(item.UiKey));
                        if (subTodo == null)
                            continue;
                        subTodo.Description = item.Description;
                        subTodo.Confirm = item.Confirm;
                        db.SubTodos.Update(subTodo);
                    }

                    db.SaveChanges();
                }
            }

            return new JobResult()
            {
                Id = jobUnit.Id,
                IsSuccess = true,
                Message = string.Empty,
                ResultObject = null,
            };
        }


        /// <summary>
        /// Удаление списка подзадач
        /// </summary>
        /// <param name="jobUnit">Единица работы</param>
        /// <returns>Результат выполнения операции</returns>
        /// <exception cref="ArgumentNullException">jobUnit == null</exception>
        /// <exception cref="Exception">jobUnit.Type != JobType.RemoveSubTodos</exception>
        /// <exception cref="Exception">jobUnit.JobObject is not IEnumerable(int) uiKeys</exception>
        public JobResult RemoveSubTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.RemoveSubTodos)
                throw new Exception($"Некорректный тип работы для данного метода(RemoveSubTodos):{jobUnit.Type}");

            if (jobUnit.JobObject is not IEnumerable<int> uiKeys)
                throw new Exception("RemoveSubTodos::Не удалось преобразовать jobUnit.JobObject");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<PostgresDbContext>())
                {
                    foreach (var key in uiKeys)
                    {
                        var subTodo = db.SubTodos.FirstOrDefault(t => t.UiKey.Equals(key));
                        if (subTodo == null)
                            continue;
                        db.SubTodos.Remove(subTodo);
                    }

                    db.SaveChanges();
                }
            }

            return new JobResult()
            {
                Id = jobUnit.Id,
                IsSuccess = true,
                Message = string.Empty,
                ResultObject = null,
            };
        }
        
        /// <summary>
        /// Обновление подзадач
        /// </summary>
        /// <param name="todo"></param>
        /// <param name="item"></param>
        private void UpdateSubTodos(Todo todo, TodoDTO item)
        {
            // todo:продумать
            // if()
            // todo.SubTodos = item.SubTodos;
        }
        
        /// <summary>
        /// Изъятие подзадач из DTO
        /// </summary>
        /// <param name="todoDTO">Данные подзадач</param>
        /// <param name="todo">Формируемая задача</param>
        private void ExtractSubTodos(TodoDTO todoDTO, Todo todo)
        {
            var extractedSubTodos = new List<SubTodo>();
            
            if (todoDTO.SubTodos == null)
            {
                todo.SubTodos = new List<SubTodo>();
                return;
            }
                
            foreach (var subTodoDTO in todoDTO.SubTodos)
            {
                extractedSubTodos.Add(new SubTodo()
                {
                    Confirm = subTodoDTO.Confirm,
                    Description = subTodoDTO.Description,
                    UiKey = subTodoDTO.UiId,
                    Todo = todo
                });
            }
            todo.SubTodos = extractedSubTodos;
        }
    }
}