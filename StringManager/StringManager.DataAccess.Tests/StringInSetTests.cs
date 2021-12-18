using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.DataAccess.Tests
{
    public class StringInSetTests
    {
        private Manufacturer testManufacturer = new Manufacturer
        {
            Name = "testManufacturer"
        };
        private StringsSet testStringsSet = new StringsSet
        {
            Name = "TestSet",
            NumberOfStrings = 3
        };
        private String[] testStrings =
        {
            new String()
            {
                ManufacturerId = 1,
                NumberOfDaysGood = 420,
                StringType = Core.Enums.StringType.WoundNikled,
                Size = 62,
                SpecificWeight = 0.01262504715
            },
            new String()
            {
                ManufacturerId = 1,
                NumberOfDaysGood = 365,
                StringType = Core.Enums.StringType.WoundNikled,
                Size = 52,
                SpecificWeight = 0.00859128949
            },
            new String()
            {
                ManufacturerId = 1,
                NumberOfDaysGood = 361,
                StringType = Core.Enums.StringType.PlainNikled,
                Size = 9,
                SpecificWeight = 0.00031965761
            }
        };

        private DbContextOptions<StringManagerStorageContext> options;

        [OneTimeSetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<StringManagerStorageContext>()
                .UseInMemoryDatabase(databaseName: "StringManagerDatabase").Options;

            using (var context = new StringManagerStorageContext(options))
            {
                context.Manufacturers.Add(testManufacturer);
                context.StringsSets.Add(testStringsSet);
                foreach(var testString in testStrings)
                    context.Strings.Add(testString);
                context.StringsInSets.Add(new StringInSet()
                {
                    Position = 1,
                    StringId = 1,
                    StringsSetId = 1
                });
                context.StringsInSets.Add(new StringInSet()
                {
                    Position = 2,
                    StringId = 2,
                    StringsSetId = 1
                });
                context.SaveChanges();
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            using (var context = new StringManagerStorageContext(options))
            {
                context.Database.EnsureDeleted();
            }
        }

        [Test]
        public void AddStringInSetCommand_success()
        {
            //Arrange
            var mockParameter = new StringInSet()
            {
                Position = 3,
                StringId = 3,
                StringsSetId = 1
            };
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var stringInSetCommand = new AddStringInSetCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(stringInSetCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(mockParameter.Position, response.Position);
                Assert.AreEqual(mockParameter.StringId, response.StringId);
                Assert.AreEqual(mockParameter.StringsSetId, response.StringsSetId);
            }
        }

        [Test]
        public void ModifyStringInSetCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var mockParameter = context.StringsInSets.FirstOrDefault(x => x.Id == 1);
                mockParameter.Position = 5;
                var commandExecutor = new CommandExecutor(context);
                var stringInSetCommand = new ModifyStringInSetCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(stringInSetCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual(mockParameter.Position, response.Position);
                Assert.AreEqual(mockParameter.StringsSetId, response.StringsSetId);
            }
        }

        [Test]
        public void RemoveStringInSetCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var stringInSetCommand = new RemoveStringInSetCommand()
                {
                    Parameter = 2
                };

                //Act
                var response = commandExecutor.Execute(stringInSetCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response.Id);
            }
        }

        [Test]
        public void RemoveStringInSetCommand_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var stringInSetCommand = new RemoveStringInSetCommand()
                {
                    Parameter = 6
                };

                //Act
                var response = commandExecutor.Execute(stringInSetCommand).Result;

                //Assert
                Assert.IsNull(response);
            }
        }
    }
}
