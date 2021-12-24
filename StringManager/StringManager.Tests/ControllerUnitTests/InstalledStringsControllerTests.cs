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

namespace StringManager.Tests.ControllerUnitTests
{
    public class InstalledStringsControllerTests : ControllerTestsBase<InstalledStringsController>
    {
        private InstalledStringsController controller;
        private string exceptionResponse = "An error occured during preparation to send an request via controller: InstalledStringsController";

        [SetUp]
        public void Setup()
        {
            controller = new InstalledStringsController(mediatorMock.Object, logger);
            controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = adminClaimPrincipal
            };
        }

        [Test]
        public void AddInstalledString_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<InstalledString>(200, new InstalledString());
            var mockResponse = new StatusCodeResponse<InstalledString>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<AddInstalledStringRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new AddInstalledStringRequest();

            //Act
            var returnedValue = controller.AddInstalledStringAsync(test).Result as ModelActionResult<InstalledString>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void AddInstalledString_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddInstalledStringRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<InstalledString>)null));
            var test = new AddInstalledStringRequest();

            //Act
            var returnedValue = controller.AddInstalledStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddInstalledString_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddInstalledStringRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new AddInstalledStringRequest();

            //Act
            var returnedValue = controller.AddInstalledStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddInstalledString_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new AddInstalledStringRequest();

            //Act
            var returnedValue = controller.AddInstalledStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}",returnedValue.result.Error);
        }

        [Test]
        public void ModifyInstalledString_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<InstalledString>(204, null);
            var mockResponse = new StatusCodeResponse<InstalledString>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyInstalledStringRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new ModifyInstalledStringRequest();

            //Act
            var returnedValue = controller.ModifyInstalledStringAsync(test).Result as ModelActionResult<InstalledString>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void ModifyInstalledString_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyInstalledStringRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<InstalledString>)null));
            var test = new ModifyInstalledStringRequest();

            //Act
            var returnedValue = controller.ModifyInstalledStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyInstalledString_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyInstalledStringRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new ModifyInstalledStringRequest();

            //Act
            var returnedValue = controller.ModifyInstalledStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyInstalledString_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new ModifyInstalledStringRequest();

            //Act
            var returnedValue = controller.ModifyInstalledStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void RemoveInstalledString_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<InstalledString>(204, null);
            var mockResponse = new StatusCodeResponse<InstalledString>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveInstalledStringRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new RemoveInstalledStringRequest();

            //Act
            var returnedValue = controller.RemoveInstalledStringAsync(test).Result as ModelActionResult<InstalledString>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void RemoveInstalledString_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveInstalledStringRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<InstalledString>)null));
            var test = new RemoveInstalledStringRequest();

            //Act
            var returnedValue = controller.RemoveInstalledStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveInstalledString_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveInstalledStringRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new RemoveInstalledStringRequest();

            //Act
            var returnedValue = controller.RemoveInstalledStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveInstalledString_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new RemoveInstalledStringRequest();

            //Act
            var returnedValue = controller.RemoveInstalledStringAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }
    }
}