using NUnit.Framework;
using System.Net.Http;

namespace StringManager.Tests.IntegrationTests.Setup
{
    public class APIWebApplicationFactory : TestWebApplicationFactory<Startup>
    {
    }

    public abstract class EndpointTestBase
    {
        public APIWebApplicationFactory factory;
        public HttpClient client;

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
