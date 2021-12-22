using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.InternalClasses;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetScaleLenghtsHandler : IRequestHandler<GetScaleLenghtsRequest, StatusCodeResponse<int[]>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ILogger<GetScaleLenghtsHandler> logger;

        public GetScaleLenghtsHandler(IQueryExecutor queryExecutor,
                                      ILogger<GetScaleLenghtsHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<int[]>> Handle(GetScaleLenghtsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetInstrumentQuery()
                {
                    Id = (int)request.InstrumentId
                };
                var instrumentFromDb = await queryExecutor.Execute(query);
                if (instrumentFromDb == null)
                {
                    string error = "Instrument of given Id: " + request.InstrumentId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<int[]>()
                    {
                        Result = new ModelActionResult<int[]>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                return new StatusCodeResponse<int[]>()
                {
                    Result = new ModelActionResult<int[]>((int)HttpStatusCode.OK, StringCalculator.GetScaleLenghtsForStrings(instrumentFromDb.ScaleLenghtBass, instrumentFromDb.ScaleLenghtTreble, instrumentFromDb.NumberOfStrings))
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during calculating strings scale lenghts for Instrument";
                logger.LogError(e, error+ "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<int[]>()
                {
                    Result = new ModelActionResult<int[]>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
