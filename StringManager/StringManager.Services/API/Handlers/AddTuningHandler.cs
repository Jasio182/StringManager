using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddTuningHandler : IRequestHandler<AddTuningRequest, StatusCodeResponse<Core.Models.Tuning>>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddTuningHandler> logger;

        public AddTuningHandler(IMapper mapper,
                                ICommandExecutor commandExecutor,
                                ILogger<AddTuningHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<Core.Models.Tuning>> Handle(AddTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to add a new Tuning";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.Tuning>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.Tuning>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var tuningToAdd = mapper.Map<Tuning>(request);
                var command = new AddTuningCommand()
                {
                    Parameter = tuningToAdd
                };
                var addedTuning = await commandExecutor.Execute(command);
                var mappedAddedTuning = mapper.Map<Core.Models.Tuning>(addedTuning);
                return new StatusCodeResponse<Core.Models.Tuning>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.Tuning>((int)HttpStatusCode.OK, mappedAddedTuning)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing adding new Tuning item";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<Core.Models.Tuning>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.Tuning>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
