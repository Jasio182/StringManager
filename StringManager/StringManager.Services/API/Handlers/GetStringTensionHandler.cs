using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using StringManager.Services.DataAnalize;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetStringTensionHandler : IRequestHandler<GetStringTensionRequest, GetStringTensionResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ILogger<GetStringTensionHandler> logger;

        public GetStringTensionHandler(IQueryExecutor queryExecutor,
                                       ILogger<GetStringTensionHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.logger = logger;
        }

        public async Task<GetStringTensionResponse> Handle(GetStringTensionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var stringQuery = new GetStringQuery()
                {
                    Id = (int)request.StringId
                };
                var stringFromDb = await queryExecutor.Execute(stringQuery);
                if (stringFromDb == null)
                {
                    logger.LogError("String of given Id of " + request.StringId + " has not been found");
                    return new GetStringTensionResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
                var toneQuery = new GetToneQuery()
                {
                    Id = (int)request.ToneId
                };
                var toneFromDb = await queryExecutor.Execute(toneQuery);
                if (toneFromDb == null)
                {
                    logger.LogError("String of given Id of " + request.ToneId + " has not been found");
                    return new GetStringTensionResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
                return new GetStringTensionResponse()
                {
                    Data = StringCalculator.GetStringTension(stringFromDb.SpecificWeight, request.ScaleLenght, toneFromDb.Frequency)
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new GetStringTensionResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
