using AutoMapper;
using MediatR;
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

        public ModifyMyInstrumentHandler(IQueryExecutor queryExecutor, IMapper mapper, ICommandExecutor commandExecutor)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
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
            catch (System.Exception)
            {
                return new ModifyMyInstrumentResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
