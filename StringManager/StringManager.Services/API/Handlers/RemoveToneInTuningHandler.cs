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
    public class RemoveToneInTuningHandler : IRequestHandler<RemoveToneInTuningRequest, StatusCodeResponse<ToneInTuning>>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveToneInTuningHandler> logger;

        public RemoveToneInTuningHandler(ICommandExecutor commandExecutor,
                                         ILogger<RemoveToneInTuningHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<ToneInTuning>> Handle(RemoveToneInTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to remove a ToneInTuning";
                    logger.LogError(error);
                    return new StatusCodeResponse<ToneInTuning>()
                    {
                        Result = new ModelActionResult<ToneInTuning>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var command = new RemoveToneInTuningCommand()
                {
                    Parameter = request.Id
                };
                var removedToneInTuningFromDb = await commandExecutor.Execute(command);
                if (removedToneInTuningFromDb == null)
                {
                    string error = "ToneInTuning of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<ToneInTuning>()
                    {
                        Result = new ModelActionResult<ToneInTuning>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                return new StatusCodeResponse<ToneInTuning>()
                {
                    Result = new ModelActionResult<ToneInTuning>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing deletion of a ToneInTuning";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<ToneInTuning>()
                {
                    Result = new ModelActionResult<ToneInTuning>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
