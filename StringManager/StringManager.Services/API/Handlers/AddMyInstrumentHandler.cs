
using AutoMapper;
using MediatR;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
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
            var queryUser = new GetUserQuery()
            {
                Id = request.UserId
            };
            var userFromDb = await queryExecutor.Execute(queryUser);
            var queryInstrument = new GetInstrumentQuery()
            {
                Id = request.InstrumentId
            };
            var instrumentFromDb = await queryExecutor.Execute(queryInstrument);
            var myInstrumentToAdd = new MyInstrument()
            {
                OwnName = request.OwnName,
                GuitarPlace = request.GuitarPlace,
                HoursPlayedWeekly = request.HoursPlayedWeekly,
                LastDeepCleaning = request.LastDeepCleaning,
                LastStringChange = request.LastStringChange,
                NextStringChange = request.NextStringChange,
                Instrument = instrumentFromDb,
                User = userFromDb
            };
            var command = new AddMyInstrumentCommand()
            {
                Parameter = myInstrumentToAdd
            };
        }
    }
}
