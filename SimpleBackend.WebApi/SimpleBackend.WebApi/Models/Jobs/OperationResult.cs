namespace SimpleBackend.WebApi.Models.Jobs
{
    /// <summary>
    /// Результат выполения операции 
    /// </summary>
    public sealed class OperationResult
    {
        /// <summary>
        /// Флаг успешности операции
        /// </summary>
        public bool IsSuccess { get; init; }
        
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; init; }
    }
}