using Newtonsoft.Json;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Tests.IntegrationTests.Setup;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StringManager.Tests.IntegrationTests
{
    internal class TuningsEndpointsTests : EndpointTestBase
    {
        public TuningsEndpointsTests() : base("TuningsEndpointsTestsDatabase")
        {
        }

        [Test, Order(1)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 6, 3)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 7, 2)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, null, 5)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 6, 3)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 7, 2)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, null, 5)]
        [TestCase(null, null, 6, 3)]
        [TestCase(null, null, 7, 2)]
        [TestCase(null, null, null, 5)]
        public async Task GetTuningsAsync(string username, string password, int? numberOfStrings, int numberOfTunings)
        {
            //Arrange
            var requestBody = new GetTuningsRequest()
            {
                NumberOfStrings = numberOfStrings
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, numberOfStrings == null ? "/Tunings" : "/Tunings?numberOfStrings=" + numberOfStrings);
            if(username != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                    System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            }

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<List<Core.Models.TuningList>>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(numberOfTunings, deserialisedData.result.Data.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-3)]
        [TestCase(1)]
        [TestCase(9)]
        public async Task GetTuningAsync(int id)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Tunings/" + id);

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<Core.Models.Tuning>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.IsNotEmpty(data);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        public async Task AddTuning_UnauthorisedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new AddTuningRequest()
            {
                Name = "testAddedTuning",
                NumberOfStrings = 8
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Tunings");
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
        [TestCase(correctTestUserUsername, correctTestUserPassword)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword)]
        public async Task AddTuning_BadRequestAsync(string username, string password)
        {
            //Arrange
            var requestBody = new AddTuningRequest()
            {
                Name = null,
                NumberOfStrings = -6
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Tunings");
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
        public async Task AddTone_SuccessAsync()
        {
            //Arrange
            var requestBody = new AddTuningRequest()
            {
                Name = "testAddedTuning",
                NumberOfStrings = 8
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Tunings");
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
            Assert.IsNotEmpty(data);
            await GetTuningsAsync(correctTestAdminUsername, correctTestAdminPassword, 8, 1);
        }

        [Test, Order(3)]
        public async Task ModifyTone_SuccessAsync()
        {
            //Arrange
            var requestBody = new ModifyTuningRequest()
            {
                Name = "testUpdatedTuning",
                NumberOfStrings = 9,
                Id = 6
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Tunings");
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
            await GetTuningsAsync(correctTestAdminUsername, correctTestAdminPassword, 9, 1);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        public async Task ModifyTuning_UnauthorisedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new ModifyTuningRequest()
            {
                Name = "testAddedTuning",
                NumberOfStrings = 8,
                Id = 6
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Tunings");
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
        [TestCase(correctTestUserUsername, correctTestUserPassword)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword)]
        public async Task ModifyTuning_BadRequestAsync(string username, string password)
        {
            //Arrange
            var requestBody = new ModifyTuningRequest()
            {
                Name = "",
                NumberOfStrings = -158,
                Id = 6
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Tunings");
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

        public async Task ModifyTuning_NotFoundAsync()
        {
            //Arrange
            var requestBody = new ModifyTuningRequest()
            {
                Name = "testAddedTuning",
                NumberOfStrings = 8,
                Id = 26
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Tunings");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));
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

        [Test, Order(4)]
        public async Task RemoveTone_SuccessAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Tunings/6");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsEmpty(data);
            await GetTuningsAsync(correctTestAdminUsername, correctTestAdminPassword, 9, 0);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        public async Task RemoveTuning_UnauthorisedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Tunings/6") ;
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
        [TestCase(correctTestUserUsername, correctTestUserPassword)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword)]
        public async Task RemoveTuning_BadRequestAsync(string username, string password)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Tunings/-3");
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
        public async Task RemoveTuning_NotFoundAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Tunings/15");
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
