using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class RemoveToneInTuningHandler : IRequestHandler<RemoveToneInTuningRequest, StatusCodeResponse>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveToneInTuningHandler> logger;

        public RemoveToneInTuningHandler(ICommandExecutor commandExecutor,
                                         ILogger<RemoveToneInTuningHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(RemoveToneInTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to remove a ToneInTuning");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
                    };
                }
                var command = new RemoveToneInTuningCommand()
                {
                    Parameter = request.Id
                };
                var removedToneInTuningFromDb = await commandExecutor.Execute(command);
                if (removedToneInTuningFromDb == null)
                {
                    string error = "Tone of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
                    };
                }
                return new StatusCodeResponse()
                {
                    Result = new NoContentResult()
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new StatusCodeResponse()
                {
                    Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError)
                };
            }
        }
    }
}
