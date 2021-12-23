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
    internal class GetTuningsHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetTuningsHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetTuningsHandler testHandler;

        private GetTuningsRequest testRequest;
        private List<Tuning> testTunings;
        private List<Core.Models.TuningList> testMappedTunings;

        public GetTuningsHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetTuningsHandler>>();

            testHandler = new GetTuningsHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testTunings = new List<Tuning>()
            {
                new Tuning()
                {
                    Id = 1,
                    Name = "testTuning1",
                    NumberOfStrings = 6,
                    TonesInTuning = new List<ToneInTuning>()
                },
                new Tuning()
                {
                    Id = 2,
                    Name = "testTuning2",
                    NumberOfStrings = 6,
                    TonesInTuning = new List<ToneInTuning>()
                }
            };
            testMappedTunings = new List<Core.Models.TuningList>()
            {
                new Core.Models.TuningList()
                {
                    Id = 1,
                    Name = "testTuning1",
                    NumberOfStrings = 6
                },
                new Core.Models.TuningList()
                {
                    Id = 2,
                    Name = "testTuning2",
                    NumberOfStrings = 6
                }
            };
            testRequest = new GetTuningsRequest()
            {
                NumberOfStrings = 6
            };
        }

        [Test]
        public void GetTuningsHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.TuningList>>((int)HttpStatusCode.OK, testMappedTunings);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningsQuery>())).Returns(Task.FromResult(testTunings));
            mockedMapper.Setup(x => x.Map<List<Core.Models.TuningList>>(It.IsAny<List<Tuning>>())).Returns(testMappedTunings);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetTuningsHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.TuningList>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting list of Tuning items");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTuningsQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
