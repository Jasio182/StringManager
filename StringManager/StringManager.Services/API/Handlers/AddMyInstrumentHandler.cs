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
    public class AddMyInstrumentHandler : IRequestHandler<AddMyInstrumentRequest, StatusCodeResponse<Core.Models.MyInstrument>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddMyInstrumentHandler> logger;

        public AddMyInstrumentHandler(IQueryExecutor queryExecutor,
                                      IMapper mapper,
                                      ICommandExecutor commandExecutor,
                                      ILogger<AddMyInstrumentHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<Core.Models.MyInstrument>> Handle(AddMyInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var queryUser = new GetUserByIdQuery()
                {
                    Id = (int)request.UserId
                };
                var userFromDb = await queryExecutor.Execute(queryUser);
                if (userFromDb == null)
                {
                    string error = "User of given Id: " + request.UserId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.MyInstrument>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                var queryInstrument = new GetInstrumentQuery()
                {
                    Id = request.InstrumentId
                };
                var instrumentFromDb = await queryExecutor.Execute(queryInstrument);
                if (instrumentFromDb == null)
                {
                    string error = "Instrument of given Id: " + request.InstrumentId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.MyInstrument>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                var myInstrumentToAdd = mapper.Map<MyInstrument>(
                    new System.Tuple<AddMyInstrumentRequest, Instrument, User>(request, instrumentFromDb, userFromDb));
                var command = new AddMyInstrumentCommand()
                {
                    Parameter = myInstrumentToAdd
                };
                var addedMyInstrument = await commandExecutor.Execute(command);
                var mappedAddedMyInstrument = mapper.Map<Core.Models.MyInstrument>(addedMyInstrument);
                return new StatusCodeResponse<Core.Models.MyInstrument>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.OK, mappedAddedMyInstrument)
                };
            }
            catch(System.Exception e)
            {
                var error = "Exception has occured during proccesing adding new MyInstrument item; exeception:" + e + " message: " + e.Message;
                logger.LogError(e, error);
                return new StatusCodeResponse<Core.Models.MyInstrument>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.MyInstrument>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
