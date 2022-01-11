namespace SimpleBackend.WebApi.Models.Jobs.Queue
{
    /// <summary>
    /// Очередь принятых в работу задач
    /// </summary>
    public sealed class AcceptedJobQueue:QueueWrapper<Job> { }
}