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
    public class RemoveStringInSetHandler : IRequestHandler<RemoveStringInSetRequest, StatusCodeResponse<StringInSet>>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveStringInSetHandler> logger;

        public RemoveStringInSetHandler(ICommandExecutor commandExecutor,
                                        ILogger<RemoveStringInSetHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<StringInSet>> Handle(RemoveStringInSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to remove a StringInSet";
                    logger.LogError(error);
                    return new StatusCodeResponse<StringInSet>()
                    {
                        Result = new ModelActionResult<StringInSet>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var command = new RemoveStringInSetCommand()
                {
                    Parameter = request.Id
                };
                var removedStringInSetFromDb = await commandExecutor.Execute(command);
                if (removedStringInSetFromDb == null)
                {
                    string error = "StringInSet of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<StringInSet>()
                    {
                        Result = new ModelActionResult<StringInSet>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                return new StatusCodeResponse<StringInSet>()
                {
                    Result = new ModelActionResult<StringInSet>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing deletion of a StringInSet; exeception:" + e + " message: " + e.Message;
                logger.LogError(e, error);
                return new StatusCodeResponse<StringInSet>()
                {
                    Result = new ModelActionResult<StringInSet>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
