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
    internal class GetMyInstrumentHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetMyInstrumentHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetMyInstrumentHandler testHandler;

        private GetMyInstrumentRequest testRequest;
        private MyInstrument testMyInstrument;
        private Core.Models.MyInstrument testMappedMyInstrument;

        public GetMyInstrumentHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetMyInstrumentHandler>>();

            testHandler = new GetMyInstrumentHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testMyInstrument = new MyInstrument()
            {
                Id = 1,
                InstalledStrings = new List<InstalledString>(),
                LastStringChange = new System.DateTime(2021, 11, 21),
                NextStringChange = new System.DateTime(2022, 2, 15),
                LastDeepCleaning = new System.DateTime(2021, 11, 21),
                NextDeepCleaning = new System.DateTime(2022, 2, 15),
                GuitarPlace = Core.Enums.WhereGuitarKept.SoftCase,
                HoursPlayedWeekly = 11,
                Instrument = new Instrument(),
                InstrumentId = 1,
                NeededLuthierVisit = true,
                OwnName = "testName",
                User = new User(),
                UserId = 1,
            };
            testMappedMyInstrument = new Core.Models.MyInstrument()
            {
                Id = 1,
                LastStringChange = new System.DateTime(2021, 11, 21),
                NextStringChange = new System.DateTime(2022, 2, 15),
                LastDeepCleaning = new System.DateTime(2021, 11, 21),
                NextDeepCleaning = new System.DateTime(2022, 2, 15),
                GuitarPlace = Core.Enums.WhereGuitarKept.SoftCase,
                HoursPlayedWeekly = 11,
                NeededLuthierVisit = true,
                OwnName = "testName",
                InstalledStrings = new List<Core.Models.InstalledString>(),
                ScaleLenghtBass = 628,
                ScaleLenghtTreble = 628,
                NumberOfStrings = 6,
                Manufacturer = "testManufacturer",
                Model = "testModel"
            };
            testRequest = new GetMyInstrumentRequest()
            {
                AccountType = Core.Enums.AccountType.Admin,
                Id = 1,
                UserId = 1
            };
        }

        [Test]
        public void GetMyInstrumentHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.OK, testMappedMyInstrument);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Returns(Task.FromResult(testMyInstrument));
            mockedMapper.Setup(x => x.Map<Core.Models.MyInstrument>(It.IsAny<MyInstrument>())).Returns(testMappedMyInstrument);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetMyInstrumentHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting MyInstrument item");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
