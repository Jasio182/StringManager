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
    public class StringTensionControllerTests : ControllerTestsBase<StringTensionController>
    {
        private StringTensionController controller;
        private string exceptionResponse = "An error occured during preparation to send an request via controller: StringTensionController";

        [SetUp]
        public void Setup()
        {
            controller = new StringTensionController(mediatorMock.Object, logger);
            controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = adminClaimPrincipal
            };
        }

        [Test]
        public void GetScaleLenghtAsync_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<int[]>(200, new int[] { });
            var mockResponse = new StatusCodeResponse<int[]>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetScaleLenghtsRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new GetScaleLenghtsRequest();

            //Act
            var returnedValue = controller.GetScaleLenghtAsync(test).Result as ModelActionResult<int[]>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetScaleLenghtAsync_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetScaleLenghtsRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new GetScaleLenghtsRequest();

            //Act
            var returnedValue = controller.GetScaleLenghtAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetScaleLenghtAsync_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new GetScaleLenghtsRequest();

            //Act
            var returnedValue = controller.GetScaleLenghtAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }


        [Test]
        public void GetStringTensionAsync_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<double?>(200, 0.0);
            var mockResponse = new StatusCodeResponse<double?>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringTensionRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new GetStringTensionRequest();

            //Act
            var returnedValue = controller.GetStringTensionAsync(test).Result as ModelActionResult<double?>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetStringTensionAsync_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringTensionRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<double?>)null));
            var test = new GetStringTensionRequest();

            //Act
            var returnedValue = controller.GetStringTensionAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStringTensionAsync_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringTensionRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new GetStringTensionRequest();

            //Act
            var returnedValue = controller.GetStringTensionAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStringTensionAsync_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new GetStringTensionRequest();

            //Act
            var returnedValue = controller.GetStringTensionAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void GetStringSizeWithCorrepondingTensionAsync_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<List<String>>(200, new List<String>());
            var mockResponse = new StatusCodeResponse<List<String>>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringSizeWithCorrepondingTensionRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new GetStringSizeWithCorrepondingTensionRequest();

            //Act
            var returnedValue = controller.GetStringSizeWithCorrepondingTensionAsync(test).Result as ModelActionResult<List<String>>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetStringSizeWithCorrepondingTensionAsync_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringSizeWithCorrepondingTensionRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<List<String>>)null));
            var test = new GetStringSizeWithCorrepondingTensionRequest();

            //Act
            var returnedValue = controller.GetStringSizeWithCorrepondingTensionAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStringSizeWithCorrepondingTensionAsync_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringSizeWithCorrepondingTensionRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new GetStringSizeWithCorrepondingTensionRequest();

            //Act
            var returnedValue = controller.GetStringSizeWithCorrepondingTensionAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStringSizeWithCorrepondingTensionAsync_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new GetStringSizeWithCorrepondingTensionRequest();

            //Act
            var returnedValue = controller.GetStringSizeWithCorrepondingTensionAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void GetStringsSetsWithCorrepondingTensionAsync_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<List<StringsSet>>(200, new List<StringsSet>());
            var mockResponse = new StatusCodeResponse<List<StringsSet>>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsSetsWithCorrepondingTensionRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new GetStringsSetsWithCorrepondingTensionRequest();

            //Act
            var returnedValue = controller.GetStringsSetsWithCorrepondingTensionAsync(test).Result as ModelActionResult<List<StringsSet>>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetStringsSetsWithCorrepondingTensionAsync_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsSetsWithCorrepondingTensionRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<List<StringsSet>>)null));
            var test = new GetStringsSetsWithCorrepondingTensionRequest();

            //Act
            var returnedValue = controller.GetStringsSetsWithCorrepondingTensionAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStringsSetsWithCorrepondingTensionAsync_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetStringsSetsWithCorrepondingTensionRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new GetStringsSetsWithCorrepondingTensionRequest();

            //Act
            var returnedValue = controller.GetStringsSetsWithCorrepondingTensionAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetStringsSetsWithCorrepondingTensionAsync_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new GetStringsSetsWithCorrepondingTensionRequest();

            //Act
            var returnedValue = controller.GetStringsSetsWithCorrepondingTensionAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }
    }
}
