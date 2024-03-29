﻿using StringManager.Core.Enums;
using System.Collections.Generic;

namespace StringManager.DataAccess.Entities
{
    public class String : EntityBase
    {
        public StringType StringType { get; set; }

        public int Size { get; set; }

        public double SpecificWeight { get; set; }

        public int NumberOfDaysGood { get; set; }

        public IEnumerable<InstalledString> InstalledStrings { get; set; }

        public IEnumerable<StringInSet> StringsInSets { get; set; }

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }
    }
}
