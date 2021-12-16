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

        private User SetupUser(int dailyMaintenance, int playStyle)
        {
            return new User()
            {
                Id = 1,
                AccountType = Core.Enums.AccountType.Admin,
                DailyMaintanance = (Core.Enums.GuitarDailyMaintanance)dailyMaintenance,
                PlayStyle = (Core.Enums.PlayStyle)playStyle
            };
        }

        private List<String> SetupStrings(int numberOfStrings, int minNumberOfDaysGood, int maxNumberOfDaysGood)
        {
            var strings = new List<String>();
            var r = new System.Random();
            for (int i = 1; i <= numberOfStrings; i++)
            {
                strings.Add(new String()
                {
                    Id = i,
                    NumberOfDaysGood = i == numberOfStrings ? minNumberOfDaysGood : r.Next(minNumberOfDaysGood, maxNumberOfDaysGood),
                    Manufacturer = testManufacturer,
                    StringType = i <= 3 ? Core.Enums.StringType.PlainNikled : Core.Enums.StringType.WoundNikled,
                    Size = i * 9,
                    SpecificWeight = 0.9 * i,
                });
            }
            return strings;
        }

        private List<InstalledString> SetupInstalledStrings(int numberOfStrings, int minNumberOfDaysGood, int maxNumberOfDaysGood)
        {
            var installedStrings = new List<InstalledString>();
            var strings = SetupStrings(numberOfStrings, minNumberOfDaysGood, maxNumberOfDaysGood);
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
                        Name = "TestTone"+i,
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

        private MyInstrument SetupMyInstrument(int numberOfStrings, int numberOfInstalledStrings, int minNumberOfDaysGood,
            int maxNumberOfDaysGood, int guitarPlace, int hoursPlayedWeekly, System.DateTime lastDeepCleaning,
            System.DateTime lastStringChange, System.DateTime nextDeepCleaning, System.DateTime nextStringChange,
            int dailyMaintenance, int playStyle)
        {
            return new MyInstrument()
            {
                Id = 1,
                OwnName = "TestName",
                GuitarPlace = (Core.Enums.WhereGuitarKept)guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                InstalledStrings = SetupInstalledStrings(numberOfInstalledStrings, minNumberOfDaysGood, maxNumberOfDaysGood),
                NeededLuthierVisit = false,
                LastDeepCleaning = lastDeepCleaning,
                LastStringChange = lastStringChange,
                NextDeepCleaning = nextDeepCleaning,
                NextStringChange = nextStringChange,
                Instrument = SetupInstrument(numberOfStrings),
                User = SetupUser(dailyMaintenance, playStyle)
            };
        }

        [Test]
        [TestCase(7, 7, 250, 360, 2, 60, "2021-03-23", "2021-03-23", "2021-07-26",
            "2021-07-20", 2, 1, 220, 280, "2021-06-13", "2021-11-27")]
        [TestCase(8, 8, 120, 500, 1, 11, "2021-02-23", "2021-02-23", "2021-04-26",
            "2021-06-20", 0, 2, 110, 295, "2021-05-13", "2021-08-11")]
        [TestCase(6, 7, 110, 200, 0, 24, "2021-08-23", "2021-08-25", "2021-10-21",
            "2021-10-20", 1, 0, 143, 210, "2021-11-13", "2022-02-03")]
        public void NumberOfDaysForStringsTest(int numberOfStrings, int numberOfInstalledStrings, int minNumberOfDaysGood,
            int maxNumberOfDaysGood, int guitarPlace, int hoursPlayedWeekly, System.DateTime lastDeepCleaning,
            System.DateTime lastStringChange, System.DateTime nextDeepCleaning, System.DateTime nextStringChange,
            int dailyMaintenance, int playStyle, int stringsMinNumberOfDaysGood, int stringsMaxNumberOfDaysGood,
            System.DateTime dateToCalculate, System.DateTime expectedDate)
        {
            //Arrange
            var dateCalculator = new DateCalculator(SetupMyInstrument(numberOfStrings, numberOfInstalledStrings, minNumberOfDaysGood,
                maxNumberOfDaysGood, guitarPlace, hoursPlayedWeekly, lastDeepCleaning, lastStringChange, nextDeepCleaning,
                nextStringChange, dailyMaintenance, playStyle));
            var testStrings = SetupStrings(numberOfStrings, stringsMinNumberOfDaysGood, stringsMaxNumberOfDaysGood).ToArray();

            //Act
            var resultNextStringChange = dateCalculator.NumberOfDaysForStrings(dateToCalculate, testStrings);

            //Assert
            Assert.IsNotNull(resultNextStringChange);
            Assert.AreEqual(expectedDate, resultNextStringChange);
        }

        [Test]
        [TestCase(7, 7, 250, 360, 2, 60, "2021-03-23", "2021-03-23", "2021-07-26",
            "2022-03-20", 2, 1, "2021-06-13", "2022-03-20")]
        [TestCase(8, 8, 120, 500, 1, 11, "2021-02-23", "2021-02-23", "2021-04-26",
            "2021-06-20", 0, 2, "2021-05-13", "2022-03-08")]
        [TestCase(6, 7, 110, 200, 0, 24, "2021-08-23", "2021-08-25", "2021-10-21",
            "2021-10-20", 1, 0, "2021-11-13", "2022-06-10")]
        public void NumberOfDaysForCleaningTest(int numberOfStrings, int numberOfInstalledStrings, int minNumberOfDaysGood,
            int maxNumberOfDaysGood, int guitarPlace, int hoursPlayedWeekly, System.DateTime lastDeepCleaning, System.DateTime lastStringChange,
            System.DateTime nextDeepCleaning, System.DateTime nextStringChange, int dailyMaintenance, int playStyle,
            System.DateTime dateToCalculate, System.DateTime expectedDate)
        {
            //Arrange
            var dateCalculator = new DateCalculator(SetupMyInstrument(numberOfStrings, numberOfInstalledStrings, minNumberOfDaysGood,
                maxNumberOfDaysGood, guitarPlace, hoursPlayedWeekly, lastDeepCleaning, lastStringChange, nextDeepCleaning,
                nextStringChange, dailyMaintenance, playStyle));

            //Act
            var nextInstrumentCleaning = dateCalculator.NumberOfDaysForCleaning(dateToCalculate);

            //Assert
            Assert.IsNotNull(nextInstrumentCleaning);
            Assert.AreEqual(expectedDate, nextInstrumentCleaning);
        }
    }
}
