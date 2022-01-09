using System;
using SimpleBackend.WebApi.Models.Worker;

namespace SimpleBackend.WebApi.Models.Jobs
{
    /// <summary>
    /// Выполняемая работа
    /// </summary>
    public sealed class Job:IGuided
    {
        /// <summary>
        /// Идентификатор работы
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Тип выполняемой работы
        /// </summary>
        public JobType Type { get; set; }

        /// <summary>
        /// Сообщение задачи
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Объект выполения задачи 
        /// </summary>
        public object JobObject { get; set; }

        /// <summary>
        /// Вывод информации о работе
        /// </summary>
        /// <returns>Информации о работе</returns>
        public override string ToString() => $"JobId:{Id}-JobType:{Type}";
        
        /// <summary>
        /// Проверка на совпадение идентификаторов Guid
        /// </summary>
        /// <param name="id">Проверяемый идентификатор</param>
        /// <returns>Результат проверки равенства идентификаторов</returns>
        public bool CompareId(Guid id) => Id.Equals(id);
    }
}