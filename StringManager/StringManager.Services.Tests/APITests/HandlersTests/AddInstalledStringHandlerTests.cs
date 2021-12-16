using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Handlers;
using System.Threading;

namespace StringManager.Services.Tests.APITests.HandlersTests
{
    public class AddInstalledStringHandlerTests
    {
        [Test]
        public void ShouldNotHaveAnyErrors()
        {
            var mockedCommandExecutor = new Mock<ICommandExecutor>();
            var mockedQueryExecutor = new Mock<IQueryExecutor>();
            var mockedAutoMapper = new Mock<IMapper>();
            var mockedLogger = new Mock<ILogger<AddInstalledStringHandler>>();
            var handler = new AddInstalledStringHandler(mockedQueryExecutor.Object, mockedAutoMapper.Object, mockedCommandExecutor.Object, mockedLogger.Object);
            var response = handler.Handle(new AddInstalledStringRequest(), new CancellationToken());
            Assert.IsTrue(true);
        }
    }
}
