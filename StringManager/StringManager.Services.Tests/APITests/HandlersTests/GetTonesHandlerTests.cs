using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Handlers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class GetTonesHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetTonesHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetTonesHandler testHandler;

        private GetTonesRequest testRequest;
        private List<Tone> testTones;
        private List<Core.Models.Tone> testMappedTones;

        public GetTonesHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetTonesHandler>>();

            testHandler = new GetTonesHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testTones = new List<Tone>()
            {
                new Tone()
                {
                    Id = 1,
                    Frequency = 0.1,
                    InstalledStrings = new List<InstalledString>(),
                    Name = "testTone1",
                    TonesInTuning = new List<ToneInTuning>(),
                    WaveLenght = 0.1
                },
                new Tone()
                {
                    Id = 2,
                    Frequency = 0.2,
                    InstalledStrings = new List<InstalledString>(),
                    Name = "testTone2",
                    TonesInTuning = new List<ToneInTuning>(),
                    WaveLenght = 0.2
                }
            };
            testMappedTones = new List<Core.Models.Tone>()
            {
                new Core.Models.Tone()
                {
                    Id = 1,
                    Frequency = 0.1,
                    Name = "testTone1",
                    WaveLenght = 0.1
                },
                new Core.Models.Tone()
                {
                    Id = 2,
                    Frequency = 0.2,
                    Name = "testTone2",
                    WaveLenght = 0.2
                }
            };
            testRequest = new GetTonesRequest();
        }

        [Test]
        public void GetTonesHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.Tone>>((int)HttpStatusCode.OK, testMappedTones);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTonesQuery>())).Returns(Task.FromResult(testTones));
            mockedMapper.Setup(x => x.Map<List<Core.Models.Tone>>(It.IsAny<List<Tone>>())).Returns(testMappedTones);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetTonesHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting list of Tone items");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetTonesQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
