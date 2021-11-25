using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveInstalledStringRequest : RequestBase<InstalledString>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
