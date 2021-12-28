using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetStringsSetHandler : IRequestHandler<GetStringsSetRequest, StatusCodeResponse<StringsSet>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetStringsSetHandler> logger;

        public GetStringsSetHandler(IQueryExecutor queryExecutor,
                                    IMapper mapper,
                                    ILogger<GetStringsSetHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<StringsSet>> Handle(GetStringsSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetStringsSetQuery()
                {
                    Id = request.Id
                };
                var stringsSetFromDb = await queryExecutor.Execute(query);
                StringsSet mappedStringsSet = null;
                if (stringsSetFromDb != null)
                {
                    mappedStringsSet = mapper.Map<StringsSet>(stringsSetFromDb);
                    mappedStringsSet.StringsInSet = mapper.Map<List<StringInSet>>(stringsSetFromDb.StringsInSet);
                }
                return new StatusCodeResponse<StringsSet>()
                {
                    Result = new ModelActionResult<StringsSet>((int)HttpStatusCode.OK, mappedStringsSet)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting StringsSet item";
                logger.LogError(e, error+ "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<StringsSet>()
                {
                    Result = new ModelActionResult<StringsSet>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
