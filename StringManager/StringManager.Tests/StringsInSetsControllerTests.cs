using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using StringManager.Controllers;
using StringManager.Core.Models;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Tests
{
    public class StringInSetControllerTests : ControllerTestsBase<StringsInSetsController>
    {
        private StringsInSetsController controller;
        private string exceptionResponse = "An error occured during preparation to send an request via controller: StringsInSetsController";

        [SetUp]
        public void Setup()
        {
            controller = new StringsInSetsController(mediatorMock.Object, logger);
            controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = adminClaimPrincipal
            };
        }

        [Test]
        public void AddStringInSet_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<StringInSet>(200, new StringInSet());
            var mockResponse = new StatusCodeResponse<StringInSet>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<AddStringInSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new AddStringInSetRequest();

            //Act
            var returnedValue = controller.AddStringInSetAsync(test).Result as ModelActionResult<StringInSet>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void AddStringInSet_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddStringInSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<StringInSet>)null));
            var test = new AddStringInSetRequest();

            //Act
            var returnedValue = controller.AddStringInSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddStringInSet_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddStringInSetRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new AddStringInSetRequest();

            //Act
            var returnedValue = controller.AddStringInSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddStringInSet_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new AddStringInSetRequest();

            //Act
            var returnedValue = controller.AddStringInSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void ModifyStringInSet_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<StringInSet>(204, null);
            var mockResponse = new StatusCodeResponse<StringInSet>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyStringInSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new ModifyStringInSetRequest();

            //Act
            var returnedValue = controller.ModifyStringInSetAsync(test).Result as ModelActionResult<StringInSet>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void ModifyStringInSet_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyStringInSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<StringInSet>)null));
            var test = new ModifyStringInSetRequest();

            //Act
            var returnedValue = controller.ModifyStringInSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyStringInSet_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyStringInSetRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new ModifyStringInSetRequest();

            //Act
            var returnedValue = controller.ModifyStringInSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyStringInSet_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new ModifyStringInSetRequest();

            //Act
            var returnedValue = controller.ModifyStringInSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void RemoveStringInSet_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<StringInSet>(204, null);
            var mockResponse = new StatusCodeResponse<StringInSet>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveStringInSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new RemoveStringInSetRequest();

            //Act
            var returnedValue = controller.RemoveStringInSetAsync(test).Result as ModelActionResult<StringInSet>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void RemoveStringInSet_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveStringInSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<StringInSet>)null));
            var test = new RemoveStringInSetRequest();

            //Act
            var returnedValue = controller.RemoveStringInSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveStringInSet_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveStringInSetRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new RemoveStringInSetRequest();

            //Act
            var returnedValue = controller.RemoveStringInSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveStringInSet_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new RemoveStringInSetRequest();

            //Act
            var returnedValue = controller.RemoveStringInSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }
    }
}
