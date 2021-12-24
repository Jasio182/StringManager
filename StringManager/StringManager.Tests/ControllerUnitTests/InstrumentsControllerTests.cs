using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using StringManager.Controllers;
using StringManager.Core.Models;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Tests.ControllerUnitTests
{
    public class InstrumentsControllerTests : ControllerTestsBase<InstrumentsController>
    {
        private InstrumentsController controller;
        private string exceptionResponse = "An error occured during preparation to send an request via controller: InstrumentsController";

        [SetUp]
        public void Setup()
        {
            controller = new InstrumentsController(mediatorMock.Object, logger);
            controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = adminClaimPrincipal
            };
        }

        [Test]
        public void GetInstruments_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<List<Instrument>>(200, new List<Instrument>());
            var mockResponse = new StatusCodeResponse<List<Instrument>>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetInstrumentsRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));

            //Act
            var returnedValue = controller.GetInstrumentsAsync().Result as ModelActionResult<List<Instrument>>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetInstruments_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetInstrumentsRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<List<Instrument>>)null));

            //Act
            var returnedValue = controller.GetInstrumentsAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetInstruments_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetInstrumentsRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            //Act
            var returnedValue = controller.GetInstrumentsAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetInstruments_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");

            //Act
            var returnedValue = controller.GetInstrumentsAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void AddInstrument_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<Instrument>(200, new Instrument());
            var mockResponse = new StatusCodeResponse<Instrument>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<AddInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new AddInstrumentRequest();

            //Act
            var returnedValue = controller.AddInstrumentAsync(test).Result as ModelActionResult<Instrument>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void AddInstrument_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<Instrument>)null));
            var test = new AddInstrumentRequest();

            //Act
            var returnedValue = controller.AddInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddInstrument_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddInstrumentRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new AddInstrumentRequest();

            //Act
            var returnedValue = controller.AddInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddInstrument_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new AddInstrumentRequest();

            //Act
            var returnedValue = controller.AddInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void ModifyInstrument_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<Instrument>(204, null);
            var mockResponse = new StatusCodeResponse<Instrument>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new ModifyInstrumentRequest();

            //Act
            var returnedValue = controller.ModifyInstrumentAsync(test).Result as ModelActionResult<Instrument>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void ModifyInstrument_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<Instrument>)null));
            var test = new ModifyInstrumentRequest();

            //Act
            var returnedValue = controller.ModifyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyInstrument_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyInstrumentRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new ModifyInstrumentRequest();

            //Act
            var returnedValue = controller.ModifyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyInstrument_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new ModifyInstrumentRequest();

            //Act
            var returnedValue = controller.ModifyInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void RemoveInstrument_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<Instrument>(204, null);
            var mockResponse = new StatusCodeResponse<Instrument>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new RemoveInstrumentRequest();

            //Act
            var returnedValue = controller.RemoveInstrumentAsync(test).Result as ModelActionResult<Instrument>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void RemoveInstrument_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveInstrumentRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<Instrument>)null));
            var test = new RemoveInstrumentRequest();

            //Act
            var returnedValue = controller.RemoveInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveInstrument_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveInstrumentRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new RemoveInstrumentRequest();

            //Act
            var returnedValue = controller.RemoveInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveInstrument_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new RemoveInstrumentRequest();

            //Act
            var returnedValue = controller.RemoveInstrumentAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }
    }
}
