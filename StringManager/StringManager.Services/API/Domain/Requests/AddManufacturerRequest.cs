using MediatR;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddManufacturerRequest : IRequest<AddManufacturerResponse>
    {
        public string Name { get; set; }
    }
}
