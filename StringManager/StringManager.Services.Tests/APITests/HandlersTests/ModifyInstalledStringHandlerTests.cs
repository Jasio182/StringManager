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
    internal class ModifyInstalledStringHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<ModifyInstalledStringHandler>> mockedLogger;

        private ModifyInstalledStringHandler testHandler;

        private ModifyInstalledStringRequest testRequest;
        private InstalledString testInstalledString;
        private String testString;
        private Tone testTone;

        public ModifyInstalledStringHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedLogger = new Mock<ILogger<ModifyInstalledStringHandler>>();

            testHandler = new ModifyInstalledStringHandler(mockedQueryExecutor.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new ModifyInstalledStringRequest()
            {
                AccountType = Core.Enums.AccountType.Admin,
                StringId = 1,
                Id = 1,
                ToneId = 1,
                UserId = 1
            };
            testInstalledString = new InstalledString()
            {
                MyInstrument = new MyInstrument(),
                Position = 1,
                String = testString,
                Tone = testTone
            };
            testString = new String()
            {
                Id = 1,
                InstalledStrings = new List<InstalledString>(),
                Size = 9,
                SpecificWeight = 0.1,
                StringsInSets = new List<StringInSet>(),
                StringType = Core.Enums.StringType.WoundBrass,
                Manufacturer = new Manufacturer(),
                ManufacturerId = 1,
                NumberOfDaysGood = 12
            };
            testTone = new Tone()
            {
                Id = 1,
                Frequency = 0.1,
                InstalledStrings = new List<InstalledString>(),
                Name = "testTone1",
                TonesInTuning = new List<ToneInTuning>(),
                WaveLenght = 0.1
            };
        }

        [Test]
        public void ModifyInstalledStringHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.NoContent, null);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstalledStringQuery>())).Returns(Task.FromResult(testInstalledString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyInstalledStringCommand>()));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyInstalledStringHandler_ShouldToneBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.BadRequest,
                null, "Tone of given Id: " + testRequest.ToneId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstalledStringQuery>())).Returns(Task.FromResult(testInstalledString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult((Tone)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyInstalledStringHandler_ShouldStringBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.BadRequest,
                null, "String of given Id: " + testRequest.ToneId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstalledStringQuery>())).Returns(Task.FromResult(testInstalledString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult((String)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyInstalledStringHandler_ShouldInstalledStringBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.NotFound,
                null, "InstalledString of given Id: " + testRequest.ToneId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstalledStringQuery>())).Returns(Task.FromResult((InstalledString)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyInstalledStringHandler_ThrowsException()
        {
            //Arrange

            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.InstalledString>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing modyfication of a InstalledString");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetInstalledStringQuery>())).Returns(Task.FromResult(testInstalledString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyInstalledStringCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
