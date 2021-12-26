using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Handlers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class AddManufacturerHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<ILogger<AddManufacturerHandler>> mockedLogger;
        private Mock<IMapper> mockedMapper;

        private AddManufacturerHandler testHandler;

        private Manufacturer testManufacturer;
        private Manufacturer testAddedManufacturer;
        private Core.Models.Manufacturer testMappedManufacturer;
        private AddManufacturerRequest testRequest;

        public AddManufacturerHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedMapper = new Mock<IMapper>();
            mockedLogger = new Mock<ILogger<AddManufacturerHandler>>();

            testHandler = new AddManufacturerHandler(mockedMapper.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testManufacturer = new Manufacturer()
            {
                Instruments = new List<Instrument>(),
                Name = "TestManufacturer",
                Strings = new List<String>()
            };
            testAddedManufacturer = new Manufacturer()
            {
                Id = 1,
                Instruments = new List<Instrument>(),
                Name = "TestManufacturer",
                Strings = new List<String>()
            };
            testMappedManufacturer = new Core.Models.Manufacturer()
            {
                Id = 1,
                Name = "TestManufacturer"
            };
            testRequest = new AddManufacturerRequest()
            {
                AccountType = Core.Enums.AccountType.Admin,
                UserId = 1,
                Name = "testManufacturer"
            };
        }

        [Test]
        public void AddManufacturerHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Manufacturer>((int)HttpStatusCode.OK, testMappedManufacturer);
            mockedMapper.Setup(x => x.Map<Manufacturer>(It.IsAny<System.Tuple<AddManufacturerRequest>>()))
                .Returns(testManufacturer);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddManufacturerCommand>())).Returns(Task.FromResult(testAddedManufacturer));
            mockedMapper.Setup(x => x.Map<Core.Models.Manufacturer>(It.IsAny<Manufacturer>())).Returns(testMappedManufacturer);

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void AddManufacturerHandler_ThrowsException()
        {
            //Arrange
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.Manufacturer>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing adding new Manufacturer item");
            mockedMapper.Setup(x => x.Map<Manufacturer>(It.IsAny<System.Tuple<AddManufacturerRequest>>()))
                .Returns(testManufacturer);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<AddManufacturerCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
