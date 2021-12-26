using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using StringManager.Controllers;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Tests.ControllerUnitTests
{
    public class TonesInTuningsControllerTests : ControllerTestsBase<TonesInTuningsController>
    {
        private TonesInTuningsController controller;
        private string exceptionResponse = "An error occured during preparation to send an request via controller: TonesInTuningsController";

        [SetUp]
        public void Setup()
        {
            controller = new TonesInTuningsController(mediatorMock.Object, logger);
            controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = adminClaimPrincipal
            };
        }


        [Test]
        public void AddToneInTuning_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<ToneInTuning>(200, new ToneInTuning());
            var mockResponse = new StatusCodeResponse<ToneInTuning>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<AddToneInTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new AddToneInTuningRequest();

            //Act
            var returnedValue = controller.AddToneInTuningAsync(test).Result as ModelActionResult<ToneInTuning>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void AddToneInTuning_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddToneInTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<ToneInTuning>)null));
            var test = new AddToneInTuningRequest();

            //Act
            var returnedValue = controller.AddToneInTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddToneInTuning_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddToneInTuningRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new AddToneInTuningRequest();

            //Act
            var returnedValue = controller.AddToneInTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddToneInTuning_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new AddToneInTuningRequest();

            //Act
            var returnedValue = controller.AddToneInTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void ModifyToneInTuning_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<ToneInTuning>(204, null);
            var mockResponse = new StatusCodeResponse<ToneInTuning>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyToneInTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new ModifyToneInTuningRequest();

            //Act
            var returnedValue = controller.ModifyToneInTuningAsync(test).Result as ModelActionResult<ToneInTuning>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void ModifyToneInTuning_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyToneInTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<ToneInTuning>)null));
            var test = new ModifyToneInTuningRequest();

            //Act
            var returnedValue = controller.ModifyToneInTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyToneInTuning_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyToneInTuningRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new ModifyToneInTuningRequest();

            //Act
            var returnedValue = controller.ModifyToneInTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyToneInTuning_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new ModifyToneInTuningRequest();

            //Act
            var returnedValue = controller.ModifyToneInTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void RemoveToneInTuning_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<ToneInTuning>(204, null);
            var mockResponse = new StatusCodeResponse<ToneInTuning>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveToneInTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new RemoveToneInTuningRequest();

            //Act
            var returnedValue = controller.RemoveToneInTuningAsync(test).Result as ModelActionResult<ToneInTuning>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void RemoveToneInTuning_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveToneInTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<ToneInTuning>)null));
            var test = new RemoveToneInTuningRequest();

            //Act
            var returnedValue = controller.RemoveToneInTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveToneInTuning_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveToneInTuningRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new RemoveToneInTuningRequest();

            //Act
            var returnedValue = controller.RemoveToneInTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveToneInTuning_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new RemoveToneInTuningRequest();

            //Act
            var returnedValue = controller.RemoveToneInTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }
    }
}
