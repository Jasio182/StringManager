using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetStringsSetsHandler : IRequestHandler<GetStringsSetsRequest, StatusCodeResponse>
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

        public async Task<StatusCodeResponse> Handle(GetStringsSetsRequest request, CancellationToken cancellationToken)
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
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedStringsSets)
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new StatusCodeResponse()
                {
                    Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError)
                };
            }
        }
    }
}
