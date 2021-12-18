namespace StringManager.DataAccess.Entities
{
    public class ToneInTuning : EntityBase
    {
        public int ToneId { get; set; }

        public Tone Tone { get; set; }

        public int TuningId { get; set; }

        public Tuning Tuning { get; set; }

        public int Position { get; set; }
    }
}
