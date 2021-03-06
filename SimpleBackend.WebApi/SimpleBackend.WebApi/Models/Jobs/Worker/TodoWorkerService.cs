using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleBackend.Database.Entities;
using SimpleBackend.WebApi.DTO;
using SimpleBackend.WebApi.Helpers;
using SimpleBackend.WebApi.Models.Enums;

namespace SimpleBackend.WebApi.Models.Jobs.Worker
{
    /// <summary>
    /// Сервис  обработки с связанный задачами
    /// </summary>
    internal sealed class TodoWorkerService
    {
        private readonly PostgresDbContextFactory _factory;

        /// <summary>
        /// Инициализация сервиса
        /// </summary>s
        /// <param name="factory">Фабрика создания контекста БД</param>
        /// <exception cref="ArgumentNullException">Отсутствуют данные для подключения</exception>
        public TodoWorkerService(PostgresDbContextFactory factory)
            => _factory = factory ?? throw new ArgumentNullException(nameof(factory), "Удалось инициализировать фабрику создания БД");


        /// <summary>
        /// Обработка запроса на получения всех задач
        /// </summary>
        /// <param name="jobUnit">Единица работы</param>
        /// <returns>Результат выполнения операции</returns>
        /// <exception cref="ArgumentNullException">jobUnit==null</exception>
        /// <exception cref="Exception">jobUnit.Type != JobType.GetAllTodos</exception>
        public JobResultDTO GetAllTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.GetAllTodos)
                throw new Exception($"Некорректный тип работы для данного метода(GetAllTodos):{jobUnit.Type}");

            IEnumerable<Todo> todos;
            using (var dbContext = _factory.CreateDbContext())
            {
                todos = dbContext.Todos.Include(t => t.SubTodos).ToList();
            }

            return new JobResultDTO()
            {
                JobId = jobUnit.JobId,
                IsSuccess = true,
                Message = string.Empty,
                ResultObject = todos.Select(MapTodoToTodoDTO)
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
        public JobResultDTO AddTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.AddTodos)
                throw new Exception($"Некорректный тип работы для данного метода(AddTodo):{jobUnit.Type}");

            if (jobUnit.JobObject is not IEnumerable<TodoDTO> todosDTO)
                throw new Exception("AddTodoAsync::Не удалось преобразовать jobUnit.JobObject");

            var todos = ConvertTodosDTOToTodos(todosDTO);
            using (var db = _factory.CreateDbContext())
            {
                db.AddRange(todos);
                db.SaveChanges();
            }

            return new JobResultDTO()
            {
                JobId = jobUnit.JobId,
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
        public JobResultDTO RemoveTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.RemoveTodos)
                throw new Exception($"Некорректный тип работы для данного метода(RemoveTodos):{jobUnit.Type}");

            if (jobUnit.JobObject is not IEnumerable<UiKeyDTO> uiKeys)
                throw new Exception("RemoveTodos::Не удалось преобразовать jobUnit.JobObject");

            using (var db = _factory.CreateDbContext())
            {
                foreach (var key in uiKeys)
                {
                    var todo = db.Todos.FirstOrDefault(t => t.UiKey.Equals(key.UiKeyItem));
                    if (todo != null)
                        db.Todos.Remove(todo);
                }

                db.SaveChanges();
            }

            return new JobResultDTO()
            {
                JobId = jobUnit.JobId,
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
        public JobResultDTO UpdateTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.UpdateTodos)
                throw new Exception($"Некорректный тип работы для данного метода(UpdateTodos):{jobUnit.Type}");

            if (jobUnit.JobObject is not IEnumerable<TodoDTO> todosDTO)
                throw new Exception("UpdateTodos::Не удалось преобразовать jobUnit.JobObject");

            using (var db = _factory.CreateDbContext())
            {
                var todoList = todosDTO.ToList();
                foreach (var todo in todoList.Select(item => db.Todos.FirstOrDefault(t => t.UiKey.Equals(item.UiId))).Where(todo => todo != null))
                    db.Todos.Remove(todo);
                var newTodos = ConvertTodosDTOToTodos(todoList);
                db.Todos.AddRange(newTodos);
                db.SaveChanges();
            }

            return new JobResultDTO()
            {
                JobId = jobUnit.JobId,
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
        public JobResultDTO AddSubTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.AddSubTodos)
                throw new Exception($"Некорректный тип работы для данного метода(AddSubTodos):{jobUnit.Type}");

            if (jobUnit.JobObject is not SubTodoRequestDTO request)
                throw new Exception("AddSubTodos::Не удалось преобразовать jobUnit.JobObject");

            using (var db = _factory.CreateDbContext())
            {
                var todo = db.Todos.FirstOrDefault(t => t.UiKey.Equals(request.TodoUiKey));
                if (todo != null)
                {
                    foreach (var item in request.SubTodos)
                    {
                        var subTodo = todo.SubTodos.FirstOrDefault(i => i.UiKey.Equals(item.UiId));
                        if (subTodo != null)
                            continue;

                        var newSubTodo = new SubTodo()
                        {
                            Confirm = item.Confirm,
                            Description = item.Description,
                            Todo = todo,
                            UiKey = item.UiId,
                            TodoId = todo.TodoId
                        };
                        db.SubTodos.Add(newSubTodo);
                    }

                    db.SaveChanges();
                }
            }

            return new JobResultDTO()
            {
                JobId = jobUnit.JobId,
                IsSuccess = true,
                Message = string.Empty,
                ResultObject = null,
            };
        }

        /// <summary>
        /// Обновление списка подзадач
        /// </summary>
        /// <param name="jobUnit">Единица работы</param>
        /// <returns>Результат выполнения операция</returns>
        /// <exception cref="ArgumentNullException">jobUnit == null</exception>
        /// <exception cref="Exception">jobUnit.Type != JobType.UpdateSubTodos</exception>
        /// <exception cref="Exception">jobUnit.JobObject is not IEnumerable(SubTodo) subTodos</exception>
        public JobResultDTO UpdateSubTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.UpdateSubTodos)
                throw new Exception($"Некорректный тип работы для данного метода(UpdateSubTodos):{jobUnit.Type}");

            if (jobUnit.JobObject is not SubTodoRequestDTO request)
                throw new Exception("UpdateSubTodos::Не удалось преобразовать jobUnit.JobObject");

            using (var db = _factory.CreateDbContext())
            {
                var subTodos = db.SubTodos.Include(st => st.Todo).Where(st => st.Todo.UiKey.Equals(request.TodoUiKey));
                db.SubTodos.RemoveRange(subTodos);
                var todo = db.Todos.FirstOrDefault(t => t.UiKey.Equals(request.TodoUiKey));
                var newSubTodos = MergeSubTodos(request.SubTodos, todo);
                db.SubTodos.AddRange(newSubTodos);
                db.SaveChanges();
            }

            return new JobResultDTO()
            {
                JobId = jobUnit.JobId,
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
        public JobResultDTO RemoveSubTodos(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.RemoveSubTodos)
                throw new Exception($"Некорректный тип работы для данного метода(RemoveSubTodos):{jobUnit.Type}");

            if (jobUnit.JobObject is not IEnumerable<UiKeyDTO> uiKeys)
                throw new Exception("RemoveSubTodos::Не удалось преобразовать jobUnit.JobObject");

            using (var db = _factory.CreateDbContext())
            {
                foreach (var key in uiKeys)
                {
                    var subTodo = db.SubTodos.FirstOrDefault(t => t.UiKey.Equals(key.UiKeyItem));
                    if (subTodo == null)
                        continue;
                    db.SubTodos.Remove(subTodo);
                }

                db.SaveChanges();
            }

            return new JobResultDTO()
            {
                JobId = jobUnit.JobId,
                IsSuccess = true,
                Message = string.Empty,
                ResultObject = null,
            };
        }


        /// <summary>
        /// Конвертация DTO задач в задачи
        /// </summary>
        /// <param name="todosDTO"></param>
        /// <returns>Список задач</returns>
        private IEnumerable<Todo> ConvertTodosDTOToTodos(IEnumerable<TodoDTO> todosDTO)
        {
            var todos = new List<Todo>();
            foreach (var todoDTO in todosDTO)
            {
                var todo = new Todo() { Confirm = todoDTO.Confirm, Title = todoDTO.Title, UiKey = todoDTO.UiId };
                MergeSubTodos(todoDTO, todo);
                todos.Add(todo);
            }

            return todos;
        }

        /// <summary>
        /// Маппинг сущность на задачи на DTO задачи
        /// </summary>
        /// <param name="todo">Задача которую нужно размапить</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private TodoDTO MapTodoToTodoDTO(Todo todo) => todo != null
            ? new TodoDTO()
            {
                Confirm = todo.Confirm,
                Title = todo.Title,
                UiId = todo.UiKey,
                SubTodos = todo.SubTodos != null
                    ? todo.SubTodos.Select((item) => new SubTodoDTO() { TodoUiId = item.UiKey, Confirm = item.Confirm, Description = item.Description, UiId = item.UiKey })
                    : new List<SubTodoDTO>(),
            }
            : null;

        /// <summary>
        /// Изъятие подзадач из DTO
        /// </summary>
        /// <param name="todoDTO">Данные подзадач</param>
        /// <param name="todo">Формируемая задача</param>
        private void MergeSubTodos(TodoDTO todoDTO, Todo todo)
        {
            if (todoDTO.SubTodos == null)
            {
                todo.SubTodos = new List<SubTodo>();
                return;
            }

            var extractedSubTodos = todoDTO.SubTodos.Select(subTodoDTO =>
                new SubTodo() { Confirm = subTodoDTO.Confirm, Description = subTodoDTO.Description, UiKey = subTodoDTO.UiId, Todo = todo }).ToList();
            todo.SubTodos = extractedSubTodos;
        }

        /// <summary>
        /// Изъятие подзадач из DTO
        /// </summary>
        /// <param name="subTodos">Список подзадач</param>
        /// <param name="todo">Обновляемая задача задача</param>
        private IEnumerable<SubTodo> MergeSubTodos(IEnumerable<SubTodoDTO> subTodos, Todo todo)
        {
            var subTodoDtos = subTodos.ToList();
            if (!subTodoDtos.Any())
                return new List<SubTodo>();

            return subTodoDtos.Select(subTodo => new SubTodo() { Confirm = subTodo.Confirm, Description = subTodo.Description, UiKey = subTodo.UiId, Todo = todo }).ToList();
        }
    }
}