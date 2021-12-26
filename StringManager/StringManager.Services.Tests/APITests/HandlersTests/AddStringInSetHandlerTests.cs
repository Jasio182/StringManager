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
    internal class AddStringInSetHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<AddStringInSetHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private AddStringInSetHandler testHandler;

        private StringsSet testStringsSet;
        private String testString;
        private StringInSet testStringInSet;
        private StringInSet testAddedStringInSet;
        private Core.Models.StringInSet testMappedStringInSet;
        private AddStringInSetRequest testRequest;

        public AddStringInSetHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<AddStringInSetHandler>>();

            testHandler = new AddStringInSetHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedCommandExecutor.Object, mockedLogger.Object);

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
            testAddedStringInSet = new StringInSet()
            {
                Id = 1,
                Position = 1,
                String = testString,
                StringId = 1,
                StringsSet = testStringsSet,
                StringsSetId = 1
            };
            testMappedStringInSet = new Core.Models.StringInSet()
            {
                Id = 1,
                Position = 1,
                Size = 9,
                SpecificWeight = 0.00032037193,
                StringType = Core.Enums.StringType.PlainNikled,
                Manufacturer = "testManufacturer",
                StringId = 1
            };
            testRequest = new AddStringInSetRequest()
            {
                StringId = 1,
                StringsSetId = 1,
                Position = 1,
                UserId = 1
            };
        }

        [Test]
        public void AddStringInSetHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.OK, testMappedStringInSet);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetQuery>())).Returns(Task.FromResult(testStringsSet));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedMapper.Setup(x => x.Map<StringInSet>(It.IsAny<System.Tuple<AddStringInSetRequest, StringsSet, String>>()))
                .Returns(testStringInSet);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddStringInSetCommand>())).Returns(Task.FromResult(testAddedStringInSet));
            mockedMapper.Setup(x => x.Map<Core.Models.StringInSet>(It.IsAny<StringInSet>())).Returns(testMappedStringInSet);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddStringInSetHandler_ShouldNotHaveBeenUnauthorised()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to add a new StringInSet");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddStringInSetHandler_ShouldStringsSetBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.BadRequest,
                null, "StringsSet of given Id: " + testRequest.StringsSetId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetQuery>())).Returns(Task.FromResult((StringsSet)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddStringInSetHandler_ShouldStringBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.BadRequest,
                null, "String of given Id: " + testRequest.StringId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetQuery>())).Returns(Task.FromResult(testStringsSet));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult((String)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddStringInSetHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing adding new StringInSet item");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetQuery>())).Returns(Task.FromResult(testStringsSet));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedMapper.Setup(x => x.Map<StringInSet>(It.IsAny<System.Tuple<AddStringInSetRequest, StringsSet, String>>()))
                .Returns(testStringInSet);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddStringInSetCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
