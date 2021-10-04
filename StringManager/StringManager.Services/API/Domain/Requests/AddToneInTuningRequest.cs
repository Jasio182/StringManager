namespace StringManager.Services.API.Domain.Requests
{
    public class AddToneInTuningRequest : RequestBase
    {
        public int ToneId { get; set; }

        public int TuningId { get; set; }

        public int Position { get; set; }
    }
}
