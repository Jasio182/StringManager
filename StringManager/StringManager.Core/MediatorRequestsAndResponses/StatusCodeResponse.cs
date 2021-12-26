using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses
{
    public class StatusCodeResponse<T>
    {
        public ModelActionResult<T> Result { get; set; }
    }
}
