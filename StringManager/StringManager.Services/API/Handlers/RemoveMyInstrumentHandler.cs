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
    public class RemoveMyInstrumentHandler : IRequestHandler<RemoveMyInstrumentRequest, StatusCodeResponse>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveMyInstrumentHandler> logger;

        public RemoveMyInstrumentHandler(ICommandExecutor commandExecutor,
                                         ILogger<RemoveMyInstrumentHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(RemoveMyInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var command = new RemoveMyInstrumentCommand()
                {
                    Parameter = request.Id
                };
                var removedMyInstrumentFromDb = await commandExecutor.Execute(command);
                if (removedMyInstrumentFromDb == null)
                {
                    string error = "MyInstrument of given Id: " + request.Id + " has not been found";
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
