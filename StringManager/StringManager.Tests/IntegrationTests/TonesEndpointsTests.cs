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
    internal class TonesEndpointsTests : EndpointTestBase
    {
        public TonesEndpointsTests() : base("TonesEndpointsTestsDatabase")
        {
        }

        [Test, Order(1)]
        [TestCase(108)]
        public async Task GetTones_ReturnsValueAsync(int numberOfTones)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Tones");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<List<Core.Models.Tone>>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(numberOfTones, deserialisedData.result.Data.Count);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        [TestCase(null, null, true)]
        public async Task AddTone_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new AddToneRequest()
            {
                Frequency = 0.1,
                WaveLenght = 0.1,
                Name = "testToneToAdd"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Tones");
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
            if(isEmpty)
                Assert.IsEmpty(data);
            else
                Assert.IsNotEmpty(data);
        }

        [Test]
        public async Task AddTone_BadRequest_WrongDataAsync()
        {
            //Arrange
            var requestBody = new AddToneRequest()
            {
                Frequency = -0.1,
                WaveLenght = 0.1,
                Name = ""
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Tones");
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

        [Test, Order(2)]
        public async Task AddTone_SuccessAsync()
        {
            //Arrange
            var requestBody = new AddToneRequest()
            {
                Frequency = 0.1,
                WaveLenght = 0.1,
                Name = "testToneToAdd"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Tones");
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
            await GetTones_ReturnsValueAsync(109);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        [TestCase(null, null, true)]
        public async Task ModifyTone_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new ModifyToneRequest()
            {
                Id = 109,
                Frequency = 0.1,
                WaveLenght = 0.1,
                Name = "testToneToAdd"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Tones");
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
        [TestCase(109, HttpStatusCode.NoContent), Order(3)]
        [TestCase(0, HttpStatusCode.BadRequest)]
        public async Task ModifyTone_AuthorisedAsync(int id, HttpStatusCode statusCode)
        {
            //Arrange
            var requestBody = new ModifyToneRequest()
            {
                Id = id,
                Frequency = 0.2,
                WaveLenght = 0.2,
                Name = "modifyTestToneToAdd"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Tones");
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
            {
                Assert.IsEmpty(data);
                await GetTones_ReturnsValueAsync(109);
            }
            else
            {
                Assert.IsNotEmpty(data);
            }
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        [TestCase(null, null, true)]
        public async Task RemoveTone_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            {
                //Arrange
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Tones/109");
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

        [Test]
        [TestCase(300, HttpStatusCode.NotFound)]
        [TestCase(109, HttpStatusCode.NoContent), Order(4)]
        public async Task RemoveTone_AuthorisedAsync(int id, HttpStatusCode statusCode)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Tones/"+id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(statusCode, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            if (statusCode == HttpStatusCode.NotFound)
            {
                Assert.IsNotEmpty(data);
            }
            else
            {
                Assert.IsEmpty(data);
                await GetTones_ReturnsValueAsync(108);
            }
        }
    }
}
