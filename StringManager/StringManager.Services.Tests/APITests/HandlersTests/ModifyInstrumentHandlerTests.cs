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
    internal class ModifyInstrumentHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<ModifyInstrumentHandler>> mockedLogger;

        private ModifyInstrumentHandler testHandler;

        private ModifyInstrumentRequest testRequest;
        private Instrument testInstrument;
        private Manufacturer testManufacturer;

        public ModifyInstrumentHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedLogger = new Mock<ILogger<ModifyInstrumentHandler>>();

            testHandler = new ModifyInstrumentHandler(mockedQueryExecutor.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new ModifyInstrumentRequest()
            {
                ManufacturerId = 1,
                Id = 1,
            };
            testInstrument = new Instrument()
            {
                Id = 1,
                ScaleLenghtBass = 628,
                ScaleLenghtTreble = 628,
                NumberOfStrings = 6,
                Manufacturer = new Manufacturer(),
                ManufacturerId = 1,
                Model = "testModel1",
                MyInstruments = new List<MyInstrument>()
            };
            testManufacturer = new Manufacturer()
            {
                Id = 1,
                Instruments = new List<Instrument>(),
                Name = "TestManufacturer",
                Strings = new List<String>()
            };
        }

        [Test]
        public void ModifyInstrumentHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Instrument>((int)HttpStatusCode.NoContent, null);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentQuery>())).Returns(Task.FromResult(testInstrument));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetManufacturerQuery>())).Returns(Task.FromResult(testManufacturer));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyInstrumentCommand>()));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyInstrumentHandler_ShouldManufacturerBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.BadRequest,
                null, "Manufacturer of given Id: " + testRequest.ManufacturerId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentQuery>())).Returns(Task.FromResult(testInstrument));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetManufacturerQuery>())).Returns(Task.FromResult((Manufacturer)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyInstrumentHandler_ShouldInstrumentBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.NotFound,
                null, "Instrument of given Id: " + testRequest.ManufacturerId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentQuery>())).Returns(Task.FromResult((Instrument)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyInstrumentHandler_ShouldNotHaveBeenUnauthorised()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to modify an Instrument");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyInstrumentHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<ToneInTuning>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing modyfication of an Instrument");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentQuery>())).Returns(Task.FromResult(testInstrument));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetManufacturerQuery>())).Returns(Task.FromResult(testManufacturer));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyInstrumentCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
