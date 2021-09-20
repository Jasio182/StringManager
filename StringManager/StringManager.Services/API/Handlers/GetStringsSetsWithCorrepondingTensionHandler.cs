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
    class GetStringsSetsWithCorrepondingTensionHandler : IRequestHandler<GetStringsSetsWithCorrepondingTensionRequest, GetStringsSetsWithCorrepondingTensionResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetStringsSetsWithCorrepondingTensionHandler> logger;

        public GetStringsSetsWithCorrepondingTensionHandler(IQueryExecutor queryExecutor,
                                                            IMapper mapper,
                                                            ILogger<GetStringsSetsWithCorrepondingTensionHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<GetStringsSetsWithCorrepondingTensionResponse> Handle(GetStringsSetsWithCorrepondingTensionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var myInstrumentQuery = new GetMyInstrumentQuery()
                {
                    Id = (int)request.MyInstrumentId
                };
                var myInstrumentFromDb = await queryExecutor.Execute(myInstrumentQuery);
                if (myInstrumentFromDb == null)
                {
                    logger.LogError("MyInstrument of given Id of " + request.MyInstrumentId + " has not been found");
                    return new GetStringsSetsWithCorrepondingTensionResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
                var tuningQuery = new GetTuningQuery()
                {
                    Id = (int)request.ResultTuningId
                };
                var resultTuningFromDb = await queryExecutor.Execute(tuningQuery);
                if (resultTuningFromDb == null)
                {
                    logger.LogError("Tuning of given Id of " + request.ResultTuningId + " has not been found");
                    return new GetStringsSetsWithCorrepondingTensionResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
                var stringSetsQuery = new GetStringsSetsQuery()
                {
                    StringType = (Core.Enums.StringType)request.StringType
                };
                var stringSetsFromDb = await queryExecutor.Execute(stringSetsQuery);
                var currentStrings = myInstrumentFromDb.InstalledStrings.Select(installedString => installedString.String).ToArray();
                var currentTuning = myInstrumentFromDb.InstalledStrings.Select(installedString => installedString.Tone).ToArray();
                var tonesFromTuning = StringCalculator.GetTonesFromTuning(resultTuningFromDb);
                var listOfStringSets = StringCalculator.GetStringsSetsWithCorrepondingTension(myInstrumentFromDb.Instrument, currentStrings, stringSetsFromDb, currentTuning, tonesFromTuning);
                var mappedListOfStringSets = mapper.Map<List<Core.Models.StringsSet>>(listOfStringSets);
                return new GetStringsSetsWithCorrepondingTensionResponse()
                {
                    Data = mappedListOfStringSets
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new GetStringsSetsWithCorrepondingTensionResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
