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
    internal class GetMyInstrumentsHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetMyInstrumentsHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetMyInstrumentsHandler testHandler;

        private GetMyInstrumentsRequest testRequest;
        private List<MyInstrument> testMyInstruments;
        private List<Core.Models.MyInstrumentList> testMappedMyInstruments;

        public GetMyInstrumentsHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetMyInstrumentsHandler>>();

            testHandler = new GetMyInstrumentsHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testMyInstruments = new List<MyInstrument>()
            {
                new MyInstrument()
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
                    OwnName = "testName1",
                    User = new User(),
                    UserId = 1,
                },
                new MyInstrument()
                {
                    Id = 1,
                    InstalledStrings = new List<InstalledString>(),
                    LastStringChange = new System.DateTime(2021, 11, 21),
                    NextStringChange = new System.DateTime(2022, 2, 15),
                    LastDeepCleaning = new System.DateTime(2021, 11, 21),
                    NextDeepCleaning = new System.DateTime(2022, 2, 15),
                    GuitarPlace = Core.Enums.WhereGuitarKept.SoftCase,
                    HoursPlayedWeekly = 15,
                    Instrument = new Instrument(),
                    InstrumentId = 1,
                    NeededLuthierVisit = true,
                    OwnName = "testName2",
                    User = new User(),
                    UserId = 1,
                }
            };
            testMappedMyInstruments = new List<Core.Models.MyInstrumentList>()
            {
                new Core.Models.MyInstrumentList()
                {
                    Id = 1,
                    OwnName = "testName1",
                    ScaleLenghtBass = 628,
                    ScaleLenghtTreble = 628,
                    NumberOfStrings = 6,
                    Manufacturer = "testManufacturer1",
                    Model = "testModel1"
                },
                new Core.Models.MyInstrumentList()
                {
                    Id = 1,
                    OwnName = "testName2",
                    ScaleLenghtBass = 628,
                    ScaleLenghtTreble = 628,
                    NumberOfStrings = 6,
                    Manufacturer = "testManufacturer1",
                    Model = "testModel2",
                }
            };
            testRequest = new GetMyInstrumentsRequest()
            {
                RequestUserId = 1
            };
        }

        [Test]
        public void GetMyInstrumentsHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.MyInstrumentList>>((int)HttpStatusCode.OK, testMappedMyInstruments);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentsQuery>())).Returns(Task.FromResult(testMyInstruments));
            mockedMapper.Setup(x => x.Map<List<Core.Models.MyInstrumentList>>(It.IsAny<List<MyInstrument>>())).Returns(testMappedMyInstruments);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetMyInstrumentsHandler_ShouldNotHaveBeenUnauthorised()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            testRequest.UserId = 2;
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.MyInstrumentList>>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to get all MyInstruments of a user: " + testRequest.RequestUserId);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetMyInstrumentsHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.MyInstrumentList>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting list of MyInstrument items");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentsQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
