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
    internal class ModifyStringHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<ModifyStringHandler>> mockedLogger;

        private ModifyStringHandler testHandler;

        private ModifyStringRequest testRequest;
        private String testString;
        private Manufacturer testManufacturer;

        public ModifyStringHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedLogger = new Mock<ILogger<ModifyStringHandler>>();

            testHandler = new ModifyStringHandler(mockedQueryExecutor.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new ModifyStringRequest()
            {
                ManufacturerId = 1,
                Id = 1,
            };
            testString = new String()
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
            testManufacturer = new Manufacturer()
            {
                Id = 1,
                Strings = new List<String>(),
                Name = "TestManufacturer",
                Instruments = new List<Instrument>()
            };
        }

        [Test]
        public void ModifyStringHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.NoContent, null);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetManufacturerQuery>())).Returns(Task.FromResult(testManufacturer));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyStringCommand>()));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyStringHandler_ShouldManufacturerBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.BadRequest,
                null, "Manufacturer of given Id: " + testRequest.ManufacturerId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetManufacturerQuery>())).Returns(Task.FromResult((Manufacturer)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyStringHandler_ShouldStringBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.NotFound,
                null, "String of given Id: " + testRequest.ManufacturerId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult((String)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyStringHandler_ShouldNotHaveBeenUnauthorised()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to modify a String");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyStringHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<ToneInTuning>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing modyfication of a String");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetManufacturerQuery>())).Returns(Task.FromResult(testManufacturer));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyStringCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
