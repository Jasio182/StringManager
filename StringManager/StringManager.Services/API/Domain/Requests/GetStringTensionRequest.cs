namespace StringManager.Services.API.Domain.Requests
{
    public class GetStringTensionRequest : RequestBase<double?>
    {
        public int? StringId { get; set; }

        public int? ToneId { get; set; }

        public int ScaleLenght { get; set; }
    }
}
