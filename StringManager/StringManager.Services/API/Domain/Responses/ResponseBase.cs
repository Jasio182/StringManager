namespace StringManager.Services.API.Domain.Responses
{
    public abstract class ResponseBase<T>
    {
        public T Data { get; set; }
    }
}
