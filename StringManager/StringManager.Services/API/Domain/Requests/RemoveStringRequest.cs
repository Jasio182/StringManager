using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveStringRequest : RequestBase<String>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
