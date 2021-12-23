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
    internal class ModifyMyInstrumentHandlerTests
    {
        private Mock<ICommandExecutor> mockedCommandExecutor;
        private Mock<IQueryExecutor> mockedQueryExecutor;
        private Mock<ILogger<ModifyMyInstrumentHandler>> mockedLogger;

        private ModifyMyInstrumentHandler testHandler;

        private ModifyMyInstrumentRequest testRequest;
        private MyInstrument testMyInstrument;

        public ModifyMyInstrumentHandlerTests() 
        {
            mockedCommandExecutor = new Mock<ICommandExecutor>();
            mockedQueryExecutor = new Mock<IQueryExecutor>();
            mockedLogger = new Mock<ILogger<ModifyMyInstrumentHandler>>();

            testHandler = new ModifyMyInstrumentHandler(mockedQueryExecutor.Object, mockedCommandExecutor.Object, mockedLogger.Object);

            testRequest = new ModifyMyInstrumentRequest()
            {
                OwnName = "ChangedName",
                Id = 1,
                UserId = 1,
                AccountType = Core.Enums.AccountType.Admin
            };
            testMyInstrument = new MyInstrument()
            {
                InstalledStrings = new List<InstalledString>(),
                Instrument = new Instrument(),
                GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
                NeededLuthierVisit = false,
                LastDeepCleaning = new System.DateTime(2021, 11, 13),
                LastStringChange = new System.DateTime(2021, 11, 13),
                NextDeepCleaning = new System.DateTime(2022, 3, 13),
                NextStringChange = new System.DateTime(2022, 3, 13),
                HoursPlayedWeekly = 12,
                OwnName = "testOwnName",
                User = new User()
            };
        }

        [Test]
        public void ModifyMyInstrumentHandler_ShouldNotHaveAnyErrors()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.NoContent, null);
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Returns(Task.FromResult(testMyInstrument));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyMyInstrumentCommand>()));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyMyInstrumentHandler_ShouldMyInstrumentBeNull()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.NotFound,
                null, "MyInstrument of given Id: " + testRequest.Id + " has not been found");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Returns(Task.FromResult((MyInstrument)null));

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }

        [Test]
        public void ModifyMyInstrumentHandler_ThrowsException()
        {
            //Arrange
            testRequest.AccountType = Core.Enums.AccountType.Admin;
            var expectedResponse = new Core.Models.ModelActionResult<ToneInTuning>((int)HttpStatusCode.InternalServerError,
                null, "Exception has occured during proccesing modyfication of a MyInstrument");
            mockedQueryExecutor.Setup(x => x.Execute(It.IsAny<GetMyInstrumentQuery>())).Returns(Task.FromResult(testMyInstrument));
            mockedCommandExecutor.Setup(x => x.Execute(It.IsAny<ModifyMyInstrumentCommand>())).Throws(new System.Exception());

            //Act
            var response = testHandler.Handle(testRequest, new CancellationToken());

            //Assert
            Assert.AreEqual(expectedResponse.statusCode, response.Result.Result.statusCode);
            Assert.AreEqual(expectedResponse.result.Error, response.Result.Result.result.Error);
            Assert.AreEqual(expectedResponse.result.Data, response.Result.Result.result.Data);
        }
    }
}
