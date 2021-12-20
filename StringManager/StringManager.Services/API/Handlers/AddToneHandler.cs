using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddToneHandler : IRequestHandler<AddToneRequest, StatusCodeResponse<Core.Models.Tone>>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddToneHandler> logger;

        public AddToneHandler(IMapper mapper,
                              ICommandExecutor commandExecutor,
                              ILogger<AddToneHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<Core.Models.Tone>> Handle(AddToneRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to add a new Tone";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.Tone>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var toneToAdd = mapper.Map<Tone>(request);
                var command = new AddToneCommand()
                {
                    Parameter = toneToAdd
                };
                var addedTone = await commandExecutor.Execute(command);
                var mappedAddedTone = mapper.Map<Core.Models.Tone>(addedTone);
                return new StatusCodeResponse<Core.Models.Tone>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.OK, mappedAddedTone)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing adding new Tone item";
                logger.LogError(e, error+ "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<Core.Models.Tone>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.Tone>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}
