using AutoMapper;
using MediatR;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddManufacturerHandler : IRequestHandler<AddManufacturerRequest, AddManufacturerResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;

        public AddManufacturerHandler(IMapper mapper, ICommandExecutor commandExecutor)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
        }

        public async Task<AddManufacturerResponse> Handle(AddManufacturerRequest request, CancellationToken cancellationToken)
        {
            var manufacturerToAdd = new Manufacturer()
            {
                Name = request.Name
            };
            var command = new AddManufacturerCommand()
            {
                Parameter = manufacturerToAdd
            };
            var addedManufacturer = await commandExecutor.Execute(command);
            var mappedAddedManufacturer = mapper.Map<Core.Models.Manufacturer>(addedManufacturer);
            return new AddManufacturerResponse()
            {
                Data = mappedAddedManufacturer
            };
        }
    }
}
