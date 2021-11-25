using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveStringInSetRequest : RequestBase<StringInSet>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
