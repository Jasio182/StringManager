﻿using System.Collections.Generic;

namespace StringManager.Core.Models
{
    public class MyInstrument
    {
        public int Id { get; set; }

        public string OwnName { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public int NumberOfStrings { get; set; }

        public int ScaleLenghtBass { get; set; }

        public int ScaleLenghtTreble { get; set; }

        public List<InstalledString> InstalledStrings { get; set; }
    }
}
