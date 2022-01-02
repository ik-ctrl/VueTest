using System;

namespace SimpleBackend.WebApi.Models.Exceptions
{
    /// <summary>
    /// Исключения при работе с очередью принятых работа в обработку
    /// </summary>
    public class AcceptingJobException:Exception 
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="message">Сообщение о исключениие</param>
        /// <param name="exception">Внутренние  исключения</param>
        public AcceptingJobException(string message=null,Exception exception=null):base(message,exception) { }
    }
}