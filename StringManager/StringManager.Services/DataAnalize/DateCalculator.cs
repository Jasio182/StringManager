using StringManager.Core.Enums;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.Services.DataAnalize
{
    public class DateCalculator
    {
        public static System.DateTime NumberOfDaysForStrings(System.DateTime dateOfChange, String[] strings, WhereGuitarKept guitarPlace, User user)
        {
            int leastNumberOfDays = strings.Select(thisString => thisString.NumberOfDaysGood).Min();
            int numberOfDays = (int)(leastNumberOfDays * GetValueFromWhereGuitarKept(guitarPlace));
            return dateOfChange.AddDays(numberOfDays);
        }

        private static double GetValueFromWhereGuitarKept(WhereGuitarKept guitarPlace)
        {
            switch (guitarPlace)
            {
                case WhereGuitarKept.Stand:
                    return 0.5;
                case WhereGuitarKept.SoftCase:
                    return 0.75;
                case WhereGuitarKept.HardCase:
                    return 1;
            }
            return 0;
        }
    }
}
