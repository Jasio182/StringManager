using NUnit.Framework;
using StringManager.DataAccess.Entities;
using StringManager.Services.InternalClasses;
using System.Collections.Generic;

namespace StringManager.Services.Tests.InternalClassesTests
{
    public class DateCalculatorTests
    {

        private Manufacturer testManufacturer = new Manufacturer()
        {
            Id = 1,
            Name = "TestManufacturer"
        };

        User user = new User()
        {
            Id = 1,
            AccountType = Core.Enums.AccountType.Admin,
            DailyMaintanance = Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings,
            PlayStyle = Core.Enums.PlayStyle.Hard
        };

        private List<String> SetupStrings(int numberOfStrings, int numberOfDaysGood)
        {
            var strings = new List<String>();
            for (int i = 1; i <= numberOfStrings; i++)
            {
                strings.Add(new String()
                {
                    Id = i,
                    NumberOfDaysGood = numberOfDaysGood,
                    Manufacturer = testManufacturer,
                    StringType = i <= 3 ? Core.Enums.StringType.PlainNikled : Core.Enums.StringType.WoundNikled,
                    Size = i * 9,
                    SpecificWeight = 0.9 * i,
                });
            }
            return strings;
        }

        private List<InstalledString> SetupInstalledStrings(int numberOfStrings)
        {
            var installedStrings = new List<InstalledString>();
            var strings = SetupStrings(numberOfStrings, 200);
            for (int i = 1; i <= numberOfStrings; i++)
            {
                installedStrings.Add(new InstalledString()
                {
                    Id = i,
                    Position = i,
                    String = strings[i-1],
                    Tone = new Tone()
                    {
                        Id = i,
                        Name = (char)65 + i + "1",
                        Frequency = 10,
                        WaveLenght = 10,
                    }
                });
            };
            return installedStrings;
        }

        private Instrument SetupInstrument(int numberOfStrings)
        {
            return new Instrument()
            {
                Id = 1,
                Manufacturer = testManufacturer,
                Model = "TestModel",
                NumberOfStrings = numberOfStrings,
                ScaleLenghtBass = 685,
                ScaleLenghtTreble = 653
            };
        }

        private MyInstrument SetupMyInstrument(int numberOfStrings, int numberOfInstalledStrings)
        {
            return new MyInstrument()
            {
                Id = 1,
                OwnName = "TestName",
                GuitarPlace = Core.Enums.WhereGuitarKept.HardCase,
                HoursPlayedWeekly = 14,
                InstalledStrings = SetupInstalledStrings(numberOfInstalledStrings),
                NeededLuthierVisit = false,
                LastDeepCleaning = new System.DateTime(2021, 03, 21),
                LastStringChange = new System.DateTime(2021, 02, 28),
                NextDeepCleaning = new System.DateTime(2021, 04, 21),
                NextStringChange = new System.DateTime(2021, 04, 25),
                Instrument = SetupInstrument(numberOfStrings),
                User = user
            };
        }

        [Test]
        public void NumberOfDaysForStrings_correctNumbers()
        {
            //Arrange
            var dateCalculator = new DateCalculator(SetupMyInstrument(6, 6));
            var testStrings = SetupStrings(6, 250).ToArray();

            //Act
            var nextStringChange = dateCalculator.NumberOfDaysForStrings(new System.DateTime(2021, 04, 20), testStrings);

            //Assert
            Assert.IsNotNull(nextStringChange);
            Assert.AreEqual(new System.DateTime(2021, 12, 01), nextStringChange);
        }

        [Test]
        public void NumberOfDaysForCleaning_changeBeforeNeededCleaning()
        {
            //Arrange
            var dateCalculator = new DateCalculator(SetupMyInstrument(6, 6));
            var testStrings = SetupStrings(6, 250).ToArray();

            //Act
            var nextInstrumentCleaning = dateCalculator.NumberOfDaysForCleaning(new System.DateTime(2021, 04, 20));

            //Assert
            Assert.IsNotNull(nextInstrumentCleaning);
            Assert.AreEqual(new System.DateTime(2022, 03, 14), nextInstrumentCleaning);
        }

        [Test]
        public void NumberOfDaysForCleaning_changeAfterNeededCleaning()
        {
            //Arrange
            var dateCalculator = new DateCalculator(SetupMyInstrument(6, 6));

            //Act
            var nextInstrumentCleaning = dateCalculator.NumberOfDaysForCleaning(new System.DateTime(2020, 04, 20));

            //Assert
            Assert.IsNotNull(nextInstrumentCleaning);
            Assert.AreEqual(new System.DateTime(2021, 04, 25), nextInstrumentCleaning);
        }
    }
}
