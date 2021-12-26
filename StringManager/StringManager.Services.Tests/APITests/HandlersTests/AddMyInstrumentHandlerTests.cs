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
    internal class AddMyInstrumentHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<AddMyInstrumentHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private AddMyInstrumentHandler testHandler;

        private User testUser;
        private Instrument testInstrument;
        private MyInstrument testMyInstrument;
        private MyInstrument testAddedMyInstrument;
        private Core.Models.MyInstrument testMappedMyInstrument;
        private AddMyInstrumentRequest testRequest;

        public AddMyInstrumentHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<AddMyInstrumentHandler>>();

            testHandler = new AddMyInstrumentHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testUser = new User()
            {
                Id = 1,
                DailyMaintanance = Core.Enums.GuitarDailyMaintanance.PlayAsIs,
                PlayStyle = Core.Enums.PlayStyle.Hard,
                AccountType = Core.Enums.AccountType.Admin,
                Email = "testEmail",
                Username = "testUsername",
                Password = "testPassword",
                MyInstruments = new List<MyInstrument>()
            };
            testInstrument = new Instrument()
            {
                Id = 1,
                Manufacturer = new Manufacturer()
                {
                    Id = 1,
                    Name = "testName"
                },
                ManufacturerId = 1,
                Model = "testModel",
                MyInstruments = new List<MyInstrument>(),
                NumberOfStrings = 6,
                ScaleLenghtBass = 628,
                ScaleLenghtTreble = 628
            };
            testMyInstrument = new MyInstrument()
            {
                InstalledStrings = new List<InstalledString>(),
                Instrument = testInstrument,
                GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
                NeededLuthierVisit = false,
                LastDeepCleaning = new System.DateTime(2021, 11, 13),
                LastStringChange = new System.DateTime(2021, 11, 13),
                NextDeepCleaning = new System.DateTime(2022, 3, 13),
                NextStringChange = new System.DateTime(2022, 3, 13),
                HoursPlayedWeekly = 12,
                OwnName = "testOwnName",
                User = testUser
            };
            testAddedMyInstrument = new MyInstrument()
            {
                Id = 1,
                InstalledStrings = new List<InstalledString>(),
                Instrument = testInstrument,
                GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
                NeededLuthierVisit = false,
                LastDeepCleaning = new System.DateTime(2021, 11, 13),
                LastStringChange = new System.DateTime(2021, 11, 13),
                NextDeepCleaning = new System.DateTime(2022, 3, 13),
                NextStringChange = new System.DateTime(2022, 3, 13),
                HoursPlayedWeekly = 12,
                OwnName = "testOwnName",
                User = testUser
            };
            testMappedMyInstrument = new Core.Models.MyInstrument()
            {
                Id = 1,
                LastDeepCleaning = new System.DateTime(2021, 11, 13),
                LastStringChange = new System.DateTime(2021, 11, 13),
                NextDeepCleaning = new System.DateTime(2022, 3, 13),
                NextStringChange = new System.DateTime(2022, 3, 13),
                HoursPlayedWeekly = 12,
                OwnName = "testOwnName",
                GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
                NeededLuthierVisit = false,
                Manufacturer = "testName",
                Model = "testModel",
                InstalledStrings = new List<Core.Models.InstalledString>(),
                NumberOfStrings = 6,
                ScaleLenghtBass = 628,
                ScaleLenghtTreble = 628
            };
            testRequest = new AddMyInstrumentRequest()
            {
                AccountType = Core.Enums.AccountType.Admin,
                UserId = 1,
                GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
                HoursPlayedWeekly = 12,
                OwnName = "testOwnName",
                NeededLuthierVisit = false,
                LastDeepCleaning = new System.DateTime(2021, 11, 13),
                LastStringChange = new System.DateTime(2021, 11, 13),
                InstrumentId = 1
            };
        }

        [Test]
        public void AddMyInstrumentHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.OK, testMappedMyInstrument);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetUserByIdQuery>())).Returns(Task.FromResult(testUser));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentQuery>())).Returns(Task.FromResult(testInstrument));
            mockedMapper.Setup(x => x.Map<MyInstrument>(It.IsAny<System.Tuple<AddMyInstrumentRequest, Instrument, User>>()))
                .Returns(testMyInstrument);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddMyInstrumentCommand>())).Returns(Task.FromResult(testAddedMyInstrument));
            mockedMapper.Setup(x => x.Map<Core.Models.MyInstrument>(It.IsAny<MyInstrument>())).Returns(testMappedMyInstrument);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddMyInstrumentHandler_ShouldInstrumentBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.BadRequest,
                null, "Instrument of given Id: " + testRequest.InstrumentId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetUserByIdQuery>())).Returns(Task.FromResult(testUser));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentQuery>())).Returns(Task.FromResult((Instrument)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddMyInstrumentHandler_ShouldUserBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.BadRequest,
                null, "User of given Id: " + testRequest.UserId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetUserByIdQuery>())).Returns(Task.FromResult((User)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddMyInstrumentHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing adding new MyInstrument item");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetUserByIdQuery>())).Returns(Task.FromResult(testUser));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstrumentQuery>())).Returns(Task.FromResult(testInstrument));
            mockedMapper.Setup(x => x.Map<MyInstrument>(It.IsAny<System.Tuple<AddMyInstrumentRequest, Instrument, User>>()))
                .Returns(testMyInstrument);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddMyInstrumentCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
