using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using StringManager.Controllers;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Tests.ControllerUnitTests
{
    public class MyInstrumentsControllerTests : ControllerTestsBase<MyInstrumentsController>
    {
        private MyInstrumentsController controller;
        private string exceptionResponse = "An error occured during preparation to send an request via controller: MyInstrumentsController";

        [SetUp]
        public void Setup()
        {
            controller = new MyInstrumentsController(mediatorMock.Object, logger);
            controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = adminClaimPrincipal
            };
        }

        [Test]
        public void GetMyInstrument_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<MyInstrument>(200, new MyInstrument());
            var mockResponse = new StatusCodeResponse<MyInstrument>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetMyInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new GetMyInstrumentRequest();

            //Act
            var returnedValue = controller.GetMyInstrumentAsync(test).Result as ModelActionResult<MyInstrument>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetMyInstrument_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetMyInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<MyInstrument>)null));
            var test = new GetMyInstrumentRequest();

            //Act
            var returnedValue = controller.GetMyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetMyInstrument_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetMyInstrumentRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new GetMyInstrumentRequest();

            //Act
            var returnedValue = controller.GetMyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetMyInstrument_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new GetMyInstrumentRequest();

            //Act
            var returnedValue = controller.GetMyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void GetMyInstruments_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<List<MyInstrumentList>>(200, new List<MyInstrumentList>());
            var mockResponse = new StatusCodeResponse<List<MyInstrumentList>>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetMyInstrumentsRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));

            //Act
            var returnedValue = controller.GetMyInstrumentsAsync(null).Result as ModelActionResult<List<MyInstrumentList>>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetMyInstruments_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetMyInstrumentsRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<List<MyInstrumentList>>)null));

            //Act
            var returnedValue = controller.GetMyInstrumentsAsync(null).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetMyInstruments_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetMyInstrumentsRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            //Act
            var returnedValue = controller.GetMyInstrumentsAsync(null).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetMyInstruments_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");

            //Act
            var returnedValue = controller.GetMyInstrumentsAsync(null).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void AddMyInstrument_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<MyInstrument>(200, new MyInstrument());
            var mockResponse = new StatusCodeResponse<MyInstrument>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<AddMyInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new AddMyInstrumentRequest();

            //Act
            var returnedValue = controller.AddMyInstrumentAsync(test).Result as ModelActionResult<MyInstrument>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void AddMyInstrument_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddMyInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<MyInstrument>)null));
            var test = new AddMyInstrumentRequest();

            //Act
            var returnedValue = controller.AddMyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddMyInstrument_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddMyInstrumentRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new AddMyInstrumentRequest();

            //Act
            var returnedValue = controller.AddMyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddMyInstrument_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new AddMyInstrumentRequest();

            //Act
            var returnedValue = controller.AddMyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void ModifyMyInstrument_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<MyInstrument>(204, null);
            var mockResponse = new StatusCodeResponse<MyInstrument>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyMyInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new ModifyMyInstrumentRequest();

            //Act
            var returnedValue = controller.ModifyMyInstrumentAsync(test).Result as ModelActionResult<MyInstrument>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void ModifyMyInstrument_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyMyInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<MyInstrument>)null));
            var test = new ModifyMyInstrumentRequest();

            //Act
            var returnedValue = controller.ModifyMyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyMyInstrument_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyMyInstrumentRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new ModifyMyInstrumentRequest();

            //Act
            var returnedValue = controller.ModifyMyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyMyInstrument_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new ModifyMyInstrumentRequest();

            //Act
            var returnedValue = controller.ModifyMyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void RemoveMyInstrument_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<MyInstrument>(204, null);
            var mockResponse = new StatusCodeResponse<MyInstrument>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveMyInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new RemoveMyInstrumentRequest();

            //Act
            var returnedValue = controller.RemoveMyInstrumentAsync(test).Result as ModelActionResult<MyInstrument>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void RemoveMyInstrument_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveMyInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<MyInstrument>)null));
            var test = new RemoveMyInstrumentRequest();

            //Act
            var returnedValue = controller.RemoveMyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveMyInstrument_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveMyInstrumentRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new RemoveMyInstrumentRequest();

            //Act
            var returnedValue = controller.RemoveMyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveMyInstrument_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new RemoveMyInstrumentRequest();

            //Act
            var returnedValue = controller.RemoveMyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }
    }
}
