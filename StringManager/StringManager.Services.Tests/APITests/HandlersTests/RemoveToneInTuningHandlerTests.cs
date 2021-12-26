using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Handlers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class RemoveToneInTuningHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<ILogger<RemoveToneInTuningHandler>> mockedLogger;

        private RemoveToneInTuningHandler testHandler;

        private RemoveToneInTuningRequest testRequest;
        private ToneInTuning testToneInTuning;

        public RemoveToneInTuningHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedLogger = new Mock<ILogger<RemoveToneInTuningHandler>>();

            testHandler = new RemoveToneInTuningHandler(mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new RemoveToneInTuningRequest()
            {
                Id = 1
            };
            testToneInTuning = new ToneInTuning()
            {
                Id = 1,
                Position = 1,
                ToneId = 1,
                TuningId = 1
            };
        }

        [Test]
        public void RemoveToneInTuningHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.NoContent, null);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveToneInTuningCommand>())).Returns(Task.FromResult(testToneInTuning));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveToneInTuningHandler_ShouldNotHaveBeenUnauthorised()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to remove an ToneInTuning");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveToneInTuningHandler_ShouldToneInTuningBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.NotFound,
                null, "ToneInTuning of given Id: " + testRequest.Id + " has not been found");
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveToneInTuningCommand>())).Returns(Task.FromResult((ToneInTuning)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveToneInTuningHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing deletion of a ToneInTuning");
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveToneInTuningCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
