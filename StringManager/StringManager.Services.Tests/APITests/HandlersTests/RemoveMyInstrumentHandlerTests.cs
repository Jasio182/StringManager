using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Handlers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    internal class RemoveMyInstrumentHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<ILogger<RemoveMyInstrumentHandler>> mockedLogger;

        private RemoveMyInstrumentHandler testHandler;

        private RemoveMyInstrumentRequest testRequest;
        private MyInstrument testMyInstrument;

        public RemoveMyInstrumentHandlerTests()
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedLogger = new Mock<ILogger<RemoveMyInstrumentHandler>>();

            testHandler = new RemoveMyInstrumentHandler(mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new RemoveMyInstrumentRequest()
            {
                Id = 1
            };
            testMyInstrument = new MyInstrument()
            {
                Id = 1,
                InstalledStrings = new List<InstalledString>(),
                LastStringChange = new System.DateTime(2021, 11, 21),
                NextStringChange = new System.DateTime(2022, 2, 15),
                LastDeepCleaning = new System.DateTime(2021, 11, 21),
                NextDeepCleaning = new System.DateTime(2022, 2, 15),
                GuitarPlace = Core.Enums.WhereGuitarKept.SoftCase,
                HoursPlayedWeekly = 11,
                Instrument = new Instrument(),
                InstrumentId = 1,
                NeededLuthierVisit = true,
                OwnName = "testName",
                User = new User(),
                UserId = 1,
            };
        }

        [Test]
        public void RemoveMyInstrumentHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.NoContent, null);
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveMyInstrumentCommand>())).Returns(Task.FromResult(testMyInstrument));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveMyInstrumentHandler_ShouldMyInstrumentBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.NotFound,
                null, "MyInstrument of given Id: " + testRequest.Id + " has not been found");
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveMyInstrumentCommand>())).Returns(Task.FromResult((MyInstrument)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void RemoveMyInstrumentHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing deletion of a MyInstrument");
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<RemoveMyInstrumentCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
