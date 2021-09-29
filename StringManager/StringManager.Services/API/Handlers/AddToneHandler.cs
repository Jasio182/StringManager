using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddToneHandler : IRequestHandler<AddToneRequest, AddToneResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddToneHandler> logger;

        public AddToneHandler(IMapper mapper,
                              ICommandExecutor commandExecutor,
                              ILogger<AddToneHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<AddToneResponse> Handle(AddToneRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to add tone");
                    return new AddToneResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var toneToAdd = mapper.Map<Tone>(request);
                var command = new AddToneCommand()
                {
                    Parameter = toneToAdd
                };
                var addedTone = await commandExecutor.Execute(command);
                var mappedAddedTone = mapper.Map<Core.Models.Tone>(addedTone);
                return new AddToneResponse()
                {
                    Data = mappedAddedTone
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new AddToneResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
