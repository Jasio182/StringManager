using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Handlers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class AddToneInTuningHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<AddToneInTuningHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private AddToneInTuningHandler testHandler;

        private AddToneInTuningRequest testRequest;
        private Tone testTone;
        private Tuning testTuning;
        private ToneInTuning testToneInTuning;
        private ToneInTuning testAddedToneInTuning;
        private Core.Models.ToneInTuning testMappedToneInTuning;

        public AddToneInTuningHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<AddToneInTuningHandler>>();

            testHandler = new AddToneInTuningHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new AddToneInTuningRequest()
            {
                Position = 1,
                ToneId = 1,
                TuningId = 1,
                UserId = 1,
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
            testAddedToneInTuning = new ToneInTuning()
            {
                Id = 1,
                Position = 1,
                Tone = testTone,
                ToneId = 1,
                Tuning = testTuning,
                TuningId = 1
            };
            testMappedToneInTuning = new Core.Models.ToneInTuning()
            {
                Frequency = 0.1,
                Id = 1,
                Position = 1,
                ToneName = "testToneName",
                WaveLenght = 0.1
            };
        }

        [Test]
        public void AddToneInTuningHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.OK, testMappedToneInTuning);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningQuery>())).Returns(Task.FromResult(testTuning));
            mockedMapper.Setup(x => x.Map<ToneInTuning>(It.IsAny<System.Tuple<AddToneInTuningRequest, Tone, Tuning>>()))
                .Returns(testToneInTuning);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddToneInTuningCommand>())).Returns(Task.FromResult(testAddedToneInTuning));
            mockedMapper.Setup(x => x.Map<Core.Models.ToneInTuning>(It.IsAny<ToneInTuning>())).Returns(testMappedToneInTuning);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddToneInTuningHandler_ShouldNotHaveBeenUnauthorized()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to add a new ToneInTuning");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddToneInTuningHandler_ShouldTuningBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.BadRequest,
                null, "Tuning of given Id: " + testRequest.TuningId + " has not been found");
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
        public void AddToneInTuningHandler_ShouldToneBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.BadRequest,
                null, "Tone of given Id: " + testRequest.ToneId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult((Tone)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddToneInTuningHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing adding new ToneInTuning item");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningQuery>())).Returns(Task.FromResult(testTuning));
            mockedMapper.Setup(x => x.Map<ToneInTuning>(It.IsAny<System.Tuple<AddToneInTuningRequest, Tone, Tuning>>()))
                .Returns(testToneInTuning);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddToneInTuningCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
