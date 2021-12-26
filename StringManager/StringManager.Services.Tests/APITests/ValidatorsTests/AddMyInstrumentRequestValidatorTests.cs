using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    public class AddMyInstrumentRequestValidatorTests
    {
        private static readonly System.DateTime[] correctDates =
        {
            System.DateTime.Now.Subtract(System.TimeSpan.FromDays(28)),
            System.DateTime.Now.Subtract(System.TimeSpan.FromDays(365)),
            System.DateTime.Now.Subtract(System.TimeSpan.FromDays(422))
        };

        private static readonly System.DateTime[] incorrectDates =
{
            System.DateTime.Now.AddDays(28),
            System.DateTime.Now.AddDays(365),
            System.DateTime.Now.AddDays(422)
        };

        private AddMyInstrumentRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AddMyInstrumentRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)1, 1, 1, true, "test1", 1, 0)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)2, 11, 51, false, "test2", 2, 1)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)0, 26, 61, true, "test3", 3, 2)]
        public void AddMyInstrumentRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            int instrumentId, bool neededLuthierVisit, string ownName, int? userId, int i)
        {
            var testAddInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)1, 1, 1, true, "test1", 1, 0)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)2, 11, 51, false, "test2", 2, 1)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)0, 26, 61, true, "test3", 3, 2)]
        public void AddMyInstrumentRequestValidator_ShouldHaveLastStringChangeErrors(Core.Enums.AccountType? accountType, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            int instrumentId, bool neededLuthierVisit, string ownName, int? userId, int i)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = incorrectDates[i],
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.LastStringChange);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)11, 1, 1, true, "test1", 1, 0)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)21, 11, 51, false, "test2", 2, 1)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)4, 26, 61, true, "test3", 3, 2)]
        public void AddMyInstrumentRequestValidator_ShouldHaveGuitarPlaceErrors(Core.Enums.AccountType? accountType, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            int instrumentId, bool neededLuthierVisit, string ownName, int? userId, int i)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.GuitarPlace);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)1, -1, 1, true, "test1", 1, 0)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)2, -11, 51, false, "test2", 2, 1)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)0, -26, 61, true, "test3", 3, 2)]
        public void AddMyInstrumentRequestValidator_ShouldHaveHoursPlayedWeeklyErrors(Core.Enums.AccountType? accountType, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            int instrumentId, bool neededLuthierVisit, string ownName, int? userId, int i)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.HoursPlayedWeekly);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)1, 1, 1, true, "test1", 1, 0)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)2, 11, 51, false, "test2", 2, 1)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)0, 26, 61, true, "test3", 3, 2)]
        public void AddMyInstrumentRequestValidator_ShouldHaveLastDeepCleaningErrors(Core.Enums.AccountType? accountType, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            int instrumentId, bool neededLuthierVisit, string ownName, int? userId, int i)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = incorrectDates[i],
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.LastDeepCleaning);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)1, 1, -1, true, "test1", 1, 0)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)2, 11, -51, false, "test2", 2, 1)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)0, 26, 0, true, "test3", 3, 2)]
        public void AddMyInstrumentRequestValidator_ShouldHaveInstrumentIdErrors(Core.Enums.AccountType? accountType, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            int instrumentId, bool neededLuthierVisit, string ownName, int? userId, int i)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.InstrumentId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)1, 1, 1, true, null, 1, 0)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)1, 1, 1, true, "", 1, 0)]
        public void AddMyInstrumentRequestValidator_ShouldHaveOwnNameErrors(Core.Enums.AccountType? accountType, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            int instrumentId, bool neededLuthierVisit, string ownName, int? userId, int i)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.OwnName);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)11, (Core.Enums.WhereGuitarKept)1, 1, -1, true, "test1", 1, 0)]
        [TestCase((Core.Enums.AccountType)(-6), (Core.Enums.WhereGuitarKept)2, 11, -51, false, "test2", 2, 1)]
        [TestCase((Core.Enums.AccountType)12, (Core.Enums.WhereGuitarKept)0, 26, 0, true, "test3", 3, 2)]
        public void AddMyInstrumentRequestValidator_ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            int instrumentId, bool neededLuthierVisit, string ownName, int? userId, int i)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)1, 1, 1, true, "test1", 0, 0)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)2, 11, 51, false, "test2", -6, 1)]
        [TestCase((Core.Enums.AccountType)0, (Core.Enums.WhereGuitarKept)0, 26, 61, true, "test3", -3, 2)]
        public void AddMyInstrumentRequestValidator_ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, Core.Enums.WhereGuitarKept guitarPlace, int hoursPlayedWeekly,
            int instrumentId, bool neededLuthierVisit, string ownName, int? userId, int i)
        {
            var testAddMyInstrumentRequestRequest = new AddMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                GuitarPlace = guitarPlace,
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
                InstrumentId = instrumentId,
                NeededLuthierVisit = neededLuthierVisit,
                OwnName = ownName
            };
            var result = validator.TestValidate(testAddMyInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }
    }
}
