using NUnit.Framework;
using System.Net.Http;

namespace StringManager.Tests.IntegrationTests.Setup
{
    public class APIWebApplicationFactory : TestWebApplicationFactory<Startup>
    {
        public APIWebApplicationFactory(string databaseName) : base(databaseName)
        {
        }
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
        private readonly string databaseName;

        public EndpointTestBase(string databaseName)
        {
            this.databaseName = databaseName;
        }

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            factory = new APIWebApplicationFactory(databaseName);
            client = factory.CreateClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            factory.DropDatabase();
        }
    }
}
