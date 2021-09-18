using StringManager.Core.Enums;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.Services.DataAnalize
{
    public class DateCalculator
    {
        private readonly MyInstrument myInstrument;
        private readonly User user;

        public DateCalculator(MyInstrument myInstrument)
        {
            this.myInstrument = myInstrument;
            user = myInstrument.User;
        }

        public System.DateTime NumberOfDaysForStrings(System.DateTime dateOfChange, String[] strings)
        {
            int leastNumberOfDays = strings.Select(thisString => thisString.NumberOfDaysGood).Min();
            int numberOfDays = (int)(leastNumberOfDays 
                * GetValueFromWhereGuitarKept(myInstrument.GuitarPlace) 
                * GetValueFromGuitarDailyMaintanance(user.DailyMaintanance)
                * GetValueFromPlayStyle(user.PlayStyle));
            return dateOfChange.AddDays(numberOfDays);
        }

        public System.DateTime NumberOfDaysForCleaning(System.DateTime dateOfCleaning)
        {
            int numberOfDays = (int)(365
                * GetValueFromWhereGuitarKept(myInstrument.GuitarPlace)
                * GetValueFromGuitarDailyMaintanance(user.DailyMaintanance)
                * GetValueFromPlayStyle(user.PlayStyle));
            var dateOfNextCleaning = dateOfCleaning.AddDays(numberOfDays);
            return (System.DateTime)(dateOfNextCleaning < myInstrument.NextStringChange 
                ? myInstrument.NextStringChange : dateOfNextCleaning);
        }


        private double GetValueFromWhereGuitarKept(WhereGuitarKept guitarPlace)
        {
            switch (guitarPlace)
            {
                case WhereGuitarKept.Stand:
                    return 0.75;
                case WhereGuitarKept.SoftCase:
                    return 0.82;
                case WhereGuitarKept.HardCase:
                    return 1;
            }
            return 0;
        }

        private double GetValueFromGuitarDailyMaintanance(GuitarDailyMaintanance guitarDailyMaintanance)
        {
            switch (guitarDailyMaintanance)
            {
                case GuitarDailyMaintanance.PlayAsIs:
                    return 0.5;
                case GuitarDailyMaintanance.CleanHands:
                    return 0.80;
                case GuitarDailyMaintanance.WipedString:
                    return 0.85;
                case GuitarDailyMaintanance.CleanHandsWipedStrings:
                    return 1;
            }
            return 0;
        }

        private double GetValueFromPlayStyle(PlayStyle playStyle)
        {
            switch (playStyle)
            {
                case PlayStyle.Hard:
                    return 0.9;
                case PlayStyle.Moderate:
                    return 0.95;
                case PlayStyle.Light:
                    return 1;
            }
            return 0;
        }
    }
}
