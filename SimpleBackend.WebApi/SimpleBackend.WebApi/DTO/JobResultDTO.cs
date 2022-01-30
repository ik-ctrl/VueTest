using System;
using SimpleBackend.WebApi.Models.Jobs;

namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Результат выполнения работы
    /// </summary>
    public class JobResultDTO: IGuided
    {
        /// <summary>
        /// Идентификатор работы
        /// </summary>
        public Guid JobId { get; set; }
        
        /// <summary>
        /// Флаг успешного выполнения задачи
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// Сообщение задачи
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Объект выполения задачи 
        /// </summary>
        public object ResultObject { get; set; }

        /// <summary>
        /// Вывод информации о работе
        /// </summary>
        /// <returns>Информации о работе</returns>
        public override string ToString() => $"JobId:{JobId}";
        
        /// <summary>
        /// Проверка на совпадение идентификаторов Guid
        /// </summary>
        /// <param name="id">Проверяемый идентификатор</param>
        /// <returns>Результат проверки равенства идентификаторов</returns>
        public bool CompareId(Guid id) => JobId.Equals(id);
    }
}