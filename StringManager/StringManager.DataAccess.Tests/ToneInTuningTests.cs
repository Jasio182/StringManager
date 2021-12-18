using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.DataAccess.Tests
{
    public class ToneInTuningTests
    {
        private Tone[] testTones =
        {
            new Tone()
            {
                Name = "TestTone1",
                Frequency = 174.61,
                WaveLenght = 234.56
            },
            new Tone()
            {
                Name = "TestTone2",
                Frequency = 141.11,
                WaveLenght = 123.45
            },
            new Tone()
            {
                Name = "TestTone3",
                Frequency = 231.22,
                WaveLenght = 151.65
            }
        };
        private Tuning[] testTunings =
        {
            new Tuning()
            {
                Name = "TestTuning1",
                NumberOfStrings = 2,
            },
            new Tuning()
            {
                Name = "TestTuning2",
                NumberOfStrings = 2,
            },
            new Tuning()
            {
                Name = "TestTuning3",
                NumberOfStrings = 2,
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
                for (int i = 0; i < testTunings.Length; i++)
                {
                    context.Tones.Add(testTones[i]);
                    context.Tunings.Add(testTunings[i]);
                }
                context.TonesInTunings.Add(new ToneInTuning()
                {
                    Position = 1,
                    ToneId = 1,
                    TuningId = 1
                });
                context.TonesInTunings.Add(new ToneInTuning()
                {
                    Position = 2,
                    ToneId = 2,
                    TuningId = 2
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
        public void AddToneInTuningCommand_success()
        {
            //Arrange
            var mockParameter = new ToneInTuning()
            {
                Position = 2,
                ToneId = 3,
                TuningId = 3
            };
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var ToneInTuningCommand = new AddToneInTuningCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(ToneInTuningCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(mockParameter.Position, response.Position);
                Assert.AreEqual(mockParameter.ToneId, response.ToneId);
                Assert.AreEqual(mockParameter.TuningId, response.TuningId);
            }
        }

        [Test]
        public void ModifyToneInTuningCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var mockParameter = context.TonesInTunings.FirstOrDefault(x => x.Id == 1);
                mockParameter.Position = 3;
                var commandExecutor = new CommandExecutor(context);
                var ToneInTuningCommand = new ModifyToneInTuningCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(ToneInTuningCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual(mockParameter.Position, response.Position);
                Assert.AreEqual(mockParameter.TuningId, response.TuningId);
            }
        }

        [Test]
        public void GetToneInTuningQuery_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var ToneInTuningQuery = new GetToneInTuningQuery()
                {
                    Id = 1
                };

                //Act
                var response = queryExecutor.Execute(ToneInTuningQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
            }
        }

        [Test]
        public void GetToneInTuningQuery_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var ToneInTuningQuery = new GetToneInTuningQuery()
                {
                    Id = 8
                };

                //Act
                var response = queryExecutor.Execute(ToneInTuningQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }

        [Test]
        public void RemoveToneInTuningCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var ToneInTuningCommand = new RemoveToneInTuningCommand()
                {
                    Parameter = 2
                };

                //Act
                var response = commandExecutor.Execute(ToneInTuningCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response.Id);
            }
        }

        [Test]
        public void RemoveToneInTuningCommand_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var ToneInTuningCommand = new RemoveToneInTuningCommand()
                {
                    Parameter = 6
                };

                //Act
                var response = commandExecutor.Execute(ToneInTuningCommand).Result;

                //Assert
                Assert.IsNull(response);
            }
        }
    }
}
