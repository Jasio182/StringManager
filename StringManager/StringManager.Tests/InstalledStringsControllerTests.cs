using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StringManager.Controllers;

namespace StringManager.Tests
{
    [TestClass]
    public class InstalledStringsControllerTests
    {
        InstalledStringsController installedStringsController;

        [TestInitialize]
        public void Initialize()
        {
            var mockMediator = new Mock<IMediator>().Object;
            var logger = new Mock<ILogger<InstalledStringsController>>().Object;
            installedStringsController = new InstalledStringsController(mockMediator, logger);
        }

        [TestMethod]
        public void AddInstalledStringAsync()
        {
            Assert.IsTrue(true);
        }
    }
}
