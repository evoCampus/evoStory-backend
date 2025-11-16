using EvoStory.Database.Models;
using EvoStory.BackendAPI.Database;
using EvoStory.BackendAPI.Repository;
using EvoStory.Database.Exceptions;
using EvoStory.Database.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoStory.BackendAPI.uTest.Repository
{
    class ChoiceRepositoryInMemoryTest
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public async Task GetChoice_ExistingChoice_ChoiceReturned()
        {
            // Arrange
            var storyId = Guid.NewGuid();
            var choiceId = Guid.NewGuid();
            var stories = new Dictionary<Guid, Story>()
            {
                {storyId, new Story(){
                    Id = storyId,
                    Title = "Some title",
                    Scenes = new List<Scene>()
                    {
                        new Scene()
                        {
                            Id = Guid.NewGuid(),
                            Choices = new List<Choice>()
                            {
                                new Choice()
                                {
                                    Id = choiceId
                                }
                            }
                        }
                    }
                }}
            };
            var mockLogger = new Mock<ILogger<ChoiceRepositoryInMemory>>();
            var mockDbContext = new Mock<IDatabase>();
            mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);

            var sut = new ChoiceRepositoryInMemory(mockLogger.Object, mockDbContext.Object);

            // Act
            var choice = await sut.GetChoice(choiceId);

            // Assert
            Assert.That(choice.Id, Is.EqualTo(choiceId));
        }

        [Test]
        public async Task GetChoice_NonExistentChoice_ExceptionThrown()
        {
            // Arrange
            var storyId = Guid.NewGuid();
            var choiceId = Guid.NewGuid();
            var stories = new Dictionary<Guid, Story>()
            {
                {storyId, new Story(){
                    Id = storyId,
                    Title = "Some title",
                    Scenes = new List<Scene>()
                    {
                        new Scene()
                        {
                            Id = Guid.NewGuid(),
                            Choices = new List<Choice>()
                            {
                                new Choice()
                                {
                                    Id = Guid.NewGuid()
                                }
                            }
                        }
                    }
                }}
            };
            var mockLogger = new Mock<ILogger<ChoiceRepositoryInMemory>>();
            var mockDbContext = new Mock<IDatabase>();
            mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);

            var sut = new ChoiceRepositoryInMemory(mockLogger.Object, mockDbContext.Object);

            // Act & Assert
            Assert.ThrowsAsync<RepositoryException>(async () => await sut.GetChoice(choiceId));
        }
    }
}
