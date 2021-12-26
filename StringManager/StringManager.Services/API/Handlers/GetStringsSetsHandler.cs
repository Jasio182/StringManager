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
    public class GetStringsSetsHandler : IRequestHandler<GetStringsSetsRequest, StatusCodeResponse<List<StringsSet>>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetStringsSetsHandler> logger;

        public GetStringsSetsHandler(IQueryExecutor queryExecutor,
                                     IMapper mapper,
                                     ILogger<GetStringsSetsHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<List<StringsSet>>> Handle(GetStringsSetsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetStringsSetsQuery()
                {
                    StringType = request.StringType
                };
                var stringsSetsFromDb = await queryExecutor.Execute(query);
                var mappedStringsSets = mapper.Map<List<StringsSet>>(stringsSetsFromDb);
                for (int i = 0; i < stringsSetsFromDb.Count; i++)
                {
                    mappedStringsSets[i].StringsInSet = mapper.Map<List<StringInSet>>(stringsSetsFromDb[i].StringsInSet);
                }
                return new StatusCodeResponse<List<StringsSet>>()
                {
                    Result = new ModelActionResult<List<StringsSet>>((int)HttpStatusCode.OK, mappedStringsSets)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting list of StringsSet items";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<List<StringsSet>>()
                {
                    Result = new ModelActionResult<List<StringsSet>>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
