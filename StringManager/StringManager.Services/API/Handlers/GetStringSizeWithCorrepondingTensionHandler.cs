using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using StringManager.Services.DataAnalize;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetStringSizeWithCorrepondingTensionHandler : IRequestHandler<GetStringSizeWithCorrepondingTensionRequest, GetStringSizeWithCorrepondingTensionResponse>
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

        public async Task<GetStringSizeWithCorrepondingTensionResponse> Handle(GetStringSizeWithCorrepondingTensionRequest request, CancellationToken cancellationToken)
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
                    return new GetStringSizeWithCorrepondingTensionResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
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
                    logger.LogError("Tone of given Id of " + request.PrimaryToneId + " has not been found");
                    return new GetStringSizeWithCorrepondingTensionResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
                toneQuery.Id = (int)request.ResultToneId;
                var resultToneFromDb = await queryExecutor.Execute(toneQuery);
                if (resultToneFromDb == null)
                {
                    logger.LogError("Tone of given Id of " + request.ResultToneId + " has not been found");
                    return new GetStringSizeWithCorrepondingTensionResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
                var stringSize = StringCalculator.GetStringSizeWithCorrepondingTension((int)request.ScaleLength, stringFromDb,
                                                                                 stringsFromDb, primaryToneFromDb,
                                                                                 resultToneFromDb);
                var stringsOfSize = stringsFromDb.Where(thisString => thisString.Size == stringSize);
                var mappedStringsOfSize = mapper.Map<List<Core.Models.String>>(stringsOfSize);
                return new GetStringSizeWithCorrepondingTensionResponse()
                {
                    Data = mappedStringsOfSize
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new GetStringSizeWithCorrepondingTensionResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
