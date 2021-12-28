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
    internal class StringsEndpointsTests : EndpointTestBase
    {
        public StringsEndpointsTests() : base("StringsEndpointsTestsDatabase")
        {
        }

        [Test, Order(1)]
        [TestCase(correctTestUserUsername, correctTestUserPassword)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword)]
        [TestCase(incorrectTestUsername, incorrectTestPassword)]
        [TestCase(null, null)]
        public async Task GetString_ReturnsValueAsync(string username, string password)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Strings");
            if (username != null)
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
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<List<Core.Models.String>>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(37, deserialisedData.result.Data.Count);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        [TestCase(null, null, true)]
        public async Task AddString_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new AddStringRequest()
            {
                ManufacturerId = 1,
                NumberOfDaysGood = 135,
                Size = 9,
                SpecificWeight = 0.00008,
                StringType = Core.Enums.StringType.PlainNikled
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Strings");
            if (username != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                    System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            }
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
        [TestCase(-3, 135, 9, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(0, 135, 9, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(3, -135, 9, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(3, 0, 9, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(3, 135, -9, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(3, 135, 0, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(3, 135, 9, -0.00008, (Core.Enums.StringType)3)]
        [TestCase(3, 135, 9, 0, (Core.Enums.StringType)3)]
        [TestCase(3, 135, 9, 0.00008, (Core.Enums.StringType)(-3))]
        [TestCase(3, 135, 9, 0.00008, (Core.Enums.StringType)8)]
        public async Task AddString_BadRequestAsync(int manufacturerId, int numberOfDaysGood, int size, double specificWeight, Core.Enums.StringType stringType)
        {
            //Arrange
            var requestBody = new AddStringRequest()
            {
                ManufacturerId = manufacturerId,
                NumberOfDaysGood = numberOfDaysGood,
                Size = size,
                SpecificWeight = specificWeight,
                StringType = stringType
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Strings");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestUserUsername}:{correctTestUserPassword}")));
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
        public async Task AddString_NotFoundAsync()
        {
            //Arrange
            var requestBody = new AddStringRequest()
            {
                ManufacturerId = 8,
                NumberOfDaysGood = 135,
                Size = 9,
                SpecificWeight = 0.00008,
                StringType = Core.Enums.StringType.PlainNikled
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Strings");
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
        public async Task AddString_SuccessAsync()
        {
            //Arrange
            var requestBody = new AddStringRequest()
            {
                ManufacturerId = 3,
                NumberOfDaysGood = 135,
                Size = 9,
                SpecificWeight = 0.00008,
                StringType = Core.Enums.StringType.PlainNikled
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Strings");
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
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<Core.Models.String>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(38, deserialisedData.result.Data.Id);
            Assert.AreEqual(0.00008, deserialisedData.result.Data.SpecificWeight);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        [TestCase(null, null, true)]
        public async Task ModifyString_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new ModifyStringRequest()
            {
                Id = 8,
                ManufacturerId = 3,
                NumberOfDaysGood = 135,
                Size = 9,
                SpecificWeight = 0.00008,
                StringType = Core.Enums.StringType.PlainNikled
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Strings");
            if (username != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                    System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            }
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
        [TestCase(0, 3, 135, 9, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(-38, 3, 135, 9, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(38, -3, 135, 9, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(38, 0, 135, 9, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(38, 3, -135, 9, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(38, 3, 0, 9, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(38, 3, 135, -9, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(38, 3, 135, 0, 0.00008, (Core.Enums.StringType)3)]
        [TestCase(38, 3, 135, 9, -0.00008, (Core.Enums.StringType)3)]
        [TestCase(38, 3, 135, 9, 0, (Core.Enums.StringType)3)]
        [TestCase(38, 3, 135, 9, 0.00008, (Core.Enums.StringType)(-3))]
        [TestCase(38, 3, 135, 9, 0.00008, (Core.Enums.StringType)8)]
        public async Task ModifyString_BadRequestAsync(int id, int manufacturerId, int numberOfDaysGood, int size, double specificWeight, Core.Enums.StringType stringType)
        {
            //Arrange
            var requestBody = new ModifyStringRequest()
            {
                Id = id,
                ManufacturerId = manufacturerId,
                NumberOfDaysGood = numberOfDaysGood,
                Size = size,
                SpecificWeight = specificWeight,
                StringType = stringType
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Strings");
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

        [Test]
        public async Task ModifyString_NotFoundAsync()
        {
            //Arrange
            var requestBody = new ModifyStringRequest()
            {
                Id = 69,
                ManufacturerId = 3,
                NumberOfDaysGood = 135,
                Size = 9,
                SpecificWeight = 0.00008,
                StringType = Core.Enums.StringType.PlainNikled
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Strings");
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
        public async Task ModifyString_SuccessAsync()
        {
            //Arrange
            var requestBody = new ModifyStringRequest()
            {
                Id = 38,
                ManufacturerId = 2,
                NumberOfDaysGood = 145,
                Size = 10,
                SpecificWeight = 0.0001,
                StringType = Core.Enums.StringType.PlainNikled
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Strings");
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

        [Test, Order(4)]
        public async Task RemoveString_SuccessAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Strings/38");
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
        [TestCase(null, null, true)]
        public async Task RemoveString_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Strings/4");
            if (username != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                    System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            }

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
        [TestCase(0)]
        [TestCase(-15)]
        public async Task RemoveString_BadRequestAsync(int id)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Strings/" + id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test]
        public async Task RemoveString_NotFoundAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Strings/68");
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
