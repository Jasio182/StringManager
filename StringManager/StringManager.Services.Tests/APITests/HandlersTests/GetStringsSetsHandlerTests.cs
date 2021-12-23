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
    internal class GetStringsSetsHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetStringsSetsHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetStringsSetsHandler testHandler;

        private GetStringsSetsRequest testRequest;
        private List<StringsSet> testStringsSet;
        private List<Core.Models.StringsSet> testMappedStringsSet;

        public GetStringsSetsHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetStringsSetsHandler>>();

            testHandler = new GetStringsSetsHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testStringsSet = new List<StringsSet>()
            {
                new StringsSet()
                {
                    Id = 1,
                    StringsInSet = new List<StringInSet>(),
                    NumberOfStrings = 6,
                    Name = "testSet1"
                }
            };
            testMappedStringsSet = new List<Core.Models.StringsSet>()
            {
                new Core.Models.StringsSet()
                {
                    Id = 1,
                    NumberOfStrings = 6,
                    Name = "testSet1",
                    StringsInSet = new List<Core.Models.StringInSet>()
                }
            };
            testRequest = new GetStringsSetsRequest()
            {
                AccountType = Core.Enums.AccountType.Admin,
                UserId = 1
            };
        }

        [Test]
        public void GetStringsSetsHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.StringsSet>>((int)HttpStatusCode.OK, testMappedStringsSet);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetsQuery>())).Returns(Task.FromResult(testStringsSet));
            mockedMapper.Setup(x => x.Map< List<Core.Models.StringsSet>>(It.IsAny<List<StringsSet>>())).Returns(testMappedStringsSet);
            mockedMapper.Setup(x => x.Map<List<Core.Models.StringInSet>>(It.IsAny<List<StringInSet>>())).Returns(new List<Core.Models.StringInSet>());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringsSetsHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.StringsSet>>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting list of StringsSet items");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetsQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
