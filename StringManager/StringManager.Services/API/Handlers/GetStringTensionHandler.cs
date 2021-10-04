using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class GetStringTensionHandler : IRequestHandler<GetStringTensionRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ILogger<GetStringTensionHandler> logger;

        public GetStringTensionHandler(IQueryExecutor queryExecutor,
                                       ILogger<GetStringTensionHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(GetStringTensionRequest request, CancellationToken cancellationToken)
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
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
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
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
                    };
                }
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(StringCalculator.GetStringTension(stringFromDb.SpecificWeight, request.ScaleLenght, toneFromDb.Frequency))
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
