using System;

namespace SimpleBackend.WebApi.Models.Jobs
{
    /// <summary>
    /// Интерфейс для сравнения GUID
    /// </summary>
    public interface IGuided
    {
        /// <summary>
        /// Идентификатор объекта 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Проверка на совпадение идентификаторов Guid
        /// </summary>
        /// <param name="id">Проверяемый идентификатор</param>
        /// <returns>Результат проверки равенства идентификаторов</returns>
        public bool CompareId(Guid id);
    }
}