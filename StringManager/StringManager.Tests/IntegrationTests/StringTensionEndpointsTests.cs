using Newtonsoft.Json;
using NUnit.Framework;
using StringManager.Core.Models;
using StringManager.Tests.IntegrationTests.Setup;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StringManager.Tests.IntegrationTests
{
    internal class StringTensionEndpointsTests : EndpointTestBase
    {
        public StringTensionEndpointsTests() : base("StringTensionEndpointsTestsDatabase")
        {
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 1)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 1)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 1)]
        [TestCase(null, null, 1)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 2)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 2)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 2)]
        [TestCase(null, null, 2)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 3)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 3)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 3)]
        [TestCase(null, null, 3)]
        public async Task GetScaleLenght_SuccessAsync(string username, string password, int instrumentId)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/StringTension/ScaleLenght/" + instrumentId);
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
            var deserialisedData = JsonConvert.DeserializeObject<ModelActionResult<int[]>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            if(instrumentId == 3)
                Assert.AreEqual(7, deserialisedData.result.Data.Length);
            else
                Assert.AreEqual(6, deserialisedData.result.Data.Length);
        }

        [TestCase(correctTestUserUsername, correctTestUserPassword, 4)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 4)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 4)]
        [TestCase(null, null, 4)]
        public async Task GetScaleLenght_BadRequestAsync(string username, string password, int instrumentId)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/StringTension/ScaleLenght/" + instrumentId);
            if (username != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                    System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            }

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, 12, 29, 27)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, 12, 29, 27)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, 12, 29, 27)]
        [TestCase(null, null, 628, 12, 29, 27)]
        public async Task GetStringSizeWithCorrepondingTension_SuccessAsync(string username, string password, int scaleLength, int stringId, int primaryToneId, int resultToneId)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/StringTension/StringsInSize/{scaleLength},{stringId},{primaryToneId},{resultToneId}");
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
            var deserialisedData = JsonConvert.DeserializeObject<ModelActionResult<List<StringsSet>>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.IsNotNull(deserialisedData.result.Data);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, -628, 12, 29, 27)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, -628, 12, 29, 27)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, -628, 12, 29, 27)]
        [TestCase(null, null, -628, 12, 29, 27)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 0, 12, 29, 27)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 0, 12, 29, 27)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 0, 12, 29, 27)]
        [TestCase(null, null, 0, 12, 29, 27)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, -12, 29, 27)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, -12, 29, 27)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, -12, 29, 27)]
        [TestCase(null, null, 628, -12, 29, 27)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, 0, 29, 27)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, 0, 29, 27)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, 0, 29, 27)]
        [TestCase(null, null, 628, 0, 29, 27)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, 12, -29, 27)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, 12, -29, 27)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, 12, -29, 27)]
        [TestCase(null, null, 628, 12, -29, 27)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, 12, 0, 27)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, 12, 0, 27)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, 12, 0, 27)]
        [TestCase(null, null, 628, 12, 0, 27)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, 12, 29, -27)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, 12, 29, -27)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, 12, 29, -27)]
        [TestCase(null, null, 628, 12, 29, -27)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, 12, 29, 0)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, 12, 29, 0)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, 12, 29, 0)]
        [TestCase(null, null, 628, 12, 29, 0)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, 500, 29, 27)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, 12, 500, 27)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, 12, 29, 500)]
        [TestCase(null, null, 628, 500, 29, 27)]
        public async Task GetStringSizeWithCorrepondingTension_BadRequestAsync(string username, string password, int scaleLength, int stringId, int primaryToneId, int resultToneId)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/StringTension/StringsInSize/{scaleLength},{stringId},{primaryToneId},{resultToneId}");
            if (username != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                    System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            }

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 2, Core.Enums.StringType.PlainNikled, 5)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 3, Core.Enums.StringType.PlainNikled, 2)]
        public async Task GetStringsSetsWithCorrepondingTension_SuccessAsync(string username, string password, int myInstrumentId, Core.Enums.StringType stringType, int resultTuningId)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/StringTension/StringsSets/{myInstrumentId},{stringType},{resultTuningId}");
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
            var deserialisedData = JsonConvert.DeserializeObject<ModelActionResult<List<StringsSet>>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.IsNotNull(deserialisedData.result.Data);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, -3, (Core.Enums.StringType)0, 2)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, -3, (Core.Enums.StringType)0, 2)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 0, (Core.Enums.StringType)0, 2)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 0, (Core.Enums.StringType)0, 2)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 3, (Core.Enums.StringType)9, 2)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 3, (Core.Enums.StringType)9, 2)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 3, (Core.Enums.StringType)(-4), 2)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 3, (Core.Enums.StringType)(-4), 2)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 3, (Core.Enums.StringType)0, -2)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 3, (Core.Enums.StringType)0, -2)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 3, (Core.Enums.StringType)0, 0)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 3, (Core.Enums.StringType)0, 0)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 300, (Core.Enums.StringType)0, 2)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 3, (Core.Enums.StringType)0, 300)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 2, (Core.Enums.StringType)0, 2)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 2, (Core.Enums.StringType)0, 2)]
        public async Task GetStringsSetsWithCorrepondingTension_BadRequestAsync(string username, string password, int myInstrumentId, Core.Enums.StringType stringType, int resultTuningId)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/StringTension/StringsSets/{myInstrumentId},{stringType},{resultTuningId}");
            if (username != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                    System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            }

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }

        [Test]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 3, (Core.Enums.StringType)0, 2)]
        [TestCase(null, null, 3, (Core.Enums.StringType)0, 2)]
        public async Task GetStringsSetsWithCorrepondingTension_UnauthorizedAsync(string username, string password, int myInstrumentId, Core.Enums.StringType stringType, int resultTuningId)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/StringTension/StringsSets/{myInstrumentId},{stringType},{resultTuningId}");
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
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, 12, 29)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, 12, 29)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, 12, 29)]
        [TestCase(null, null, 628, 12, 29)]
        public async Task GetStringTension_SuccessAsync(string username, string password, int scaleLenght, int stringId, int toneId)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/StringTension/StringTension/{stringId},{toneId},{scaleLenght}");
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
            var deserialisedData = JsonConvert.DeserializeObject<ModelActionResult<double?>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.IsNotNull(deserialisedData.result.Data);
        }

        [Test]
        [TestCase(correctTestUserUsername, correctTestUserPassword, -628, 12, 29)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, -628, 12, 29)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, -628, 12, 29)]
        [TestCase(null, null, -628, 12, 29)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 0, 12, 29)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 0, 12, 29)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 0, 12, 29)]
        [TestCase(null, null, 0, 12, 29)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, -12, 29)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, -12, 29)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, -12, 29)]
        [TestCase(null, null, 628, -12, 29)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, 0, 29)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, 0, 29)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, 0, 29)]
        [TestCase(null, null, 628, 0, 29)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, 12, -29)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, 12, -29)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, 12, -29)]
        [TestCase(null, null, 628, 12, -29)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, 12, 0)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, 12, 0)]
        [TestCase(incorrectTestUsername, incorrectTestPassword, 628, 12, 0)]
        [TestCase(null, null, 628, 12, 0)]
        [TestCase(correctTestUserUsername, correctTestUserPassword, 628, 500, 29)]
        [TestCase(correctTestAdminUsername, correctTestAdminPassword, 628, 12, 500)]
        [TestCase(null, null, 628, 500, 500)]
        public async Task GetStringTension_BadRequestAsync(string username, string password, int scaleLenght, int stringId, int toneId)
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/StringTension/StringTension/{stringId},{toneId},{scaleLenght}");
            if (username != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                    System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));
            }

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(data);
        }
    }
}
