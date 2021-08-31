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
    public class GetStringsManufacturersHandler : IRequestHandler<GetStringsManufacturersRequest, GetStringsManufacturersResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetStringsManufacturersHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetStringsManufacturersResponse> Handle(GetStringsManufacturersRequest request, CancellationToken cancellationToken)
        {
            var query = new GetStringsManufacturersQuery();
            var stringsManufacturersFromDb = await queryExecutor.Execute(query);
            var mappedStringsManufacturers = mapper.Map<List<Manufacturer>>(stringsManufacturersFromDb);
            return new GetStringsManufacturersResponse()
            {
                Data = mappedStringsManufacturers
            };
        }
    }
}
