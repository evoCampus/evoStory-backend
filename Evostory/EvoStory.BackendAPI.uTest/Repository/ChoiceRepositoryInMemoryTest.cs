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
        private Guid storyId;
        private Guid sceneId;
        private Guid choiceId;
        private Choice expectedChoice;
        private Mock<ILogger<ChoiceRepositoryInMemory>> mockLogger;
        private Mock<IDatabase> mockDbContext;
        private ChoiceRepositoryInMemory sut;

        [SetUp]
        public void Setup()
        {
            storyId = Guid.NewGuid();
            sceneId = Guid.NewGuid();
            choiceId = Guid.NewGuid();
            expectedChoice = new Choice()
            {
                Id = choiceId,
                ChoiceText = "Some choice text",
                NextSceneId = Guid.NewGuid()
            };
            mockLogger = new Mock<ILogger<ChoiceRepositoryInMemory>>();
            mockDbContext = new Mock<IDatabase>();
            sut = new ChoiceRepositoryInMemory(mockLogger.Object, mockDbContext.Object);
        }

        [Test]
        public void GetChoice_ExistingChoice_ChoiceReturned()
        {
            // Arrange
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
            mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);
            // Act
            var choice = sut.GetChoice(choiceId);

            // Assert
            Assert.That(choice.Id, Is.EqualTo(choiceId));
        }

        [Test]
        public void GetChoice_NonExistentChoice_ExceptionThrown()
        {
            // Arrange
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
            mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);
            // Act & Assert
            Assert.That(() => sut.GetChoice(choiceId), Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void CreateChoice_ValidChoice_ChoiceIsAddToRepository()
        {
            //Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {storyId, new Story(){
                    Id= storyId,
                    Title="Some title",
                    Scenes = new List<Scene>()
                    {
                        new Scene()
                        {
                            Id = sceneId,
                            Choices = new List<Choice>(1)
                        }
                    }
                }}
            };
            mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);
            //Act
            var actualChoice = sut.CreateChoice(expectedChoice, sceneId);
            //Assert
            Assert.That(actualChoice.Id, Is.EqualTo(expectedChoice.Id));
            Assert.That(actualChoice.ChoiceText, Is.EqualTo(expectedChoice.ChoiceText));
            Assert.That(actualChoice.NextSceneId, Is.EqualTo(expectedChoice.NextSceneId));
        }

        [Test]
        public void CreateChoice_NonExistentScene_ExeptionThrown()
        {
            //Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {storyId, new Story()
                {
                    Id = storyId,
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
            mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);
            //Act & Assert
            Assert.That(() => sut.CreateChoice(expectedChoice, Guid.NewGuid()), Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void DeleteChoice_ExistingChoice_ChoiceDeleteFromRepository()
        {
            //Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {storyId, new Story()
                {
                    Id = storyId,
                    Title = "Some title",
                    Scenes = new List<Scene>()
                    {
                        new Scene()
                        {
                            Id = Guid.NewGuid(),
                            Choices = new List<Choice>()
                            {
                                expectedChoice
                            }
                        }
                    }
                }
                }
            };
            mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);
            //Act
            var deletedChoice = sut.DeleteChoice(choiceId);
            //Assert
            Assert.That(deletedChoice.Id, Is.EqualTo(expectedChoice.Id));
            Assert.That(deletedChoice.ChoiceText, Is.EqualTo(expectedChoice.ChoiceText));
            Assert.That(deletedChoice.NextSceneId, Is.EqualTo(expectedChoice.NextSceneId));
        }

        [Test]
        public void DeleteChoice_NonExistentChoice_ExeptionThrown()
        {
            //Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {storyId, new Story()
                {
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
            mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);
            //Act & Assert
            Assert.That(() => sut.DeleteChoice(Guid.NewGuid()), Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void GetChoices_NoChoiceExists_EmptyListReturned()
        {
            //Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {storyId, new Story()
                {
                    Id = storyId,
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
            mockDbContext.SetupGet(m=>m.Stories)
                .Returns(stories);
            //Act
            var numberOfChoices = sut.GetChoices().Count();
            //Assert
            Assert.That(numberOfChoices, Is.EqualTo(0));
        }

        [Test]
        public void GetChoices_ExistentChoicesFromOneStory_ListReturned()
        {
            //Arrange
            var stories = new Dictionary<Guid, Story>()
            {
                {storyId, new Story()
                {
                    Id = storyId,
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
            mockDbContext.SetupGet(m=>m.Stories)
                .Returns(stories);
            //Act
            var numberOfChoices= sut.GetChoices().Count();
            //Assert
            Assert.That(numberOfChoices, Is.EqualTo(1));
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
            mockDbContext.SetupGet(m => m.Stories)
                .Returns(stories);
            //Act
            var numberOfChoices = sut.GetChoices().Count();
            //Assert
            Assert.That(numberOfChoices, Is.EqualTo(3));
        }
    }
}
