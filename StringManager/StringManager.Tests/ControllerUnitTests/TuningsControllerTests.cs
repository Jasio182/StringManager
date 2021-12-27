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
    public class TuningsControllerTests : ControllerTestsBase<TuningsController>
    {
        private TuningsController controller;
        private string exceptionResponse = "An error occured during preparation to send an request via controller: TuningsController";

        [SetUp]
        public void Setup()
        {
            controller = new TuningsController(mediatorMock.Object, logger);
            controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = adminClaimPrincipal
            };
        }

        [Test]
        public void GetTuning_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<Tuning>(200, new Tuning());
            var mockResponse = new StatusCodeResponse<Tuning>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new GetTuningRequest();

            //Act
            var returnedValue = controller.GetTuningAsync(test).Result as ModelActionResult<Tuning>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetTuning_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<Tuning>)null));
            var test = new GetTuningRequest();

            //Act
            var returnedValue = controller.GetTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetTuning_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetTuningRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new GetTuningRequest();

            //Act
            var returnedValue = controller.GetTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetTuning_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new GetTuningRequest();

            //Act
            var returnedValue = controller.GetTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void GetTunings_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<List<TuningList>>(200, new List<TuningList>());
            var mockResponse = new StatusCodeResponse<List<TuningList>>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetTuningsRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));

            //Act
            var returnedValue = controller.GetTuningsAsync(null).Result as ModelActionResult<List<TuningList>>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void GetTunings_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetTuningsRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<List<TuningList>>)null));

            //Act
            var returnedValue = controller.GetTuningsAsync(null).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetTunings_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetTuningsRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();

            //Act
            var returnedValue = controller.GetTuningsAsync(null).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void GetTunings_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");

            //Act
            var returnedValue = controller.GetTuningsAsync(null).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void AddTuning_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<Tuning>(200, new Tuning());
            var mockResponse = new StatusCodeResponse<Tuning>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<AddTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new AddTuningRequest();

            //Act
            var returnedValue = controller.AddTuningAsync(test).Result as ModelActionResult<Tuning>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void AddTuning_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<Tuning>)null));
            var test = new AddTuningRequest();

            //Act
            var returnedValue = controller.AddTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddTuning_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<AddTuningRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new AddTuningRequest();

            //Act
            var returnedValue = controller.AddTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void AddTuning_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new AddTuningRequest();

            //Act
            var returnedValue = controller.AddTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void ModifyTuning_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<Tuning>(204, null);
            var mockResponse = new StatusCodeResponse<Tuning>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new ModifyTuningRequest();

            //Act
            var returnedValue = controller.ModifyTuningAsync(test).Result as ModelActionResult<Tuning>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void ModifyTuning_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<Tuning>)null));
            var test = new ModifyTuningRequest();

            //Act
            var returnedValue = controller.ModifyTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyTuning_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<ModifyTuningRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new ModifyTuningRequest();

            //Act
            var returnedValue = controller.ModifyTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void ModifyTuning_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new ModifyTuningRequest();

            //Act
            var returnedValue = controller.ModifyTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }

        [Test]
        public void RemoveTuning_returnsValue()
        {
            //Arrange
            var mockResponseValue = new ModelActionResult<Tuning>(204, null);
            var mockResponse = new StatusCodeResponse<Tuning>()
            {
                Result = mockResponseValue
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse));
            var test = new RemoveTuningRequest();

            //Act
            var returnedValue = controller.RemoveTuningAsync(test).Result as ModelActionResult<Tuning>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(mockResponseValue, returnedValue);
        }

        [Test]
        public void RemoveTuning_returnsNull()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveTuningRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((StatusCodeResponse<Tuning>)null));
            var test = new RemoveTuningRequest();

            //Act
            var returnedValue = controller.RemoveTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveTuning_throwsException()
        {
            //Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoveTuningRequest>(), It.IsAny<CancellationToken>())).Throws<System.Exception>();
            var test = new RemoveTuningRequest();

            //Act
            var returnedValue = controller.RemoveTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(500, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual(exceptionResponse, returnedValue.result.Error);
        }

        [Test]
        public void RemoveTuning_invalidState()
        {
            //Arrange
            controller.ModelState.AddModelError("Data", "Data is invalid");
            var test = new RemoveTuningRequest();

            //Act
            var returnedValue = controller.RemoveTuningAsync(test).Result as ModelActionResult<object>;

            //Assert
            Assert.IsNotNull(returnedValue);
            Assert.AreEqual(400, returnedValue.statusCode);
            Assert.IsNull(returnedValue.result.Data);
            Assert.AreEqual("{\"Data\":[\"Data is invalid\"]}", returnedValue.result.Error);
        }
    }
}
