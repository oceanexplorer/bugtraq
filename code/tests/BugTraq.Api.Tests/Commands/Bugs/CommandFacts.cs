using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Commands;
using BugTraq.Api.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BugTraq.Api.Tests.Commands.Bugs
{
    public class CommandFacts
    {
        private readonly DbContextOptions<BugTraqContext> _inMemoryDbOptions;

        public CommandFacts()
        {
            _inMemoryDbOptions = new DbContextOptionsBuilder<BugTraqContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        public class AddBugCommand : CommandFacts
        {
            [Fact]
            public async Task AddedRecordsShouldBePersisted()
            {
                // Arrange
                var user = new User(new Guid("00000000-0000-0000-0000-000000000001"), "Sarah", "Smith");
                var bugOneCommand = new AddBug.Command{ Title = "This is bug one", Description = "BugOne", Status = "new", UserId = user.UserId};
                var bugTwoCommand = new AddBug.Command{ Title = "This is bug two", Description = "BugTwo", Status = "active", UserId = user.UserId};
                
                await using (var dbContext = new BugTraqContext(_inMemoryDbOptions))
                {
                    dbContext.Add(user);
                    dbContext.SaveChanges();
                }

                // Act
                await using (var dbContext = new BugTraqContext(_inMemoryDbOptions))
                {
                    await new AddBug.Handler(dbContext).Handle(bugOneCommand,CancellationToken.None);
                    await new AddBug.Handler(dbContext).Handle(bugTwoCommand,CancellationToken.None);

                    // Assert
                    dbContext.Bugs.Count().Should().Be(2, "we added two records to the database");
                }
            }
        }
    }
}