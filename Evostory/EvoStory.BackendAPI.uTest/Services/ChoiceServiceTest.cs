using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Exceptions;
using EvoStory.BackendAPI.Repository;
using EvoStory.BackendAPI.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace EvoStory.BackendAPI.uTest.Services
{
    class ChoiceServiceTest
    {
        private Guid sceneId;
        private Guid nextSceneId;
        private const string choiceText = "Some choice text.";
        private Guid choiceId;
        private Choice choice;
        private ChoiceDTO expectedChoiceDTO;
        private Mock<ILogger<ChoiceService>> mockLogger;
        private Mock<IChoiceRepository> mockChoiceRepository;
        private Mock<IDTOConversionService> mockDTOConversionService;
        private ChoiceService sut;

        [SetUp]
        public void Setup()
        {
            sceneId = Guid.NewGuid();
            nextSceneId = Guid.NewGuid();
            choiceId = Guid.NewGuid();
            choice = new Choice()
            {
                Id = choiceId,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            expectedChoiceDTO = new ChoiceDTO()
            {
                Id = choiceId,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            mockLogger = new Mock<ILogger<ChoiceService>>();
            mockChoiceRepository = new Mock<IChoiceRepository>();
            mockDTOConversionService = new Mock<IDTOConversionService>();
            sut = new ChoiceService(mockChoiceRepository.Object, mockDTOConversionService.Object, mockLogger.Object);
        }
        [Test]
        public void CreateChoice_ValidCreateChoiceDTO_ChoiceIsAddToRepository()
        {
            // Arrange
            var createChoiceDTO = new CreateChoiceDTO()
            {
                SceneId = sceneId,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            mockChoiceRepository.Setup(m => m.CreateChoice(It.IsAny<Choice>(), It.IsAny<Guid>()))
                .Returns(choice);
            mockDTOConversionService.Setup(m => m.ConvertChoiceToChoiceDTO(It.IsAny<Choice>()))
                .Returns(expectedChoiceDTO);
            // Act
            var actualChoiceDTO = sut.CreateChoice(createChoiceDTO);

            // Assert
            Assert.That(actualChoiceDTO.Id, Is.EqualTo(expectedChoiceDTO.Id));
            Assert.That(actualChoiceDTO.ChoiceText, Is.EqualTo(expectedChoiceDTO.ChoiceText));
            Assert.That(actualChoiceDTO.NextSceneId, Is.EqualTo(expectedChoiceDTO.NextSceneId));
        }

        [Test]
        public void CreateChoice_NonExistingScene_ExeptionThrown()
        {
            //Arrange
            var createChoiceDTO = new CreateChoiceDTO()
            {
                SceneId = sceneId,
                NextSceneId = nextSceneId,
                ChoiceText = choiceText
            };
            mockChoiceRepository.Setup(m => m.CreateChoice(It.IsAny<Choice>(), It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no existent scene with id: {sceneId}"));
            //Act & Assert
            Assert.That(()=> sut.CreateChoice(createChoiceDTO), Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void GetChoice_ValidChoice_ChoiceDTOReturned()
        {
            //Arrange
            mockChoiceRepository.Setup(m => m.GetChoice(It.IsAny<Guid>()))
                .Returns(choice);
            mockDTOConversionService.Setup(m => m.ConvertChoiceToChoiceDTO(It.IsAny<Choice>()))
                .Returns(expectedChoiceDTO);
            //Act
            var actualChoiceDTO = sut.GetChoice(choiceId);
            //Assert
            Assert.That(actualChoiceDTO.Id, Is.EqualTo(expectedChoiceDTO.Id));
            Assert.That(actualChoiceDTO.ChoiceText, Is.EqualTo(expectedChoiceDTO.ChoiceText));
            Assert.That(actualChoiceDTO.NextSceneId, Is.EqualTo(expectedChoiceDTO.NextSceneId));
        }

        [Test]
        public void GetChoice_NonExistentChoice_ExeptionThrown()
        {
            //Arrange
            mockChoiceRepository.Setup(m => m.GetChoice(It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no choice with id: {choiceId}"));
            //Act & Assert
            Assert.That(()=>sut.GetChoice(choiceId),Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void DeleteChoice_ExistingChoice_ChoiceDeletedFromRepository()
        {
            //Arragne
            mockChoiceRepository.Setup(m => m.DeleteChoice(It.IsAny<Guid>()))
                .Returns(choice);
            mockDTOConversionService.Setup(m=>m.ConvertChoiceToChoiceDTO(It.IsAny<Choice>()))
                .Returns(expectedChoiceDTO);
            //Act
            var actualChoiceDTO = sut.DeleteChoice(choiceId);
            //Assert
            Assert.That(actualChoiceDTO.Id, Is.EqualTo(expectedChoiceDTO.Id));
            Assert.That(actualChoiceDTO.ChoiceText, Is.EqualTo(expectedChoiceDTO.ChoiceText));
            Assert.That(actualChoiceDTO.NextSceneId, Is.EqualTo(expectedChoiceDTO.NextSceneId));
        }

        [Test]
        public void Delete_NonExistentChoice_ExeptionThrown()
        {
            //Arrange
            mockChoiceRepository.Setup(m => m.DeleteChoice(It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no choice with id: {choiceId}"));
            //Act & Assert
            Assert.That(()=> sut.DeleteChoice(choiceId),Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void GetChoices_NoChoiceExists_EmptyListReturned()
        {
            //Arrange
            var choices = new List<Choice>();
            mockChoiceRepository.Setup(m=>m.GetChoices())
                .Returns(choices);
            //Act
            var numberOfChoices = sut.GetChoices().Count();
            //Assert
            Assert.That(numberOfChoices, Is.EqualTo(0));
        }

        [Test]
        public void GetChoices_ChoiceExists_ListReturned()
        {
            //Arramge
            var choices = new List<Choice>()
            {
                new Choice()
                {
                    Id = Guid.NewGuid(),
                },
                new Choice()
                {
                    Id = Guid.NewGuid(),
                }
            };
            mockChoiceRepository.Setup(m=>m.GetChoices())
                .Returns(choices);
            //Act
            var numberOfChoice = sut.GetChoices().Count();
            //Assert
            Assert.That(numberOfChoice, Is.EqualTo(2));
        }
    }
}
