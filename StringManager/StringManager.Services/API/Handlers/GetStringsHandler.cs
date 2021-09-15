using AutoMapper;
using MediatR;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetStringsHandler : IRequestHandler<GetStringsRequest, GetStringsResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetStringsHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetStringsResponse> Handle(GetStringsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetStringsQuery();
                var stringsFromDb = await queryExecutor.Execute(query);
                var mappedStrings = mapper.Map<List<String>>(stringsFromDb);
                return new GetStringsResponse()
                {
                    Data = mappedStrings
                };
            }
            catch (System.Exception)
            {
                return new GetStringsResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
