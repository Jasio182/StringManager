using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.DataAccess.Tests
{
    public class ManufacturerTests
    {
        private String[] testStrings =
        {
            new String()
            {
                NumberOfDaysGood = 128,
                Size = 11,
                SpecificWeight = 0.0001,
                StringType = Core.Enums.StringType.PlainNikled,
                ManufacturerId = 1
            },
            new String()
            {
                NumberOfDaysGood = 128,
                Size = 9,
                SpecificWeight = 0.00007,
                StringType = Core.Enums.StringType.PlainNikled,
                ManufacturerId = 2
            },
            new String()
            {
                Id = 3,
                NumberOfDaysGood = 128,
                Size = 52,
                SpecificWeight = 0.0008,
                StringType = Core.Enums.StringType.WoundNikled,
                ManufacturerId = 3
            }
        };

        private Instrument[] testInstruments =
        {
            new Instrument()
            {
                ScaleLenghtTreble = 628,
                ScaleLenghtBass = 628,
                NumberOfStrings = 6,
                Model = "TestModel1",
                ManufacturerId = 1
            },
            new Instrument()
            {
                ScaleLenghtTreble = 631,
                ScaleLenghtBass = 658,
                NumberOfStrings = 7,
                Model = "TestModel2",
                ManufacturerId = 2
            },
            new Instrument()
            {
                ScaleLenghtTreble = 631,
                ScaleLenghtBass = 658,
                NumberOfStrings = 7,
                Model = "TestModel3",
                ManufacturerId = 3
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
                for(int i = 0; i < testInstruments.Length; i++)
                {
                    context.Instruments.Add(testInstruments[i]);
                    context.Strings.Add(testStrings[i]);
                }
                context.Manufacturers.Add(new Manufacturer()
                {
                    Name = "TestManufacturer1"
                });
                context.Manufacturers.Add(new Manufacturer()
                {
                    Name = "TestManufacturer2"
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
        public void AddManufacturerCommand_success()
        {
            //Arrange
            var mockParameter = new Manufacturer()
            {
                Name = "TestManufacturer3"
            };
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var manufacturerCommand = new AddManufacturerCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(manufacturerCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(mockParameter.Name, response.Name);
            }
        }

        [Test]
        public void ModifyManufacturerCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var mockParameter = context.Manufacturers.FirstOrDefault(x => x.Id == 1);
                mockParameter.Name = "testChangedName";
                var commandExecutor = new CommandExecutor(context);
                var manufacturerCommand = new ModifyManufacturerCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(manufacturerCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual(mockParameter.Name, response.Name);
            }
        }

        [Test]
        public void GetManufacturerQuery_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var manufacturerQuery = new GetManufacturerQuery()
                {
                    Id = 1
                };

                //Act
                var response = queryExecutor.Execute(manufacturerQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.IsNotNull(response.Strings);
                Assert.IsNotNull(response.Instruments);
            }
        }

        [Test]
        public void GetManufacturerQuery_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var manufacturerQuery = new GetManufacturerQuery()
                {
                    Id = 8
                };

                //Act
                var response = queryExecutor.Execute(manufacturerQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }

        [Test]
        public void RemoveManufacturerCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var manufacturerCommand = new RemoveManufacturerCommand()
                {
                    Parameter = 2
                };

                //Act
                var response = commandExecutor.Execute(manufacturerCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response.Id);
            }
        }

        [Test]
        public void RemoveManufacturerCommand_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var manufacturerCommand = new RemoveManufacturerCommand()
                {
                    Parameter = 6
                };

                //Act
                var response = commandExecutor.Execute(manufacturerCommand).Result;

                //Assert
                Assert.IsNull(response);
            }
        }
    }
}
