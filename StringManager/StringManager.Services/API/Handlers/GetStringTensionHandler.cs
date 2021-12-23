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
    public class GetStringTensionHandler : IRequestHandler<GetStringTensionRequest, StatusCodeResponse<double?>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ILogger<GetStringTensionHandler> logger;

        public GetStringTensionHandler(IQueryExecutor queryExecutor,
                                       ILogger<GetStringTensionHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<double?>> Handle(GetStringTensionRequest request, CancellationToken cancellationToken)
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
                    string error = "String of given Id: " + request.StringId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<double?>()
                    {
                        Result = new ModelActionResult<double?>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                var toneQuery = new GetToneQuery()
                {
                    Id = (int)request.ToneId
                };
                var toneFromDb = await queryExecutor.Execute(toneQuery);
                if (toneFromDb == null)
                {
                    string error = "Tone of given Id: " + request.ToneId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<double?>()
                    {
                        Result = new ModelActionResult<double?>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                return new StatusCodeResponse<double?>()
                {
                    Result = new ModelActionResult<double?>((int)HttpStatusCode.OK, StringCalculator.GetStringTension(stringFromDb.SpecificWeight, request.ScaleLenght, toneFromDb.Frequency))
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during calculating string tension";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<double?>()
                {
                    Result = new ModelActionResult<double?>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
