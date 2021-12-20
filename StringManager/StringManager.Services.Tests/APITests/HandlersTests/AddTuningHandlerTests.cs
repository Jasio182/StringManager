using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Handlers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class AddTuningHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<ILogger<AddTuningHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private AddTuningHandler testHandler;

        private AddTuningRequest testRequest;
        private Tuning testTuning;
        private Tuning testAddedTuning;
        private Core.Models.Tuning testMappedTuning;

        public AddTuningHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<AddTuningHandler>>();

            testHandler = new AddTuningHandler(mockedMapper.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new AddTuningRequest()
            {
                Name = "testName",
                NumberOfStrings = 7,
                UserId = 1
            };
            testTuning = new Tuning()
            {
                Name = "testTuningName",
                NumberOfStrings = 7,
                TonesInTuning = new List<ToneInTuning>()
            };
            testAddedTuning = new Tuning()
            {
                Id = 1,
                Name = "testTuningName",
                NumberOfStrings = 7,
                TonesInTuning = new List<ToneInTuning>()
            };
            testMappedTuning = new Core.Models.Tuning()
            {
                Id = 1,
                Name = "testTuningName",
                NumberOfStrings = 7,
                TonesInTuning = new List<Core.Models.ToneInTuning>()
            };
        }

        [Test]
        public void AddTuningHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tuning>((int)HttpStatusCode.OK, testMappedTuning);
            mockedMapper.Setup(x => x.Map<Tuning>(It.IsAny<AddTuningRequest>()))
                .Returns(testTuning);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddTuningCommand>())).Returns(Task.FromResult(testAddedTuning));
            mockedMapper.Setup(x => x.Map<Core.Models.Tuning>(It.IsAny<Tuning>())).Returns(testMappedTuning);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddTuningHandler_ShouldNotHaveBeenUnauthorised()
        {
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tuning>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to add a new Tuning");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddTuningHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tuning>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing adding new Tuning item");
            mockedMapper.Setup(x => x.Map<Tuning>(It.IsAny<AddTuningRequest>()))
            .Returns(testTuning);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddTuningCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
