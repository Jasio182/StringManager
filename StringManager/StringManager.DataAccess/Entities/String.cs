using StringManager.Core.Enums;
using System.Collections.Generic;

namespace StringManager.DataAccess.Entities
{
    public class String : EntityBase
    {
        public StringType StringType { get; set; }

        public int Size { get; set; }

        public double SpecificWeight { get; set; }

        public IEnumerable<InstalledString> InstalledStrings { get; set; }

        public IEnumerable<StringInSet> StringSets { get; set; }
    }
}
