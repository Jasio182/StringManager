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
    public class RemoveInstrumentHandler : IRequestHandler<RemoveInstrumentRequest, StatusCodeResponse<Instrument>>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveInstrumentHandler> logger;

        public RemoveInstrumentHandler(ICommandExecutor commandExecutor,
                                       ILogger<RemoveInstrumentHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<Instrument>> Handle(RemoveInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to remove an Instrument";
                    logger.LogError(error);
                    return new StatusCodeResponse<Instrument>()
                    {
                        Result = new ModelActionResult<Instrument>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var command = new RemoveInstrumentCommand()
                {
                    Parameter = request.Id
                };
                var removedInstrumentFromDb = await commandExecutor.Execute(command);
                if (removedInstrumentFromDb == null)
                {
                    string error = "Instrument of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Instrument>()
                    {
                        Result = new ModelActionResult<Instrument>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                return new StatusCodeResponse<Instrument>()
                {
                    Result = new ModelActionResult<Instrument>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing deletion of an Instrument";
                logger.LogError(e, error+"; exeception: " + e + " message: " + e.Message);
                return new StatusCodeResponse<Instrument>()
                {
                    Result = new ModelActionResult<Instrument>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
