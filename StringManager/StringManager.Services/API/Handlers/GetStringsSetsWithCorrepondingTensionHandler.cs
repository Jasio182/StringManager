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
    class GetStringsSetsWithCorrepondingTensionHandler : IRequestHandler<GetStringsSetsWithCorrepondingTensionRequest, StatusCodeResponse>
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

        public async Task<StatusCodeResponse> Handle(GetStringsSetsWithCorrepondingTensionRequest request, CancellationToken cancellationToken)
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
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
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
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
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
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedListOfStringSets)
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
