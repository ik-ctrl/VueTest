using System;
using NUnit.Framework;
using SimpleBackend.WebApi.Models.Jobs;
using SimpleBackend.WebApi.Models.Jobs.Storage;
using SimpleBackend.WebApi.Models.Worker;

namespace SimpleBackend.Tests.ApiTests
{
    [TestFixture]
    [Description("Проверка работы очереди принятых задач")]
    public class QueueWrapperTests
    {
        private QueueWrapper<Job> _queue;

        [SetUp]
        [Description("Создание тестового объекта")]
        public void Setup() => _queue = new AcceptedJobQueue();

        [TearDown]
        [Description("Очистка тестового объекта")]
        public void Cleanup() => _queue = null;

        [Test]
        [Description("Добавление работы в очередь")]
        public void Enqueue_Test()
        {
            var job = new Job()
            {
                Id = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            };
            _queue.Enqueue(job);
            Assert.AreEqual(false, _queue.IsEmpty);
        }

        [Test]
        [Description("Проверка на выпадения исключения")]
        public void Enqueue_Exceptions_Test() => Assert.Throws<ArgumentNullException>(() => _queue.Enqueue(null));

        [Test]
        [Description("Флаг пустой очереди")]
        public void IsEmpty_Test()
        {
            Assert.AreEqual(true, _queue.IsEmpty);
            var job = new Job()
            {
                Id = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            };
            _queue.Enqueue(job);
            Assert.AreEqual(false, _queue.IsEmpty);
        }

        [Test]
        [Description("Изъятие работы из очереди")]
        public void Dequeue_Test()
        {
            var testJobId = Guid.NewGuid();
            _queue.Enqueue(new Job()
            {
                Id = testJobId,
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            });

            _queue.Enqueue(new Job()
            {
                Id = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            });

            _queue.Enqueue(new Job()
            {
                Id = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            });
            _queue.Enqueue(new Job()
            {
                Id = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            });
            var job = _queue.Dequeue();
            Assert.AreEqual(testJobId, job.Id);
        }

        [Test]
        [Description("Изъятие работы из пустой очереди")]
        public void Dequeue_EmptyQueue_Test()
        {
            var job = _queue.Dequeue();
            Assert.AreEqual(null, job);
        }

        [Test]
        [Description("Проверка наличия работы в очереди")]
        public void CheckJob_Test()
        {
            var testJobId = Guid.NewGuid();
            _queue.Enqueue(new Job()
            {
                Id = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            });

            _queue.Enqueue(new Job()
            {
                Id = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            });

            _queue.Enqueue(new Job()
            {
                Id = testJobId,
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            });
            _queue.Enqueue(new Job()
            {
                Id = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            });
            var successfulCheckResult = _queue.CheckJob(testJobId);
            Assert.AreEqual(true, successfulCheckResult);
            var wrongJobId = Guid.NewGuid();
            var unsuccessfulCheckResult = _queue.CheckJob(wrongJobId);
            Assert.AreEqual(false, unsuccessfulCheckResult);
        }

        [Test]
        [Description("Проверка наличия работы в пустой очереди")]
        public void CheckJob_EmptyQueue_Test()
        {
            var unsuccessfulCheckResult = _queue.CheckJob(Guid.NewGuid());
            Assert.AreEqual(false, unsuccessfulCheckResult);
        }

        [Test]
        [Description("Проверка очистки очереди")]
        public void ClearQueue_Test()
        {
            _queue.Enqueue(new Job()
            {
                Id = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            });

            _queue.Enqueue(new Job()
            {
                Id = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            });

            _queue.Enqueue(new Job()
            {
                Id = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            });
            _queue.Enqueue(new Job()
            {
                Id = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodos,
                JobObject = new object()
            });
            Assert.IsFalse(_queue.IsEmpty);
            _queue.ClearQueue();
            Assert.IsTrue(_queue.IsEmpty);
        }

        [Test]
        [Description("Проверка очистки пустой очереди")]
        public void ClearQueue_EmptyQueue_Test()
        {
            Assert.IsTrue(_queue.IsEmpty);
            _queue.ClearQueue();
            Assert.IsTrue(_queue.IsEmpty);
        }
    }
}