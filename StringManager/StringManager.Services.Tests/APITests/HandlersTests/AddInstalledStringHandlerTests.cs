using AutoMapper;
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
    public class AddInstalledStringHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<AddInstalledStringHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;
        private AddInstalledStringHandler testHandler;
        private String testString;
        private Tone testTone;
        private MyInstrument testMyInstrument;
        private InstalledString testInstalledString;
        private InstalledString testAddedInstalledString;
        private Core.Models.InstalledString testMappedInstalledString;
        private AddInstalledStringRequest testRequest;

        public AddInstalledStringHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<AddInstalledStringHandler>>();
            testHandler = new AddInstalledStringHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedCommandExecutor.Object, mockedLogger.Object);
            testString = new String()
            {
                Id = 1,
                Manufacturer = new Manufacturer(),
                NumberOfDaysGood = 200,
                InstalledStrings = new List<InstalledString>(),
                Size = 9,
                SpecificWeight = 0.00032037193,
                StringType = Core.Enums.StringType.PlainNikled,
                StringSets = new List<StringInSet>()
            };
            testTone = new Tone()
            {
                Id = 1,
                InstalledStrings = new List<InstalledString>(),
                Name = "E4",
                Frequency = 329.63,
                TonesInTuning = new List<ToneInTuning>(),
                WaveLenght = 104.66
            };
            testMyInstrument = new MyInstrument()
            {
                Id = 1,
                InstalledStrings = new List<InstalledString>(),
                Instrument = new Instrument()
                {
                    Id = 1,
                    Manufacturer = new Manufacturer(),
                    Model = "testInstrument",
                    MyInstruments = new List<MyInstrument>(),
                    NumberOfStrings = 6,
                    ScaleLenghtBass = 628,
                    ScaleLenghtTreble = 628
                },
                GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
                NeededLuthierVisit = false,
                LastDeepCleaning = new System.DateTime(2021, 11, 13),
                LastStringChange = new System.DateTime(2021, 11, 13),
                NextDeepCleaning = new System.DateTime(2022, 3, 13),
                NextStringChange = new System.DateTime(2022, 3, 13),
                HoursPlayedWeekly = 12,
                OwnName = "Nazwa",
                User = new User()
                {
                    AccountType = Core.Enums.AccountType.Admin,
                    DailyMaintanance = Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings,
                    Email = "testEmail",
                    Id = 1,
                    MyInstruments = new List<MyInstrument>(),
                    Password = "testPassword",
                    PlayStyle = Core.Enums.PlayStyle.Moderate,
                    Username = "testUsername"
                }
            };
            testInstalledString = new InstalledString()
            {
                MyInstrument = testMyInstrument,
                Position = 1,
                String = testString,
                Tone = testTone
            };
            testAddedInstalledString = new InstalledString()
            {
                Id = 1,
                MyInstrument = testMyInstrument,
                Position = 1,
                String = testString,
                Tone = testTone
            };
            testMappedInstalledString = new Core.Models.InstalledString()
            {
                Id = 1,
                Frequency = 329.63,
                Manufacturer = "testManufacturer",
                Position = 1,
                Size = 9,
                SpecificWeight = 0.00032037193,
                StringId = 1,
                StringType = Core.Enums.StringType.PlainNikled,
                ToneId = 1,
                ToneName = "E4",
                WaveLenght = 104.66
            };
            testRequest = new AddInstalledStringRequest()
            {
                AccountType = Core.Enums.AccountType.Admin,
                MyInstrumentId = 1,
                Position = 1,
                StringId = 1,
                ToneId = 1,
                UserId = 1
            };
        }

        [Test]
        public void AddInstalledStringHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.OK, testMappedInstalledString);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Returns(Task.FromResult(testMyInstrument));
            mockedMapper.Setup(x => x.Map<InstalledString>(It.IsAny<System.Tuple<AddInstalledStringRequest, MyInstrument, String, Tone>>()))
                .Returns(testInstalledString);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddInstalledStringCommand>())).Returns(Task.FromResult(testAddedInstalledString));
            mockedMapper.Setup(x => x.Map<Core.Models.InstalledString>(It.IsAny<InstalledString>())).Returns(testMappedInstalledString);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());
            
            //Assert
            Assert.IsTrue(true);
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddInstalledStringHandler_ShouldMyInstrumentBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.BadRequest, null, "MyInstrument of given Id: " + testRequest.MyInstrumentId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Returns(Task.FromResult((MyInstrument)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.IsTrue(true);
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddInstalledStringHandler_ShouldToneBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.BadRequest, null, "Tone of given Id: " + testRequest.ToneId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult((Tone)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.IsTrue(true);
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddInstalledStringHandler_ShouldStringBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.BadRequest, null, "String of given Id: " + testRequest.StringId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult((String)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.IsTrue(true);
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddInstalledStringHandler_ThrowsException()
        {
            //Arrange
            var expectedException = new System.Exception();
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.InternalServerError, null, "Exception has occured during proccesing adding new InstalledString item");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Returns(Task.FromResult(testMyInstrument));
            mockedMapper.Setup(x => x.Map<InstalledString>(It.IsAny<System.Tuple<AddInstalledStringRequest, MyInstrument, String, Tone>>()))
                .Returns(testInstalledString);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddInstalledStringCommand>())).Throws(expectedException);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.IsTrue(true);
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
