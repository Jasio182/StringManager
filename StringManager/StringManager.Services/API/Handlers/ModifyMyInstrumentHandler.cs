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
    public class ModifyMyInstrumentHandler : IRequestHandler<ModifyMyInstrumentRequest, ModifyMyInstrumentResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyMyInstrumentHandler> logger;

        public ModifyMyInstrumentHandler(IQueryExecutor queryExecutor,
                                         IMapper mapper,
                                         ICommandExecutor commandExecutor,
                                         ILogger<ModifyMyInstrumentHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<ModifyMyInstrumentResponse> Handle(ModifyMyInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var myInstrumentQuery = new GetMyInstrumentQuery()
                {
                    Id = request.Id
                };
                var myInstrumentFromDb = await queryExecutor.Execute(myInstrumentQuery);
                if (myInstrumentFromDb == null)
                {
                    logger.LogError("MyInstrument of given Id of " + request.Id + " has not been found");
                    return new ModifyMyInstrumentResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var myInstrumentToUpdate = myInstrumentFromDb;
                if (request.GuitarPlace != null)
                    myInstrumentToUpdate.GuitarPlace = (Core.Enums.WhereGuitarKept)request.GuitarPlace;
                if (request.HoursPlayedWeekly != null)
                    myInstrumentToUpdate.HoursPlayedWeekly = (int)request.HoursPlayedWeekly;
                if (request.OwnName != null)
                    myInstrumentToUpdate.OwnName = request.OwnName;
                if (request.LastDeepCleaning != null)
                    myInstrumentToUpdate.LastDeepCleaning = (System.DateTime)request.LastDeepCleaning;
                if (request.LastStringChange != null)
                    myInstrumentToUpdate.LastStringChange = (System.DateTime)request.LastStringChange;
                //myInstrumentToUpdate.NextStringChange = request.NextStringChange; //todo
                var command = new ModifyMyInstrumentCommand()
                {
                    Parameter = myInstrumentToUpdate
                };
                var modifiedMyInstrument = await commandExecutor.Execute(command);
                var mappedModifiedMyInstrument = mapper.Map<Core.Models.MyInstrument>(modifiedMyInstrument);
                return new ModifyMyInstrumentResponse()
                {
                    Data = mappedModifiedMyInstrument
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new ModifyMyInstrumentResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
