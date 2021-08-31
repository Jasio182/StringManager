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
    public class GetMyInstrumentHandler : IRequestHandler<GetMyInstrumentRequest, GetMyInstrumentResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetMyInstrumentHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetMyInstrumentResponse> Handle(GetMyInstrumentRequest request, CancellationToken cancellationToken)
        {
            var query = new GetMyInstrumentQuery()
            {
                Id = request.Id
            };
            var myInstrumentFromDb = await queryExecutor.Execute(query);
            var mappedMyInstrument = mapper.Map<MyInstrument>(myInstrumentFromDb);
            mappedMyInstrument.InstalledStrings = mapper.Map<List<InstalledString>>(myInstrumentFromDb.InstalledStrings);
            return new GetMyInstrumentResponse()
            {
                Data = mappedMyInstrument
            };
        }
    }
}
