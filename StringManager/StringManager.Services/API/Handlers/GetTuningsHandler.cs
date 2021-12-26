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
    public class GetTuningsHandler : IRequestHandler<GetTuningsRequest, StatusCodeResponse<List<TuningList>>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetTuningsHandler> logger;

        public GetTuningsHandler(IQueryExecutor queryExecutor,
                                 IMapper mapper,
                                 ILogger<GetTuningsHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<List<TuningList>>> Handle(GetTuningsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetTuningsQuery()
                {
                    NumberOfStrings = request.NumberOfStrings
                };
                var tuningsFromDb = await queryExecutor.Execute(query);
                var mappedTuningsFromDb = mapper.Map<List<TuningList>>(tuningsFromDb);
                return new StatusCodeResponse<List<TuningList>>()
                {
                    Result = new ModelActionResult<List<TuningList>>((int)HttpStatusCode.OK, mappedTuningsFromDb)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting list of Tuning items";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<List<TuningList>>()
                {
                    Result = new ModelActionResult<List<TuningList>>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
