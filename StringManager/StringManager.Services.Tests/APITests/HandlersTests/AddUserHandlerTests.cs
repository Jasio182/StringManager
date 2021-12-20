using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Handlers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class AddUserHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<ILogger<AddUserHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private AddUserHandler testHandler;

        private AddUserRequest testRequest;
        private User testUser;
        private User testAddedUser;
        private Core.Models.User testMappedUser;

        public AddUserHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<AddUserHandler>>();

            testHandler = new AddUserHandler(mockedMapper.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new AddUserRequest()
            {
                PlayStyle = Core.Enums.PlayStyle.Hard,
                AccountTypeToAdd = Core.Enums.AccountType.Admin,
                DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                Email = "testEmail",
                Password = "testPassword",
                UserId = 1,
                Username = "testUsername"
            };
            testUser = new User()
            {
                PlayStyle = Core.Enums.PlayStyle.Hard,
                AccountType = Core.Enums.AccountType.Admin,
                DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                Email = "testEmail",
                Password = "testPassword",
                Username = "testUsername",
                MyInstruments = new List<MyInstrument>()
            };
            testAddedUser = new User()
            {
                Id = 1,
                PlayStyle = Core.Enums.PlayStyle.Hard,
                AccountType = Core.Enums.AccountType.Admin,
                DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                Email = "testEmail",
                Password = "testPassword",
                Username = "testUsername",
                MyInstruments = new List<MyInstrument>()
            };
            testMappedUser = new Core.Models.User()
            {
                Id = 1,
                PlayStyle = Core.Enums.PlayStyle.Hard,
                AccountType = Core.Enums.AccountType.Admin,
                DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                Email = "testEmail",
                Username = "testUsername"
            };
        }

        [Test]
        public void AddUserHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.User>((int)HttpStatusCode.OK, testMappedUser);
            mockedMapper.Setup(x => x.Map<User>(It.IsAny<AddUserRequest>()))
                .Returns(testUser);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddUserCommand>())).Returns(Task.FromResult(testAddedUser));
            mockedMapper.Setup(x => x.Map<Core.Models.User>(It.IsAny<User>())).Returns(testMappedUser);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddUserHandler_ShouldNotHaveBeenUnauthorised()
        {
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.User>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to add a new Admin User");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddUserHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.User>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing adding new User item");
            mockedMapper.Setup(x => x.Map<User>(It.IsAny<AddUserRequest>()))
            .Returns(testUser);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddUserCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
