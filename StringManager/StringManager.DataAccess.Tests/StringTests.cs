using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.DataAccess.Tests
{
    public class StringTests
    {
        private Manufacturer testManufacturer = new Manufacturer
        {
            Name = "testManufacturer"
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
                context.Strings.Add(new String()
                {
                    ManufacturerId = 1,
                    NumberOfDaysGood = 420,
                    StringType = Core.Enums.StringType.WoundNikled,
                    Size = 62,
                    SpecificWeight = 0.01262504715
                });
                context.Strings.Add(new String()
                {
                    ManufacturerId = 1,
                    NumberOfDaysGood = 365,
                    StringType = Core.Enums.StringType.WoundNikled,
                    Size = 52,
                    SpecificWeight = 0.00859128949
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
        public void AddStringCommand_success()
        {
            //Arrange
            var mockParameter = new String()
            {
                ManufacturerId = 1,
                NumberOfDaysGood = 361,
                StringType = Core.Enums.StringType.PlainNikled,
                Size = 9,
                SpecificWeight = 0.00031965761
            };
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var StringCommand = new AddStringCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(StringCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(mockParameter.SpecificWeight, response.SpecificWeight);
                Assert.AreEqual(mockParameter.NumberOfDaysGood, response.NumberOfDaysGood);
                Assert.AreEqual(mockParameter.StringType, response.StringType);
                Assert.AreEqual(mockParameter.Size, response.Size);
            }
        }

        [Test]
        public void ModifyStringCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var mockParameter = context.Strings.FirstOrDefault(x => x.Id == 1);
                mockParameter.StringType = Core.Enums.StringType.PlainBrass;
                var commandExecutor = new CommandExecutor(context);
                var StringCommand = new ModifyStringCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(StringCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual(mockParameter.StringType, response.StringType);
                Assert.AreEqual(mockParameter.SpecificWeight, response.SpecificWeight);
            }
        }

        [Test]
        public void GetStringsQuery()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var StringQuery = new GetStringsQuery();

                //Act
                var response = queryExecutor.Execute(StringQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Count >= 2);
            }
        }

        [Test]
        public void GetStringQuery_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var StringQuery = new GetStringQuery()
                {
                    Id = 1
                };

                //Act
                var response = queryExecutor.Execute(StringQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
            }
        }

        [Test]
        public void GetStringQuery_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var StringQuery = new GetStringQuery()
                {
                    Id = 8
                };

                //Act
                var response = queryExecutor.Execute(StringQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }

        [Test]
        public void RemoveStringCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var StringCommand = new RemoveStringCommand()
                {
                    Parameter = 2
                };

                //Act
                var response = commandExecutor.Execute(StringCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response.Id);
            }
        }

        [Test]
        public void RemoveStringCommand_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var StringCommand = new RemoveStringCommand()
                {
                    Parameter = 6
                };

                //Act
                var response = commandExecutor.Execute(StringCommand).Result;

                //Assert
                Assert.IsNull(response);
            }
        }
    }
}
