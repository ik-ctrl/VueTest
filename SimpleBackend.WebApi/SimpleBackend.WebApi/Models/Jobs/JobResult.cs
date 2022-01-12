using System;

namespace SimpleBackend.WebApi.Models.Jobs
{
    /// <summary>
    /// Результат выполнения работы
    /// </summary>
    public class JobResult: IGuided
    {
        /// <summary>
        /// Идентификатор работы
        /// </summary>
        public Guid Id { get; set; }
        
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
        public override string ToString() => $"JobId:{Id}";
        
        /// <summary>
        /// Проверка на совпадение идентификаторов Guid
        /// </summary>
        /// <param name="id">Проверяемый идентификатор</param>
        /// <returns>Результат проверки равенства идентификаторов</returns>
        public bool CompareId(Guid id) => Id.Equals(id);
    }
}