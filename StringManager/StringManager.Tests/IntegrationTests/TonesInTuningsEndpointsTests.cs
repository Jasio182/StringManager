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
    internal class TonesInTuningsEndpointsTests : EndpointTestBase
    {
        public TonesInTuningsEndpointsTests() : base("TonesInTuningsEndpointsTestsDatabase")
        {
        }

        [Test, Order(1)]
        public async Task AddToneInTuning_SuccessAsync()
        {
            //Arrange
            int position = 8;
            var requestBody = new AddToneInTuningRequest()
            {
                Position = position,
                ToneId = 66,
                TuningId = 4
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/TonesInTunings");
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
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<Core.Models.ToneInTuning>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(33, deserialisedData.result.Data.Id);
            Assert.AreEqual(position, deserialisedData.result.Data.Position);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        public async Task AddToneInTuning_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new AddToneInTuningRequest()
            {
                Position = 8,
                ToneId = 66,
                TuningId = 4
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/TonesInTunings");
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
        public async Task AddToneInTuning_BadRequestAsync(string username, string password, int position, int toneId, int tuningId)
        {
            //Arrange
            var requestBody = new AddToneInTuningRequest()
            {
                Position = position,
                ToneId = toneId,
                TuningId = tuningId
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/TonesInTunings");
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
        public async Task ModifyToneInTuning_SuccessAsync()
        {
            //Arrange
            var requestBody = new ModifyToneInTuningRequest()
            {
                Id = 33,
                Position = 15,
                ToneId = 44,
                TuningId = 3
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/TonesInTunings");
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
        public async Task ModifyToneInTuning_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new ModifyToneInTuningRequest()
            {
                Id = 33,
                Position = 8,
                ToneId = 66,
                TuningId = 4
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/TonesInTunings");
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
        public async Task ModifyToneInTuning_BadRequestAsync(string username, string password, int position, int toneId, int tuningId, int id)
        {
            //Arrange
            var requestBody = new ModifyToneInTuningRequest()
            {
                Id = id,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/TonesInTunings");
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
        public async Task ModifyToneInTuning_NotFoundAsync()
        {
            //Arrange
            var requestBody = new ModifyToneInTuningRequest()
            {
                Id = 66,
                Position = 8,
                ToneId = 66,
                TuningId = 4
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/TonesInTunings");
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
        public async Task RemoveToneInTuning_SuccessAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/TonesInTunings/33");
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
        public async Task RemoveToneInTuning_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/TonesInTunings/31");
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
        public async Task RemoveToneInTuning_BadRequestAsync(string username, string password, int id)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/TonesInTunings/" + id);
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
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/TonesInTunings/66");
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
