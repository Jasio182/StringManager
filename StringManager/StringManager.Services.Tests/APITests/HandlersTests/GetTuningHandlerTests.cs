using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Handlers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class GetTuningHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetTuningHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetTuningHandler testHandler;

        private GetTuningRequest testRequest;
        private Tuning testTuning;
        private Core.Models.Tuning testMappedTuning;

        public GetTuningHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetTuningHandler>>();

            testHandler = new GetTuningHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testTuning = new Tuning()
            {
                Id = 1,
                Name = "testName",
                NumberOfStrings = 6,
                TonesInTuning = new List<ToneInTuning>()
            };
            testMappedTuning = new Core.Models.Tuning()
            {
                Id = 1,
                Name = "testName",
                NumberOfStrings = 6,
                TonesInTuning = new List<Core.Models.ToneInTuning>()
            };
            testRequest = new GetTuningRequest()
            {
                Id = 1,
            };
        }

        [Test]
        public void GetTuningHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tuning>((int)HttpStatusCode.OK, testMappedTuning);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningQuery>())).Returns(Task.FromResult(testTuning));
            mockedMapper.Setup(x => x.Map<Core.Models.Tuning>(It.IsAny<Tuning>())).Returns(testMappedTuning);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetTuningHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tuning>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting a Tuning item");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
