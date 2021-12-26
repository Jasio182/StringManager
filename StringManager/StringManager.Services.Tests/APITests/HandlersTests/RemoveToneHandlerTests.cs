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
    internal class RemoveToneHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<ILogger<RemoveToneHandler>> mockedLogger;

        private RemoveToneHandler testHandler;

        private RemoveToneRequest testRequest;
        private Tone testTone;

        public RemoveToneHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedLogger = new Mock<ILogger<RemoveToneHandler>>();

            testHandler = new RemoveToneHandler(mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new RemoveToneRequest()
            {
                Id = 1
            };
            testTone = new Tone()
            {
                Id = 1,
                Name = "testTone",
                Frequency = 0.1,
                WaveLenght = 0.1
            };
        }

        [Test]
        public void RemoveToneHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.NoContent, null);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveToneCommand>())).Returns(Task.FromResult(testTone));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveToneHandler_ShouldNotHaveBeenUnauthorised()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to remove an Tone");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveToneHandler_ShouldToneBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.NotFound,
                null, "Tone of given Id: " + testRequest.Id + " has not been found");
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveToneCommand>())).Returns(Task.FromResult((Tone)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveToneHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing deletion of a Tone");
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveToneCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
