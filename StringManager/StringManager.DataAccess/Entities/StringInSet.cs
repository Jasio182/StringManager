
namespace StringManager.DataAccess.Entities
{
    public class StringInSet : EntityBase
    {
        public int Position { get; set; }
        
        public StringsSet StringsSet { get; set; }

        public String String { get; set; }
    }
}
