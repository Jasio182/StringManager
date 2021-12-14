using NUnit.Framework;
using StringManager.Services.InternalClasses;

namespace StringManager.Services.Tests.InternalClassesTests
{
    public class PasswordHashingTest
    {
        [Test]
        public void HashingPasswordTest()
        {
            //Act
            var passwordHashed = PasswordHashing.HashPassword("testPassword");
            //Asset
            Assert.IsNotNull(passwordHashed);
            Assert.AreEqual("clzUvrti0Td/KuoyN5l3xc1HPPc3t2RpKZ21NJo9WAg=", passwordHashed);
        }
    }
}
