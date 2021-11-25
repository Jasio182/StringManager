using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveManufacturerRequest : RequestBase<Manufacturer>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
