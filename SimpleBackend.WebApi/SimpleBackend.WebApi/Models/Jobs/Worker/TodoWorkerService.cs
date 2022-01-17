using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleBackend.Database;
using SimpleBackend.Database.Entities;
using SimpleBackend.WebApi.Models.Worker;

namespace SimpleBackend.WebApi.Models.Jobs.Worker
{
    /// <summary>
    /// Сервис  обработки с связанный задачами
    /// </summary>
    internal sealed class TodoWorkerService
    {
        private readonly PostgresDbContext _context;

        /// <summary>
        /// Инициализация сервиса
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public TodoWorkerService(PostgresDbContext context)
            => _context = context ?? throw new ArgumentNullException(nameof(context));

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
            using (var db = _context.Clone())
            {
                todos = db.Todos.Include(t => t.SubTodos).ToList();
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
                throw new Exception($"Некорректный тип работы для данного метода(AddTodoAsync):{jobUnit.Type}");

            if (jobUnit.JobObject is not IEnumerable<Todo> todos)
                throw new Exception("AddTodoAsync::Не удалось преобразовать jobUnit.JobObject");

            using (var db = _context.Clone())
            {
                db.AddRange(todos);
                db.SaveChanges();
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

            using (var db = _context.Clone())
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

            if (jobUnit.JobObject is not IEnumerable<Todo> todos)
                throw new Exception("UpdateTodos::Не удалось преобразовать jobUnit.JobObject");

            using (var db = _context.Clone())
            {
                foreach (var item in todos)
                {
                    var todo = db.Todos.FirstOrDefault(t => t.UiKey.Equals(item.UiKey));
                    if (todo == null)
                        continue;
                    todo.Confirm = item.Confirm;
                    todo.Title = item.Title;
                    todo.SubTodos = item.SubTodos;
                    db.Todos.Update(todo);
                }

                db.SaveChanges();
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

            using (var db = _context.Clone())
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

            using (var db = _context.Clone())
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

            using (var db = _context.Clone())
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

            return new JobResult()
            {
                Id = jobUnit.Id,
                IsSuccess = true,
                Message = string.Empty,
                ResultObject = null,
            };
        }
    }
}