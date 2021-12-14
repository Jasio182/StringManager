using StringManager.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace StringManager.Services.InternalClasses
{
    public static class StringCalculator
    {
        public static int[] GetScaleLenghtsForStrings(int ScaleLenghtBass, int ScaleLenghtTreble, int numberOfStrings)
        {
            var result = new int[numberOfStrings];
            var scaleLengthDifference = GetStringScaleLengthDifference(ScaleLenghtBass, ScaleLenghtTreble, numberOfStrings);
            for (int i = 0; i < numberOfStrings; i++)
            {
                result[i] = ScaleLenghtTreble + scaleLengthDifference * i;
            }
            result[numberOfStrings - 1] = ScaleLenghtBass;
            return result;
        }

        public static double GetStringTension(double specificWeight, int scaleLength, double frequency)
        {
            var scaleLengthInMeters = scaleLength / 1000.0;
            return System.Math.Round(specificWeight * System.Math.Pow(2.0 * scaleLengthInMeters * frequency, 2), 3);
        }

        public static int GetStringSizeWithCorrepondingTension(int scaleLength, String currentString, IEnumerable<String> allStrings, Tone primaryTone, Tone resultTone)
        {
            var currentTension = GetStringTension(currentString.SpecificWeight, scaleLength, primaryTone.Frequency);
            double differenceOfTension = System.Math.Abs(currentTension - GetStringTension(currentString.SpecificWeight, scaleLength, resultTone.Frequency));
            int result = currentString.Size;
            var distinctStrings = allStrings.GroupBy(thisString => thisString.Size).Select(y => y.First());
            if (primaryTone.Frequency > resultTone.Frequency)
                distinctStrings = distinctStrings.Where(thisString => thisString.SpecificWeight > currentString.SpecificWeight);
            else
                distinctStrings = distinctStrings.Where(thisString => thisString.SpecificWeight <= currentString.SpecificWeight);
            foreach (var thisString in distinctStrings)
            {
                double tempDifferenceOfTension = System.Math.Abs(currentTension - GetStringTension(thisString.SpecificWeight, scaleLength, resultTone.Frequency));
                if (tempDifferenceOfTension < differenceOfTension)
                {
                    differenceOfTension = tempDifferenceOfTension;
                    result = thisString.Size;
                }
            }
            return result;
        }

        public static IEnumerable<StringsSet> GetStringsSetsWithCorrepondingTension(Instrument instrument, String[] currentStrings, IEnumerable<StringsSet> allStringsSets, Tone[] primaryTuning, Tone[] resultTuning)
        {
            var scaleLenghts = GetScaleLenghtsForStrings(instrument.ScaleLenghtBass,
                instrument.ScaleLenghtTreble, instrument.NumberOfStrings);
            var currentAverageTension = GetAverageStringTension(currentStrings,
                scaleLenghts, primaryTuning);
            var correctStringSets = allStringsSets.Where(stringSet => stringSet.NumberOfStrings == instrument.NumberOfStrings);
            List<StringsSet> result = new List<StringsSet>();
            double tensionDifference = System.Math.Abs(currentAverageTension - GetAverageStringTension(currentStrings, scaleLenghts, resultTuning));
            foreach (var stringsSet in correctStringSets)
            {
                var tempTensionDifference = System.Math.Abs(currentAverageTension - GetAverageStringTension(GetStringsFromStringSet(stringsSet), scaleLenghts, resultTuning));
                if (tempTensionDifference < tensionDifference)
                {
                    result.Clear();
                    result.Add(stringsSet);
                    tensionDifference = tempTensionDifference;
                }
                else if (tempTensionDifference == tensionDifference)
                {
                    result.Add(stringsSet);
                }
            }
            return result;
        }

        public static Tone[] GetTonesFromTuning(Tuning tuning)
        {
            return tuning.TonesInTuning.Select(toneInTuning => toneInTuning.Tone).ToArray();
        }

        private static int GetStringScaleLengthDifference(int scaleLenghtBass, int scaleLenghtTreble, int numberOfStrings)
        {
            return scaleLenghtBass != scaleLenghtTreble
                ? ((scaleLenghtBass - scaleLenghtTreble) / (numberOfStrings - 1))
                : 0;
        }

        private static double GetAverageStringTension(String[] strings, int[] scaleLenghts, Tone[] tones)
        {
            if (strings.Length != scaleLenghts.Length)
                throw new System.Exception("number of strings and scale lenghts are not equal");
            double sumOfTensions = .0;
            for (int i = 0; i < strings.Length; i++)
            {
                sumOfTensions += strings[i].SpecificWeight * System.Math.Pow(2.0 * scaleLenghts[i] * tones[i].Frequency, 2) / 386.4;
            }
            return sumOfTensions / strings.Length;
        }

        private static String[] GetStringsFromStringSet(StringsSet stringsSet)
        {
            return stringsSet.StringsInSet.Select(stringInSet => stringInSet.String).ToArray();
        }
    }
}
