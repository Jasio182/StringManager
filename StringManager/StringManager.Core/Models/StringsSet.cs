using System.Collections.Generic;

namespace StringManager.Core.Models
{
    public class StringsSet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfStrings { get; set; }

        public List<StringInSet> StringsInSet { get; set; }
    }
}
