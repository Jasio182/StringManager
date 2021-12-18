namespace StringManager.DataAccess.Entities
{
    public class InstalledString : EntityBase 
    {
        public int Position { get; set; }

        public int MyInstrumentId { get; set; }

        public MyInstrument MyInstrument { get; set; }

        public int StringId { get; set; }

        public String String { get; set; }

        public int ToneId { get; set; }

        public Tone Tone { get; set; }
    }
}
