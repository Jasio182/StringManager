using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
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
    internal class AddStringHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<AddStringHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private AddStringHandler testHandler;

        private String testString;
        private Manufacturer testManufacturer;
        private String testAddedString;
        private Core.Models.String testMappedString;
        private AddStringRequest testRequest;

        public AddStringHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<AddStringHandler>>();

            testHandler = new AddStringHandler(mockedQueryExecutor.Object, mockedMapper.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testManufacturer = new Manufacturer()
            {
                Id = 1,
                Name = "testName"
            };
            testString = new String()
            {
                Manufacturer = testManufacturer,
                NumberOfDaysGood = 200,
                InstalledStrings = new List<InstalledString>(),
                Size = 9,
                SpecificWeight = 0.00032037193,
                StringType = Core.Enums.StringType.PlainNikled,
                StringsInSets = new List<StringInSet>()
            };
            testAddedString = new String()
            {
                Id = 1,
                Manufacturer = testManufacturer,
                NumberOfDaysGood = 200,
                InstalledStrings = new List<InstalledString>(),
                Size = 9,
                SpecificWeight = 0.00032037193,
                StringType = Core.Enums.StringType.PlainNikled,
                StringsInSets = new List<StringInSet>()
            };
            testMappedString = new Core.Models.String()
            {
                Id = 1,
                Manufacturer = "testName",
                NumberOfDaysGood = 200,
                Size = 9,
                SpecificWeight = 0.00032037193,
                StringType = Core.Enums.StringType.PlainNikled
            };
            testRequest = new AddStringRequest()
            {
                UserId = 1,
                ManufacturerId = 1,
                NumberOfDaysGood = 200,
                Size = 9,
                SpecificWeight = 0.00032037193,
                StringType = Core.Enums.StringType.PlainNikled
            };
        }

        [Test]
        public void AddStringHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.OK, testMappedString);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetManufacturerQuery>())).Returns(Task.FromResult(testManufacturer));
            mockedMapper.Setup(x => x.Map<String>(It.IsAny<System.Tuple<AddStringRequest, Manufacturer>>()))
                .Returns(testString);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddStringCommand>())).Returns(Task.FromResult(testAddedString));
            mockedMapper.Setup(x => x.Map<Core.Models.String>(It.IsAny<String>())).Returns(testMappedString);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddStringHandler_ShouldNotHaveBeenUnauthorised()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.User;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.Unauthorized,
                null, testRequest.UserId == null ? "NonAdmin User of Id: " + testRequest.UserId : "Unregistered user" + " tried to add a new String");

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddStringHandler_ShouldManufacturerBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.BadRequest,
                null, "Manufacturer of given Id: " + testRequest.ManufacturerId + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetManufacturerQuery>())).Returns(Task.FromResult((Manufacturer)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddStringHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing adding new String item");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetManufacturerQuery>())).Returns(Task.FromResult(testManufacturer));
            mockedMapper.Setup(x => x.Map<String>(It.IsAny<System.Tuple<AddStringRequest, Manufacturer>>()))
                .Returns(testString);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddStringCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
