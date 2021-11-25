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
    public class RemoveStringHandler : IRequestHandler<RemoveStringRequest, StatusCodeResponse<String>>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveStringHandler> logger;

        public RemoveStringHandler(ICommandExecutor commandExecutor,
                                   ILogger<RemoveStringHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<String>> Handle(RemoveStringRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to remove a String";
                    logger.LogError(error);
                    return new StatusCodeResponse<String>()
                    {
                        Result = new ModelActionResult<String>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var command = new RemoveStringCommand()
                {
                    Parameter = request.Id
                };
                var removedStringFromDb = await commandExecutor.Execute(command);
                if (removedStringFromDb == null)
                {
                    string error = "String of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<String>()
                    {
                        Result = new ModelActionResult<String>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                return new StatusCodeResponse<String>()
                {
                    Result = new ModelActionResult<String>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing deletion of a String; exeception:" + e + " message: " + e.Message;
                logger.LogError(e, error);
                return new StatusCodeResponse<String>()
                {
                    Result = new ModelActionResult<String>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
