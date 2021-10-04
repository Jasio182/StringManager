using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    class AddInstalledStringHandler : IRequestHandler<AddInstalledStringRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddInstalledStringHandler> logger;

        public AddInstalledStringHandler(IQueryExecutor queryExecutor,
                                         IMapper mapper, 
                                         ICommandExecutor commandExecutor,
                                         ILogger<AddInstalledStringHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(AddInstalledStringRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var queryString = new GetStringQuery()
                {
                    Id = request.StringId
                };
                var stringFromDb = await queryExecutor.Execute(queryString);
                if (stringFromDb == null)
                {
                    string error = "String of given Id: " + request.StringId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
                    };
                }
                var queryTone = new GetToneQuery()
                {
                    Id = request.ToneId
                };
                var toneFromDb = await queryExecutor.Execute(queryTone);
                if (toneFromDb == null)
                {
                    string error = "Tone of given Id: " + request.ToneId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
                    };
                }
                var queryMyInstrument = new GetMyInstrumentQuery()
                {
                    Id = request.MyInstrumentId,
                    UserId = (int)request.UserId,
                    AccountType = (Core.Enums.AccountType)request.AccountType
                };
                var myInstrumentFromDb = await queryExecutor.Execute(queryMyInstrument);
                if (myInstrumentFromDb == null)
                {
                    string error = "MyInstrument of given Id: " + request.MyInstrumentId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
                    };
                }
                var installedStringToAdd = mapper.Map<InstalledString>(
                    new System.Tuple<AddInstalledStringRequest, MyInstrument, String, Tone>(
                        request, myInstrumentFromDb, stringFromDb, toneFromDb));
                var command = new AddInstalledStringCommand()
                {
                    Parameter = installedStringToAdd
                };
                var addedInstalledString = await commandExecutor.Execute(command);
                var mappedAddedInstalledString = mapper.Map<Core.Models.InstalledString>(addedInstalledString);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedAddedInstalledString)
                };
            }
            catch(System.Exception e)
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
