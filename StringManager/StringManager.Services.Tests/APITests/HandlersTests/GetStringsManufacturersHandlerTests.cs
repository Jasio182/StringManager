using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Handlers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class GetStringsManufacturersHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetStringsManufacturersHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetStringsManufacturersHandler testHandler;

        private GetStringsManufacturersRequest testRequest;
        private List<Manufacturer> testStringsManufacturers;
        private List<Core.Models.Manufacturer> testMappedStringsManufacturers;

        public GetStringsManufacturersHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetStringsManufacturersHandler>>();

            testHandler = new GetStringsManufacturersHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testStringsManufacturers = new List<Manufacturer>()
            {
                new Manufacturer()
                {
                    Id = 1,
                    Name = "testManufacturer1",
                    Strings = new List<String>(),
                    Instruments = new List<Instrument>()
                },
                new Manufacturer()
                {
                    Id = 1,
                    Name = "testManufacturer2",
                    Strings = new List<String>(),
                    Instruments = null
                },
            };
            testMappedStringsManufacturers = new List<Core.Models.Manufacturer>()
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
            testRequest = new GetStringsManufacturersRequest();
        }

        [Test]
        public void GetStringsManufacturersHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.Manufacturer>>((int)HttpStatusCode.OK, testMappedStringsManufacturers);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsManufacturersQuery>())).Returns(Task.FromResult(testStringsManufacturers));
            mockedMapper.Setup(x => x.Map<List<Core.Models.Manufacturer>>(It.IsAny<List<Manufacturer>>())).Returns(testMappedStringsManufacturers);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringsManufacturersHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.Manufacturer>>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting list of Manufacturer items that have connected String items to it");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsManufacturersQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
