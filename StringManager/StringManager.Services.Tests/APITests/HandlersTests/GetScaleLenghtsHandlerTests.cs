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
    internal class GetScaleLenghtsHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetScaleLenghtsHandler>> mockedLogger;

        private GetScaleLenghtsHandler testHandler;

        private GetScaleLenghtsRequest testRequest;
        private int[] testScaleLenghts = { 628, 628, 628, 628, 628, 628 };
        private Instrument testInstrument;


        public GetScaleLenghtsHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedLogger = new Mock<ILogger<GetScaleLenghtsHandler>>();

            testHandler = new GetScaleLenghtsHandler(mockedQueryExecutor.Object, mockedLogger.Object);

            testRequest = new GetScaleLenghtsRequest()
            {
                AccountType = Core.Enums.AccountType.Admin,
                InstrumentId = 1,
                UserId = 1
            };
            testInstrument = new Instrument()
            {
                Id = 1,
                ScaleLenghtBass = 628,
                ScaleLenghtTreble = 628,
                NumberOfStrings = 6,
                Manufacturer = new Manufacturer(),
                ManufacturerId = 1,
                Model = "testModel",
                MyInstruments = new List<MyInstrument>()
            };
        }

        [Test]
        public void GetScaleLenghtsHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<int[]>((int)HttpStatusCode.OK, testScaleLenghts);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentQuery>())).Returns(Task.FromResult(testInstrument));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetScaleLenghtsHandler_ShouldInstrumentBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<int[]>((int)HttpStatusCode.BadRequest,
                null, "Instrument of given Id: " + testRequest.InstrumentId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentQuery>())).Returns(Task.FromResult((Instrument)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetScaleLenghtsHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<int[]>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during calculating strings scale lenghts for Instrument");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
