using System;

namespace SimpleBackend.WebApi.Interfaces
{
    /// <summary>
    /// Интерфейс для сравнения GUID
    /// </summary>
    public interface IGuided
    {
        /// <summary>
        /// Идентификатор объекта 
        /// </summary>
        public Guid JobId { get; set; }

        /// <summary>
        /// Проверка на совпадение идентификаторов Guid
        /// </summary>
        /// <param name="id">Проверяемый идентификатор</param>
        /// <returns>Результат проверки равенства идентификаторов</returns>
        public bool CompareId(Guid id);
    }
}