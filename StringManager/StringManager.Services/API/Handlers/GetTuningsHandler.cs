using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
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
    public class GetTuningsHandler : IRequestHandler<GetTuningsRequest, GetTuningsResponse>
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

        public async Task<GetTuningsResponse> Handle(GetTuningsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetTuningsQuery()
                {
                    NumberOfStrings = request.NumberOfStrings
                };
                var tuningsFromDb = await queryExecutor.Execute(query);
                var mappedTuningsFromDb = mapper.Map<List<Core.Models.TuningList>>(tuningsFromDb);
                return new GetTuningsResponse()
                {
                    Data = mappedTuningsFromDb
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new GetTuningsResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
