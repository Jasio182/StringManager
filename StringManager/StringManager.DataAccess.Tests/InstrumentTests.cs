using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.DataAccess.Tests
{
    public class InstrumentTests
    {
        private Manufacturer[] testManufacturers =
        {
            new Manufacturer()
            {
                Name = "Manufacturer1"
            },
            new Manufacturer()
            {
                Name = "Manufacturer2"
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
                foreach(var testManufacturer in testManufacturers)
                    context.Manufacturers.Add(testManufacturer);
                context.Instruments.Add(new Instrument()
                {
                    ScaleLenghtTreble = 628,
                    ScaleLenghtBass = 628,
                    NumberOfStrings = 6,
                    Model = "TestModel1",
                    ManufacturerId = 1
                });
                context.Instruments.Add(new Instrument()
                {
                    ScaleLenghtTreble = 631,
                    ScaleLenghtBass = 658,
                    NumberOfStrings = 7,
                    Model = "TestModel2",
                    ManufacturerId = 2
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
        public void AddInstrumentCommand_success()
        {
            //Arrange
            var mockParameter = new Instrument()
            {
                ScaleLenghtTreble = 631,
                ScaleLenghtBass = 658,
                NumberOfStrings = 7,
                Model = "TestModel3",
                ManufacturerId = 1
            };
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var installedStringCommand = new AddInstrumentCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(installedStringCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(mockParameter.ScaleLenghtTreble, response.ScaleLenghtTreble);
                Assert.AreEqual(mockParameter.ScaleLenghtBass, response.ScaleLenghtBass);
                Assert.AreEqual(mockParameter.NumberOfStrings, response.NumberOfStrings);
                Assert.AreEqual(mockParameter.Model, response.Model);
                Assert.AreEqual(mockParameter.ManufacturerId, response.ManufacturerId);
            }
        }

        [Test]
        public void ModifyInstrumentCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var mockParameter = context.Instruments.FirstOrDefault(x => x.Id == 1);
                mockParameter.Model = "testChangedModelName";
                var commandExecutor = new CommandExecutor(context);
                var installedStringCommand = new ModifyInstrumentCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(installedStringCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual(mockParameter.ScaleLenghtTreble, response.ScaleLenghtTreble);
                Assert.AreEqual(mockParameter.ScaleLenghtBass, response.ScaleLenghtBass);
                Assert.AreEqual(mockParameter.NumberOfStrings, response.NumberOfStrings);
                Assert.AreEqual(mockParameter.Model, response.Model);
                Assert.AreEqual(mockParameter.ManufacturerId, response.ManufacturerId);
            }
        }

        [Test]
        public void GetInstrumentsQuery()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var installedStringQuery = new GetInstrumentsQuery();

                //Act
                var response = queryExecutor.Execute(installedStringQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Count >= 2);
            }
        }

        [Test]
        public void GetInstrumentQuery_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var installedStringQuery = new GetInstrumentQuery()
                {
                    Id = 1
                };

                //Act
                var response = queryExecutor.Execute(installedStringQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual(testManufacturers[0].Id, response.ManufacturerId);
                Assert.AreEqual(testManufacturers[0].Name, response.Manufacturer.Name);
            }
        }

        [Test]
        public void GetInstrumentQuery_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var instrumentQuery = new GetInstrumentQuery()
                {
                    Id = 8
                };

                //Act
                var response = queryExecutor.Execute(instrumentQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }

        [Test]
        public void RemoveInstrumentCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var instrumentCommand = new RemoveInstrumentCommand()
                {
                    Parameter = 2
                };

                //Act
                var response = commandExecutor.Execute(instrumentCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response.Id);
            }
        }

        [Test]
        public void RemoveInstrumentCommand_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var instrumentCommand = new RemoveInstrumentCommand()
                {
                    Parameter = 6
                };

                //Act
                var response = commandExecutor.Execute(instrumentCommand).Result;

                //Assert
                Assert.IsNull(response);
            }
        }
    }
}
