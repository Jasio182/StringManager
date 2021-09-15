using AutoMapper;
using MediatR;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class ModifyInstalledStringHandler : IRequestHandler<ModifyInstalledStringRequest, ModifyInstalledStringResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;

        public ModifyInstalledStringHandler(IQueryExecutor queryExecutor, IMapper mapper, ICommandExecutor commandExecutor)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
        }

        public async Task<ModifyInstalledStringResponse> Handle(ModifyInstalledStringRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var installedStringQuery = new GetInstalledStringQuery()
                {
                    Id = request.Id
                };
                var installedStringFromDb = await queryExecutor.Execute(installedStringQuery);
                if (installedStringFromDb == null)
                {
                    return new ModifyInstalledStringResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var installedStringToUpdate = installedStringFromDb;
                if (request.StringId != null)
                {
                    var stringQuery = new GetStringQuery()
                    {
                        Id = (int)request.StringId
                    };
                    var stringFromDb = await queryExecutor.Execute(stringQuery);
                    if (stringFromDb == null)
                    {
                        return new ModifyInstalledStringResponse()
                        {
                            Error = new ErrorModel(ErrorType.BadRequest)
                        };
                    }
                    installedStringToUpdate.String = stringFromDb;
                }
                if (request.ToneId != null)
                {
                    var toneQuery = new GetToneQuery()
                    {
                        Id = (int)request.ToneId
                    };
                    var toneFromDb = await queryExecutor.Execute(toneQuery);
                    if (toneFromDb == null)
                    {
                        return new ModifyInstalledStringResponse()
                        {
                            Error = new ErrorModel(ErrorType.BadRequest)
                        };
                    }
                    installedStringToUpdate.Tone = toneFromDb;
                }
                var command = new ModifyInstalledStringCommand()
                {
                    Parameter = installedStringToUpdate
                };
                var modifiedInstalledString = await commandExecutor.Execute(command);
                var mappedModifiedInstalledString = mapper.Map<Core.Models.InstalledString>(modifiedInstalledString);
                return new ModifyInstalledStringResponse()
                {
                    Data = mappedModifiedInstalledString
                };
            }
            catch (System.Exception)
            {
                return new ModifyInstalledStringResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}
