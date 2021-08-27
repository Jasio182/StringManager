using StringManager.Core.Enums;
using System.Collections.Generic;

namespace StringManager.DataAccess.Entities
{
    public class String : EntityBase
    {
        public StringType StringType { get; set; }

        public int Size { get; set; }

        public double SpecificWeight { get; set; }

        public List<InstalledString> InstalledStrings { get; set; }

        public List<StringInSet> StringSets { get; set; }
    }
}
