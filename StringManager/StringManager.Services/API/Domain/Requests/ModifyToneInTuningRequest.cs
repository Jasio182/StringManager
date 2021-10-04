namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyToneInTuningRequest : RequestBase
    {
        public int Id { get; set; }

        public int? ToneId { get; set; }

        public int? TuningId { get; set; }

        public int? Position { get; set; }
    }
}
