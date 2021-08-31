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
    class GetInstrumentsManufacturersHandler : IRequestHandler<GetInstrumentsManufacturersRequest, GetInstrumentsManufacturersResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetInstrumentsManufacturersHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetInstrumentsManufacturersResponse> Handle(GetInstrumentsManufacturersRequest request, CancellationToken cancellationToken)
        {
            var query = new GetInstrumentsManufacturersQuery();
            var instrumentsManufacturersFromDb = await queryExecutor.Execute(query);
            var mappedInstrumentsManufacturers = mapper.Map<List<Manufacturer>>(instrumentsManufacturersFromDb);
            return new GetInstrumentsManufacturersResponse()
            {
                Data = mappedInstrumentsManufacturers
            };
        }
    }
}
