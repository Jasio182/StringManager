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
    internal class StringsSetsEndpointsTests : EndpointTestBase
    {
        public StringsSetsEndpointsTests() : base("StringsSetsEndpointsTestsDatabase")
        {
        }

        [Test]
        [TestCase(incorrectTestUsername, incorrectTestPassword, Core.Enums.StringType.PlainNikled)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, Core.Enums.StringType.PlainNylon)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, Core.Enums.StringType.WoundBrass)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, Core.Enums.StringType.WoundNikled)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, Core.Enums.StringType.WoundNylon)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, null)]
        [TestCase(null, null, Core.Enums.StringType.PlainNikled)]
        [TestCase(null, null, Core.Enums.StringType.PlainNylon)]
        [TestCase(null, null, Core.Enums.StringType.WoundNikled)]
        [TestCase(null, null, Core.Enums.StringType.WoundBrass)]
        [TestCase(null, null, Core.Enums.StringType.WoundNylon)]
        [TestCase(null, null, null)]
        public async Task GetStringsSets_UnauthorizedAsync(string username, string password, Core.Enums.StringType? stringType)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, stringType == null ? "/StringsSets" : "/StringsSets?stringType=" + stringType);
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
            Assert.IsEmpty(data);
        }

        [Test, Order(1)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, Core.Enums.StringType.PlainNikled, 5)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, Core.Enums.StringType.PlainNylon, 1)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, Core.Enums.StringType.WoundBrass, 2)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, Core.Enums.StringType.WoundNikled, 3)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, Core.Enums.StringType.WoundNylon, 1)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, null, 6)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, Core.Enums.StringType.PlainNikled, 5)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, Core.Enums.StringType.PlainNylon, 1)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, Core.Enums.StringType.WoundBrass, 2)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, Core.Enums.StringType.WoundNikled, 3)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, Core.Enums.StringType.WoundNylon, 1)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, null, 6)]
        public async Task GetStringsSets_ReturnsValueAsync(string username, string password, Core.Enums.StringType? stringType, int count)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, stringType == null ? "/StringsSets" : "/StringsSets?stringType=" + stringType);
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
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<List<Core.Models.StringsSet>>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(count, deserialisedData.result.Data.Count);
        }

        [Test]
        [TestCase(incorrectTestUsername, incorrectTestPassword)]
        [TestCase(null, null)]
        public async Task GetStringsSet_UnauthorizedAsync(string username, string password)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/StringsSets/3");
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
            Assert.IsEmpty(data);
        }

        [Test, Order(2)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, -3, true)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 0, true)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 1, false)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 2, false)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 3, false)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, -3, true)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 0, true)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 1, false)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 2, false)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 3, false)]
        public async Task GetStringsSet_SuccessAsync(string username, string password, int id, bool isNull)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/StringsSets/" + id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<Core.Models.StringsSet>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            if (isNull)
                Assert.IsNull(deserialisedData.result.Data);
            else
                Assert.IsNotNull(deserialisedData.result.Data);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        [TestCase(null, null, true)]
        public async Task AddStringsSet_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new AddStringsSetRequest()
            {
                Name = "testStringsSetName",
                NumberOfStrings = 12
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/StringsSets");
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
        [TestCase("", 12)]
        [TestCase(null, 12)]
        [TestCase("testStringsSetName", -12)]
        [TestCase("testStringsSetName", 0)]
        public async Task AddStringsSet_BadRequestAsync(string name, int numberOfStrings)
        {
            //Arrange
            var requestBody = new AddStringsSetRequest()
            {
                Name = name,
                NumberOfStrings = numberOfStrings
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/StringsSets");
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
        public async Task AddStringsSet_SuccessAsync()
        {
            //Arrange
            var requestBody = new AddStringsSetRequest()
            {
                Name = "testStringsSetName",
                NumberOfStrings = 12
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/StringsSets");
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
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<Core.Models.StringsSet>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(7, deserialisedData.result.Data.Id);
            Assert.AreEqual(12, deserialisedData.result.Data.NumberOfStrings);
            Assert.AreEqual("testStringsSetName", deserialisedData.result.Data.Name);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        [TestCase(null, null, true)]
        public async Task ModifyStringsSet_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new ModifyStringsSetRequest()
            {
                Id = 3,
                Name = "testChangedStringsSetName",
                NumberOfStrings = 9
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/StringsSets");
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
        [TestCase(-5, 9)]
        [TestCase(0, 9)]
        [TestCase(5, -9)]
        [TestCase(5, 0)]
        public async Task ModifyStringsSet_BadRequestAsync(int id,  int numberOfStrings)
        {
            //Arrange
            var requestBody = new ModifyStringsSetRequest()
            {
                Id = id,
                Name = "testChangedStringsSetName",
                NumberOfStrings = numberOfStrings
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/StringsSets");
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
        public async Task ModifyStringsSet_NotFoundAsync()
        {
            //Arrange
            var requestBody = new ModifyStringsSetRequest()
            {
                Id = 81,
                Name = "testChangedStringsSetName",
                NumberOfStrings = 9
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/StringsSets");
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

        [Test, Order(4)]
        public async Task ModifyStringsSet_SuccessAsync()
        {
            //Arrange
            var requestBody = new ModifyStringsSetRequest()
            {
                Id = 3,
                Name = "testChangedStringsSetName",
                NumberOfStrings = 9
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/StringsSets");
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

        [Test, Order(5)]
        public async Task RemoveStringsSet_SuccessAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/StringsSets/4");
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
        public async Task RemoveStringsSet_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/StringsSets/4");
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
        public async Task RemoveStringsSet_BadRequestAsync(int id)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/StringsSets/" + id);
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
        public async Task RemoveStringsSet_NotFoundAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/StringsSets/" +82);
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
