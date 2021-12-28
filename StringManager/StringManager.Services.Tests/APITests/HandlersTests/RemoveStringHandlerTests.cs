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
    internal class RemoveStringHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<ILogger<RemoveStringHandler>> mockedLogger;

        private RemoveStringHandler testHandler;

        private RemoveStringRequest testRequest;
        private String testString;

        public RemoveStringHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedLogger = new Mock<ILogger<RemoveStringHandler>>();

            testHandler = new RemoveStringHandler(mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new RemoveStringRequest()
            {
                Id = 1
            };
            testString = new String()
            {
                Id = 1,
                NumberOfDaysGood = 200,
                InstalledStrings = new List<InstalledString>(),
                Size = 9,
                SpecificWeight = 0.00032037193,
                StringType = Core.Enums.StringType.PlainNikled,
                StringsInSets = new List<StringInSet>()
            };
        }

        [Test]
        public void RemoveStringHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.NoContent, null);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveStringCommand>())).Returns(Task.FromResult(testString));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveStringHandler_ShouldNotHaveBeenUnauthorized()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to remove an String");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveStringHandler_ShouldStringBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.NotFound,
                null, "String of given Id: " + testRequest.Id + " has not been found");
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveStringCommand>())).Returns(Task.FromResult((String)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveStringHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing deletion of a String");
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveStringCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
