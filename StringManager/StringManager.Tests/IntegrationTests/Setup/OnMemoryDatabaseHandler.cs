using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess;
using StringManager.DataAccess.Entities;

namespace StringManager.Tests.IntegrationTests.Setup
{
    internal class OnMemoryDatabaseHandler
    {
        private StringManagerStorageContext context;

        public OnMemoryDatabaseHandler(StringManagerStorageContext context)
        {
            this.context = context;
        }

        private Tone[] SetupTones()
        {
            Tone[] tones =
            {
                new Tone()
                {
                    Name = "C0",
                    Frequency = 16.35,
                    WaveLenght = 2109.89
                },
                new Tone()
                {
                    Name = "C#0",
                    Frequency = 17.32,
                    WaveLenght = 1991.47
                },
                new Tone()
                {
                    Name = "D0",
                    Frequency = 18.35,
                    WaveLenght = 1879.69
                },
                new Tone()
                {
                    Name = "D#0",
                    Frequency = 19.45,
                    WaveLenght = 1774.20
                },
                new Tone()
                {
                    Name = "E0",
                    Frequency = 20.60,
                    WaveLenght = 1674.62
                },
                new Tone()
                {
                    Name = "F0",
                    Frequency = 21.83,
                    WaveLenght = 1580.63
                },
                new Tone()
                {
                    Name = "F#0",
                    Frequency = 23.12,
                    WaveLenght = 1491.91
                },
                new Tone()
                {
                    Name = "G0",
                    Frequency = 24.50,
                    WaveLenght = 1408.18
                },
                new Tone()
                {
                    Name = "G#0",
                    Frequency = 25.96,
                    WaveLenght = 1329.14
                },
                new Tone()
                {
                    Name = "A0",
                    Frequency = 27.50,
                    WaveLenght = 1254.55
                },
                new Tone()
                {
                    Name = "A#0",
                    Frequency = 29.14,
                    WaveLenght = 1184.13
                },
                new Tone()
                {
                    Name = "B0",
                    Frequency = 30.87,
                    WaveLenght = 1117.67
                },
                new Tone()
                {
                    Name = "C1",
                    Frequency = 32.70,
                    WaveLenght = 1054.94
                },
                new Tone()
                {
                    Name = "C#1",
                    Frequency = 34.65,
                    WaveLenght = 995.73
                },
                new Tone()
                {
                    Name = "D1",
                    Frequency = 36.71,
                    WaveLenght = 939.85
                },
                new Tone()
                {
                    Name = "D#1",
                    Frequency = 38.89,
                    WaveLenght = 887.10
                },
                new Tone()
                {
                    Name = "E1",
                    Frequency = 41.20,
                    WaveLenght = 837.31
                },
                new Tone()
                {
                    Name = "F1",
                    Frequency = 43.65,
                    WaveLenght = 790.31
                },
                new Tone()
                {
                    Name = "F#1",
                    Frequency = 46.25,
                    WaveLenght = 745.96
                },
                new Tone()
                {
                    Name = "G1",
                    Frequency = 49.00,
                    WaveLenght = 704.09
                },
                new Tone()
                {
                    Name = "G#1",
                    Frequency = 51.91,
                    WaveLenght = 664.57
                },
                new Tone()
                {
                    Name = "A1",
                    Frequency = 55.00,
                    WaveLenght = 627.27
                },
                new Tone()
                {
                    Name = "A#1",
                    Frequency = 58.27   ,
                    WaveLenght = 592.07
                },
                new Tone()
                {
                    Name = "B1",
                    Frequency = 61.74,
                    WaveLenght = 558.84
                },
                new Tone()
                {
                    Name = "C2",
                    Frequency = 65.41,
                    WaveLenght = 527.47
                },
                new Tone()
                {
                    Name = "C#2",
                    Frequency = 69.30,
                    WaveLenght = 497.87
                },
                new Tone()
                {
                    Name = "D2",
                    Frequency = 73.42,
                    WaveLenght = 469.92
                },
                new Tone()
                {
                    Name = "D#2",
                    Frequency = 77.78,
                    WaveLenght = 443.55
                },
                new Tone()
                {
                    Name = "E2",
                    Frequency = 82.41,
                    WaveLenght = 418.65
                },
                new Tone()
                {
                    Name = "F2",
                    Frequency = 87.31,
                    WaveLenght = 395.16
                },
                new Tone()
                {
                    Name = "F#2",
                    Frequency = 92.50,
                    WaveLenght = 372.98
                },
                new Tone()
                {
                    Name = "G2",
                    Frequency = 98.00,
                    WaveLenght = 352.04
                },
                new Tone()
                {
                    Name = "G#2",
                    Frequency = 103.83,
                    WaveLenght = 332.29
                },
                new Tone()
                {
                    Name = "A2",
                    Frequency = 110.00,
                    WaveLenght = 313.64
                },
                new Tone()
                {
                    Name = "A#2",
                    Frequency = 116.54,
                    WaveLenght = 296.03
                },
                new Tone()
                {
                    Name = "B2",
                    Frequency = 123.47,
                    WaveLenght = 279.42
                },
                new Tone()
                {
                    Name = "C3",
                    Frequency = 130.81,
                    WaveLenght = 263.74
                },
                new Tone()
                {
                    Name = "C#3",
                    Frequency = 138.59,
                    WaveLenght = 248.93
                },
                new Tone()
                {
                    Name = "D3",
                    Frequency = 146.83,
                    WaveLenght = 234.96
                },
                new Tone()
                {
                    Name = "D#3",
                    Frequency = 155.56,
                    WaveLenght = 221.77
                },
                new Tone()
                {
                    Name = "E3",
                    Frequency = 164.81,
                    WaveLenght = 209.33
                },
                new Tone()
                {
                    Name = "F3",
                    Frequency = 174.61,
                    WaveLenght = 197.58
                },
                new Tone()
                {
                    Name = "F#3",
                    Frequency = 185.00,
                    WaveLenght = 186.49
                },
                new Tone()
                {
                    Name = "G3",
                    Frequency = 196.00,
                    WaveLenght = 176.02
                },
                new Tone()
                {
                    Name = "G#3",
                    Frequency = 207.65,
                    WaveLenght = 166.14
                },
                new Tone()
                {
                    Name = "A3",
                    Frequency = 220.00,
                    WaveLenght = 156.82
                },
                new Tone()
                {
                    Name = "A#3",
                    Frequency = 233.08,
                    WaveLenght = 148.02
                },
                new Tone()
                {
                    Name = "B3",
                    Frequency = 246.94,
                    WaveLenght = 139.71
                },
                new Tone()
                {
                    Name = "C4",
                    Frequency = 261.63,
                    WaveLenght = 131.87
                },
                new Tone()
                {
                    Name = "C#4",
                    Frequency = 277.18,
                    WaveLenght = 124.47
                },
                new Tone()
                {
                    Name = "D4",
                    Frequency = 293.66,
                    WaveLenght = 117.48
                },
                new Tone()
                {
                    Name = "D#4",
                    Frequency = 311.13,
                    WaveLenght = 110.89
                },
                new Tone()
                {
                    Name = "E4",
                    Frequency = 329.63,
                    WaveLenght = 104.66
                },
                new Tone()
                {
                    Name = "F4",
                    Frequency = 349.23,
                    WaveLenght = 98.79
                },
                new Tone()
                {
                    Name = "F#4",
                    Frequency = 369.99,
                    WaveLenght = 93.24
                },
                new Tone()
                {
                    Name = "G4",
                    Frequency = 392.00,
                    WaveLenght = 88.01
                },
                new Tone()
                {
                    Name = "G#4",
                    Frequency = 415.30,
                    WaveLenght = 83.07
                },
                new Tone()
                {
                    Name = "A4",
                    Frequency = 440.00,
                    WaveLenght = 78.41
                },
                new Tone()
                {
                    Name = "A#4",
                    Frequency = 466.16,
                    WaveLenght = 74.01
                },
                new Tone()
                {
                    Name = "B4",
                    Frequency = 493.88,
                    WaveLenght = 69.85
                },
                new Tone()
                {
                    Name = "C5",
                    Frequency = 523.25,
                    WaveLenght = 65.93
                },
                new Tone()
                {
                    Name = "C#5",
                    Frequency = 554.37,
                    WaveLenght = 62.23
                },
                new Tone()
                {
                    Name = "D5",
                    Frequency = 587.33,
                    WaveLenght = 58.74
                },
                new Tone()
                {
                    Name = "D#5",
                    Frequency = 622.25,
                    WaveLenght = 55.44
                },
                new Tone()
                {
                    Name = "E5",
                    Frequency = 659.25,
                    WaveLenght = 52.33
                },
                new Tone()
                {
                    Name = "F5",
                    Frequency = 698.46,
                    WaveLenght = 49.39
                },
                new Tone()
                {
                    Name = "F#5",
                    Frequency = 739.99,
                    WaveLenght = 46.62
                },
                new Tone()
                {
                    Name = "G5",
                    Frequency = 783.99,
                    WaveLenght = 44.01
                },
                new Tone()
                {
                    Name = "G#5",
                    Frequency = 830.61,
                    WaveLenght = 41.54
                },
                new Tone()
                {
                    Name = "A55",
                    Frequency = 880.00,
                    WaveLenght = 39.20
                },
                new Tone()
                {
                    Name = "A#5",
                    Frequency = 932.33,
                    WaveLenght = 37.00
                },
                new Tone()
                {
                    Name = "B5",
                    Frequency = 987.77,
                    WaveLenght = 34.93
                },
                new Tone()
                {
                    Name = "C6",
                    Frequency = 1046.50,
                    WaveLenght = 32.97
                },
                new Tone()
                {
                    Name = "C#6",
                    Frequency = 1108.73,
                    WaveLenght = 31.12
                },
                new Tone()
                {
                    Name = "D6",
                    Frequency = 1174.66,
                    WaveLenght = 29.37
                },
                new Tone()
                {
                    Name = "D#6",
                    Frequency = 1244.51,
                    WaveLenght = 27.72
                },
                new Tone()
                {
                    Name = "E6",
                    Frequency = 1318.51,
                    WaveLenght = 26.17
                },
                new Tone()
                {
                    Name = "F6",
                    Frequency = 1396.91,
                    WaveLenght = 24.70
                },
                new Tone()
                {
                    Name = "F#6",
                    Frequency = 1479.98,
                    WaveLenght = 23.31
                },
                new Tone()
                {
                    Name = "G6",
                    Frequency = 1567.98,
                    WaveLenght = 22.00
                },
                new Tone()
                {
                    Name = "G#6",
                    Frequency = 1661.22,
                    WaveLenght = 20.77
                },
                new Tone()
                {
                    Name = "A6",
                    Frequency = 1760.00,
                    WaveLenght = 19.60
                },
                new Tone()
                {
                    Name = "A#6",
                    Frequency = 1864.66,
                    WaveLenght = 18.50
                },
                new Tone()
                {
                    Name = "B6",
                    Frequency = 1975.53,
                    WaveLenght = 17.46
                },
                new Tone()
                {
                    Name = "C7",
                    Frequency = 2093.00,
                    WaveLenght = 16.48
                },
                new Tone()
                {
                    Name = "C#7",
                    Frequency = 2217.46,
                    WaveLenght = 15.56
                },
                new Tone()
                {
                    Name = "D7",
                    Frequency = 2349.32,
                    WaveLenght = 14.69
                },
                new Tone()
                {
                    Name = "D#7",
                    Frequency = 2489.02,
                    WaveLenght = 13.86
                },
                new Tone()
                {
                    Name = "E7",
                    Frequency = 2637.02,
                    WaveLenght = 13.08
                },
                new Tone()
                {
                    Name = "F7",
                    Frequency = 2793.83,
                    WaveLenght = 12.35
                },
                new Tone()
                {
                    Name = "F#7",
                    Frequency = 2959.96,
                    WaveLenght = 11.66
                },
                new Tone()
                {
                    Name = "G7",
                    Frequency = 3135.96,
                    WaveLenght = 11.00
                },
                new Tone()
                {
                    Name = "G#7",
                    Frequency = 3322.44,
                    WaveLenght = 10.38
                },
                new Tone()
                {
                    Name = "A7",
                    Frequency = 3520.00,
                    WaveLenght = 9.80
                },
                new Tone()
                {
                    Name = "A#7",
                    Frequency = 3729.31,
                    WaveLenght = 9.25
                },
                new Tone()
                {
                    Name = "B7",
                    Frequency = 3951.07,
                    WaveLenght = 8.73
                },
                new Tone()
                {
                    Name = "C8",
                    Frequency = 4186.01,
                    WaveLenght = 8.24
                },
                new Tone()
                {
                    Name = "C#8",
                    Frequency = 4434.92,
                    WaveLenght = 7.78
                },
                new Tone()
                {
                    Name = "D8",
                    Frequency = 4698.63,
                    WaveLenght = 7.34
                },
                new Tone()
                {
                    Name = "D#8",
                    Frequency = 4978.03,
                    WaveLenght = 6.93
                },
                new Tone()
                {
                    Name = "E8",
                    Frequency = 5274.04,
                    WaveLenght = 6.54
                },
                new Tone()
                {
                    Name = "F8",
                    Frequency = 5587.65,
                    WaveLenght = 6.17
                },
                new Tone()
                {
                    Name = "F#8",
                    Frequency = 5919.91,
                    WaveLenght = 5.83
                },
                new Tone()
                {
                    Name = "G8",
                    Frequency = 6271.93,
                    WaveLenght = 5.50
                },
                new Tone()
                {
                    Name = "G#8",
                    Frequency = 6644.88,
                    WaveLenght = 5.19
                },
                new Tone()
                {
                    Name = "A8",
                    Frequency = 7040.00,
                    WaveLenght = 4.90
                },
                new Tone()
                {
                    Name = "A#8",
                    Frequency = 7458.62,
                    WaveLenght = 4.63
                },
                new Tone()
                {
                    Name = "B8",
                    Frequency = 7902.13,
                    WaveLenght = 4.37
                },
            };
            return tones;
        }

        private Tuning[] SetupTunings()
        {
            Tuning[] tunings =
            {

            };
            return tunings;
        }

        private ToneInTuning[] SetupTonesInTunings()
        {
            ToneInTuning[] tonesInTunings =
            {

            };
            return tonesInTunings;
        }

        private Manufacturer[] SetupManufactureres()
        {
            Manufacturer[] manufacturers = 
            {
                new Manufacturer()
                {
                    Name = "Ernie Ball"
                },
                new Manufacturer()
                {
                    Name = "D'Addario"
                },
                new Manufacturer()
                {
                    Name = "Elixir"
                },
                new Manufacturer()
                {
                    Name = "Fender"
                },
                new Manufacturer()
                {
                    Name = "Gibson"
                },
                new Manufacturer()
                {
                    Name = "Mayones"
                }
            };
            return manufacturers;
        }

        private String[] SetupStrings()
        {
            String[] strings = 
            {

            };
            return strings;
        }

        private StringsSet[] SetupStringsSets()
        {
            StringsSet[] stringsSets = 
            { 

            };
            return stringsSets;
        }

        private StringInSet[] SetupStringsInSets()
        {
            StringInSet[] stringsInSets =
            {

            };
            return stringsInSets;
        }

        private Instrument[] SetupInstruments()
        {
            Instrument[] instruments =
            {

            };
            return instruments;
        }

        private User[] SetupUsers()
        {
            User[] users =
            {
                new User()
                {
                    PlayStyle = Core.Enums.PlayStyle.Hard,
                    AccountType = Core.Enums.AccountType.Admin,
                    DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                    Email = "testAdminEmail",
                    Username = "testAdmin",
                    Password = "7ctgYaP9ySyZor1UNHJVMC48sr5B7J5F7yX3PT/Pot0="
                },
                new User()
                {
                    PlayStyle = Core.Enums.PlayStyle.Moderate,
                    AccountType = Core.Enums.AccountType.User,
                    DailyMaintanance = Core.Enums.GuitarDailyMaintanance.PlayAsIs,
                    Email = "testUserEmail",
                    Username = "testUser",
                    Password = "pZwdQFRFP8pAoVczHMP5Zuv9EdI1n2wgLtDxfbIlFEA="
                }
            };
            return users;
        }

        private MyInstrument[] SetupMyInstruments()
        {
            MyInstrument[] myInstruments =
            {

            };
            return myInstruments;
        }

        private InstalledString[] SetupInstalledStrings()
        {
            InstalledString[] installedStrings =
            {

            };
            return installedStrings;
        }


        public void InitializeDbForTests()
        {
            context.Database.EnsureCreated();

            foreach (var tone in SetupTones())
            {
                context.Tones.Add(tone);
            }
            foreach (var tuning in SetupTunings())
            {
                context.Tunings.Add(tuning);
            }
            foreach (var toneInTuning in SetupTonesInTunings())
            {
                context.TonesInTunings.Add(toneInTuning);
            }
            foreach (var manufacturer in SetupManufactureres())
            {
                context.Manufacturers.Add(manufacturer);
            }
            foreach (var thisString in SetupStrings())
            {
                context.Strings.Add(thisString);
            };
            foreach (var thisStringsSet in SetupStringsSets())
            {
                context.StringsSets.Add(thisStringsSet);
            };
            foreach (var thisStringInSet in SetupStringsInSets())
            {
                context.StringsInSets.Add(thisStringInSet);
            };
            foreach (var instrument in SetupInstruments())
            {
                context.Instruments.Add(instrument);
            };
          foreach (var user in SetupUsers())
            {
                context.Users.Add(user);
            };
            foreach (var myInstrument in SetupMyInstruments())
            {
                context.MyInstruments.Add(myInstrument);
            };
            foreach (var installedString in SetupInstalledStrings())
            {
                context.InstalledStrings.Add(installedString);
            };
            context.SaveChanges();
            var test = context.Users.ToListAsync();
        }

        public void DropTestDb()
        {
            context.Database.EnsureDeleted();
        }
    }
}
