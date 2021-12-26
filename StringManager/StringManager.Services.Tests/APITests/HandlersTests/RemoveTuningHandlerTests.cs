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
    internal class RemoveTuningHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<ILogger<RemoveTuningHandler>> mockedLogger;

        private RemoveTuningHandler testHandler;

        private RemoveTuningRequest testRequest;
        private Tuning testTuning;

        public RemoveTuningHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedLogger = new Mock<ILogger<RemoveTuningHandler>>();

            testHandler = new RemoveTuningHandler(mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new RemoveTuningRequest()
            {
                Id = 1
            };
            testTuning = new Tuning()
            {
                Id = 1,
                Name = "testTuning",
                NumberOfStrings = 6
            };
        }

        [Test]
        public void RemoveTuningHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tuning>((int)HttpStatusCode.NoContent, null);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveTuningCommand>())).Returns(Task.FromResult(testTuning));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveTuningHandler_ShouldNotHaveBeenUnauthorised()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tuning>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to remove an Tuning");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveTuningHandler_ShouldTuningBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tuning>((int)HttpStatusCode.NotFound,
                null, "Tuning of given Id: " + testRequest.Id + " has not been found");
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveTuningCommand>())).Returns(Task.FromResult((Tuning)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveTuningHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tuning>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing deletion of a Tuning");
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveTuningCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
