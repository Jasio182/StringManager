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
    public class RemoveToneHandler : IRequestHandler<RemoveToneRequest, StatusCodeResponse<Tone>>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveToneHandler> logger;

        public RemoveToneHandler(ICommandExecutor commandExecutor,
                                 ILogger<RemoveToneHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<Tone>> Handle(RemoveToneRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to remove a Tone";
                    logger.LogError(error);
                    return new StatusCodeResponse<Tone>()
                    {
                        Result = new ModelActionResult<Tone>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var command = new RemoveToneCommand()
                {
                    Parameter = request.Id
                };
                var removedToneFromDb = await commandExecutor.Execute(command);
                if (removedToneFromDb == null)
                {
                    string error = "Tone of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Tone>()
                    {
                        Result = new ModelActionResult<Tone>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                return new StatusCodeResponse<Tone>()
                {
                    Result = new ModelActionResult<Tone>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing deletion of a Tone";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<Tone>()
                {
                    Result = new ModelActionResult<Tone>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
