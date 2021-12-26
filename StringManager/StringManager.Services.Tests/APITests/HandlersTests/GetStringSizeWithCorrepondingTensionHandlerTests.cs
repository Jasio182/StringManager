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
    internal class GetStringSizeWithCorrepondingTensionHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetStringSizeWithCorrepondingTensionHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetStringSizeWithCorrepondingTensionHandler testHandler;

        private List<Core.Models.String> testResultStrings;
        private String testPrimaryString;
        private Tone testPrimaryTone;
        private Tone testResultTone;
        private List<String> testAllStrings;
        private GetStringSizeWithCorrepondingTensionRequest testRequest;

        public GetStringSizeWithCorrepondingTensionHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetStringSizeWithCorrepondingTensionHandler>>();

            testHandler = new GetStringSizeWithCorrepondingTensionHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testRequest = new GetStringSizeWithCorrepondingTensionRequest()
            {
                ScaleLength = 123,
                StringId = 1,
                PrimaryToneId = 1,
                ResultToneId = 2
            };
            testPrimaryString = new String()
            {
                Id = 1,
                InstalledStrings = new List<InstalledString>(),
                Size = 9,
                SpecificWeight = 0.1,
                StringsInSets = new List<StringInSet>(),
                StringType = Core.Enums.StringType.PlainBrass,
                Manufacturer = new Manufacturer(),
                ManufacturerId = 1,
                NumberOfDaysGood = 12
            };
            testPrimaryTone = new Tone()
            {
                InstalledStrings = new List<InstalledString>(),
                Frequency = 123.4,
                Id = 2,
                TonesInTuning = new List<ToneInTuning>(),
                Name = "testTone1",
                WaveLenght = 0.1
            };
            testResultTone = new Tone()
            {
                InstalledStrings = new List<InstalledString>(),
                Frequency = 182.4,
                Id = 2,
                TonesInTuning = new List<ToneInTuning>(),
                Name = "testTone2",
                WaveLenght = 0.2
            };
            testAllStrings = new List<String>()
            {
                testPrimaryString,
                new String()
                {
                    Id = 1,
                    InstalledStrings = new List<InstalledString>(),
                    Size = 9,
                    SpecificWeight = 0.1,
                    StringsInSets = new List<StringInSet>(),
                    StringType = Core.Enums.StringType.PlainNikled,
                    Manufacturer = new Manufacturer(),
                    ManufacturerId = 2,
                    NumberOfDaysGood = 12
                },
                new String()
                {
                    Id = 1,
                    InstalledStrings = new List<InstalledString>(),
                    Size = 9,
                    SpecificWeight = 0.1,
                    StringsInSets = new List<StringInSet>(),
                    StringType = Core.Enums.StringType.PlainNylon,
                    Manufacturer = new Manufacturer(),
                    ManufacturerId = 3,
                    NumberOfDaysGood = 12
                }
            };
            testResultStrings = new List<Core.Models.String>()
            {
                new Core.Models.String()
                {
                    Id = 1,
                    Size = 9,
                    SpecificWeight = 0.1,
                    StringType = Core.Enums.StringType.PlainBrass,
                    Manufacturer = "testManufacturer1",
                    NumberOfDaysGood = 12
                },
                new Core.Models.String()
                {
                    Id = 1,
                    Size = 9,
                    SpecificWeight = 0.1,
                    StringType = Core.Enums.StringType.PlainNikled,
                    Manufacturer = "testManufacturer2",
                    NumberOfDaysGood = 12
                },
                new Core.Models.String()
                {
                    Id = 1,
                    Size = 9,
                    SpecificWeight = 0.1,
                    StringType = Core.Enums.StringType.PlainNylon,
                    Manufacturer = "testManufacturer3",
                    NumberOfDaysGood = 12
                },
            };
        }

        [Test]
        public void GetStringSizeWithCorrepondingTensionHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.String>>((int)HttpStatusCode.OK, testResultStrings);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testPrimaryString));
            mockedQueryExecutor.Setup(x=>x.Execute(It.IsAny<GetStringsQuery>())).Returns(Task.FromResult(testAllStrings));
            mockedQueryExecutor.SetupSequence(x=>x.Execute(It.IsAny<GetToneQuery>()))
                .Returns(Task.FromResult(testPrimaryTone))
                .Returns(Task.FromResult(testResultTone));
            mockedMapper.Setup(x => x.Map<List<Core.Models.String>>(It.IsAny<IEnumerable<String>>())).Returns(testResultStrings);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringSizeWithCorrepondingTensionHandler_ShouldResultToneBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.String>>((int)HttpStatusCode.BadRequest,
                null, "Tone of given Id: " + testRequest.ResultToneId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testPrimaryString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsQuery>())).Returns(Task.FromResult(testAllStrings));
            mockedQueryExecutor.SetupSequence(x => x.Execute(It.IsAny<GetToneQuery>()))
                .Returns(Task.FromResult(testPrimaryTone))
                .Returns(Task.FromResult((Tone)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringSizeWithCorrepondingTensionHandler_ShouldPrimaryToneBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.String>>((int)HttpStatusCode.BadRequest,
                null, "Tone of given Id: " + testRequest.PrimaryToneId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testPrimaryString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsQuery>())).Returns(Task.FromResult(testAllStrings));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult((Tone)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringSizeWithCorrepondingTensionHandler_ShouldStringListBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.String>>((int)HttpStatusCode.BadRequest,
                null, "Strings list is empty");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testPrimaryString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsQuery>())).Returns(Task.FromResult((List<String>)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringSizeWithCorrepondingTensionHandler_ShouldPrimaryStringBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.String>>((int)HttpStatusCode.BadRequest,
                null, "String of given Id: " + testRequest.StringId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult((String)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringSizeWithCorrepondingTensionHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.String>>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during calculating List of Strings with corresponding tensions");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testPrimaryString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsQuery>())).Returns(Task.FromResult(testAllStrings));
            mockedQueryExecutor.SetupSequence(x => x.Execute(It.IsAny<GetToneQuery>()))
                .Returns(Task.FromResult(testPrimaryTone))
                .Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
