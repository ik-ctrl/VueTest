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
        /// <param name="request">Список добавляемых задач</param>
        /// <returns>Единица работы</returns>
        public Job GenerateAddTodosJob(Guid jobId,IEnumerable<TodoDTO> request)
            => GenerateJobUnit(jobId, string.Empty, JobType.AddTodos, request);
        
        /// <summary>
        /// Метод генерации единицы работы для запроса всех задач
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="request">Список идентификаторов ключей</param>
        /// <returns>Единица работы</returns>
        public Job GenerateRemoveTodosJob(Guid jobId,IEnumerable<UiKeyDTO> request)
            => GenerateJobUnit(jobId, string.Empty, JobType.RemoveTodos, request);
        
        /// <summary>
        /// Метод генерации единицы работы для запроса всех задач
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="request">Список обновляемых задач</param>
        /// <returns>Единица работы</returns>
        public Job GenerateUpdateTodosJob(Guid jobId,IEnumerable<TodoDTO> request)
            => GenerateJobUnit(jobId, string.Empty, JobType.UpdateTodos, request);
        
        /// <summary>
        /// Метод генерации единицы работы для добавления подзадач к задаче
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="request">Список обновляемых под задач</param>
        /// <returns>Единица работы</returns>
        public Job GenerateAddSubTodosJob(Guid jobId,IEnumerable<SubTodoDTO> request)
            => GenerateJobUnit(jobId, string.Empty, JobType.AddSubTodos, request);
        
        /// <summary>
        /// Метод генерации единицы работы для обновления подзадач
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="request">Список обновляемых задач</param>
        /// <returns>Единица работы</returns>
        public Job GenerateUpdateSubTodosJob(Guid jobId,IEnumerable<SubTodoDTO> request)
            => GenerateJobUnit(jobId, string.Empty, JobType.UpdateSubTodos, request);
        
        /// <summary>
        /// Метод генерации единицы работы для удаления списка подзадач
        /// </summary>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="request">Список идентификаторов ключей</param>
        /// <returns>Единица работы</returns>
        public Job GenerateRemoveSubTodosJob(Guid jobId,IEnumerable<UiKeyDTO> request)
            => GenerateJobUnit(jobId, string.Empty, JobType.RemoveSubTodos, request);
    }
}