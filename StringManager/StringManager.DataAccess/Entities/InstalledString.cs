namespace StringManager.DataAccess.Entities
{
    public class InstalledString : EntityBase 
    {
        public int Position { get; set; }

        public MyInstrument MyInstrument { get; set; }

        public String String { get; set; }

        public Tone Tone { get; set; }
    }
}
