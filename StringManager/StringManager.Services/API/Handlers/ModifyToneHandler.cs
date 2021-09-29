using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class ModifyToneHandler : IRequestHandler<ModifyToneRequest, ModifyToneResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyToneHandler> logger;

        public ModifyToneHandler(IQueryExecutor queryExecutor,
                                 IMapper mapper,
                                 ICommandExecutor commandExecutor,
                                 ILogger<ModifyToneHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<ModifyToneResponse> Handle(ModifyToneRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId + " tried to modify a tone");
                    return new ModifyToneResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var query = new GetToneQuery()
                {
                    Id = request.Id
                };
                var toneFromDb = await queryExecutor.Execute(query);
                if (toneFromDb == null)
                {
                    logger.LogError("User of given Id of " + query.Id + " has not been found");
                    return new ModifyToneResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var toneToUpdate = toneFromDb;
                if (request.Name != null)
                    toneToUpdate.Name = request.Name;
                if (request.Frequency != null)
                    toneToUpdate.Frequency = (int)request.Frequency;
                if (request.WaveLenght != null)
                    toneToUpdate.WaveLenght = (int)request.WaveLenght;
                var command = new ModifyToneCommand()
                {
                    Parameter = toneToUpdate
                };
                var modifiedTone = await commandExecutor.Execute(command);
                var mappedModifiedTone = mapper.Map<Core.Models.Tone>(modifiedTone);
                return new ModifyToneResponse()
                {
                    Data = mappedModifiedTone
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new ModifyToneResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
