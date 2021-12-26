using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using StringManager.Controllers;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Tests.ControllerUnitTests
{
    public class StringsControllerTests : ControllerTestsBase<StringsController>
    {
        private StringsController controller;
        private string exceptionResponse = "An error occured during preparation to send an request via controller: StringsController";

        [SetUp]
        public void Setup()
        {
            controller = new StringsController(mediatorMock.Object, logger);
            controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = adminClaimPrincipal
            };
        }

        [Test]
        public void GetStrings_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<List<String>>(200, new List<String>());
            var mockResponse = new StatusCodeResponse<List<String>>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));

            //Act
            var returnedValue = controller.GetStringsAsync().Result as ModelActionResult<List<String>>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetStrings_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<List<String>>)null));

            //Act
            var returnedValue = controller.GetStringsAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStrings_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();

            //Act
            var returnedValue = controller.GetStringsAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStrings_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");

            //Act
            var returnedValue = controller.GetStringsAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void AddString_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<String>(200, new String());
            var mockResponse = new StatusCodeResponse<String>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<AddStringRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new AddStringRequest();

            //Act
            var returnedValue = controller.AddStringAsync(test).Result as ModelActionResult<String>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void AddString_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddStringRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<String>)null));
            var test = new AddStringRequest();

            //Act
            var returnedValue = controller.AddStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddString_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddStringRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new AddStringRequest();

            //Act
            var returnedValue = controller.AddStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddString_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new AddStringRequest();

            //Act
            var returnedValue = controller.AddStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void ModifyString_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<String>(204, null);
            var mockResponse = new StatusCodeResponse<String>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyStringRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new ModifyStringRequest();

            //Act
            var returnedValue = controller.ModifyStringAsync(test).Result as ModelActionResult<String>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void ModifyString_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyStringRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<String>)null));
            var test = new ModifyStringRequest();

            //Act
            var returnedValue = controller.ModifyStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyString_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyStringRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new ModifyStringRequest();

            //Act
            var returnedValue = controller.ModifyStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyString_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new ModifyStringRequest();

            //Act
            var returnedValue = controller.ModifyStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void RemoveString_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<String>(204, null);
            var mockResponse = new StatusCodeResponse<String>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveStringRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new RemoveStringRequest();

            //Act
            var returnedValue = controller.RemoveStringAsync(test).Result as ModelActionResult<String>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void RemoveString_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveStringRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<String>)null));
            var test = new RemoveStringRequest();

            //Act
            var returnedValue = controller.RemoveStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveString_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveStringRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new RemoveStringRequest();

            //Act
            var returnedValue = controller.RemoveStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveString_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new RemoveStringRequest();

            //Act
            var returnedValue = controller.RemoveStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }
    }
}
