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
    internal class GetStringsSetsWithCorrepondingTensionHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetStringsSetsWithCorrepondingTensionHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetStringsSetsWithCorrepondingTensionHandler testHandler;

        private List<Core.Models.StringsSet> testResultStringsSets;
        private MyInstrument testMyInstrument;
        private Tuning testResultTuning;
        private List<StringsSet> testAllStringsSets;
        private GetStringsSetsWithCorrepondingTensionRequest testRequest;

        public GetStringsSetsWithCorrepondingTensionHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetStringsSetsWithCorrepondingTensionHandler>>();

            testHandler = new GetStringsSetsWithCorrepondingTensionHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testResultStringsSets = new List<Core.Models.StringsSet>()
            {
                new Core.Models.StringsSet()
                {
                    StringsInSet = new List<Core.Models.StringInSet>()
                    {
                        new Core.Models.StringInSet()
                        {
                            Size = 1,
                            SpecificWeight = 1.1,
                            StringId = 1,
                            StringType = Core.Enums.StringType.PlainNikled,
                            Id = 1,
                            Manufacturer = "testManufacturer1",
                            Position = 1
                        },
                        new Core.Models.StringInSet()
                        {
                            Size = 2,
                            SpecificWeight = 1.1,
                            StringId = 2,
                            StringType = Core.Enums.StringType.PlainNikled,
                            Id = 2,
                            Manufacturer = "testManufacturer1",
                            Position = 2
                        }
                    },
                    NumberOfStrings = 2,
                    Id = 1,
                    Name = "testSet1"
                },
                new Core.Models.StringsSet()
                {
                    StringsInSet = new List<Core.Models.StringInSet>()
                    {
                        new Core.Models.StringInSet()
                        {
                            Size = 1,
                            SpecificWeight = 1.1,
                            StringId = 1,
                            StringType = Core.Enums.StringType.PlainNikled,
                            Id = 3,
                            Manufacturer = "testManufacturer1",
                            Position = 1
                        },
                        new Core.Models.StringInSet()
                        {
                            Size = 2,
                            SpecificWeight = 1.1,
                            StringId = 2,
                            StringType = Core.Enums.StringType.PlainNikled,
                            Id = 4,
                            Manufacturer = "testManufacturer1",
                            Position = 2
                        }
                    },
                    NumberOfStrings = 2,
                    Id = 2,
                    Name = "testSet2"
                }
            };
            testMyInstrument = new MyInstrument()
            {
                Id = 1,
                InstalledStrings = new List<InstalledString>()
                {
                    new InstalledString()
                    {
                        String = new String()
                        {
                            Size = 1,
                            SpecificWeight = 1.1,
                            StringType = Core.Enums.StringType.PlainNikled,
                            Id = 3,
                            ManufacturerId = 1,
                            NumberOfDaysGood = 1,
                        },
                        StringId = 3,
                        Id = 1,
                        MyInstrumentId = 1,
                        MyInstrument = testMyInstrument,
                        Position = 1,
                        ToneId = 1,
                        Tone = new Tone()
                        {
                            Id = 1,
                            Frequency = 1,
                            Name = "testTone1",
                            WaveLenght = 1
                        }
                    },
                    new InstalledString()
                    {
                        String = new String()
                        {
                            Size = 1,
                            SpecificWeight = 1.1,
                            StringType = Core.Enums.StringType.PlainNikled,
                            Id = 4,
                            ManufacturerId = 1,
                            NumberOfDaysGood = 1,
                        },
                        StringId = 3,
                        Id = 1,
                        MyInstrumentId = 1,
                        MyInstrument = testMyInstrument,
                        Position = 1,
                        ToneId = 1,
                        Tone = new Tone()
                        {
                            Id = 2,
                            Frequency = 2,
                            Name = "testTone2",
                            WaveLenght = 2
                        }
                    }
                },
                LastStringChange = new System.DateTime(2021, 11, 21),
                NextStringChange = new System.DateTime(2022, 2, 15),
                LastDeepCleaning = new System.DateTime(2021, 11, 21),
                NextDeepCleaning = new System.DateTime(2022, 2, 15),
                GuitarPlace = Core.Enums.WhereGuitarKept.SoftCase,
                HoursPlayedWeekly = 11,
                Instrument = new Instrument()
                {
                    ScaleLenghtBass = 1,
                    ScaleLenghtTreble = 1,
                    NumberOfStrings = 2,
                    Id =1,
                    Manufacturer = new Manufacturer(),
                    ManufacturerId = 1,
                    Model = "testModel",
                    MyInstruments = new List<MyInstrument>()
                },
                InstrumentId = 1,
                NeededLuthierVisit = true,
                OwnName = "testName",
                User = new User()
                {
                    PlayStyle = Core.Enums.PlayStyle.Hard,
                    AccountType = Core.Enums.AccountType.Admin,
                    DailyMaintanance = Core.Enums.GuitarDailyMaintanance.PlayAsIs,
                    Email = "testMail",
                    Id = 1,
                    MyInstruments = new List<MyInstrument>() { testMyInstrument },
                    Password = "testPass1",
                    Username = "testUsername1"
                },
                UserId = 1,
            };
            testResultTuning = new Tuning()
            {
                NumberOfStrings = 2,
                Id = 1,
                Name = "testTuning",
                TonesInTuning = new List<ToneInTuning>()
                {
                    new ToneInTuning()
                    {
                        Id = 1,
                        Position = 1,
                        Tone = new Tone()
                        {
                            Id = 1,
                            Frequency = 0.1,
                            Name = "testName1",
                            WaveLenght = 0.1
                        },
                        ToneId = 1,
                        TuningId = 1
                    },
                    new ToneInTuning()
                    {
                        Id = 2,
                        Position = 2,
                        Tone = new Tone()
                        {
                            Id = 2,
                            Frequency = 0.2,
                            Name = "testName2",
                            WaveLenght = 0.2
                        },
                        ToneId = 2,
                        TuningId = 1
                    }
                }
            };
            testAllStringsSets = new List<StringsSet>()
            {
                new StringsSet()
                {
                    StringsInSet = new List<StringInSet>()
                    {
                        new StringInSet()
                        {
                            StringId = 1,
                            Id = 1,
                            Position = 1,
                            String = new String()
                            {
                                Size = 1,
                                SpecificWeight = 1,
                                StringType = Core.Enums.StringType.PlainNikled,
                                ManufacturerId = 1,
                                Id = 1,
                                NumberOfDaysGood = 1
                            }
                        },
                        new StringInSet()
                        {
                            StringId = 2,
                            Id = 2,
                            Position = 1,
                            String = new String()
                            {
                                Size = 1,
                                SpecificWeight = 1,
                                StringType = Core.Enums.StringType.PlainNikled,
                                ManufacturerId = 1,
                                Id = 2,
                                NumberOfDaysGood = 1
                            }
                        }
                    },
                    NumberOfStrings = 2,
                    Id = 1,
                    Name = "testSet1"
                },
                new StringsSet()
                {
                    StringsInSet = new List<StringInSet>()
                    {
                        new StringInSet()
                        {
                            StringId = 1,
                            Id = 1,
                            Position = 1,
                            String = new String()
                            {
                                Size = 1,
                                SpecificWeight = 1,
                                StringType = Core.Enums.StringType.PlainNikled,
                                ManufacturerId = 1,
                                Id = 1,
                                NumberOfDaysGood = 1
                            }
                        },
                        new StringInSet()
                        {
                            StringId = 2,
                            Id = 2,
                            Position = 1,
                            String = new String()
                            {
                                Size = 1,
                                SpecificWeight = 1,
                                StringType = Core.Enums.StringType.PlainNikled,
                                ManufacturerId = 1,
                                Id = 2,
                                NumberOfDaysGood = 1
                            }
                        }
                    },
                    NumberOfStrings = 2,
                    Id = 2,
                    Name = "testSet2"
                },
            };
            testRequest = new GetStringsSetsWithCorrepondingTensionRequest()
            {
                StringType = Core.Enums.StringType.PlainNikled,
                MyInstrumentId = 1,
                ResultTuningId = 1,
                UserId = 1,
                AccountType = Core.Enums.AccountType.Admin
            };
        }

        [Test]
        public void GetStringsSetsWithCorrepondingTensionHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.StringsSet>>((int)HttpStatusCode.OK, testResultStringsSets);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Returns(Task.FromResult(testMyInstrument));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningQuery>())).Returns(Task.FromResult(testResultTuning));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetsQuery>())).Returns(Task.FromResult(testAllStringsSets));
            mockedMapper.Setup(x => x.Map<List<Core.Models.StringsSet>>(It.IsAny<IEnumerable<StringsSet>>())).Returns(testResultStringsSets);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringsSetsWithCorrepondingTensionHandler_ShouldAllStingsSetsBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.StringsSet>>((int)HttpStatusCode.BadRequest,
                null, "StringsSets list is empty");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Returns(Task.FromResult(testMyInstrument));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningQuery>())).Returns(Task.FromResult(testResultTuning));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetsQuery>())).Returns(Task.FromResult((List<StringsSet>)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }


        [Test]
        public void GetStringsSetsWithCorrepondingTensionHandler_ShouldTuningBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.StringsSet>>((int)HttpStatusCode.BadRequest,
                null, "Tuning of given Id: " + testRequest.ResultTuningId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Returns(Task.FromResult(testMyInstrument));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningQuery>())).Returns(Task.FromResult((Tuning)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringsSetsWithCorrepondingTensionHandler_ShouldMyInstrumentBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.StringsSet>>((int)HttpStatusCode.BadRequest,
                null, "MyInstrument of given Id: " + testRequest.ResultTuningId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Returns(Task.FromResult((MyInstrument)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringsSetsWithCorrepondingTensionHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.StringsSet>>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during calculating List of StringsSets with corresponding Tensions");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Returns(Task.FromResult(testMyInstrument));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningQuery>())).Returns(Task.FromResult(testResultTuning));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetsQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
