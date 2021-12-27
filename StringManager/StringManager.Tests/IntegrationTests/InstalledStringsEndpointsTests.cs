using Newtonsoft.Json;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Tests.IntegrationTests.Setup;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StringManager.Tests.IntegrationTests
{
    internal class InstalledStringsEndpointsTests : EndpointTestBase
    {
        public InstalledStringsEndpointsTests() : base("InstalledStringsEndpointsTestsDatabase")
        {
        }

        [Test]
        [TestCase(13, 77, 7, 1, HttpStatusCode.OK), Order(1)]
        [TestCase(-13, 77, 7, 1, HttpStatusCode.BadRequest)]
        [TestCase(0, 77, 7, 1, HttpStatusCode.BadRequest)]
        [TestCase(13, -77, 7, 1, HttpStatusCode.BadRequest)]
        [TestCase(13, 0, 7, 1, HttpStatusCode.BadRequest)]
        [TestCase(13, 77, -7, 1, HttpStatusCode.BadRequest)]
        [TestCase(13, 77, 0, 1, HttpStatusCode.BadRequest)]
        [TestCase(13, 77, 7, -1, HttpStatusCode.BadRequest)]
        [TestCase(13, 77, 7, 0, HttpStatusCode.BadRequest)]
        public async Task AddInstalledString_AuthorisedAsync(int stringId, int toneId, int position, int myInstrumentId, HttpStatusCode statusCode)
        {
            //Arrange
            var requestBody = new AddInstalledStringRequest()
            {
                StringId = stringId,
                ToneId = toneId,
                Position = position,
                MyInstrumentId = myInstrumentId
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/InstalledStrings");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(statusCode, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        [TestCase(null, null, true)]
        public async Task AddInstalledString_UnauthorisedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new AddInstalledStringRequest()
            {
                StringId = 13,
                ToneId = 77,
                Position = 7,
                MyInstrumentId = 1
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/InstalledStrings");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            if (isEmpty)
                Assert.IsEmpty(data);
            else
                Assert.IsNotEmpty(data);
        }

        [Test]
        [TestCase(300, HttpStatusCode.NotFound)]
        [TestCase(14, HttpStatusCode.NoContent), Order(2)]
        [TestCase(0, HttpStatusCode.BadRequest)]
        public async Task ModifyInstalledString_AuthorisedAsync(int id, HttpStatusCode statusCode)
        {
            //Arrange
            var requestBody = new ModifyInstalledStringRequest()
            {
                Id = id,
                StringId = 1
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/InstalledStrings");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(statusCode, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            if (statusCode == HttpStatusCode.NoContent)
                Assert.IsEmpty(data);
            else
                Assert.IsNotEmpty(data);
        }


        [Test]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        [TestCase(null, null, true)]
        public async Task ModifyInstalledString_UnauthorisedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new ModifyInstalledStringRequest()
            {
                Id = 109,
                StringId = 1
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/InstalledStrings");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            if (isEmpty)
                Assert.IsEmpty(data);
            else
                Assert.IsNotEmpty(data);
        }

        [Test]
        [TestCase(300, HttpStatusCode.NotFound)]
        [TestCase(14, HttpStatusCode.NoContent), Order(3)]
        public async Task RemoveInstalledString_AuthorisedAsync(int id, HttpStatusCode statusCode)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/InstalledStrings/" + id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(statusCode, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            if (statusCode == HttpStatusCode.NotFound)
                Assert.IsNotEmpty(data);
            else
                Assert.IsEmpty(data);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        [TestCase(null, null, true)]
        public async Task RemoveInstalledString_UnauthorisedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/InstalledStrings/11");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            if (isEmpty)
                Assert.IsEmpty(data);
            else
                Assert.IsNotEmpty(data);
        }
    }
}
