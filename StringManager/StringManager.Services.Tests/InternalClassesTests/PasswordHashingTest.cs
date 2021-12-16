using NUnit.Framework;
using StringManager.Services.InternalClasses;

namespace StringManager.Services.Tests.InternalClassesTests
{
    public class PasswordHashingTest
    {
        [Test]
        [TestCase("testPassword", "clzUvrti0Td/KuoyN5l3xc1HPPc3t2RpKZ21NJo9WAg=")]
        [TestCase("test", "SN/q7dBUJ3mb3P3Xn16G0pMG1ZK9bXIPcnRiZXcnb1Q=")]
        [TestCase("Pass", "v3cgMpksZDWY6XnwS42LBOdkAORkS+4SXOVZvzHx0kU=")]
        public void HashingPasswordTest(string testPassword, string testExpectedResult)
        {
            //Act
            var passwordHashed = PasswordHashing.HashPassword(testPassword);
            //Asset
            Assert.IsNotNull(passwordHashed);
            Assert.AreEqual(testExpectedResult, passwordHashed);
        }
    }
}
