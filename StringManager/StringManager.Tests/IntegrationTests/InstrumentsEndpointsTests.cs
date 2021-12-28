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
    internal class InstrumentsEndpointsTests : EndpointTestBase
    {
        public InstrumentsEndpointsTests() : base("InstrumentsEndpointsTestsDatabase")
        {
        }

        [Test]
        [TestCase(incorrectTestUsername, incorrectTestPassword)]
        [TestCase(null, null)]
        public async Task GetInstruments_UnauthorizedAsync(string username, string password)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Instruments");
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
        [TestCase(correctTestUserUsername, correctTestUserPassword)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword)]
        public async Task GetInstruments_ReturnsValueAsync(string username, string password)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Instruments");
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
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<List<Core.Models.Instrument>>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(3, deserialisedData.result.Data.Count);
        }

        [Test]
        [TestCase(incorrectTestUsername, incorrectTestPassword)]
        [TestCase(null, null)]
        public async Task AddInstrument_UnauthorizedAsync(string username, string password)
        {
            //Arrange
            var requestBody = new AddInstrumentRequest()
            {
                ManufacturerId = 1,
                ScaleLenghtBass = 648,
                ScaleLenghtTreble = 648,
                NumberOfStrings = 6,
                Model = "Sabre"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Instruments");
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
        [TestCase(-1, 648, 648, 6, "Sabre")]
        [TestCase(0, 648, 648, 6, "Sabre")]
        [TestCase(1, -648, 648, 6, "Sabre")]
        [TestCase(1, 0, 648, 6, "Sabre")]
        [TestCase(1, 648, -648, 6, "Sabre")]
        [TestCase(1, 648, 0, 6, "Sabre")]
        [TestCase(1, 648, 648, -6, "Sabre")]
        [TestCase(1, 648, 648, 0, "Sabre")]
        [TestCase(1, 648, 648, 6, "")]
        [TestCase(1, 648, 648, 6, null)]
        public async Task AddInstrument_BadRequestAsync(int manufacturerId, int scaleLenghtBass, int scaleLenghtTreble, int numberOfStrings, string model)
        {
            //Arrange
            var requestBody = new AddInstrumentRequest()
            {
                ManufacturerId = manufacturerId,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                NumberOfStrings = numberOfStrings,
                Model = model
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Instruments");
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

        [Test, Order(2)]
        public async Task AddInstrument_SuccessAsync()
        {
            //Arrange
            var requestBody = new AddInstrumentRequest()
            {
                ManufacturerId = 1,
                ScaleLenghtBass = 648,
                ScaleLenghtTreble = 648,
                NumberOfStrings = 6,
                Model = "Sabre"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Instruments");
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
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<Core.Models.Instrument>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(648, deserialisedData.result.Data.ScaleLenghtBass);
            Assert.AreEqual("Sabre", deserialisedData.result.Data.Model);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, false)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, true)]
        [TestCase(null, null, true)]
        public async Task ModifyInstrument_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestBody = new ModifyInstrumentRequest()
            {
                Id = 4,
                ManufacturerId = 1,
                ScaleLenghtBass = 686,
                ScaleLenghtTreble = 648,
                NumberOfStrings = 8,
                Model = "Majesty 8"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Instruments");
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
        [TestCase(-4, 1, 648, 648, 6)]
        [TestCase(0, 1, 648, 648, 6)]
        [TestCase(2, 20, 648, 648, 6)]
        [TestCase(4, -1, 648, 648, 6)]
        [TestCase(4, 0, 648, 648, 6)]
        [TestCase(4, 1, -648, 648, 6)]
        [TestCase(4, 1, 0, 648, 6)]
        [TestCase(4, 1, 648, -648, 6)]
        [TestCase(4, 1, 648, 0, 6)]
        [TestCase(4, 1, 648, 648, -6)]
        [TestCase(4, 1, 648, 648, 0)]
        public async Task ModifyInstrument_BadRequestAsync(int id, int manufacturerId, int scaleLenghtBass, int scaleLenghtTreble, int numberOfStrings)
        {
            //Arrange
            var requestBody = new ModifyInstrumentRequest()
            {
                Id = id,
                ManufacturerId = manufacturerId,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                NumberOfStrings = numberOfStrings,
                Model = "Axis"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Instruments");
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
        public async Task ModifyInstrument_NotFoundAsync()
        {
            //Arrange
            var requestBody = new ModifyInstrumentRequest()
            {
                Id = 10,
                ManufacturerId = 1,
                ScaleLenghtBass = 648,
                ScaleLenghtTreble = 648,
                NumberOfStrings = 6,
                Model = "Axis"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Instruments");
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
        public async Task ModifyInstrument_SuccessAsync()
        {
            //Arrange
            var requestBody = new ModifyInstrumentRequest()
            {
                Id = 4,
                ManufacturerId = 1,
                ScaleLenghtBass = 686,
                ScaleLenghtTreble = 648,
                NumberOfStrings = 8,
                Model = "Majesty 8"
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/Instruments");
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
        public async Task RemoveInstrument_SuccessAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Instruments/4");
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
        public async Task RemoveInstrument_UnauthorizedAsync(string username, string password, bool isEmpty)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Instruments/4");
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
        public async Task RemoveInstrument_BadRequestAsync(int id)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Instruments/"+id);
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
        public async Task RemoveInstrument_NotFoundAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Instruments/22");
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
