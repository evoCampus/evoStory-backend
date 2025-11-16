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
        private Guid _storyId;
        private Guid _sceneId;
        private Guid _choiceId;
        private Choice _expectedChoice;
        private Mock<ILogger<ChoiceRepositoryInMemory>> _mockLogger;
        private Mock<IDatabase> _mockDbContext;
        private ChoiceRepositoryInMemory _sut;

        [SetUp]
        public void Setup()
        {
            _storyId = Guid.NewGuid();
            _sceneId = Guid.NewGuid();
            _choiceId = Guid.NewGuid();
            _expectedChoice = new Choice()
            {
                Id = _choiceId,
                ChoiceText = "Some choice text",
                NextSceneId = Guid.NewGuid()
            };
            _mockLogger = new Mock<ILogger<ChoiceRepositoryInMemory>>();
            _mockDbContext = new Mock<IDatabase>();
            _sut = new ChoiceRepositoryInMemory(_mockLogger.Object, _mockDbContext.Object);
        }

        [Test]
        public async Task GetChoice_ExistingChoice_ChoiceReturned()
        {
            // Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {_storyId, new Story(){
                    Id = _storyId,
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
                                    Id = _choiceId
                                }
                            }
                        }
                    }
                }}
            };
            _mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);

            // Act
            var choice = await sut.GetChoice(choiceId);

            // Assert
            Assert.That(choice.Id, Is.EqualTo(_choiceId));
        }

        [Test]
        public async Task GetChoice_NonExistentChoice_ExceptionThrown()
        {
            // Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {_storyId, new Story(){
                    Id = _storyId,
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
            _mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);

            var sut = new ChoiceRepositoryInMemory(mockLogger.Object, mockDbContext.Object);

            // Act & Assert
            Assert.ThrowsAsync<RepositoryException>(async () => await sut.GetChoice(choiceId));
        }
    }
}
