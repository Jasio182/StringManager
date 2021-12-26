using NUnit.Framework;
using System.Net.Http;

namespace StringManager.Tests.IntegrationTests.Setup
{
    public class APIWebApplicationFactory : TestWebApplicationFactory<Startup>
    {
    }

    public abstract class EndpointTestBase
    {
        protected APIWebApplicationFactory factory;
        protected HttpClient client;

        protected readonly string correctTestUserUsername = "testUser";
        protected readonly string correctTestUserPassword = "testUserPass";
        protected readonly string correctTestAdminUsername = "testAdmin";
        protected readonly string correctTestAdminPassword = "testAdminPass";
        protected readonly string incorrectTestUsername = "testWrong";
        protected readonly string incorrectTestPassword = "testWrongPass";

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            factory = new APIWebApplicationFactory();
            client = factory.CreateClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            factory.dropDatabase();
        }
    }
}
