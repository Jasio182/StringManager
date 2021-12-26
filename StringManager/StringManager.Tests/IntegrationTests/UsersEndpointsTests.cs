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
    internal class UsersEndpointsTests : EndpointTestBase
    {
        public UsersEndpointsTests() : base("UsersEndpointsTestsDatabase")
        {
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        public async Task GetUsers_Unauthorised(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Users");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));

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

        [Test, Order(2)]
        public async Task GetUsers_ReturnsValueAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Users");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestAdminUsername}:{correctTestAdminPassword}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<List<Core.Models.User>>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(11, deserialisedData.result.Data.Count);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword)]
        public async Task GetUser_SelfReturnsValueAsync(string username, string password)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Users/single");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<Core.Models.User>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(username, deserialisedData.result.Data.Username);
        }

        [Test]
        public async Task GetUser_Unauthorised_WrongLoginDataAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Users/single");
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

        [Test, Order(1)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, Core.Enums.AccountType.User, 1)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, null, 2)] 
        [TestCase(incorrectTestUsername, incorrectTestPassword, Core.Enums.AccountType.User, 3)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, null, 4)]
        [TestCase(null, null, Core.Enums.AccountType.User, 5)]
        [TestCase(null, null, null, 6)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, Core.Enums.AccountType.User, 7)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, null, 8)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, Core.Enums.AccountType.Admin, 9)]
        public async Task AddUser_SuccessfulAsync(string username, string password, Core.Enums.AccountType? accountTypeToAdd, int i)
        {
            //Arrange
            var requestBody = new AddUserRequest()
            {
                PlayStyle = Core.Enums.PlayStyle.Light,
                DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                Email = "testAddedEmail" + i,
                Username = "testAddedUsername" + i,
                Password = "testAddedPassword" + i,
                AccountTypeToAdd = accountTypeToAdd
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Users");
            if(username != null)
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
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword)]
        [TestCase(incorrectTestUsername, incorrectTestPassword)]
        public async Task AddUser_UnauthorisedAsync(string username, string password)
        {
            //Arrange
            var requestBody = new AddUserRequest()
            {
                PlayStyle = Core.Enums.PlayStyle.Light,
                DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                Email = "testAddedEmail",
                Username = "testAddedUsername",
                Password = "testAddedPassword",
                AccountTypeToAdd = Core.Enums.AccountType.Admin
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Users");
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
            Assert.IsNotEmpty(data);
        }

        [Test]
        public async Task AddUser_WrongDataAsync()
        {
            //Arrange
            var requestBody = new AddUserRequest()
            {
                PlayStyle = Core.Enums.PlayStyle.Light,
                DailyMaintanance = (Core.Enums.GuitarDailyMaintanance)6,
                Email = "",
                Username = null,
                Password = "testAddedPassword",
                AccountTypeToAdd = Core.Enums.AccountType.Admin
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Users");
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
        [TestCase(correctTestUserUsername, correctTestUserPassword, 1, Core.Enums.AccountType.Admin, false)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 1, null, false)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, null, Core.Enums.AccountType.Admin, false)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, null, Core.Enums.AccountType.User, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 1, null, true)]
        public async Task ModifyUser_UnauthorisedAsync(string username, string password,
            int? id, Core.Enums.AccountType? accountTypeToUpdate, bool isEmpty)
        {
            //Arrange
            var requestBody = new ModifyUserRequest()
            {
                Id = id,
                PlayStyle = Core.Enums.PlayStyle.Light,
                DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                Email = "testChangedEmail",
                Username = "testChangedUsername",
                Password = "testChangedPassword",
                AccountTypeToUpdate = accountTypeToUpdate
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Users");
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

        [Test, Order(3)]
        [TestCase("testAddedUsername1", "testAddedPassword1", 3, null, 1)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 3, Core.Enums.AccountType.Admin, 3)]
        [TestCase("testUpdatedUsername3", "testUpdatedPassword3", 11, Core.Enums.AccountType.User, 4)]
        [TestCase("testUpdatedUsername4", "testUpdatedPassword4", null, null, 5)]
        public async Task ModifyUser_SuccessfulAsync(string username, string password,
            int? id, Core.Enums.AccountType? accountTypeToUpdate, int i)
        {
            //Arrange
            var requestBody = new ModifyUserRequest()
            {
                PlayStyle = Core.Enums.PlayStyle.Hard,
                DailyMaintanance = Core.Enums.GuitarDailyMaintanance.PlayAsIs,
                Email = "testUpdatedEmail" + i,
                Username = "testUpdatedUsername" + i,
                Password = "testUpdatedPassword" + i,
            };
            if(username == correctTestAdminUsername)
            {
                requestBody.AccountTypeToUpdate = accountTypeToUpdate;
            }
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Users");
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
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }
    }
}
