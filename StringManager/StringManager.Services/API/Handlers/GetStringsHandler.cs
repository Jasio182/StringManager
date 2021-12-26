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
    public class GetStringsHandler : IRequestHandler<GetStringsRequest, StatusCodeResponse<List<String>>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetStringsHandler> logger;

        public GetStringsHandler(IQueryExecutor queryExecutor,
                                 IMapper mapper,
                                 ILogger<GetStringsHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<List<String>>> Handle(GetStringsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetStringsQuery();
                var stringsFromDb = await queryExecutor.Execute(query);
                var mappedStrings = mapper.Map<List<String>>(stringsFromDb);
                return new StatusCodeResponse<List<String>>()
                {
                    Result = new ModelActionResult<List<String>>((int)HttpStatusCode.OK, mappedStrings)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting list of String items";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<List<String>>()
                {
                    Result = new ModelActionResult<List<String>>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
