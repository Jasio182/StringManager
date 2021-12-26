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

        protected const string correctTestUserUsername = "testUser";
        protected const string correctTestUserPassword = "testUserPass";
        protected const string correctTestAdminUsername = "testAdmin";
        protected const string correctTestAdminPassword = "testAdminPass";
        protected const string incorrectTestUsername = "testWrong";
        protected const string incorrectTestPassword = "testWrongPass";
        private readonly string databaseName;

        public EndpointTestBase(string databaseName)
        {
            this.databaseName = databaseName;
        }

        [OneTimeSetUp]
        public void Setup()
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
