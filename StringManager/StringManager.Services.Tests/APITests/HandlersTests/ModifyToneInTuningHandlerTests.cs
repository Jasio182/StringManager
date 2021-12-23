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
    internal class ModifyToneInTuningHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<ModifyToneInTuningHandler>> mockedLogger;

        private ModifyToneInTuningHandler testHandler;

        private ModifyToneInTuningRequest testRequest;
        private ToneInTuning testToneInTuning;
        private Tone testTone;
        private Tuning testTuning;

        public ModifyToneInTuningHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedLogger = new Mock<ILogger<ModifyToneInTuningHandler>>();

            testHandler = new ModifyToneInTuningHandler(mockedQueryExecutor.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new ModifyToneInTuningRequest()
            {
                Id = 1,
                ToneId = 1,
                TuningId = 1
            };
            testTone = new Tone()
            {
                Id = 1,
                Name = "testToneName",
                InstalledStrings = new List<InstalledString>(),
                Frequency = 0.1,
                TonesInTuning = new List<ToneInTuning>(),
                WaveLenght = 0.1
            };
            testTuning = new Tuning()
            {
                NumberOfStrings = 6,
                Id = 1,
                Name = "testTuning",
                TonesInTuning = new List<ToneInTuning>()
            };
            testToneInTuning = new ToneInTuning()
            {
                Position = 1,
                Tone = testTone,
                ToneId = 1,
                Tuning = testTuning,
                TuningId = 1
            };
        }

        [Test]
        public void ModifyToneInTuningHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.NoContent, null);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneInTuningQuery>())).Returns(Task.FromResult(testToneInTuning));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningQuery>())).Returns(Task.FromResult(testTuning));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyToneInTuningCommand>()));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyToneInTuningHandler_ShouldTuningBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.BadRequest,
                null, "Tuning of given Id: " + testRequest.TuningId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneInTuningQuery>())).Returns(Task.FromResult(testToneInTuning));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningQuery>())).Returns(Task.FromResult((Tuning)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }


        [Test]
        public void ModifyToneInTuningHandler_ShouldToneBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.BadRequest,
                null, "Tone of given Id: " + testRequest.ToneId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneInTuningQuery>())).Returns(Task.FromResult(testToneInTuning));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult((Tone)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyToneInTuningHandler_ShouldToneInTuningBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.NotFound,
                null, "ToneInTuning of given Id: " + testRequest.Id + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneInTuningQuery>())).Returns(Task.FromResult((ToneInTuning)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyToneInTuningHandler_ShouldNotHaveBeenUnauthorised()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to modify a ToneInTuning");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyToneInTuningHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing modyfication of a ToneInTuning");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneInTuningQuery>())).Returns(Task.FromResult(testToneInTuning));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningQuery>())).Returns(Task.FromResult(testTuning));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyToneInTuningCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
