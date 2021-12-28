using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Handlers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class AddStringsSetHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<ILogger<AddStringsSetHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private AddStringsSetHandler testHandler;

        private AddStringsSetRequest testRequest;
        private StringsSet testStringsSet;
        private StringsSet testAddedStringsSet;
        private Core.Models.StringsSet testMappedStringsSet;

        public AddStringsSetHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<AddStringsSetHandler>>();

            testHandler = new AddStringsSetHandler(mockedMapper.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new AddStringsSetRequest()
            {
                Name = "testName",
                NumberOfStrings = 6,
                UserId = 1
            };
            testStringsSet = new StringsSet()
            {
                Name = "testSetName",
                StringsInSet = new List<StringInSet>(),
                NumberOfStrings = 6
            };
            testAddedStringsSet = new StringsSet()
            {
                Id = 1,
                Name = "testSetName",
                StringsInSet = new List<StringInSet>(),
                NumberOfStrings = 6
            };
            testMappedStringsSet = new Core.Models.StringsSet()
            {
                Id = 1,
                StringsInSet = new List<Core.Models.StringInSet>(),
                NumberOfStrings = 6,
                Name = "testSetName"
            };
        }

        [Test]
        public void AddStringsSetHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringsSet>((int)HttpStatusCode.OK, testMappedStringsSet);
            mockedMapper.Setup(x => x.Map<StringsSet>(It.IsAny<AddStringsSetRequest>()))
                .Returns(testStringsSet);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddStringsSetCommand>())).Returns(Task.FromResult(testAddedStringsSet));
            mockedMapper.Setup(x => x.Map<Core.Models.StringsSet>(It.IsAny<StringsSet>())).Returns(testMappedStringsSet);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddStringsSetHandler_ShouldNotHaveBeenUnauthorized()
        {
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringsSet>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to add a new StringsSet");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddStringsSetHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringsSet>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing adding new StringsSet item");
                mockedMapper.Setup(x => x.Map<StringsSet>(It.IsAny<AddStringsSetRequest>()))
                .Returns(testStringsSet);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddStringsSetCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
