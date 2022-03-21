using NUnit.Framework;

namespace SimpleBackend.Tests.ApiTests
{
    [TestFixture]
    public class TokensStorageServiceTests
    {

                
        [TestCase(TestName="Простое проверка токенов")]
        public void SimpleCheckToken_Test()
        {
            
        }
        
        [TestCase(TestName="Потокобезопасное проверка токенов")]
        public void ThreadSafeCheckToken_Test()
        {
            
        }
        
        [TestCase(TestName="Простое добавление токенов")]
        public void SimpleAddToken_Test()
        {

        }
        
        [TestCase(TestName="Потокобезопасное добавление токенов")]
        public void ThreadSafeAddToken_Test()
        {
            
        }

        [TestCase(TestName="Простое удаление токенов")]
        public void SimpleDeleteToken_Test()
        {
            
        }
        
        [TestCase(TestName="Потокобезопасное удаление токенов")]
        public void ThreadSafeDeleteToken_Test()
        {
            
        }
    }
}