using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using SimpleBackend.WebApi.Helpers.Extensions;
using SimpleBackend.WebApi.Models.Enums;

namespace SimpleBackend.Tests.HelpersTests
{
    [TestFixture]
    public class EnumsDisplayTest
    {
        [TestCase(ErrorCodeType.NoError,"")]
        [TestCase(ErrorCodeType.UnknownError,"Неизвестная ошибка")]
        public void GetEnumWithDisplayNameAttribute(ErrorCodeType errorCode,string expectedDisplayName)
        {
            var result = errorCode.GetAttribute<DisplayAttribute>();
            Assert.AreEqual(expectedDisplayName,result.Name);
        }
        
        [TestCase(TestEnumWithoutDisplayAttribute.TestEnum)]
        public void GetEnumWithoutDisplayNameAttribute(TestEnumWithoutDisplayAttribute testCode)
        {
            var result = testCode.GetAttribute<DisplayAttribute>();
            Assert.AreEqual(null,result);
        }
        
        [TestCase(ErrorCodeType.NoError,"")]
        [TestCase(ErrorCodeType.UnknownError,"Неизвестная ошибка")]
        public void GetEnumNormalName(ErrorCodeType errorCode,string expectedDisplayName)
        {
            var result = errorCode.GetName();
            Assert.AreEqual(expectedDisplayName,result);
        }
        
        [TestCase(TestEnumWithoutDisplayAttribute.TestEnum)]
        public void GetEnumNameWithoutDisplayAttribute(TestEnumWithoutDisplayAttribute testCode)
        {
            var result = testCode.GetName();
            Assert.AreEqual(string.Empty,result);
        }
        
    }
}