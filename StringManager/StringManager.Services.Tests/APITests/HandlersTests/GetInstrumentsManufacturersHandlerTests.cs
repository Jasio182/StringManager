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
    internal class GetInstrumentsManufacturersHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetInstrumentsManufacturersHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetInstrumentsManufacturersHandler testHandler;

        private GetInstrumentsManufacturersRequest testRequest;
        private List<Manufacturer> testInstrumentsManufacturers;
        private List<Core.Models.Manufacturer> testMappedInstrumentsManufacturers;

        public GetInstrumentsManufacturersHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetInstrumentsManufacturersHandler>>();

            testHandler = new GetInstrumentsManufacturersHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testInstrumentsManufacturers = new List<Manufacturer>()
            {
                new Manufacturer()
                {
                    Id = 1,
                    Name = "testManufacturer1",
                    Strings = null,
                    Instruments = new List<Instrument>()
                },
                new Manufacturer()
                {
                    Id = 1,
                    Name = "testManufacturer2",
                    Strings = new List<String>(),
                    Instruments = new List<Instrument>()
                },
            };
            testMappedInstrumentsManufacturers = new List<Core.Models.Manufacturer>()
            {
                new Core.Models.Manufacturer()
                {
                    Id = 1,
                    Name = "testManufacturer1"
                },
                new Core.Models.Manufacturer()
                {
                    Id = 1,
                    Name = "testManufacturer2"
                }
            };
            testRequest = new GetInstrumentsManufacturersRequest();
        }

        [Test]
        public void GetInstrumentsManufacturersHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.Manufacturer>>((int)HttpStatusCode.OK, testMappedInstrumentsManufacturers);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentsManufacturersQuery>())).Returns(Task.FromResult(testInstrumentsManufacturers));
            mockedMapper.Setup(x => x.Map<List<Core.Models.Manufacturer>>(It.IsAny<List<Manufacturer>>())).Returns(testMappedInstrumentsManufacturers);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetInstrumentsManufacturersHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.Manufacturer>>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting list of Manufacturer items that have connected Instrument items to it");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentsManufacturersQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
