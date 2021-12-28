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
    public class StringsSetsControllerTests : ControllerTestsBase<StringsSetsController>
    {
        private StringsSetsController controller;
        private string exceptionResponse = "An error occured during preparation to send an request via controller: StringsSetsController";

        [SetUp]
        public void Setup()
        {
            controller = new StringsSetsController(mediatorMock.Object, logger);
            controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = adminClaimPrincipal
            };
        }

        [Test]
        public void GetStringsSet_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<StringsSet>(200, new StringsSet());
            var mockResponse = new StatusCodeResponse<StringsSet>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new GetStringsSetRequest();

            //Act
            var returnedValue = controller.GetStringsSetAsync(test).Result as ModelActionResult<StringsSet>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetStringsSet_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<StringsSet>)null));
            var test = new GetStringsSetRequest();

            //Act
            var returnedValue = controller.GetStringsSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStringsSet_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsSetRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new GetStringsSetRequest();

            //Act
            var returnedValue = controller.GetStringsSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStringsSet_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new GetStringsSetRequest();

            //Act
            var returnedValue = controller.GetStringsSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void GetStringsSets_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<List<StringsSet>>(200, new List<StringsSet>());
            var mockResponse = new StatusCodeResponse<List<StringsSet>>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsSetsRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));

            //Act
            var returnedValue = controller.GetStringsSetsAsync(null).Result as ModelActionResult<List<StringsSet>>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetStringsSets_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsSetsRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<List<StringsSet>>)null));

            //Act
            var returnedValue = controller.GetStringsSetsAsync(null).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStringsSets_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsSetsRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            //Act
            var returnedValue = controller.GetStringsSetsAsync(null).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStringsSets_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");

            //Act
            var returnedValue = controller.GetStringsSetsAsync(null).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void AddStringsSet_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<StringsSet>(200, new StringsSet());
            var mockResponse = new StatusCodeResponse<StringsSet>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<AddStringsSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new AddStringsSetRequest();

            //Act
            var returnedValue = controller.AddStringsSetAsync(test).Result as ModelActionResult<StringsSet>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void AddStringsSet_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddStringsSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<StringsSet>)null));
            var test = new AddStringsSetRequest();

            //Act
            var returnedValue = controller.AddStringsSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddStringsSet_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddStringsSetRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new AddStringsSetRequest();

            //Act
            var returnedValue = controller.AddStringsSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddStringsSet_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new AddStringsSetRequest();

            //Act
            var returnedValue = controller.AddStringsSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void ModifyStringsSet_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<StringsSet>(204, null);
            var mockResponse = new StatusCodeResponse<StringsSet>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyStringsSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new ModifyStringsSetRequest();

            //Act
            var returnedValue = controller.ModifyStringsSetAsync(test).Result as ModelActionResult<StringsSet>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void ModifyStringsSet_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyStringsSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<StringsSet>)null));
            var test = new ModifyStringsSetRequest();

            //Act
            var returnedValue = controller.ModifyStringsSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyStringsSet_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyStringsSetRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new ModifyStringsSetRequest();

            //Act
            var returnedValue = controller.ModifyStringsSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyStringsSet_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new ModifyStringsSetRequest();

            //Act
            var returnedValue = controller.ModifyStringsSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void RemoveStringsSet_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<StringsSet>(204, null);
            var mockResponse = new StatusCodeResponse<StringsSet>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveStringsSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new RemoveStringsSetRequest();

            //Act
            var returnedValue = controller.RemoveStringsSetAsync(test).Result as ModelActionResult<StringsSet>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void RemoveStringsSet_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveStringsSetRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<StringsSet>)null));
            var test = new RemoveStringsSetRequest();

            //Act
            var returnedValue = controller.RemoveStringsSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveStringsSet_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveStringsSetRequest>(), It.IsAny<CancellationToken>())).Throws<Exception>();
            var test = new RemoveStringsSetRequest();

            //Act
            var returnedValue = controller.RemoveStringsSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveStringsSet_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new RemoveStringsSetRequest();

            //Act
            var returnedValue = controller.RemoveStringsSetAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }
    }
}
