using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using StringManager.Controllers;
using StringManager.Core.Models;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Tests
{
    internal class UsersControllerTests : ControllerTestsBase<UsersController>
    {
        private UsersController controller;
        private string exceptionResponse = "An error occured during preparation to send an request via controller: UsersController";

        [SetUp]
        public void Setup()
        {
            controller = new UsersController(mediatorMock.Object, logger);
            controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = adminClaimPrincipal
            };
        }

        [Test]
        public void GetUser_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<User>(200, new User());
            var mockResponse = new StatusCodeResponse<User>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetUserRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));

            //Act
            var returnedValue = controller.GetUserAsync().Result as ModelActionResult<User>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetUser_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetUserRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<User>)null));

            //Act
            var returnedValue = controller.GetUserAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetUser_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetUserRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();

            //Act
            var returnedValue = controller.GetUserAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetUser_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");

            //Act
            var returnedValue = controller.GetUserAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void GetUsers_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<List<User>>(200, new List<User>());
            var mockResponse = new StatusCodeResponse<List<User>>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetUsersRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new GetUsersRequest();

            //Act
            var returnedValue = controller.GetUsersAsync(test).Result as ModelActionResult<List<User>>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetUsers_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetUsersRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<List<User>>)null));
            var test = new GetUsersRequest();

            //Act
            var returnedValue = controller.GetUsersAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetUsers_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetUsersRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new GetUsersRequest();

            //Act
            var returnedValue = controller.GetUsersAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetUsers_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new GetUsersRequest();

            //Act
            var returnedValue = controller.GetUsersAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void AddUser_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<User>(200, new User());
            var mockResponse = new StatusCodeResponse<User>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<AddUserRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new AddUserRequest();

            //Act
            var returnedValue = controller.AddUserAsync(test).Result as ModelActionResult<User>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void AddUser_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddUserRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<User>)null));
            var test = new AddUserRequest();

            //Act
            var returnedValue = controller.AddUserAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddUser_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddUserRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new AddUserRequest();

            //Act
            var returnedValue = controller.AddUserAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddUser_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new AddUserRequest();

            //Act
            var returnedValue = controller.AddUserAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void ModifyUser_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<User>(204, null);
            var mockResponse = new StatusCodeResponse<User>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyUserRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new ModifyUserRequest();

            //Act
            var returnedValue = controller.ModifyUserAsync(test).Result as ModelActionResult<User>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void ModifyUser_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyUserRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<User>)null));
            var test = new ModifyUserRequest();

            //Act
            var returnedValue = controller.ModifyUserAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyUser_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyUserRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new ModifyUserRequest();

            //Act
            var returnedValue = controller.ModifyUserAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyUser_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new ModifyUserRequest();

            //Act
            var returnedValue = controller.ModifyUserAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }
    }
}
