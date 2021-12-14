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
        public void GetScaleLenghtsForStringsTest()
        {
            //Arrange
            var expectedResult = new int[] {653, 658, 663, 668, 673, 678, 685 };

            //Act
            var scaleLenghts = StringCalculator.GetScaleLenghtsForStrings(685, 653, 7);
            
            //Asset
            Assert.IsNotNull(scaleLenghts);
            Assert.AreEqual(expectedResult, scaleLenghts);
        }

        [Test]
        public void GetStringTensionTest()
        {
            //Arrange
            double expectedResult = 118.497;

            //Act
            var stringTension = StringCalculator.GetStringTension(0.00227867663, 653, 174.61);

            //Asset
            Assert.IsNotNull(stringTension);
            Assert.AreEqual(expectedResult, stringTension);
        }

        [Test]
        public void GetTonesInTuningTest()
        {
            //Arrange
            var testTonesInTuning = new List<ToneInTuning>();
            for(int i = 1; i<=6;i++)
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
            Assert.AreEqual(testTonesInTuning.Select(x=>x.Tone).ToArray(), tonesFromTuning);
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
                SpecificWeight = 0.00001794*17.8579673228
            };
            var testAllStrings = new List<String>() { testCurrentString };
            for(int i = 2; i <= 20; i++)
            {
                testAllStrings.Add(new String()
                {
                    Id = i,
                    NumberOfDaysGood = 180,
                    StringType = i < 5 ? Core.Enums.StringType.PlainNikled : Core.Enums.StringType.WoundNikled,
                    Size = 9+i,
                    SpecificWeight = (0.00002215+0.000004*i)*17.8579673228
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
            Assert.AreEqual(stringSizeWithCorrepondingTension, 11);
        }

        [Test]
        public void GetStringsSetsWithCorrepondingTensionTest()
        {
            //Arrange
            const int testScaleLenght = 653;

            //Act
            //var stringSizeWithCorrepondingTension = StringCalculator.GetStringsSetsWithCorrepondingTension();

            //Asset
            //Assert.IsNotNull(stringSizeWithCorrepondingTension);
            //Assert.AreEqual(stringSizeWithCorrepondingTension, 11);
        }
    }
}
