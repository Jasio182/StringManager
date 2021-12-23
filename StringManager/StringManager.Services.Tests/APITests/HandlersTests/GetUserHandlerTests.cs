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
    internal class GetUserHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetUserHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetUserHandler testHandler;

        private GetUserRequest testRequest;
        private User testUser;
        private Core.Models.User testMappedUser;

        public GetUserHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetUserHandler>>();

            testHandler = new GetUserHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testUser = new User()
            {
                Id = 1,
                PlayStyle = Core.Enums.PlayStyle.Moderate,
                AccountType = Core.Enums.AccountType.User,
                DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                Email = "testEmail",
                Password = "testPassword",
                Username = "testUsername"
            };
            testMappedUser = new Core.Models.User()
            {
                Id = 1,
                PlayStyle = Core.Enums.PlayStyle.Moderate,
                AccountType = Core.Enums.AccountType.User,
                DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                Email = "testEmail",
                Username = "testUsername",
            };
            testRequest = new GetUserRequest()
            {
                UserId = 1,
            };
        }

        [Test]
        public void GetUserHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.User>((int)HttpStatusCode.OK, testMappedUser);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetUserByIdQuery>())).Returns(Task.FromResult(testUser));
            mockedMapper.Setup(x => x.Map<Core.Models.User>(It.IsAny<User>())).Returns(testMappedUser);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetUserHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.User>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting a User item");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetUserByIdQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
