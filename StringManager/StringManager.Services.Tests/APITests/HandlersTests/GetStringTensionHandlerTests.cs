using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Handlers;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class GetStringTensionHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetStringTensionHandler>> mockedLogger;

        private GetStringTensionHandler testHandler;

        private double testResultStringTension;
        private String testString;
        private Tone testTone;
        private GetStringTensionRequest testRequest;

        public GetStringTensionHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedLogger = new Mock<ILogger<GetStringTensionHandler>>();

            testHandler = new GetStringTensionHandler(mockedQueryExecutor.Object, mockedLogger.Object);

            testRequest = new GetStringTensionRequest()
            {
                ScaleLenght = 628,
                StringId = 1,
                ToneId = 1
            };
            testString = new String()
            {
                Id = 1,
                Size = 52,
                SpecificWeight = 0.00002,
                StringType = Core.Enums.StringType.PlainNikled,
                ManufacturerId = 1,
                NumberOfDaysGood = 328
            };
            testTone = new Tone()
            {
                Id = 1,
                Frequency = 328.6,
                Name = "testTone",
                WaveLenght = 22.3
            };
            testResultStringTension = 3.407;
        }

        [Test]
        public void GetStringTensionHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<double?>((int)HttpStatusCode.OK, testResultStringTension);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult(testTone));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringTensionHandler_ShouldToneBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<double?>((int)HttpStatusCode.BadRequest,
                null, "Tone of given Id: " + testRequest.ToneId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Returns(Task.FromResult((Tone)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringTensionHandler_ShouldStringBeNull()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<double?>((int)HttpStatusCode.BadRequest,
                null, "String of given Id: " + testRequest.StringId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult((String)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringTensionHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<double?>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during calculating string tension");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringQuery>())).Returns(Task.FromResult(testString));
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetToneQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
