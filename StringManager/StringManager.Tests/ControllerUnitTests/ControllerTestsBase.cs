using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using StringManager.Core.Enums;
using System.Security.Claims;

namespace StringManager.Tests.ControllerUnitTests
{
    public abstract class ControllerTestsBase<T>
    {
        protected Mock<IMediator> mediatorMock;
        protected ILogger<T> logger;

        public ControllerTestsBase()
        {
            mediatorMock = new Mock<IMediator>();
            logger = new Mock<ILogger<T>>().Object;
        }

        protected ClaimsPrincipal adminClaimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, AccountType.Admin.ToString()),
                }, "TestAuthentication"));
    }
}
