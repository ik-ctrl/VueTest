using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnit.Framework;
using SimpleBackend.WebApi.Models.Tokens;

namespace SimpleBackend.Tests.ApiTests
{
    [TestFixture]
    public class TokensStorageServiceTests
    {
        [TestCase(TestName = "Простое проверка токенов")]
        public void SimpleValidateToken_Test()
        {
            var service = new TokenStorageService();
            for (var i = 0; i < 100; i++)
            {
                var endTime = i % 2 == 0
                    ? DateTime.Now.AddDays(3)
                    : DateTime.Now.AddDays(-3);
                service.AddToken(i, $"Token {i + 1}", endTime);
            }

            var trueResult = service.ValidateToken(4, "Token 5");
            Assert.AreEqual(true, trueResult);

            var randomTokenResult = service.ValidateToken(4, "sdfsdfsdfsdf");
            Assert.AreEqual(false, randomTokenResult);

            var unknownUserResult = service.ValidateToken(10000, "sdasdasd");
            Assert.AreEqual(false, unknownUserResult);

            var endTimeResult = service.ValidateToken(5, "Token 6");
            Assert.AreEqual(false, endTimeResult);
        }

        [TestCase(TestName = "Потокобезопасное проверка токенов")]
        public void ThreadSafeValidateToken_Test()
        {
            var tasks = new List<Task>();
            var service = new TokenStorageService(true);
            for (var i = 0; i < 100; i++)
            {
                var endTime = i % 2 == 0
                    ? DateTime.Now.AddDays(3)
                    : DateTime.Now.AddDays(-3);
                service.AddToken(i, $"Token {i + 1}", endTime);
            }

            for (int i = 0; i < 100; i++)
            {
                var i1 = i;
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        service.ValidateToken(i1, $"Token {i1 + 1}");
                    }
                    catch (Exception e)
                    {
                        Assert.Fail(e.Message);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Assert.Pass();
        }

        [TestCase(TestName = "Простое добавление токенов")]
        public void SimpleAddToken_Test()
        {
            var service = new TokenStorageService();
            for (var i = 0; i < 100; i++)
            {
                var endTime = i % 2 == 0
                    ? DateTime.Now.AddDays(3)
                    : DateTime.Now.AddDays(-3);
                service.AddToken(i, $"Token {i + 1}", endTime);
            }

            var trueResult = service.ValidateToken(4, "Token 5");
            Assert.AreEqual(true, trueResult);
        }

        [TestCase(TestName = "Потокобезопасное добавление токенов")]
        public void ThreadSafeAddToken_Test()
        {
            var service = new TokenStorageService(true);
            var tasks = new List<Task>();
            for (var i = 0; i < 100; i++)
            {
                var i1 = i;
                tasks.Add(Task.Run(() =>
                {
                    var endTime = i1 % 2 == 0
                        ? DateTime.Now.AddDays(3)
                        : DateTime.Now.AddDays(-3);
                    service.AddToken(i1, $"Token {i1 + 1}", endTime);
                }));
            }

            Task.WaitAll(tasks.ToArray());
            var trueResult = service.ValidateToken(4, "Token 5");
            Assert.AreEqual(true, trueResult);
        }

        [TestCase(TestName = "Простое удаление токенов")]
        public void SimpleDeleteToken_Test()
        {
            var service = new TokenStorageService();
            for (var i = 0; i < 100; i++)
            {
                var endTime = i % 2 == 0
                    ? DateTime.Now.AddDays(3)
                    : DateTime.Now.AddDays(-3);
                service.AddToken(i, $"Token {i + 1}", endTime);
            }

            service.DeleteToken(1);
            service.DeleteToken(1000);
            service.DeleteToken(23);
            var result1 = service.ValidateToken(1, "Token 2");
            Assert.AreEqual(false,result1);
        }

        [TestCase(TestName = "Потокобезопасное удаление токенов")]
        public void ThreadSafeDeleteToken_Test()
        {
            var service = new TokenStorageService(true);
            for (var i = 0; i < 100; i++)
            {
                var endTime = i % 2 == 0
                    ? DateTime.Now.AddDays(3)
                    : DateTime.Now.AddDays(-3);
                service.AddToken(i, $"Token {i + 1}", endTime);
            }

            var tasks = new List<Task>();
            for (var i = 0; i < 50; i++)
            {
                var i1 = i;
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        service.DeleteToken(i1);
                    }
                    catch (Exception ex)
                    {
                        Assert.Fail(ex.Message);
                    }
                }));
                Task.WaitAll(tasks.ToArray());
                Assert.Pass();
            }
        }
    }
}