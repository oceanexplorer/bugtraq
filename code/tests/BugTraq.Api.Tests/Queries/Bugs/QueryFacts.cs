using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BugTraq.Api.Mappers;
using BugTraq.Api.Models;
using BugTraq.Api.Queries;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BugTraq.Api.Tests.Queries.Bugs
{
    public class QueryFacts
    {
        private DbContextOptions<BugTraqContext> _inMemoryDbOptions;
        private IMapper _mapper;

        public QueryFacts()
        {
           _inMemoryDbOptions = new DbContextOptionsBuilder<BugTraqContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
        
        public class GetBugsQuery : QueryFacts
        {
            public GetBugsQuery()
            {
                _mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new BugProfile()); }).CreateMapper();
            }
            
            [Fact]
            public async Task ShouldReturnSameNumberOfRecordsAsAdded()
            {
                // Arrange
                var user = new User (new Guid("00000000-0000-0000-0000-000000000001"), "Sarah", "Smith");
                var bugOne = new Bug("This is bug one", "BugOne", "New", user.UserId);
                var bugTwo = new Bug("This is big two", "BugTwo", "Closed", user.UserId);
                var getBugsQuery = new GetBugs.Query();

                await using (var dbContext = new BugTraqContext(_inMemoryDbOptions))
                {
                    dbContext.Add(user);
                    dbContext.Add(bugOne);
                    dbContext.Add(bugTwo);
                    dbContext.SaveChanges();
                }
                
                // Act
                await using (var dbContext = new BugTraqContext(_inMemoryDbOptions))
                {
                    var bugs = await new GetBugs.Handler(dbContext,  _mapper)
                        .Handle(getBugsQuery, It.IsAny<CancellationToken>());
                    
                    // Assert
                    bugs.Count.Should().Be(2, "because two records were added to the database so two should have been returned");
                }
            }
        }
        
        public class GetBugQuery : QueryFacts
        {
            public GetBugQuery()
            {
                _mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new BugProfile()); }).CreateMapper();
            }
        
            [Fact]
            public async Task ShouldReturnOnlyOneRecordWithAMatchingId()
            {
                // Arrange
                var user = new User (new Guid("00000000-0000-0000-0000-000000000001"),"Sarah","Smith");
                var bugOne = new Bug("This is bug one", "BugOne", "New", user.UserId);
                var bugTwo = new Bug("This is big two", "BugTwo", "Closed", user.UserId);
                var getBugQuery = new GetBug.Query(1);
                
                await using (var dbContext = new BugTraqContext(_inMemoryDbOptions))
                {
                    dbContext.Add(user);
                    dbContext.Add(bugOne);
                    dbContext.Add(bugTwo);
                    dbContext.SaveChanges();
                }
                
                // Act
                await using (var dbContext = new BugTraqContext(_inMemoryDbOptions))
                {
                    var bug = await new GetBug.Handler(dbContext, _mapper)
                        .Handle(getBugQuery, It.IsAny<CancellationToken>());
                    
                    // Assert
                    bug.Title.Should().Be(bugOne.Title, "the retrieved bug should be the same as the one we saved.");
                }
            }
        }
    }
}