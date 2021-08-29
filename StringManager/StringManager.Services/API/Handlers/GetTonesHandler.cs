using AutoMapper;
using MediatR;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    class GetTonesHandler : IRequestHandler<GetTonesRequest, GetTonesResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetTonesHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetTonesResponse> Handle(GetTonesRequest request, CancellationToken cancellationToken)
        {
            var query = new GetTonesQuery();
            var tonesFromDb = await queryExecutor.Execute(query);
            var mappedTones = mapper.Map<List<Tone>>(tonesFromDb);
            var response = new GetTonesResponse()
            {
                Data = mappedTones
            };
            return response;
        }
    }
}
