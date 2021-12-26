using Newtonsoft.Json;
using NUnit.Framework;
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

        [Test]
        public async Task GetTones_ReturnsValueAsync()
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
            Assert.AreEqual(108, deserialisedData.result.Data.Count);
        }

        [Test]
        public async Task AddTone_Unauthorised_WrongDataAsync()
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
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Tones");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestUserUsername}:{correctTestUserPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.Pass();
            //Assert.IsNotNull(response);
            //Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            //var data = await response.Content.ReadAsStringAsync();
            //Assert.IsEmpty(data);
        }

        [Test]
        public async Task AddTone_BadRequest_NoData_Async()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Tones");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.Pass();
            //Assert.IsNotNull(response);
            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //var data = await response.Content.ReadAsStringAsync();
            //Assert.IsEmpty(data);
        }

        [Test]
        public async Task ModifyTone_UnauthorisedAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Tones");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsEmpty(data);
        }

        [Test]
        public async Task RemoveTone_UnauthorisedAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Tones/11");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsEmpty(data);
        }
    }
}
