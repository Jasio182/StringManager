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
    public class AddInstrumentHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<AddInstrumentHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private AddInstrumentHandler testHandler;

        private Manufacturer testManufacturer;
        private Instrument testInstrument;
        private Instrument testAddedInstrument;
        private Core.Models.Instrument testMappedInstrument;
        private AddInstrumentRequest testRequest;

        public AddInstrumentHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<AddInstrumentHandler>>();

            testHandler = new AddInstrumentHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testManufacturer = new Manufacturer()
            {
                Id = 1,
                Instruments = new List<Instrument>(),
                Name = "TestManufacturer",
                Strings = new List<String>()
            };
            testInstrument = new Instrument()
            {
                Manufacturer = testManufacturer,
                Model = "testInstrument",
                MyInstruments = new List<MyInstrument>(),
                NumberOfStrings = 6,
                ScaleLenghtBass = 628,
                ScaleLenghtTreble = 628
            };
            testAddedInstrument = new Instrument()
            {
                Id = 1,
                Manufacturer = testManufacturer,
                Model = "testModel",
                MyInstruments = new List<MyInstrument>(),
                NumberOfStrings = 6,
                ScaleLenghtBass = 628,
                ScaleLenghtTreble = 628
            };
            testMappedInstrument = new Core.Models.Instrument()
            {
                Id = 1,
                Manufacturer = "testManufacturer",
                Model = "testModel",
                NumberOfStrings = 6,
                ScaleLenghtBass = 628,
                ScaleLenghtTreble = 628
            };
            testRequest = new AddInstrumentRequest()
            {
                AccountType = Core.Enums.AccountType.Admin,
                ManufacturerId = 1,
                Model = "testModel",
                NumberOfStrings = 6,
                ScaleLenghtBass = 628,
                ScaleLenghtTreble = 628,
                UserId = 1
            };
        }

        [Test]
        public void AddInstrumentHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Instrument>((int)HttpStatusCode.OK, testMappedInstrument);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetManufacturerQuery>())).Returns(Task.FromResult(testManufacturer));
            mockedMapper.Setup(x => x.Map<Instrument>(It.IsAny<System.Tuple<AddInstrumentRequest, Manufacturer>>()))
                .Returns(testInstrument);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddInstrumentCommand>())).Returns(Task.FromResult(testAddedInstrument));
            mockedMapper.Setup(x => x.Map<Core.Models.Instrument>(It.IsAny<Instrument>())).Returns(testMappedInstrument);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddInstrumentHandler_ShouldManufacturerBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Instrument>((int)HttpStatusCode.BadRequest,
                null, "Manufacturer of given Id: " + testRequest.ManufacturerId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetManufacturerQuery>())).Returns(Task.FromResult((Manufacturer)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddInstrumentHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Instrument>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing adding new Instrument item");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetManufacturerQuery>())).Returns(Task.FromResult(testManufacturer));
            mockedMapper.Setup(x => x.Map<Instrument>(It.IsAny<System.Tuple<AddInstrumentRequest, Manufacturer>>()))
                .Returns(testInstrument);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddInstrumentCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
