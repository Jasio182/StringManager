using StringManager.Core.Models;

namespace StringManager.Services.API.Domain
{
    public class StatusCodeResponse<T> : ResponseBase
    {
        public ModelActionResult<T> Result { get; set; }
    }
}
