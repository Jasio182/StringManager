using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace StringManager.Core.Models
{
    public class ModelResult<T>
    {
        public string Error { get; set; }
        public T Data { get; set; }
    }

    public class ModelActionResult<T> : IActionResult
    {
        public ModelResult<T> result { get; private set; }
        public readonly int statusCode;

        public ModelActionResult(int statusCode, T data, string error = null)
        {
            result = new ModelResult<T>
            {
                Data = data,
                Error = error
            };
            this.statusCode = statusCode;
        }


        async Task IActionResult.ExecuteResultAsync(ActionContext context)
        {
            if (statusCode == (int)HttpStatusCode.NoContent)
            {
                result = null;
            }
            var objectResult = new ObjectResult(result)
            {
                StatusCode = statusCode
            };
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
