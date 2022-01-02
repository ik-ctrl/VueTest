using System;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using SimpleBackend.WebApi.Models.Jobs;
using SimpleBackend.WebApi.Models.Worker;


namespace SimpleBackend.Tests.ApiTests
{
    [TestFixture]
    [Description("Проверка работы очереди принятых задач")]
    public class AcceptedJobQueueTests
    {
        private AcceptedJobQueue _queue;
        private List<string> _logger;
        [SetUp]
        public void Setup()
        {
            _logger = new List<string>();
            var mock = new Mock<ILogger>();
            mock.Setup((m) => m.LogTrace(It.IsAny<string>())).Callback<string>(message => _logger.Add(message));
            mock.Setup((m) => m.LogCritical(It.IsAny<string>())).Callback<string>(message => _logger.Add(message));
            mock.Setup((m) => m.LogError(It.IsAny<string>())).Callback<string>(message => _logger.Add(message));
            mock.Setup((m) => m.LogDebug(It.IsAny<string>())).Callback<string>(message => _logger.Add(message));
            mock.Setup((m) => m.LogInformation(It.IsAny<string>())).Callback<string>(message => _logger.Add(message));
            _queue = new AcceptedJobQueue(mock.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            _queue = null;
            _logger = null;
        }
        
        
        [Test]
        public void EnqueueJob_Test()
        {
            var job = new Job()
            {
                JobId = Guid.NewGuid(),
                Message = "new Job",
                Type = JobType.AddTodo,
                JobObject = new object()
            };
            _queue.EnqueueJob(job);
            Assert.AreEqual(1,_logger.Count());
            Assert.Pass();
        }
    }
}