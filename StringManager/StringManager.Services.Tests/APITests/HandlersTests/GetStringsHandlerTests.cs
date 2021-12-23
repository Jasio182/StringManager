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
    internal class GetStringsHandlerTests
    {
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<GetStringsHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private GetStringsHandler testHandler;

        private GetStringsRequest testRequest;
        private List<String> testStrings;
        private List<Core.Models.String> testMappedStrings;

        public GetStringsHandlerTests()
        {
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<GetStringsHandler>>();

            testHandler = new GetStringsHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedLogger.Object);

            testStrings = new List<String>()
            {
                new String()
                {
                    Id = 1,
                    InstalledStrings = new List<InstalledString>(),
                    Size = 9,
                    SpecificWeight = 0.1,
                    StringsInSets = new List<StringInSet>(),
                    StringType = Core.Enums.StringType.PlainBrass,
                    Manufacturer = new Manufacturer(),
                    ManufacturerId = 1,
                    NumberOfDaysGood = 12
                },
                new String()
                {
                    Id = 2,
                    InstalledStrings = new List<InstalledString>(),
                    Size = 9,
                    SpecificWeight = 0.1,
                    StringsInSets = new List<StringInSet>(),
                    StringType = Core.Enums.StringType.PlainBrass,
                    Manufacturer = new Manufacturer(),
                    ManufacturerId = 1,
                    NumberOfDaysGood = 12
                }
            };
            testMappedStrings = new List<Core.Models.String>()
            {
                new Core.Models.String()
                {
                    Id = 1,
                    Size = 9,
                    SpecificWeight = 0.1,
                    StringType = Core.Enums.StringType.PlainBrass,
                    Manufacturer = "testManufacturer1",
                    NumberOfDaysGood = 12
                },
                new Core.Models.String()
                {
                    Id = 2,
                    Size = 9,
                    SpecificWeight = 0.1,
                    StringType = Core.Enums.StringType.PlainBrass,
                    Manufacturer = "testManufacturer2",
                    NumberOfDaysGood = 12
                }
            };
            testRequest = new GetStringsRequest();
        }

        [Test]
        public void GetStringsHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<List<Core.Models.String>>((int)HttpStatusCode.OK, testMappedStrings);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsQuery>())).Returns(Task.FromResult(testStrings));
            mockedMapper.Setup(x => x.Map<List<Core.Models.String>>(It.IsAny<List<String>>())).Returns(testMappedStrings);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void GetStringsHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing getting list of String items");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetStringsQuery>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
