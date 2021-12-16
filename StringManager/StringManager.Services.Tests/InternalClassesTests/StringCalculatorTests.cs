using NUnit.Framework;
using StringManager.DataAccess.Entities;
using StringManager.Services.InternalClasses;
using System.Collections.Generic;
using System.Linq;

namespace StringManager.Services.Tests.InternalClassesTests
{
    public class StringCalculatorTests
    {
        [Test]
        [TestCase(new int[] { 653, 658, 663, 668, 673, 678, 685 }, 685, 653, 7)]
        [TestCase(new int[] { 628, 628, 628, 628, 628, 628 }, 628, 628, 6)]
        [TestCase(new int[] { 653, 668, 683, 698, 713, 728, 743, 762 }, 762, 653, 8)]
        public void GetScaleLenghtsForStringsTest(int[] expectedResult, int scaleLenghtBass, int scaleLenghtTreble, int numberOfStrings)
        {
            //Act
            var scaleLenghts = StringCalculator.GetScaleLenghtsForStrings(scaleLenghtBass, scaleLenghtTreble, numberOfStrings);

            //Asset
            Assert.IsNotNull(scaleLenghts);
            Assert.AreEqual(expectedResult, scaleLenghts);
        }

        [Test]
        [TestCase(0.00227867663, 653, 174.61, 118.497)]
        [TestCase(0.01188304861, 685, 61.74, 85.016)]
        [TestCase(0.00032037193, 628, 329.63, 54.915)]
        public void GetStringTensionTest(double specificWeight, int scaleLenght, double frequency, double expectedResult)
        {
            //Act
            var stringTension = StringCalculator.GetStringTension(specificWeight, scaleLenght, frequency);

            //Asset
            Assert.IsNotNull(stringTension);
            Assert.AreEqual(expectedResult, stringTension);
        }

        [Test]
        public void GetTonesInTuningTest()
        {
            //Arrange
            var testTonesInTuning = new List<ToneInTuning>();
            for (int i = 1; i <= 6; i++)
            {
                testTonesInTuning.Add(new ToneInTuning()
                {
                    Tone = new Tone()
                    {
                        Name = "Tone_" + i,
                        Id = i,
                        Frequency = 20.3 * i,
                        WaveLenght = 22.5 * i
                    },
                });
            }
            var testTuning = new Tuning()
            {
                Id = 1,
                NumberOfStrings = 6,
                Name = "testTuning",
                TonesInTuning = testTonesInTuning
            };

            //Act
            var tonesFromTuning = StringCalculator.GetTonesFromTuning(testTuning);

            //Asset
            Assert.IsNotNull(tonesFromTuning);
            Assert.AreEqual(testTonesInTuning.Select(x => x.Tone).ToArray(), tonesFromTuning);
        }

        [Test]
        public void GetStringSizeWithCorrepondingTensionTest()
        {
            //Arrange
            const int testScaleLenght = 653;
            var testCurrentString = new String()
            {
                Id = 1,
                Size = 9,
                StringType = Core.Enums.StringType.PlainNikled,
                NumberOfDaysGood = 180,
                SpecificWeight = 0.00001794 * 17.8579673228
            };
            var testAllStrings = new List<String>() { testCurrentString };
            for (int i = 2; i <= 20; i++)
            {
                testAllStrings.Add(new String()
                {
                    Id = i,
                    NumberOfDaysGood = 180,
                    StringType = i < 5 ? Core.Enums.StringType.PlainNikled : Core.Enums.StringType.WoundNikled,
                    Size = 9 + i,
                    SpecificWeight = (0.00002215 + 0.000004 * i) * 17.8579673228
                });
            }
            var testPrimaryTone = new Tone()
            {
                Id = 1,
                Name = "F3",
                Frequency = 174.61,
                WaveLenght = 197.58
            };
            var testResultTone = new Tone()
            {
                Id = 2,
                Name = "D3",
                Frequency = 146.83,
                WaveLenght = 234.96
            };

            //Act
            var stringSizeWithCorrepondingTension = StringCalculator.GetStringSizeWithCorrepondingTension(testScaleLenght, testCurrentString, testAllStrings, testPrimaryTone, testResultTone);

            //Asset
            Assert.IsNotNull(stringSizeWithCorrepondingTension);
            Assert.AreEqual(11, stringSizeWithCorrepondingTension);
        }

        [Test]
        public void GetStringsSetsWithCorrepondingTensionTest()
        {
            //Arrange
            var testInstrument = new Instrument()
            {
                Id = 1,
                Model = "TestModel",
                NumberOfStrings = 7,
                ScaleLenghtBass = 685,
                ScaleLenghtTreble = 653
            };
            var testAllStrings = new List<String>();
            for (int i = 0; i < 60; i++)
            {
                testAllStrings.Add(new String()
                {
                    Id = i + 1,
                    NumberOfDaysGood = 180,
                    StringType = i < 10 ? Core.Enums.StringType.PlainNikled : Core.Enums.StringType.WoundNikled,
                    Size = 9 + i,
                    SpecificWeight = (0.00002215 + 0.000004 * i) * 17.8579673228
                });
            }
            var testCurrentStrings = new String[]
            {
                testAllStrings[1],
                testAllStrings[4],
                testAllStrings[8],
                testAllStrings[17],
                testAllStrings[27],
                testAllStrings[37],
                testAllStrings[47]
            };
            var testAllStringSets = new List<StringsSet>();
            for (int i = 0; i < 5; i++)
            {
                var stringsInSet = new List<StringInSet>();
                for (int j = 0; j < 7; j++)
                {
                    stringsInSet.Add(new StringInSet()
                    {
                        Id = i + 1,
                        Position = j + 1,
                        String = testAllStrings[i + j * 8]
                    });
                }
                testAllStringSets.Add(new StringsSet()
                {
                    Id = i + 1,
                    Name = "Set" + (i + 1),
                    NumberOfStrings = 7,
                    StringsInSet = stringsInSet
                });
            }
            var testPrimaryTuning = new Tone[]
            {
                new Tone()
                {
                    Id = 1,
                    Name = "B1",
                    Frequency = 61.74,
                    WaveLenght = 558.84
                },
                new Tone()
                {
                    Id = 2,
                    Name = "E2",
                    Frequency = 82.41,
                    WaveLenght = 418.65
                },
                new Tone()
                {
                    Id = 3,
                    Name = "A2",
                    Frequency = 110.00,
                    WaveLenght = 313.64
                },
                new Tone()
                {
                    Id = 4,
                    Name = "D3",
                    Frequency = 146.83,
                    WaveLenght = 234.96
                },
                new Tone()
                {
                    Id= 5,
                    Name = "G3",
                    Frequency = 196.00,
                    WaveLenght = 176.02
                },
                new Tone()
                {
                    Id= 6,
                    Name = "B3",
                    Frequency = 246.94,
                    WaveLenght = 139.71
                },
                new Tone()
                {
                    Id= 7,
                    Name = "E4",
                    Frequency = 329.63,
                    WaveLenght = 104.66
                }
            };
            var testResultTuning = testPrimaryTuning;

            //Act
            IEnumerable<StringsSet> stringsSetsWithCorrepondingTension = StringCalculator.GetStringsSetsWithCorrepondingTension(testInstrument, testCurrentStrings, testAllStringSets, testPrimaryTuning, testResultTuning);

            //Asset
            Assert.IsNotNull(stringsSetsWithCorrepondingTension);
            Assert.AreEqual(testAllStringSets[0], stringsSetsWithCorrepondingTension.First());
        }
    }
}