using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    public class AddMyInstrumentRequestValidatorTests
    {
        public static class SystemTime
        {
            public static System.Func<System.DateTime> Now = () => new System.DateTime(2021,01,01);
        }

        private AddMyInstrumentRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AddMyInstrumentRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, "2011-1-11", (Core.Enums.WhereGuitarKept)1, 1, "2011-1-11", 1, true, "test1", 1)]
        [TestCase((Core.Enums.AccountType)0, "2012-7-22", (Core.Enums.WhereGuitarKept)2, 11, "2020-3-14", 51, false, "test2", 2)]
        [TestCase((Core.Enums.AccountType)0, "2020-3-14", (Core.Enums.WhereGuitarKept)0, 26, "2012-7-22", 61, true, "test3", 3)]
        public void ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, System.DateTime lastStringChange, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            System.DateTime lestDeepCleaning, int instrumentId, bool neededLuthierVisit, string ownName, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = lastStringChange,
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = lestDeepCleaning,
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, "2022-1-11", (Core.Enums.WhereGuitarKept)1, 1, "2011-1-11", 1, true, "test1", 1)]
        [TestCase((Core.Enums.AccountType)0, "2023-7-22", (Core.Enums.WhereGuitarKept)2, 11, "2020-3-14", 51, false, "test2", 2)]
        [TestCase((Core.Enums.AccountType)0, "2024-3-14", (Core.Enums.WhereGuitarKept)0, 26, "2012-7-22", 61, true, "test3", 3)]
        public void ShouldHaveLastStringChangeErrors(Core.Enums.AccountType? accountType, System.DateTime lastStringChange, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            System.DateTime lestDeepCleaning, int instrumentId, bool neededLuthierVisit, string ownName, int? userId)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = lastStringChange,
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = lestDeepCleaning,
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.LastStringChange);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, "2011-1-11", (Core.Enums.WhereGuitarKept)11, 1, "2011-1-11", 1, true, "test1", 1)]
        [TestCase((Core.Enums.AccountType)0, "2012-7-22", (Core.Enums.WhereGuitarKept)21, 11, "2020-3-14", 51, false, "test2", 2)]
        [TestCase((Core.Enums.AccountType)0, "2020-3-14", (Core.Enums.WhereGuitarKept)4, 26, "2012-7-22", 61, true, "test3", 3)]
        public void ShouldHaveGuitarPlaceErrors(Core.Enums.AccountType? accountType, System.DateTime lastStringChange, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            System.DateTime lestDeepCleaning, int instrumentId, bool neededLuthierVisit, string ownName, int? userId)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = lastStringChange,
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = lestDeepCleaning,
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.GuitarPlace);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, "2011-1-11", (Core.Enums.WhereGuitarKept)1, -1, "2011-1-11", 1, true, "test1", 1)]
        [TestCase((Core.Enums.AccountType)0, "2012-7-22", (Core.Enums.WhereGuitarKept)2, -11, "2020-3-14", 51, false, "test2", 2)]
        [TestCase((Core.Enums.AccountType)0, "2020-3-14", (Core.Enums.WhereGuitarKept)0, -26, "2012-7-22", 61, true, "test3", 3)]
        public void ShouldHaveHoursPlayedWeeklyErrors(Core.Enums.AccountType? accountType, System.DateTime lastStringChange, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            System.DateTime lestDeepCleaning, int instrumentId, bool neededLuthierVisit, string ownName, int? userId)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = lastStringChange,
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = lestDeepCleaning,
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.HoursPlayedWeekly);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, "2011-1-11", (Core.Enums.WhereGuitarKept)1, 1, "2022-1-11", 1, true, "test1", 1)]
        [TestCase((Core.Enums.AccountType)0, "2012-7-22", (Core.Enums.WhereGuitarKept)2, 11, "2023-7-22", 51, false, "test2", 2)]
        [TestCase((Core.Enums.AccountType)0, "2020-3-14", (Core.Enums.WhereGuitarKept)0, 26, "2024-3-14", 61, true, "test3", 3)]
        public void ShouldHaveLastDeepCleaningErrors(Core.Enums.AccountType? accountType, System.DateTime lastStringChange, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            System.DateTime lestDeepCleaning, int instrumentId, bool neededLuthierVisit, string ownName, int? userId)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = lastStringChange,
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = lestDeepCleaning,
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.LastDeepCleaning);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, "2011-1-11", (Core.Enums.WhereGuitarKept)1, 1, "2011-1-11", -1, true, "test1", 1)]
        [TestCase((Core.Enums.AccountType)0, "2012-7-22", (Core.Enums.WhereGuitarKept)2, 11, "2020-3-14", -51, false, "test2", 2)]
        [TestCase((Core.Enums.AccountType)0, "2020-3-14", (Core.Enums.WhereGuitarKept)0, 26, "2012-7-22", 0, true, "test3", 3)]
        public void ShouldHaveInstrumentIdErrors(Core.Enums.AccountType? accountType, System.DateTime lastStringChange, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            System.DateTime lestDeepCleaning, int instrumentId, bool neededLuthierVisit, string ownName, int? userId)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = lastStringChange,
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = lestDeepCleaning,
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.InstrumentId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, "2011-1-11", (Core.Enums.WhereGuitarKept)1, 1, "2011-1-11", 1, true, null, 1)]
        public void ShouldHaveOwnNameErrors(Core.Enums.AccountType? accountType, System.DateTime lastStringChange, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            System.DateTime lestDeepCleaning, int instrumentId, bool neededLuthierVisit, string ownName, int? userId)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = lastStringChange,
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = lestDeepCleaning,
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.OwnName);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)11, "2011-1-11", (Core.Enums.WhereGuitarKept)1, -1, "2011-1-11", 1, true, "test1", 1)]
        [TestCase((Core.Enums.AccountType)6, "2012-7-22", (Core.Enums.WhereGuitarKept)2, -11, "2020-3-14", 51, false, "test2", 2)]
        [TestCase(null, "2020-3-14", (Core.Enums.WhereGuitarKept)0, -26, "2012-7-22", 61, true, "test3", 3)]
        public void ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, System.DateTime lastStringChange, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            System.DateTime lestDeepCleaning, int instrumentId, bool neededLuthierVisit, string ownName, int? userId)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = lastStringChange,
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = lestDeepCleaning,
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, "2011-1-11", (Core.Enums.WhereGuitarKept)1, -1, "2011-1-11", 1, true, "test1", 0)]
        [TestCase((Core.Enums.AccountType)0, "2012-7-22", (Core.Enums.WhereGuitarKept)2, -11, "2020-3-14", 51, false, "test2", -6)]
        [TestCase((Core.Enums.AccountType)0, "2020-3-14", (Core.Enums.WhereGuitarKept)0, -26, "2012-7-22", 61, true, "test3", null)]
        public void ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, System.DateTime lastStringChange, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            System.DateTime lestDeepCleaning, int instrumentId, bool neededLuthierVisit, string ownName, int? userId)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = lastStringChange,
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = lestDeepCleaning,
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.HoursPlayedWeekly);
        }
    }
}
