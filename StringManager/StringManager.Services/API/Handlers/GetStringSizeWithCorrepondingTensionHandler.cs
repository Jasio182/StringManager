using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.InternalClasses;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetStringSizeWithCorrepondingTensionHandler : IRequestHandler<GetStringSizeWithCorrepondingTensionRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetStringSizeWithCorrepondingTensionHandler> logger;

        public GetStringSizeWithCorrepondingTensionHandler(IQueryExecutor queryExecutor,
                                                           IMapper mapper,
                                                           ILogger<GetStringSizeWithCorrepondingTensionHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(GetStringSizeWithCorrepondingTensionRequest request, CancellationToken cancellationToken)
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
                var stringsQuery = new GetStringsQuery();
                var stringsFromDb = await queryExecutor.Execute(stringsQuery);
                var toneQuery = new GetToneQuery()
                {
                    Id = (int)request.PrimaryToneId
                };
                var primaryToneFromDb = await queryExecutor.Execute(toneQuery);
                if (primaryToneFromDb == null)
                {
                    string error = "Tone of given Id: " + request.PrimaryToneId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
                    };
                }
                toneQuery.Id = (int)request.ResultToneId;
                var resultToneFromDb = await queryExecutor.Execute(toneQuery);
                if (resultToneFromDb == null)
                {
                    string error = "Tone of given Id: " + request.ResultToneId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
                    };
                }
                var stringSize = StringCalculator.GetStringSizeWithCorrepondingTension((int)request.ScaleLength, stringFromDb,
                                                                                 stringsFromDb, primaryToneFromDb,
                                                                                 resultToneFromDb);
                var stringsOfSize = stringsFromDb.Where(thisString => thisString.Size == stringSize);
                var mappedStringsOfSize = mapper.Map<List<Core.Models.String>>(stringsOfSize);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedStringsOfSize)
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
