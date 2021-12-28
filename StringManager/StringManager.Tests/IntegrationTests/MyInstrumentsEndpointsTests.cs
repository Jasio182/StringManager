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
    internal class MyInstrumentsEndpointsTests : EndpointTestBase
    {
        public MyInstrumentsEndpointsTests() : base("MyInstrumentsEndpointsTestsDatabase")
        {
        }

        [Test]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 1, true)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, null, true)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 1, false)]
        [TestCase(null, null, 1, true)]
        [TestCase(null, null, null, true)]
        public async Task GetMyInstruments_UnauthorizedAsync(string username, string password, int? requestUserId, bool isEmpty)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUserId == null ? "/MyInstruments" : "/MyInstruments?requestUserId=" + requestUserId);
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
            if(isEmpty)
                Assert.IsEmpty(data);
            else
                Assert.IsNotEmpty(data);
        }

        [Test, Order(1)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 2, 2)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, null, 2)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 2, 2)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 1, 1)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, null, 3)]
        public async Task GetMyInstruments_ReturnsValueAsync(string username, string password, int? requestUserId, int count)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUserId == null ? "/MyInstruments" : "/MyInstruments?requestUserId=" + requestUserId);
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
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<List<Core.Models.MyInstrumentList>>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(count, deserialisedData.result.Data.Count);
        }

        [Test]
        [TestCase(incorrectTestUsername, incorrectTestPassword)]
        [TestCase(null, null)]
        public async Task GetMyInstrument_UnauthorizedAsync(string username, string password)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/MyInstruments/3");
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
        [TestCase(correctTestUserUsername, correctTestUserPassword, 3, true)]
        public async Task GetMyInstrument_SuccessAsync(string username, string password, int id, bool isNull)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/MyInstruments/"+id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<Core.Models.MyInstrument>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            if(isNull)
                Assert.IsNull(deserialisedData.result.Data);
            else
                Assert.IsNotNull(deserialisedData.result.Data);
        }

        [Test]
        [TestCase(incorrectTestUsername, incorrectTestPassword)]
        [TestCase(null, null)]
        public async Task AddMyInstrument_UnauthorizedAsync(string username, string password)
        {
            //Arrange
            var requestBody = new AddMyInstrumentRequest()
            {
                NeededLuthierVisit = false,
                LastStringChange = System.DateTime.Now,
                LastDeepCleaning = System.DateTime.Now,
                GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
                HoursPlayedWeekly = 30,
                InstrumentId = 1,
                OwnName = "Gibol"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/MyInstruments");
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
            Assert.IsEmpty(data);
        }

        [Test]
        [TestCase(15, 0, (Core.Enums.WhereGuitarKept)2, 12, 2)]
        [TestCase(0, 15, (Core.Enums.WhereGuitarKept)2, 12, 2)]
        [TestCase(0, 0, (Core.Enums.WhereGuitarKept)16, 12, 2)]
        [TestCase(0, 0, (Core.Enums.WhereGuitarKept)(-13), 12, 2)]
        [TestCase(0, 0, (Core.Enums.WhereGuitarKept)2, -12, 2)]
        [TestCase(0, 0, (Core.Enums.WhereGuitarKept)2, 12, -2)]
        [TestCase(0, 0, (Core.Enums.WhereGuitarKept)2, 12, 0)]
        public async Task AddMyInstrument_BadRequestAsync(int lastStringChange, int lastDeepCleaning,
            Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly, int instrumentId)
        {
            //Arrange
            var requestBody = new AddMyInstrumentRequest()
            {
                NeededLuthierVisit = false,
                LastStringChange = System.DateTime.Now.AddDays(lastStringChange),
                LastDeepCleaning = System.DateTime.Now.AddDays(lastDeepCleaning),
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                InstrumentId = instrumentId,
                OwnName = "Gibol"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/MyInstruments");
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

        [Test, Order(3)]
        public async Task AddMyInstrument_SuccessAsync()
        {
            //Arrange
            var requestBody = new AddMyInstrumentRequest()
            {
                NeededLuthierVisit = false,
                LastStringChange = System.DateTime.Now,
                LastDeepCleaning = System.DateTime.Now,
                GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
                HoursPlayedWeekly = 30,
                InstrumentId = 1,
                OwnName = "Gibol"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/MyInstruments");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{correctTestUserUsername}:{correctTestUserPassword}")));
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<Core.Models.MyInstrument>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(4, deserialisedData.result.Data.Id);
            Assert.AreEqual(Core.Enums.WhereGuitarKept.Stand, deserialisedData.result.Data.GuitarPlace);
            Assert.AreEqual("Gibol", deserialisedData.result.Data.OwnName);
        }

        [Test]
        [TestCase(incorrectTestUsername, incorrectTestPassword)]
        [TestCase(null, null)]
        public async Task ModifyMyInstrument_UnauthorizedAsync(string username, string password)
        {
            //Arrange
            var requestBody = new ModifyMyInstrumentRequest()
            {
                Id = 3,
                NeededLuthierVisit = false,
                LastStringChange = System.DateTime.Now,
                LastDeepCleaning = System.DateTime.Now,
                GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
                HoursPlayedWeekly = 30,
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/MyInstruments");
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
            Assert.IsEmpty(data);
        }

        [Test]
        [TestCase(0, 0, 0, (Core.Enums.WhereGuitarKept)2, 12)]
        [TestCase(-3, 0, 0, (Core.Enums.WhereGuitarKept)2, 12)]
        [TestCase(3, 15, 0, (Core.Enums.WhereGuitarKept)2, 12)]
        [TestCase(3, 0, 15, (Core.Enums.WhereGuitarKept)2, 12)]
        [TestCase(3, 0, 0, (Core.Enums.WhereGuitarKept)(-15), 12)]
        [TestCase(3, 0, 0, (Core.Enums.WhereGuitarKept)16, 12)]
        [TestCase(3, 0, 0, (Core.Enums.WhereGuitarKept)2, -12)]
        public async Task ModifyMyInstrument_BadRequestAsync(int id, int lastStringChange, int lastDeepCleaning,
            Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly)
        {
            //Arrange
            var requestBody = new ModifyMyInstrumentRequest()
            {
                Id = id,
                NeededLuthierVisit = false,
                LastStringChange = System.DateTime.Now.AddDays(lastStringChange),
                LastDeepCleaning = System.DateTime.Now.AddDays(lastDeepCleaning),
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                OwnName = "Gibol"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/MyInstruments");
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
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 9)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 3)]
        public async Task ModifyMyInstrument_NotFoundAsync(string username, string password, int id)
        {
            //Arrange
            var requestBody = new ModifyMyInstrumentRequest()
            {
                Id = id,
                NeededLuthierVisit = false,
                LastStringChange = System.DateTime.Now,
                LastDeepCleaning = System.DateTime.Now,
                GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
                HoursPlayedWeekly = 30,
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/MyInstruments");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
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
        public async Task ModifyMyInstrument_SuccessAsync()
        {
            //Arrange
            var requestBody = new ModifyMyInstrumentRequest()
            {
                Id = 3,
                NeededLuthierVisit = false,
                LastStringChange = System.DateTime.Now,
                LastDeepCleaning = System.DateTime.Now,
                GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
                HoursPlayedWeekly = 30,
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/MyInstruments");
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
        public async Task RemoveMyInstrument_SuccessAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/MyInstruments/4");
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
        [TestCase(incorrectTestUsername, incorrectTestPassword)]
        [TestCase(null, null)]
        public async Task RemoveMyInstrument_UnauthorizedAsync(string username, string password)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/MyInstruments/4");
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

        [Test]
        [TestCase(0)]
        [TestCase(-15)]
        public async Task RemoveMyInstrument_BadRequestAsync(int id)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/MyInstruments/" + id);
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
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 9)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 3)]
        public async Task RemoveMyInstrument_NotFoundAsync(string username, string password, int id)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/MyInstruments/"+id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));

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
