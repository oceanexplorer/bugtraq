using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Commands;
using BugTraq.Api.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BugTraq.Api.Tests.Commands.Users
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

        public class AddUserCommand : CommandFacts
        {
            [Fact]
            public async Task AddedRecordsShouldBePersisted()
            {
                // Arrange
                var userOneCommand = new AddUser.Command { FirstName = "Sarah", Surname = "Smith"};
                var userTwoCommand = new AddUser.Command { FirstName = "Michael", Surname = "Bailey"};
                
                // Act
                await using var dbContext = new BugTraqContext(_inMemoryDbOptions);
                await new AddUser.Handler(dbContext).Handle(userOneCommand,CancellationToken.None);
                await new AddUser.Handler(dbContext).Handle(userTwoCommand,CancellationToken.None);

                // Assert
                dbContext.Users.Count().Should().Be(2, "we added two records to the database");
            }
        }
    }
}