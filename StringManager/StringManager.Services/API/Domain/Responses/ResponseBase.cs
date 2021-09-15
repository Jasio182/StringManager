namespace StringManager.Services.API.Domain.Responses
{
    public abstract class ResponseBase<T> : ErrorResponseBase
    {
        public T Data { get; set; }
    }
}
