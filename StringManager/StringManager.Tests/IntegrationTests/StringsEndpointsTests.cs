using Newtonsoft.Json;
using NUnit.Framework;
using StringManager.Tests.IntegrationTests.Setup;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StringManager.Tests.IntegrationTests
{
    internal class StringsEndpointsTests : EndpointTestBase
    {
        [Test]
        public async Task GetString_ReturnsValueAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Strings");

            //Act
            var response = await client.SendAsync(requestMessage);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = await response.Content.ReadAsStringAsync();
            var deserialisedData = JsonConvert.DeserializeObject<Core.Models.ModelActionResult<List<Core.Models.String>>>(data);
            Assert.IsNull(deserialisedData.result.Error);
            Assert.AreEqual(1, deserialisedData.result.Data.Count);
        }
    }
}
