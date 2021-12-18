using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.DataAccess.Tests
{
    public class ToneTests
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
                context.Tones.Add(new Tone()
                {
                    Name = "TestName1",
                    Frequency = 174.61,
                    WaveLenght = 234.56
                });
                context.Tones.Add(new Tone()
                {
                    Name = "TestName2",
                    Frequency = 141.11,
                    WaveLenght = 123.45
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
        public void AddToneCommand_success()
        {
            //Arrange
            var mockParameter = new Tone()
            {
                Name = "TestName3",
                Frequency = 231.22,
                WaveLenght = 151.65
            };
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var ToneCommand = new AddToneCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(ToneCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(mockParameter.Name, response.Name);
                Assert.AreEqual(mockParameter.Frequency, response.Frequency);
                Assert.AreEqual(mockParameter.WaveLenght, response.WaveLenght);
            }
        }

        [Test]
        public void ModifyToneCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var mockParameter = context.Tones.FirstOrDefault(x => x.Id == 1);
                mockParameter.Name = "testChangedName";
                var commandExecutor = new CommandExecutor(context);
                var ToneCommand = new ModifyToneCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(ToneCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual(mockParameter.Name, response.Name);
            }
        }

        [Test]
        public void GetTonesQuery_all()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var ToneQuery = new GetTonesQuery();

                //Act
                var response = queryExecutor.Execute(ToneQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Count >= 2);
            }
        }

        [Test]
        public void GetToneQuery_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var ToneQuery = new GetToneQuery()
                {
                    Id = 1
                };

                //Act
                var response = queryExecutor.Execute(ToneQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual("TestName1", response.Name);
            }
        }

        [Test]
        public void GetToneQuery_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var ToneQuery = new GetToneQuery()
                {
                    Id = 8,
                };

                //Act
                var response = queryExecutor.Execute(ToneQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }

        [Test]
        public void RemoveToneCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var ToneCommand = new RemoveToneCommand()
                {
                    Parameter = 2
                };

                //Act
                var response = commandExecutor.Execute(ToneCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response.Id);
            }
        }

        [Test]
        public void RemoveToneCommand_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var ToneCommand = new RemoveToneCommand()
                {
                    Parameter = 6
                };

                //Act
                var response = commandExecutor.Execute(ToneCommand).Result;

                //Assert
                Assert.IsNull(response);
            }
        }
    }
}
