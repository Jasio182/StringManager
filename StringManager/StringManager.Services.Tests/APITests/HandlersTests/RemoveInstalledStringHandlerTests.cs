using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Handlers;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class RemoveInstalledStringHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<ILogger<RemoveInstalledStringHandler>> mockedLogger;

        private RemoveInstalledStringHandler testHandler;

        private RemoveInstalledStringRequest testRequest;
        private InstalledString testInstalledString;

        public RemoveInstalledStringHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedLogger = new Mock<ILogger<RemoveInstalledStringHandler>>();

            testHandler = new RemoveInstalledStringHandler(mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new RemoveInstalledStringRequest()
            {
                Id = 1
            };
            testInstalledString = new InstalledString()
            {
                Id = 1,
                StringId = 1,
                MyInstrumentId = 1,
                Position = 1,
                ToneId = 1,
            };
        }

        [Test]
        public void RemoveInstalledStringHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.NoContent, null);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveInstalledStringCommand>())).Returns(Task.FromResult(testInstalledString));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveInstalledStringHandler_ShouldNotHaveBeenUnauthorized()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to remove an InstalledString");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveInstalledStringHandler_ShouldInstalledStringBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.NotFound,
                null, "Instrument of given Id: " + testRequest.Id + " has not been found");
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveInstalledStringCommand>())).Returns(Task.FromResult((InstalledString)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveInstalledStringHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing deletion of an InstalledString");
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveInstalledStringCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
