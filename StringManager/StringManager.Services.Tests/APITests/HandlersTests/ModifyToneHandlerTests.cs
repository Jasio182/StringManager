using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Handlers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class ModifyToneHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<ModifyToneHandler>> mockedLogger;

        private ModifyToneHandler testHandler;

        private ModifyToneRequest testRequest;
        private Tone testTone;

        public ModifyToneHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedLogger = new Mock<ILogger<ModifyToneHandler>>();

            testHandler = new ModifyToneHandler(mockedQueryExecutor.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testTone = new Tone()
            {
                Name = "testToneName",
                InstalledStrings = new List<InstalledString>(),
                Frequency = 0.1,
                TonesInTuning = new List<ToneInTuning>(),
                WaveLenght = 0.1
            };
            testRequest = new ModifyToneRequest()
            {
                Id = 1,
                Frequency = 6
            };
        }

        [Test]
        public void ModifyToneHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.NoContent, null);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyToneCommand>()));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyToneHandler_ShouldToneBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.NotFound,
                null, "Tone of given Id: " + testRequest.Id + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult((Tone)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyToneHandler_ShouldNotHaveBeenUnauthorised()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to modify a Tone");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyToneHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing modyfication of a Tone");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyToneCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
