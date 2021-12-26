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
        public async Task GetBaseTones_ReturnsValueAsync(int numberOfTests)
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
            Assert.AreEqual(numberOfTests, deserialisedData.result.Data.Count);
        }

        [Test]
        public async Task AddTone_Unauthorised_WrongLoginDataAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Tones");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{incorrectTestUsername}:{incorrectTestPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsEmpty(data);
        }

        [Test]
        public async Task AddTone_Unauthorised_UserAccountAsync()
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
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestUserUsername}:{correctTestUserPassword}")));
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
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
            await GetBaseTones_ReturnsValueAsync(109);
        }

        [Test]
        public async Task ModifyTone_Unauthorised_WrongLoginDataAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Tones");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{incorrectTestUsername}:{incorrectTestPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsEmpty(data);
        }

        [Test]
        public async Task ModifyTone_Unauthorised_UserAccountAsync()
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
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestUserUsername}:{correctTestUserPassword}")));
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test]
        public async Task ModifyTone_BadRequest_WrongDataAsync()
        {
            //Arrange
            var requestBody = new AddToneRequest()
            {
                Frequency = -0.1,
                WaveLenght = 0.1,
                Name = ""
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
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test, Order(3)]
        public async Task ModifyTone_SuccessAsync()
        {
            //Arrange
            var requestBody = new ModifyToneRequest()
            {
                Id = 109,
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
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsEmpty(data);
            await GetBaseTones_ReturnsValueAsync(109);
        }

        [Test]
        public async Task RemoveTone_Unauthorised_WrongLoginDataAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Tones/109");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{incorrectTestUsername}:{incorrectTestPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsEmpty(data);
        }

        [Test]
        public async Task RemoveTone_Unauthorised_UserAccountAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Tones/109");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestUserUsername}:{correctTestUserPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test, Order(4)]
        public async Task RemoveTone_SuccessAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Tones/109");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsEmpty(data);
            await GetBaseTones_ReturnsValueAsync(108);
        }
    }
}
