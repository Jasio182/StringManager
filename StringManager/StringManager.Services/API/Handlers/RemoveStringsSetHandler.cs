using MediatR;
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
    public class RemoveStringsSetHandler : IRequestHandler<RemoveStringsSetRequest, StatusCodeResponse<StringsSet>>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveStringsSetHandler> logger;

        public RemoveStringsSetHandler(ICommandExecutor commandExecutor,
                                       ILogger<RemoveStringsSetHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<StringsSet>> Handle(RemoveStringsSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to remove a StringsSet";
                    logger.LogError(error);
                    return new StatusCodeResponse<StringsSet>()
                    {
                        Result = new ModelActionResult<StringsSet>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var command = new RemoveStringsSetCommand()
                {
                    Parameter = request.Id
                };
                var removedStringsSetFromDb = await commandExecutor.Execute(command);
                if (removedStringsSetFromDb == null)
                {
                    string error = "StringsSet of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<StringsSet>()
                    {
                        Result = new ModelActionResult<StringsSet>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                return new StatusCodeResponse<StringsSet>()
                {
                    Result = new ModelActionResult<StringsSet>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing deletion of a StringsSet";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<StringsSet>()
                {
                    Result = new ModelActionResult<StringsSet>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
