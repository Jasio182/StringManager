using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.InternalClasses;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetStringsSetsWithCorrepondingTensionHandler : IRequestHandler<GetStringsSetsWithCorrepondingTensionRequest, StatusCodeResponse<List<Core.Models.StringsSet>>>
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

        public async Task<StatusCodeResponse<List<StringsSet>>> Handle(GetStringsSetsWithCorrepondingTensionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var myInstrumentQuery = new GetMyInstrumentQuery()
                {
                    Id = (int)request.MyInstrumentId,
                    UserId = (int)request.UserId,
                    AccountType = (Core.Enums.AccountType)request.AccountType
                };
                var myInstrumentFromDb = await queryExecutor.Execute(myInstrumentQuery);
                if (myInstrumentFromDb == null)
                {
                    string error = "MyInstrument of given Id: " + request.MyInstrumentId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<List<StringsSet>>()
                    {
                        Result = new ModelActionResult<List<StringsSet>>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                var tuningQuery = new GetTuningQuery()
                {
                    Id = (int)request.ResultTuningId
                };
                var resultTuningFromDb = await queryExecutor.Execute(tuningQuery);
                if (resultTuningFromDb == null)
                {
                    string error = "Tuning of given Id: " + request.ResultTuningId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<List<StringsSet>>()
                    {
                        Result = new ModelActionResult<List<StringsSet>>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                if(resultTuningFromDb.NumberOfStrings != myInstrumentFromDb.Instrument.NumberOfStrings)
                {
                    string error = "Tuning of given Id: " + request.ResultTuningId + " does not have same amount of strings as specified MyInstrument of Id: " + request.MyInstrumentId;
                    logger.LogError(error);
                    return new StatusCodeResponse<List<StringsSet>>()
                    {
                        Result = new ModelActionResult<List<StringsSet>>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                var stringSetsQuery = new GetStringsSetsQuery()
                {
                    StringType = (Core.Enums.StringType)request.StringType
                };
                var stringSetsFromDb = await queryExecutor.Execute(stringSetsQuery);
                if (stringSetsFromDb == null)
                {
                    string error = "StringsSets list is empty";
                    logger.LogError(error);
                    return new StatusCodeResponse<List<StringsSet>>()
                    {
                        Result = new ModelActionResult<List<StringsSet>>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                var currentStrings = myInstrumentFromDb.InstalledStrings.Select(installedString => installedString.String).ToArray();
                var currentTuning = myInstrumentFromDb.InstalledStrings.Select(installedString => installedString.Tone).ToArray();
                var tonesFromTuning = StringCalculator.GetTonesFromTuning(resultTuningFromDb);
                var listOfStringSets = StringCalculator.GetStringsSetsWithCorrepondingTension(myInstrumentFromDb.Instrument, currentStrings, stringSetsFromDb, currentTuning, tonesFromTuning);
                var mappedListOfStringSets = mapper.Map<List<StringsSet>>(listOfStringSets);
                return new StatusCodeResponse<List<StringsSet>>()
                {
                    Result = new ModelActionResult<List<StringsSet>>((int)HttpStatusCode.OK, mappedListOfStringSets)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during calculating List of StringsSets with corresponding Tensions";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<List<StringsSet>>()
                {
                    Result = new ModelActionResult<List<StringsSet>>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
