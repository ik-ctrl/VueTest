namespace SimpleBackend.WebApi.Models.Jobs.Storage
{
    /// <summary>
    /// Очередь принятых в работу задач
    /// </summary>
    public sealed class AcceptedJobQueue:QueueWrapper<Job> { }
}