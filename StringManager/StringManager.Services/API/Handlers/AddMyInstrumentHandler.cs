
using AutoMapper;
using MediatR;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddMyInstrumentHandler : IRequestHandler<AddMyInstrumentRequest, AddMyInstrumentResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;

        public AddMyInstrumentHandler(IQueryExecutor queryExecutor, IMapper mapper, ICommandExecutor commandExecutor)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
        }

        public async Task<AddMyInstrumentResponse> Handle(AddMyInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var queryUser = new GetUserQuery()
                {
                    Id = request.UserId
                };
                var userFromDb = await queryExecutor.Execute(queryUser);
                if (userFromDb == null)
                {
                    return new AddMyInstrumentResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
                var queryInstrument = new GetInstrumentQuery()
                {
                    Id = request.InstrumentId
                };
                var instrumentFromDb = await queryExecutor.Execute(queryInstrument);
                if (instrumentFromDb == null)
                {
                    return new AddMyInstrumentResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
                var myInstrumentToAdd = new MyInstrument()
                {
                    OwnName = request.OwnName,
                    GuitarPlace = request.GuitarPlace,
                    HoursPlayedWeekly = request.HoursPlayedWeekly,
                    LastDeepCleaning = request.LastDeepCleaning,
                    LastStringChange = request.LastStringChange,
                    //NextStringChange = request.NextStringChange,  // todo
                    Instrument = instrumentFromDb,
                    User = userFromDb
                };
                var command = new AddMyInstrumentCommand()
                {
                    Parameter = myInstrumentToAdd
                };
                var addedMyInstrument = await commandExecutor.Execute(command);
                var mappedAddedMyInstrument = mapper.Map<Core.Models.MyInstrument>(addedMyInstrument);
                return new AddMyInstrumentResponse()
                {
                    Data = mappedAddedMyInstrument
                };
            }
            catch(System.Exception)
            {
                return new AddMyInstrumentResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
