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
    internal class StringsInSetsEndpointsTests : EndpointTestBase
    {
        public StringsInSetsEndpointsTests() : base("StringsInSetsEndpointsTestsDatabase")
        {
        }

        [Test, Order(1)]
        public async Task AddStringInSets_SuccessAsync()
        {
            //Arrange
            int position = 8;
            var requestBody = new AddStringInSetRequest()
            {
                StringId = 3,
                StringsSetId = 4,
                Position = position
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/StringsInSets");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<Core.Models.StringInSet>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(38, deserialisedData.result.Data.Id);
            Assert.AreEqual(position, deserialisedData.result.Data.Position);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        public async Task AddStringInSets_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new AddStringInSetRequest()
            {
                StringId = 3,
                StringsSetId = 4,
                Position = 8
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/StringsInSets");
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
        [TestCase(correctTestUserUsername, correctTestUserPassword, -11, 66, 5)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 0, 66, 5)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 11, 0, 5)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 11, -66, 5)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 11, 66, 0)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 11, 66, -5)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, -11, 66, 5)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 0, 66, 5)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, 300, 5)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, 0, 5)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, -66, 5)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, 66, 7)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, 66, 0)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, 66, -5)]
        public async Task AddStringInSets_BadRequestAsync(string username, string password, int stringId, int stringsSetId, int position)
        {
            //Arrange
            var requestBody = new AddStringInSetRequest()
            {
                StringId = stringId,
                StringsSetId = stringsSetId,
                Position = position
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/StringsInSets");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test, Order(2)]
        public async Task ModifyStringInSets_SuccessAsync()
        {
            //Arrange
            var requestBody = new ModifyStringInSetRequest()
            {
                Id = 38,
                StringId = 8,
                StringsSetId = 2,
                Position = 9
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/StringsInSets");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsEmpty(data);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        public async Task ModifyStringInSets_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new ModifyStringInSetRequest()
            {
                Id = 33,
                StringId = 8,
                StringsSetId = 2,
                Position = 9
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/StringsInSets");
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
        [TestCase(correctTestUserUsername, correctTestUserPassword, -11, 66, 5, 22)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 0, 66, 5, 22)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 11, 0, 5, 22)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 11, -66, 5, 22)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 11, 66, 0, 22)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 11, 66, -5, 22)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 11, 66, 5, 0)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 11, 66, 5, -33)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, -11, 66, 5, 22)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 0, 66, 5, 22)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, 300, 5, 22)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, 0, 5, 22)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, -66, 5, 22)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, 66, 7, 22)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, 66, 0, 22)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, 66, -5, 22)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, 66, 5, 0)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 11, 66, 5, -33)]
        public async Task ModifyStringInSets_BadRequestAsync(string username, string password, int stringId, int stringsSetId, int position, int id)
        {
            //Arrange
            var requestBody = new ModifyStringInSetRequest()
            {
                Id = id,
                StringId = stringId,
                StringsSetId = stringsSetId,
                Position = position
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/StringsInSets");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test]
        public async Task ModifyStringInSets_NotFoundAsync()
        {
            //Arrange
            var requestBody = new ModifyStringInSetRequest()
            {
                Id = 66,
                StringId = 8,
                StringsSetId = 2,
                Position = 9
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/StringsInSets");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test, Order(3)]
        public async Task RemoveStringInSets_SuccessAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/StringsInSets/38");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsEmpty(data);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        public async Task RemoveStringInSets_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/StringsInSets/31");
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

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 0)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, -32)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 0)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, -32)]
        public async Task RemoveStringInSets_BadRequestAsync(string username, string password, int id)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/StringsInSets/" + id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test]
        public async Task RemoveToneInTuning_NotFoundAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/StringsInSets/66");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }
    }
}
