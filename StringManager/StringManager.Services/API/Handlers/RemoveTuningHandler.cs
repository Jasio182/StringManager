using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class RemoveTuningHandler : IRequestHandler<RemoveTuningRequest, StatusCodeResponse<Tuning>>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveTuningHandler> logger;

        public RemoveTuningHandler(ICommandExecutor commandExecutor,
                                   ILogger<RemoveTuningHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<Tuning>> Handle(RemoveTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to remove a Tuning";
                    logger.LogError(error);
                    return new StatusCodeResponse<Tuning>()
                    {
                        Result = new ModelActionResult<Tuning>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var command = new RemoveTuningCommand()
                {
                    Parameter = request.Id
                };
                var removedTuningFromDb = await commandExecutor.Execute(command);
                if (removedTuningFromDb == null)
                {
                    string error = "Tuning of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Tuning>()
                    {
                        Result = new ModelActionResult<Tuning>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                return new StatusCodeResponse<Tuning>()
                {
                    Result = new ModelActionResult<Tuning>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing deletion of a Tuning";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<Tuning>()
                {
                    Result = new ModelActionResult<Tuning>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
