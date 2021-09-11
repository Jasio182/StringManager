using AutoMapper;
using MediatR;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
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
            var myInstrumentQuery = new GetMyInstrumentQuery()
            {
                Id = request.Id
            };
            var myInstrumentFromDb = await queryExecutor.Execute(myInstrumentQuery);
            var myInstrumentToUpdate = myInstrumentFromDb;
            myInstrumentToUpdate.OwnName = request.OwnName;
            myInstrumentToUpdate.LastDeepCleaning = request.LastDeepCleaning;
            myInstrumentToUpdate.LastStringChange = request.LastStringChange;
            myInstrumentToUpdate.NextStringChange = request.NextStringChange;
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
    }
}
