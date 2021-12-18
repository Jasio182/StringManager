using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.DataAccess.Tests
{
    public class TuningTests
    {
        private DbContextOptions<StringManagerStorageContext> options;

        [OneTimeSetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<StringManagerStorageContext>()
                .UseInMemoryDatabase(databaseName: "StringManagerDatabase").Options;

            using (var context = new StringManagerStorageContext(options))
            {
                context.Tunings.Add(new Tuning()
                {
                    Name = "TestTuning1",
                    NumberOfStrings = 6
                });
                context.Tunings.Add(new Tuning()
                {
                    Name = "TestTuning2",
                    NumberOfStrings = 7
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
        public void AddTuningCommand_success()
        {
            //Arrange
            var mockParameter = new Tuning()
            {
                Name = "TestTuning3",
                NumberOfStrings = 8
            };
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var TuningCommand = new AddTuningCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(TuningCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(mockParameter.Name, response.Name);
                Assert.AreEqual(mockParameter.NumberOfStrings, response.NumberOfStrings);
            }
        }

        [Test]
        public void ModifyTuningCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var mockParameter = context.Tunings.FirstOrDefault(x => x.Id == 1);
                mockParameter.Name = "testChangedName";
                var commandExecutor = new CommandExecutor(context);
                var TuningCommand = new ModifyTuningCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(TuningCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual(mockParameter.Name, response.Name);
                Assert.AreEqual(mockParameter.NumberOfStrings, response.NumberOfStrings);
            }
        }

        [Test]
        public void GetTuningsQuery_succes()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var TuningQuery = new GetTuningsQuery()
                {
                    NumberOfStrings = 6
                };

                //Act
                var response = queryExecutor.Execute(TuningQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Count == 1);
            }
        }

        [Test]
        public void GetTuningsQuery_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var TuningQuery = new GetTuningsQuery()
                {
                    NumberOfStrings = 5
                };

                //Act
                var response = queryExecutor.Execute(TuningQuery).Result;

                //Assert
                Assert.IsEmpty(response);
            }
        }

        [Test]
        public void GetTuningQuery_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var TuningQuery = new GetTuningQuery()
                {
                    Id = 1
                };

                //Act
                var response = queryExecutor.Execute(TuningQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
            }
        }

        [Test]
        public void GetTuningQuery_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var TuningQuery = new GetTuningQuery()
                {
                    Id = 8
                };

                //Act
                var response = queryExecutor.Execute(TuningQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }

        [Test]
        public void RemoveTuningCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var TuningCommand = new RemoveTuningCommand()
                {
                    Parameter = 2
                };

                //Act
                var response = commandExecutor.Execute(TuningCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response.Id);
            }
        }

        [Test]
        public void RemoveTuningCommand_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var TuningCommand = new RemoveTuningCommand()
                {
                    Parameter = 6
                };

                //Act
                var response = commandExecutor.Execute(TuningCommand).Result;

                //Assert
                Assert.IsNull(response);
            }
        }
    }
}
