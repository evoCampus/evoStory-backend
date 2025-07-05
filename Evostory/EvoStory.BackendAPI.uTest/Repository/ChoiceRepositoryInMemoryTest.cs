using Evostory.Story.Models;
using EvoStory.BackendAPI.Database;
using EvoStory.BackendAPI.Exceptions;
using EvoStory.BackendAPI.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;

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
        public void GetChoice_ExistingChoice_ChoiceReturned()
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
            var choice = _sut.GetChoice(_choiceId);

            // Assert
            Assert.That(choice.Id, Is.EqualTo(_choiceId));
        }

        [Test]
        public void GetChoice_NonExistentChoice_ExceptionThrown()
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

            // Act & Assert
            Assert.That(() => _sut.GetChoice(_choiceId), Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void CreateChoice_ValidChoice_ChoiceIsAddedToRepository()
        {
            //Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {_storyId, new Story(){
                    Id= _storyId,
                    Title="Some title",
                    Scenes = new List<Scene>()
                    {
                        new Scene()
                        {
                            Id = _sceneId,
                            Choices = new List<Choice>(1)
                        }
                    }
                }}
            };
            _mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);

            //Act
            var actualChoice = _sut.CreateChoice(_expectedChoice, _sceneId);

            //Assert
            Assert.That(actualChoice.Id, Is.EqualTo(_expectedChoice.Id));
            Assert.That(actualChoice.ChoiceText, Is.EqualTo(_expectedChoice.ChoiceText));
            Assert.That(actualChoice.NextSceneId, Is.EqualTo(_expectedChoice.NextSceneId));
        }

        [Test]
        public void CreateChoice_NonExistentScene_ExceptionThrown()
        {
            //Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {_storyId, new Story()
                {
                    Id = _storyId,
                    Title="Some title",
                    Scenes = new List<Scene>()
                    {
                        new Scene()
                        {
                            Id = Guid.NewGuid(),
                            Choices = new List<Choice>(1)
                        }
                    }
                }
                }
            };
            _mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);

            //Act & Assert
            Assert.That(() => _sut.CreateChoice(_expectedChoice, Guid.NewGuid()), Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void DeleteChoice_ExistingChoice_ChoiceDeletedFromRepository()
        {
            //Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {_storyId, new Story()
                {
                    Id = _storyId,
                    Title = "Some title",
                    Scenes = new List<Scene>()
                    {
                        new Scene()
                        {
                            Id = Guid.NewGuid(),
                            Choices = new List<Choice>()
                            {
                                _expectedChoice
                            }
                        }
                    }
                }
                }
            };
            _mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);

            //Act
            var deletedChoice = _sut.DeleteChoice(_choiceId);

            //Assert
            Assert.That(deletedChoice.Id, Is.EqualTo(_expectedChoice.Id));
            Assert.That(deletedChoice.ChoiceText, Is.EqualTo(_expectedChoice.ChoiceText));
            Assert.That(deletedChoice.NextSceneId, Is.EqualTo(_expectedChoice.NextSceneId));
        }

        [Test]
        public void DeleteChoice_NonExistentChoice_ExceptionThrown()
        {
            //Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {_storyId, new Story()
                {
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
                                    Id = Guid.NewGuid(),
                                    ChoiceText = "Some choice text",
                                    NextSceneId = Guid.NewGuid()
                                }
                            }
                        }
                    }
                }
                }
            };
            _mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);

            //Act & Assert
            Assert.That(() => _sut.DeleteChoice(Guid.NewGuid()), Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void GetChoices_NoChoiceExists_EmptyListReturned()
        {
            //Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {_storyId, new Story()
                {
                    Id = _storyId,
                    Title = "Some title",
                    Scenes= new List<Scene>()
                    {
                        new Scene()
                        {
                            Id = Guid.NewGuid(),
                            Choices = new List<Choice>()
                        }
                    }
                }
                }
            };
            _mockDbContext.SetupGet(m=>m.Stories)
                .Returns(stories);

            //Act
            var choices = _sut.GetChoices();

            //Assert
            Assert.That(choices.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetChoices_ExistentChoicesFromOneStory_ListReturned()
        {
            //Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {_storyId, new Story()
                {
                    Id = _storyId,
                    Title = "Some Title",
                    Scenes= new List<Scene>()
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
                }
                }
            };
            _mockDbContext.SetupGet(m=>m.Stories)
                .Returns(stories);

            //Act
            var choices= _sut.GetChoices();
            
            //Assert
            Assert.That(choices.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetChoices_ExistingChoicesFromTwoStory_ListReturned()
        {
            //Arrange
            var storyId1 = Guid.NewGuid();
            var storyId2 = Guid.NewGuid();
            var stories = new Dictionary<Guid, Story>()
            {
                {storyId1, new Story()
                {
                    Id = storyId1,
                    Title = "Some title",
                    Scenes= new List<Scene>()
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
                }
                },
                {storyId2, new Story()
                {
                    Id = storyId2,
                    Title = "Some other title",
                    Scenes= new List<Scene>()
                    {
                        new Scene()
                        {
                            Id = Guid.NewGuid(),
                            Choices = new List<Choice>()
                            {
                                new Choice()
                                {
                                    Id = Guid.NewGuid()
                                },
                                new Choice()
                                {
                                    Id = Guid.NewGuid()
                                }
                            }
                        }
                    }
                }
                }
            };
            _mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);

            //Act
            var choices = _sut.GetChoices();
            
            //Assert
            Assert.That(choices.Count(), Is.EqualTo(3));
        }
    }
}
