using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.InternalClasses;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class ModifyMyInstrumentHandler : IRequestHandler<ModifyMyInstrumentRequest, StatusCodeResponse<MyInstrument>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyMyInstrumentHandler> logger;

        public ModifyMyInstrumentHandler(IQueryExecutor queryExecutor,
                                         ICommandExecutor commandExecutor,
                                         ILogger<ModifyMyInstrumentHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<MyInstrument>> Handle(ModifyMyInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var myInstrumentQuery = new GetMyInstrumentQuery()
                {
                    Id = request.Id,
                    UserId = (int)request.UserId,
                    AccountType = (Core.Enums.AccountType)request.AccountType
                };
                var myInstrumentFromDb = await queryExecutor.Execute(myInstrumentQuery);
                if (myInstrumentFromDb == null)
                {
                    string error = "MyInstrument of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<MyInstrument>()
                    {
                        Result = new ModelActionResult<MyInstrument>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                var myInstrumentToUpdate = myInstrumentFromDb;
                if (request.GuitarPlace != null)
                    myInstrumentToUpdate.GuitarPlace = (Core.Enums.WhereGuitarKept)request.GuitarPlace;
                if (request.HoursPlayedWeekly != null)
                    myInstrumentToUpdate.HoursPlayedWeekly = (int)request.HoursPlayedWeekly;
                if (request.OwnName != null)
                    myInstrumentToUpdate.OwnName = request.OwnName;
                if (request.LastDeepCleaning != null || request.LastStringChange != null)
                {
                    var dateCalculator = new DateCalculator(myInstrumentFromDb);
                    if (request.LastDeepCleaning != null)
                    {
                        myInstrumentToUpdate.LastDeepCleaning = (System.DateTime)request.LastDeepCleaning;                       
                        myInstrumentToUpdate.NextDeepCleaning = dateCalculator.NumberOfDaysForCleaning((System.DateTime)request.LastDeepCleaning);
                    }
                    if (request.LastStringChange != null)
                    {
                        myInstrumentToUpdate.LastStringChange = (System.DateTime)request.LastStringChange;
                        var currentStrings = myInstrumentFromDb.InstalledStrings.Select(InstalledString => InstalledString.String).ToArray();
                        myInstrumentToUpdate.NextStringChange = dateCalculator.NumberOfDaysForStrings((System.DateTime)request.LastStringChange, currentStrings);
                        if (myInstrumentToUpdate.NextDeepCleaning < myInstrumentToUpdate.NextStringChange)
                            myInstrumentToUpdate.NextDeepCleaning = myInstrumentToUpdate.NextStringChange;
                    }
                }
                var command = new ModifyMyInstrumentCommand()
                {
                    Parameter = myInstrumentToUpdate
                };
                await commandExecutor.Execute(command);
                return new StatusCodeResponse<MyInstrument>()
                {
                    Result = new ModelActionResult<MyInstrument>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing modyfication of a MyInstrument; exeception:" + e + " message: " + e.Message;
                logger.LogError(e, error);
                return new StatusCodeResponse<MyInstrument>()
                {
                    Result = new ModelActionResult<MyInstrument>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
