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
    internal class GetStringsSetHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetStringsSetHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetStringsSetHandler testHandler;

        private GetStringsSetRequest testRequest;
        private StringsSet testStringsSet;
        private Core.Models.StringsSet testMappedStringsSet;

        public GetStringsSetHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetStringsSetHandler>>();

            testHandler = new GetStringsSetHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testStringsSet = new StringsSet()
            {
                Id = 1,
                StringsInSet = new List<StringInSet>(),
                NumberOfStrings = 6,
                Name = "testSet1"
            };
            testMappedStringsSet = new Core.Models.StringsSet()
            {
                Id = 1,
                NumberOfStrings = 6,
                Name = "testSet1",
                StringsInSet = new List<Core.Models.StringInSet>()
            };
            testRequest = new GetStringsSetRequest()
            {
                AccountType = Core.Enums.AccountType.Admin,
                Id = 1,
                UserId = 1
            };
        }

        [Test]
        public void GetStringsSetHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringsSet>((int)HttpStatusCode.OK, testMappedStringsSet);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetQuery>())).Returns(Task.FromResult(testStringsSet));
            mockedMapper.Setup(x => x.Map<Core.Models.StringsSet>(It.IsAny<StringsSet>())).Returns(testMappedStringsSet);
            mockedMapper.Setup(x => x.Map<List<Core.Models.StringInSet>>(It.IsAny<List<StringInSet>>())).Returns(new List<Core.Models.StringInSet>());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringsSetHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.StringsSet>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting StringsSet item");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsSetQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
