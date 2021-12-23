using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
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
    internal class GetUsersHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetUsersHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetUsersHandler testHandler;

        private GetUsersRequest testRequest;
        private List<User> testUsers;
        private List<Core.Models.User> testMappedUsers;

        public GetUsersHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetUsersHandler>>();

            testHandler = new GetUsersHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testUsers = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    PlayStyle = Core.Enums.PlayStyle.Moderate,
                    AccountType = Core.Enums.AccountType.User,
                    DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                    Email = "testEmail1",
                    Password = "testPassword1",
                    Username = "testUsername1"
                },
                new User()
                {
                    Id = 2,
                    PlayStyle = Core.Enums.PlayStyle.Light,
                    AccountType = Core.Enums.AccountType.Admin,
                    DailyMaintanance = Core.Enums.GuitarDailyMaintanance.PlayAsIs,
                    Email = "testEmail2",
                    Password = "testPassword2",
                    Username = "testUsername2"
                }
            };
            testMappedUsers = new List<Core.Models.User>()
            {
                new Core.Models.User()
                {
                    Id = 1,
                    PlayStyle = Core.Enums.PlayStyle.Moderate,
                    AccountType = Core.Enums.AccountType.User,
                    DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                    Email = "testEmail1",
                    Username = "testUsername1"
                },
                new Core.Models.User()
                {
                    Id = 2,
                    PlayStyle = Core.Enums.PlayStyle.Light,
                    AccountType = Core.Enums.AccountType.Admin,
                    DailyMaintanance = Core.Enums.GuitarDailyMaintanance.PlayAsIs,
                    Email = "testEmail2",
                    Username = "testUsername2"
                }
            };
            testRequest = new GetUsersRequest()
            {
                AccountType = Core.Enums.AccountType.Admin,
                UserId = 1
            };
        }

        [Test]
        public void GetUsersHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.User>>((int)HttpStatusCode.OK, testMappedUsers);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetUsersQuery>())).Returns(Task.FromResult(testUsers));
            mockedMapper.Setup(x => x.Map<List<Core.Models.User>>(It.IsAny<List<User>>())).Returns(testMappedUsers);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetUsersHandler_ShouldNotHaveBeenUnauthorised()
        {
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.User>>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to get list of all User items");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetUsersHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.User>>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting list of User items");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetUsersQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
