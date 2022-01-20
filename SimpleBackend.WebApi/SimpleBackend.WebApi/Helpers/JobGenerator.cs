using System;
using System.Collections.Generic;
using SimpleBackend.WebApi.DTO;
using SimpleBackend.WebApi.Models.DTO;
using SimpleBackend.WebApi.Models.Jobs;
using SimpleBackend.WebApi.Models.Worker;

namespace SimpleBackend.WebApi.Helpers
{
    /// <summary>
    /// Генератор единиц работы
    /// </summary>
    public class JobGenerator
    {
        /// <summary>
        /// Основной метод генерации единиц работы
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="message">Дополнительное сообщение</param>
        /// <param name="type">Тип работы</param>
        /// <param name="jobObject">Объёкт работы</param>
        /// <returns>Единица работы</returns>
        private Job GenerateJobUnit(Guid jobId, string message, JobType type, object jobObject)
            => new Job()
            {
                Id = jobId,
                Message = message ?? string.Empty,
                Type = type,
                JobObject = jobObject
            };

        /// <summary>
        /// Метод генерации единицы работы для запроса всех задач
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <returns>Единица работы</returns>
        public Job GenerateGetAllTodosJob(Guid jobId)
            => GenerateJobUnit(jobId, string.Empty, JobType.GetAllTodos, null);
        
        /// <summary>
        /// Метод генерации единицы работы для запроса всех задач
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="todos">Список добавляемых задач</param>
        /// <returns>Единица работы</returns>
        public Job GenerateAddTodosJob(Guid jobId,IEnumerable<TodoDTO> todos)
            => GenerateJobUnit(jobId, string.Empty, JobType.AddTodos, todos);
        
        /// <summary>
        /// Метод генерации единицы работы для запроса всех задач
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="uiKeys">Список идентификаторов ключей</param>
        /// <returns>Единица работы</returns>
        public Job GenerateRemoveTodosJob(Guid jobId,IEnumerable<int> uiKeys)
            => GenerateJobUnit(jobId, string.Empty, JobType.RemoveTodos, uiKeys);
        
        /// <summary>
        /// Метод генерации единицы работы для запроса всех задач
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="todos">Список обновляемых задач</param>
        /// <returns>Единица работы</returns>
        public Job GenerateUpdateTodosJob(Guid jobId,IEnumerable<TodoDTO> todos)
            => GenerateJobUnit(jobId, string.Empty, JobType.UpdateTodos, todos);
        
        
        /// <summary>
        /// Метод генерации единицы работы для запроса всех задач
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="todos">Список обновляемых задач</param>
        /// <returns>Единица работы</returns>
        public Job GenerateAddSubTodosJob(Guid jobId,IEnumerable<SubTodoDTO> todos)
            => GenerateJobUnit(jobId, string.Empty, JobType.UpdateTodos, todos);
        
        //
        // /// <summary>
        // /// Добавления подзадач
        // /// </summary>
        // /// <param name="jobUnit">Единица работы</param>
        // /// <returns>Результат выполнения операции</returns>
        // /// <exception cref="ArgumentNullException">jobUnit == null</exception>
        // /// <exception cref="Exception">jobUnit.Type != JobType.AddSubTodos</exception>
        // /// <exception cref="Exception">jobUnit.JobObject is not IEnumerable(SubTodo) subTodos</exception>
        // /// <exception cref="Exception">подзадача с таким же графическим идентификатором уже присутствует</exception>
        // public JobResult AddSubTodos(Job jobUnit)
        // {
        //     if (jobUnit == null)
        //         throw new ArgumentNullException(nameof(jobUnit));
        //
        //     if (jobUnit.Type != JobType.AddSubTodos)
        //         throw new Exception($"Некорректный тип работы для данного метода(AddSubTodos):{jobUnit.Type}");
        //
        //     if (jobUnit.JobObject is not IEnumerable<SubTodo> subTodos)
        //         throw new Exception("AddSubTodos::Не удалось преобразовать jobUnit.JobObject");
        //     
        //     using (var scope = _scopeFactory.CreateScope())
        //     {
        //         using (var db = scope.ServiceProvider.GetRequiredService<PostgresDbContext>())
        //         {
        //             foreach (var item in subTodos)
        //             {
        //                 var subTodo = db.SubTodos.FirstOrDefault(t => t.UiKey.Equals(item.UiKey));
        //                 if (subTodo != null)
        //                     throw new Exception("AddSubTodos::Не удалось добавить подзадачу,т.к. подзадача с таким же графическим идентификатором уже присутствует");
        //                 db.SubTodos.Add(item);
        //             }
        //
        //             db.SaveChanges();
        //         }
        //     }
        //
        //     return new JobResult()
        //     {
        //         Id = jobUnit.Id,
        //         IsSuccess = true,
        //         Message = string.Empty,
        //         ResultObject = null,
        //     };
        // }
        //
        // /// <summary>
        // /// Обновление списка подзадач
        // /// </summary>
        // /// <param name="jobUnit">Единица работы</param>
        // /// <returns>Результат выполнения операции</returns>
        // /// <exception cref="ArgumentNullException">jobUnit == null</exception>
        // /// <exception cref="Exception">jobUnit.Type != JobType.UpdateSubTodos</exception>
        // /// <exception cref="Exception">jobUnit.JobObject is not IEnumerable(SubTodo) subTodos</exception>
        // public JobResult UpdateSubTodos(Job jobUnit)
        // {
        //     if (jobUnit == null)
        //         throw new ArgumentNullException(nameof(jobUnit));
        //
        //     if (jobUnit.Type != JobType.UpdateSubTodos)
        //         throw new Exception($"Некорректный тип работы для данного метода(UpdateSubTodos):{jobUnit.Type}");
        //
        //     if (jobUnit.JobObject is not IEnumerable<SubTodo> subTodos)
        //         throw new Exception("UpdateSubTodos::Не удалось преобразовать jobUnit.JobObject");
        //
        //     using (var scope = _scopeFactory.CreateScope())
        //     {
        //         using (var db = scope.ServiceProvider.GetRequiredService<PostgresDbContext>())
        //         {
        //             foreach (var item in subTodos)
        //             {
        //                 var subTodo = db.SubTodos.FirstOrDefault(t => t.UiKey.Equals(item.UiKey));
        //                 if (subTodo == null)
        //                     continue;
        //                 subTodo.Description = item.Description;
        //                 subTodo.Confirm = item.Confirm;
        //                 db.SubTodos.Update(subTodo);
        //             }
        //
        //             db.SaveChanges();
        //         }
        //     }
        //
        //     return new JobResult()
        //     {
        //         Id = jobUnit.Id,
        //         IsSuccess = true,
        //         Message = string.Empty,
        //         ResultObject = null,
        //     };
        // }
        //
        //
        // /// <summary>
        // /// Удаление списка подзадач
        // /// </summary>
        // /// <param name="jobUnit">Единица работы</param>
        // /// <returns>Результат выполнения операции</returns>
        // /// <exception cref="ArgumentNullException">jobUnit == null</exception>
        // /// <exception cref="Exception">jobUnit.Type != JobType.RemoveSubTodos</exception>
        // /// <exception cref="Exception">jobUnit.JobObject is not IEnumerable(int) uiKeys</exception>
        // public JobResult RemoveSubTodos(Job jobUnit)
        // {
        //     if (jobUnit == null)
        //         throw new ArgumentNullException(nameof(jobUnit));
        //
        //     if (jobUnit.Type != JobType.RemoveSubTodos)
        //         throw new Exception($"Некорректный тип работы для данного метода(RemoveSubTodos):{jobUnit.Type}");
        //
        //     if (jobUnit.JobObject is not IEnumerable<int> uiKeys)
        //         throw new Exception("RemoveSubTodos::Не удалось преобразовать jobUnit.JobObject");
        //
        //     using (var scope = _scopeFactory.CreateScope())
        //     {
        //         using (var db = scope.ServiceProvider.GetRequiredService<PostgresDbContext>())
        //         {
        //             foreach (var key in uiKeys)
        //             {
        //                 var subTodo = db.SubTodos.FirstOrDefault(t => t.UiKey.Equals(key));
        //                 if (subTodo == null)
        //                     continue;
        //                 db.SubTodos.Remove(subTodo);
        //             }
        //
        //             db.SaveChanges();
        //         }
        //     }
        //
        //     return new JobResult()
        //     {
        //         Id = jobUnit.Id,
        //         IsSuccess = true,
        //         Message = string.Empty,
        //         ResultObject = null,
        //     };
        // }
        
        
        



    }
}