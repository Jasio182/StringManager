using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class RemoveInstalledStringHandler : IRequestHandler<RemoveInstalledStringRequest, StatusCodeResponse<InstalledString>>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveInstalledStringHandler> logger;

        public RemoveInstalledStringHandler(ICommandExecutor commandExecutor,
                                            ILogger<RemoveInstalledStringHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<InstalledString>> Handle(RemoveInstalledStringRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to remove an InstalledString";
                    logger.LogError(error);
                    return new StatusCodeResponse<InstalledString>()
                    {
                        Result = new ModelActionResult<InstalledString>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var command = new RemoveInstalledStringCommand()
                {
                    Parameter = request.Id
                };
                var removedInstalledStringFromDb = await commandExecutor.Execute(command);
                if (removedInstalledStringFromDb == null)
                {
                    string error = "Instrument of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<InstalledString>()
                    {
                        Result = new ModelActionResult<InstalledString>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                return new StatusCodeResponse<InstalledString>()
                {
                    Result = new ModelActionResult<InstalledString>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing deletion of an InstalledString";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<InstalledString>()
                {
                    Result = new ModelActionResult<InstalledString>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
