using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class ModifyMyInstrumentRequestValidatorTests
    {
        private static System.DateTime[] correctDates =
        {
            System.DateTime.Now.Subtract(System.TimeSpan.FromDays(28)),
            System.DateTime.Now.Subtract(System.TimeSpan.FromDays(365)),
            System.DateTime.Now.Subtract(System.TimeSpan.FromDays(422))
        };

        private static System.DateTime[] incorrectDates =
{
            System.DateTime.Now.AddDays(28),
            System.DateTime.Now.AddDays(365),
            System.DateTime.Now.AddDays(422)
        };

        private ModifyMyInstrumentRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new ModifyMyInstrumentRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 0)]
        [TestCase((Core.Enums.AccountType)1, 6, 7, 1, 1)]
        [TestCase((Core.Enums.AccountType)0, 9, 12, 1, 2)]
        public void ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int id, int hoursPlayedWeekly, int i)
        {
            var testModifyMyInstrumentRequest = new ModifyMyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
            };
            var result = validator.TestValidate(testModifyMyInstrumentRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 0)]
        [TestCase((Core.Enums.AccountType)1, 6, 7, 1, 1)]
        [TestCase((Core.Enums.AccountType)0, 9, 12, 1, 2)]
        public void ShouldHaveLastStringChangeErrors(Core.Enums.AccountType? accountType, int? userId, int id, int hoursPlayedWeekly, int i)
        {
            var testModifyMyInstrumentRequest = new ModifyMyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                LastStringChange = incorrectDates[i],
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
            };
            var result = validator.TestValidate(testModifyMyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.LastStringChange);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, -1, 0)]
        [TestCase((Core.Enums.AccountType)1, 6, 7, -15, 1)]
        [TestCase((Core.Enums.AccountType)0, 9, 12, -18, 2)]
        public void ShouldHaveHoursPlayedWeeklyErrors(Core.Enums.AccountType? accountType, int? userId, int id, int hoursPlayedWeekly, int i)
        {
            var testModifyMyInstrumentRequest = new ModifyMyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
            };
            var result = validator.TestValidate(testModifyMyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.HoursPlayedWeekly);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 0)]
        [TestCase((Core.Enums.AccountType)1, 6, 7, 1, 1)]
        [TestCase((Core.Enums.AccountType)0, 9, 12, 1, 2)]
        public void ShouldHaveLastDeepCleaningErrors(Core.Enums.AccountType? accountType, int? userId, int id, int hoursPlayedWeekly, int i)
        {
            var testModifyMyInstrumentRequest = new ModifyMyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = incorrectDates[i],
            };
            var result = validator.TestValidate(testModifyMyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.LastDeepCleaning);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1, 1, 0)]
        [TestCase((Core.Enums.AccountType)1, 6, -7, 1, 1)]
        [TestCase((Core.Enums.AccountType)0, 9, 0, 1, 2)]
        public void ShouldHaveIdErrors(Core.Enums.AccountType? accountType, int? userId, int id, int hoursPlayedWeekly, int i)
        {
            var testModifyMyInstrumentRequest = new ModifyMyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
            };
            var result = validator.TestValidate(testModifyMyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1, 1, 0)]
        [TestCase((Core.Enums.AccountType)1, 0, 7, 1, 1)]
        [TestCase((Core.Enums.AccountType)0, null, 12, 1, 2)]
        public void ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, int id, int hoursPlayedWeekly, int i)
        {
            var testModifyMyInstrumentRequest = new ModifyMyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
            };
            var result = validator.TestValidate(testModifyMyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)6, 1, 1, 1, 0)]
        [TestCase((Core.Enums.AccountType)(-1), 6, 7, 1, 1)]
        [TestCase(null, 9, 12, 1, 2)]
        public void ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, int id, int hoursPlayedWeekly, int i)
        {
            var testModifyMyInstrumentRequest = new ModifyMyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                LastStringChange = correctDates[i],
                HoursPlayedWeekly = hoursPlayedWeekly,
                LastDeepCleaning = correctDates[i],
            };
            var result = validator.TestValidate(testModifyMyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}
