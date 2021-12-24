using NUnit.Framework;
using StringManager.Tests.IntegrationTests.Setup;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StringManager.Tests.IntegrationTests
{
    internal class InstalledStringsEndpointsTests : EndpointTestBase
    {
        [Test]
        public async Task RemoveInstalledString_UnauthorisedAsync()
        {
            //Arrange
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/InstalledStrings/11");

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
