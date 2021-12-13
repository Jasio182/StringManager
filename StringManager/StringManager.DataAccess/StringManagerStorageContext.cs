using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;

namespace StringManager.DataAccess
{
    public class StringManagerStorageContext : DbContext
    {
        public StringManagerStorageContext(DbContextOptions<StringManagerStorageContext> opt) : base(opt) { }

        public DbSet<InstalledString> InstalledStrings { get; set; }

        public DbSet<Instrument> Instruments { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<MyInstrument> MyInstruments { get; set; }

        public DbSet<String> Strings { get; set; }

        public DbSet<StringInSet> StringsInSets { get; set; }

        public DbSet<StringsSet> StringsSets { get; set; }

        public DbSet<Tone> Tones { get; set; }

        public DbSet<ToneInTuning> TonesInTunings { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Tuning> Tunings { get; set; }
    }
}
