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
    internal class ModifyStringInSetHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<ModifyStringInSetHandler>> mockedLogger;

        private ModifyStringInSetHandler testHandler;

        private ModifyStringInSetRequest testRequest;
        private StringInSet testStringInSet;
        private String testString;
        private StringsSet testStringsSet;

        public ModifyStringInSetHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedLogger = new Mock<ILogger<ModifyStringInSetHandler>>();

            testHandler = new ModifyStringInSetHandler(mockedQueryExecutor.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new ModifyStringInSetRequest()
            {
                Id = 1,
                StringId = 1,
                StringsSetId = 1
            };
            testStringsSet = new StringsSet()
            {
                Id = 1,
                StringsInSet = new List<StringInSet>(),
                NumberOfStrings = 6,
                Name = "testName"
            };
            testString = new String()
            {
                Id = 1,
                Manufacturer = new Manufacturer(),
                NumberOfDaysGood = 200,
                InstalledStrings = new List<InstalledString>(),
                Size = 9,
                SpecificWeight = 0.00032037193,
                StringType = Core.Enums.StringType.PlainNikled,
                StringsInSets = new List<StringInSet>()
            };
            testStringInSet = new StringInSet()
            {
                Position = 1,
                String = testString,
                StringId = 1,
                StringsSet = testStringsSet,
                StringsSetId = 1
            };
        }

        [Test]
        public void ModifyStringInSetHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.NoContent, null);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringInSetQuery>())).Returns(Task.FromResult(testStringInSet));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetQuery>())).Returns(Task.FromResult(testStringsSet));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyStringInSetCommand>()));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyStringInSetHandler_ShouldStringsSetBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.BadRequest,
                null, "StringsSet of given Id: " + testRequest.StringsSetId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringInSetQuery>())).Returns(Task.FromResult(testStringInSet));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetQuery>())).Returns(Task.FromResult((StringsSet)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }


        [Test]
        public void ModifyStringInSetHandler_ShouldStringBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.BadRequest,
                null, "String of given Id: " + testRequest.StringId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringInSetQuery>())).Returns(Task.FromResult(testStringInSet));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult((String)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyStringInSetHandler_ShouldStringInSetBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.NotFound,
                null, "StringInSet of given Id: " + testRequest.Id + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringInSetQuery>())).Returns(Task.FromResult((StringInSet)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyStringInSetHandler_ShouldNotHaveBeenUnauthorised()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to modify a StringInSet");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyStringInSetHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing modyfication of a StringInSet");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringInSetQuery>())).Returns(Task.FromResult(testStringInSet));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetQuery>())).Returns(Task.FromResult(testStringsSet));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyStringInSetCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
