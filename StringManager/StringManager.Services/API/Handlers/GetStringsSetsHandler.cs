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
    public class GetStringsSetsHandler : IRequestHandler<GetStringsSetsRequest, GetStringsSetsResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetStringsSetsHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetStringsSetsResponse> Handle(GetStringsSetsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetStringsSetsQuery()
                {
                    StringType = request.StringType
                };
                var stringSetsFromDb = await queryExecutor.Execute(query);
                var mappedStringSets = mapper.Map<List<StringsSet>>(stringSetsFromDb);
                for (int i = 0; i < stringSetsFromDb.Count; i++)
                {
                    mappedStringSets[i].StringsInSet = mapper.Map<List<StringInSet>>(stringSetsFromDb[i].StringsInSet);
                }
                return new GetStringsSetsResponse()
                {
                    Data = mappedStringSets
                };
            }
            catch (System.Exception)
            {
                return new GetStringsSetsResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
