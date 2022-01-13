using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        // AddTodo = 1,
        // RemoveTodo=2,
        // UpdateTodos=3,
        // GetAllTodos=4,
        // AddSubTodos=5,
        // RemoveSubTodos=6,
        // UpdateSubTodos=7,
        
        /// <summary>
        /// Асинхронная обработка запроса на получения всех задач
        /// </summary>
        /// <param name="jobUnit">Единица работы</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">jobUnit==null</exception>
        /// <exception cref="Exception">jobUnit.Type != JobType.GetAllTodos</exception>
        public async Task<JobResult> GetAllTodosAsync(Job jobUnit)
        {
            if (jobUnit == null)
                throw new ArgumentNullException(nameof(jobUnit));

            if (jobUnit.Type != JobType.GetAllTodos)
                throw new Exception($"Некорректный тип работы для данного метода(GetAllTodos):{jobUnit.Type}");
            
            IEnumerable<Todo> todos;
            using (var db = _context.Clone())
            {
                todos = await db.Todos.Include(t => t.SubTodos).ToListAsync();
            }
            return new JobResult()
            {
                Id = jobUnit.Id,
                IsSuccess = true,
                Message = string.Empty,
                ResultObject = todos
            };
        }


        public JobResult AddTodo()
        {
            return null;
        }


        public JobResult RemoveTodo()
        {
            return null;
        }


        public JobResult UpdateTodos()
        {
            return null;
        }


        public JobResult AddSubTodos()
        {
            return null;
        }

        public JobResult RemoveSubTodos()
        {
            return null;
        }


        public JobResult UpdateSubTodos()
        {
            return null;
        }
    }
}