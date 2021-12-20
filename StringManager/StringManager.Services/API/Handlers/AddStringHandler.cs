using AutoMapper;
using MediatR;
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
    public class AddStringHandler : IRequestHandler<AddStringRequest, StatusCodeResponse<Core.Models.String>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddStringHandler> logger;

        public AddStringHandler(IQueryExecutor queryExecutor,
                                IMapper mapper,
                                ICommandExecutor commandExecutor,
                                ILogger<AddStringHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<Core.Models.String>> Handle(AddStringRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to add a new String";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.String>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var queryManufacturer = new GetManufacturerQuery()
                {
                    Id = request.ManufacturerId
                };
                var manufacturerFromDb = await queryExecutor.Execute(queryManufacturer);
                if (manufacturerFromDb == null)
                {
                    string error = "Manufacturer of given Id: " + request.ManufacturerId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.String>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                var stringToAdd = mapper.Map<String>(
                    new System.Tuple<AddStringRequest, Manufacturer>(request, manufacturerFromDb));
                var command = new AddStringCommand()
                {
                    Parameter = stringToAdd
                };
                var addedString = await commandExecutor.Execute(command);
                var mappedAddedString = mapper.Map<Core.Models.String>(addedString);
                return new StatusCodeResponse<Core.Models.String>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.OK, mappedAddedString)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing adding new String item";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<Core.Models.String>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.String>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
