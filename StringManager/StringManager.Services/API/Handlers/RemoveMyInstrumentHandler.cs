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
    public class RemoveMyInstrumentHandler : IRequestHandler<RemoveMyInstrumentRequest, StatusCodeResponse<MyInstrument>>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveMyInstrumentHandler> logger;

        public RemoveMyInstrumentHandler(ICommandExecutor commandExecutor,
                                         ILogger<RemoveMyInstrumentHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<MyInstrument>> Handle(RemoveMyInstrumentRequest request, CancellationToken cancellationToken)
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
                    return new StatusCodeResponse<MyInstrument>()
                    {
                        Result = new ModelActionResult<MyInstrument>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                return new StatusCodeResponse<MyInstrument>()
                {
                    Result = new ModelActionResult<MyInstrument>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing deletion of a MyInstrument";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<MyInstrument>()
                {
                    Result = new ModelActionResult<MyInstrument>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
