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
    public class GetStringsSetHandler : IRequestHandler<GetStringsSetRequest, GetStringsSetResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetStringsSetHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetStringsSetResponse> Handle(GetStringsSetRequest request, CancellationToken cancellationToken)
        {
            var query = new GetStringsSetQuery()
            {
                Id = request.Id
            };
            var stringsSetFromDb = await queryExecutor.Execute(query);
            var mappedStringsSet = mapper.Map<StringsSet>(stringsSetFromDb);
            mappedStringsSet.StringsInSet = mapper.Map<List<StringInSet>>(stringsSetFromDb.StringsInSet);
            return new GetStringsSetResponse()
            {
                Data = mappedStringsSet
            };
        }
    }
}
