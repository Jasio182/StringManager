using AutoMapper;
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
    internal class AddToneHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<ILogger<AddToneHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private AddToneHandler testHandler;

        private AddToneRequest testRequest;
        private Tone testTone;
        private Tone testAddedTone;
        private Core.Models.Tone testMappedTone;

        public AddToneHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<AddToneHandler>>();

            testHandler = new AddToneHandler(mockedMapper.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new AddToneRequest()
            {
                Name = "testName",
                Frequency = 0.1,
                UserId = 1,
                WaveLenght = 0.1
            };
            testTone = new Tone()
            {
                Name = "testToneName",
                InstalledStrings = new List<InstalledString>(),
                Frequency = 0.1,
                TonesInTuning = new List<ToneInTuning>(),
                WaveLenght = 0.1
            };
            testAddedTone = new Tone()
            {
                Id = 1,
                Name = "testToneName",
                InstalledStrings = new List<InstalledString>(),
                Frequency = 0.1,
                TonesInTuning = new List<ToneInTuning>(),
                WaveLenght = 0.1
            };
            testMappedTone = new Core.Models.Tone()
            {
                Id = 1,
                Name = "testToneName",
                Frequency = 0.1,
                WaveLenght = 0.1
            };
        }

        [Test]
        public void AddToneHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.OK, testMappedTone);
            mockedMapper.Setup(x => x.Map<Tone>(It.IsAny<AddToneRequest>()))
                .Returns(testTone);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddToneCommand>())).Returns(Task.FromResult(testAddedTone));
            mockedMapper.Setup(x => x.Map<Core.Models.Tone>(It.IsAny<Tone>())).Returns(testMappedTone);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddToneHandler_ShouldNotHaveBeenUnauthorised()
        {
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to add a new Tone");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddToneHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing adding new Tone item");
            mockedMapper.Setup(x => x.Map<Tone>(It.IsAny<AddToneRequest>()))
            .Returns(testTone);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddToneCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
