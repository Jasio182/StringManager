using AutoMapper;
using MediatR;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetTuningsHandler : IRequestHandler<GetTuningsRequest, GetTuningsResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetTuningsHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetTuningsResponse> Handle(GetTuningsRequest request, CancellationToken cancellationToken)
        {
            var query = new GetTuningsQuery()
            {
                NumberOfStrings = request.NumberOfStrings
            };
            var tuningsFromDb = await queryExecutor.Execute(query);
            var mappedTuningsFromDb = mapper.Map<List<Core.Models.TuningList>>(tuningsFromDb);
            return new GetTuningsResponse()
            {
                Data = mappedTuningsFromDb
            };
        }
    }
}
