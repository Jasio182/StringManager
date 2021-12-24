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
    internal class ManufacturersControllerTests : ControllerTestsBase<ManufacturersController>
    {
        private ManufacturersController controller;
        private string exceptionResponse = "An error occured during preparation to send an request via controller: ManufacturersController";

        [SetUp]
        public void Setup()
        {
            controller = new ManufacturersController(mediatorMock.Object, logger);
            controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = adminClaimPrincipal
            };
        }

        [Test]
        public void GetStringsManufacturers_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<List<Manufacturer>>(200, new List<Manufacturer>());
            var mockResponse = new StatusCodeResponse<List<Manufacturer>>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsManufacturersRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));

            //Act
            var returnedValue = controller.GetStringsManufacturersAsync().Result as ModelActionResult<List<Manufacturer>>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetStringsManufacturers_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsManufacturersRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<List<Manufacturer>>)null));

            //Act
            var returnedValue = controller.GetStringsManufacturersAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStringsManufacturers_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsManufacturersRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            //Act
            var returnedValue = controller.GetStringsManufacturersAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStringsManufacturers_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");

            //Act
            var returnedValue = controller.GetStringsManufacturersAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void GetInstrumentsManufacturers_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<List<Manufacturer>>(200, new List<Manufacturer>());
            var mockResponse = new StatusCodeResponse<List<Manufacturer>>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetInstrumentsManufacturersRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));

            //Act
            var returnedValue = controller.GetInstrumentsManufacturersAsync().Result as ModelActionResult<List<Manufacturer>>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetInstrumentsManufacturers_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetInstrumentsManufacturersRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<List<Manufacturer>>)null));

            //Act
            var returnedValue = controller.GetInstrumentsManufacturersAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetInstrumentsManufacturers_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetInstrumentsManufacturersRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            //Act
            var returnedValue = controller.GetInstrumentsManufacturersAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetInstrumentsManufacturers_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");

            //Act
            var returnedValue = controller.GetInstrumentsManufacturersAsync().Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void AddManufacturer_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<Manufacturer>(200, new Manufacturer());
            var mockResponse = new StatusCodeResponse<Manufacturer>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<AddManufacturerRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new AddManufacturerRequest();

            //Act
            var returnedValue = controller.AddManufacturerAsync(test).Result as ModelActionResult<Manufacturer>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void AddManufacturer_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddManufacturerRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<Manufacturer>)null));
            var test = new AddManufacturerRequest();

            //Act
            var returnedValue = controller.AddManufacturerAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddManufacturer_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddManufacturerRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new AddManufacturerRequest();

            //Act
            var returnedValue = controller.AddManufacturerAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddManufacturer_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new AddManufacturerRequest();

            //Act
            var returnedValue = controller.AddManufacturerAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void ModifyManufacturer_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<Manufacturer>(204, null);
            var mockResponse = new StatusCodeResponse<Manufacturer>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyManufacturerRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new ModifyManufacturerRequest();

            //Act
            var returnedValue = controller.ModifyManufacturerAsync(test).Result as ModelActionResult<Manufacturer>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void ModifyManufacturer_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyManufacturerRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<Manufacturer>)null));
            var test = new ModifyManufacturerRequest();

            //Act
            var returnedValue = controller.ModifyManufacturerAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyManufacturer_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyManufacturerRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new ModifyManufacturerRequest();

            //Act
            var returnedValue = controller.ModifyManufacturerAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyManufacturer_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new ModifyManufacturerRequest();

            //Act
            var returnedValue = controller.ModifyManufacturerAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void RemoveManufacturer_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<Manufacturer>(204, null);
            var mockResponse = new StatusCodeResponse<Manufacturer>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveManufacturerRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new RemoveManufacturerRequest();

            //Act
            var returnedValue = controller.RemoveManufacturerAsync(test).Result as ModelActionResult<Manufacturer>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void RemoveManufacturer_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveManufacturerRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<Manufacturer>)null));
            var test = new RemoveManufacturerRequest();

            //Act
            var returnedValue = controller.RemoveManufacturerAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveManufacturer_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveManufacturerRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new RemoveManufacturerRequest();

            //Act
            var returnedValue = controller.RemoveManufacturerAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveManufacturer_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new RemoveManufacturerRequest();

            //Act
            var returnedValue = controller.RemoveManufacturerAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }
    }
}
