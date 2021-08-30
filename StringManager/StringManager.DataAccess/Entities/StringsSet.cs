using System.Collections.Generic;

namespace StringManager.DataAccess.Entities
{
    public class StringsSet : EntityBase
    {
        public string Name { get; set; }

        public int NumberOfStrings { get; set; }

        public IEnumerable<StringInSet> StringsInSet { get; set; }
    }
}
