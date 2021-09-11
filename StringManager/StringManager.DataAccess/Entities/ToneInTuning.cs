namespace StringManager.DataAccess.Entities
{
    public class ToneInTuning : EntityBase
    {
        public Tone Tone { get; set; }

        public Tuning Tuning { get; set; }

        public int Position { get; set; }
    }
}
