using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
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
    internal class GetInstrumentsHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetInstrumentsHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetInstrumentsHandler testHandler;

        private GetInstrumentsRequest testRequest;
        private List<Instrument> testInstruments;
        private List<Core.Models.Instrument> testMappedInstruments;

        public GetInstrumentsHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetInstrumentsHandler>>();

            testHandler = new GetInstrumentsHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testInstruments = new List<Instrument>()
            {
                new Instrument()
                {
                    Id = 1,
                    ScaleLenghtBass = 628,
                    ScaleLenghtTreble = 628,
                    NumberOfStrings = 6,
                    Manufacturer = new Manufacturer(),
                    ManufacturerId = 1,
                    Model = "testModel1",
                    MyInstruments = new List<MyInstrument>()
                },
                new Instrument()
                {
                    Id = 2,
                    ScaleLenghtBass = 632,
                    ScaleLenghtTreble = 632,
                    NumberOfStrings = 7,
                    Manufacturer = new Manufacturer(),
                    ManufacturerId = 2,
                    Model = "testModel2",
                    MyInstruments = new List<MyInstrument>()
                },
            };
            testMappedInstruments = new List<Core.Models.Instrument>()
            {
                new Core.Models.Instrument()
                {
                    Id = 1,
                    ScaleLenghtBass = 628,
                    ScaleLenghtTreble = 628,
                    NumberOfStrings = 6,
                    Manufacturer = "testManufacturer1",
                    Model = "testModel1"
                },
                new Core.Models.Instrument()
                {
                    Id = 2,
                    ScaleLenghtBass = 632,
                    ScaleLenghtTreble = 632,
                    NumberOfStrings = 7,
                    Manufacturer = "testManufacturer2",
                    Model = "testModel2"
                }
            };
            testRequest = new GetInstrumentsRequest();
        }

        [Test]
        public void GetInstrumentsHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.Instrument>>((int)HttpStatusCode.OK, testMappedInstruments);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentsQuery>())).Returns(Task.FromResult(testInstruments));
            mockedMapper.Setup(x => x.Map<List<Core.Models.Instrument>>(It.IsAny<List<Instrument>>())).Returns(testMappedInstruments);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetInstrumentsHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.Instrument>>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting list of Instrument items");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentsQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
