using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using StringManager.Services.InternalClasses;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetScaleLenghtsHandler : IRequestHandler<GetScaleLenghtsRequest, GetScaleLenghtsResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ILogger<GetScaleLenghtsHandler> logger;

        public GetScaleLenghtsHandler(IQueryExecutor queryExecutor,
                                      ILogger<GetScaleLenghtsHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.logger = logger;
        }

        public async Task<GetScaleLenghtsResponse> Handle(GetScaleLenghtsRequest request, CancellationToken cancellationToken)
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
                    logger.LogError("Instrument of given Id of " + request.InstrumentId + " has not been found");
                    return new GetScaleLenghtsResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
                return new GetScaleLenghtsResponse()
                {
                    Data = StringCalculator.GetScaleLenghtsForStrings(instrumentFromDb.ScaleLenghtBass, instrumentFromDb.ScaleLenghtTreble, instrumentFromDb.NumberOfStrings)
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new GetScaleLenghtsResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
