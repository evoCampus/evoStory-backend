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
        [Test]
        public void CreateChoice_ValidCreateChoiceDTO_ChoiceIsAddToRepository()
        {
            // Arrange
            var sceneId = Guid.NewGuid();
            const string choiceText = "Some choice text.";
            var nextSceneId = Guid.NewGuid();
            var choiceId = Guid.NewGuid();
            var createChoiceDTO = new CreateChoiceDTO()
            {
                SceneId = sceneId,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            var choice = new Choice()
            {
                Id = choiceId,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            var expectedChoiceDTO = new ChoiceDTO()
            {
                Id = choiceId,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            var loggerMock = new Mock<ILogger<ChoiceService>>();
            var choiceRepositoryMock = new Mock<IChoiceRepository>();
            choiceRepositoryMock.Setup(m => m.CreateChoice(It.IsAny<Choice>(), It.IsAny<Guid>()))
                .Returns(choice);
            var dtoConversionServiceMock = new Mock<IDTOConversionService>();
            dtoConversionServiceMock.Setup(m => m.ConvertChoiceToChoiceDTO(It.IsAny<Choice>()))
                .Returns(expectedChoiceDTO);

            var sut = new ChoiceService(choiceRepositoryMock.Object, dtoConversionServiceMock.Object, loggerMock.Object);

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
            var sceneId = Guid.NewGuid();
            const string choiceText = "Some choice text";
            var nextSceneId = Guid.NewGuid();
            var choiceId = Guid.NewGuid();
            var createChoiceDTO = new CreateChoiceDTO()
            {
                SceneId = sceneId,
                NextSceneId = nextSceneId,
                ChoiceText = choiceText
            };
            var expectedChoiceDTO = new ChoiceDTO()
            {
                Id = choiceId,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            var mockLogger = new Mock<ILogger<ChoiceService>>();
            var mockChoiceRepository = new Mock<IChoiceRepository>();
            mockChoiceRepository.Setup(m => m.CreateChoice(It.IsAny<Choice>(), It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no existent scene with id: {sceneId}"));
            var mockDTOConversionService = new Mock<IDTOConversionService>();
            var sut = new ChoiceService(mockChoiceRepository.Object, mockDTOConversionService.Object, mockLogger.Object);
            //Act & Assert
            Assert.That(()=> sut.CreateChoice(createChoiceDTO), Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void GetChoice_ValidChoice_ChoiceDTOReturned()
        {
            //Arrange
            var choiceId = Guid.NewGuid();
            var nextSceneId = Guid.NewGuid();
            const string choiceText = " Some choice text";
            var choice = new Choice()
            {
                Id = choiceId,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            var expectedChoiceDTO = new ChoiceDTO()
            {
                Id = choiceId,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            var mockLogger = new Mock<ILogger<ChoiceService>>();
            var mockChoiceRepository = new Mock<IChoiceRepository>();
            mockChoiceRepository.Setup(m => m.GetChoice(It.IsAny<Guid>()))
                .Returns(choice);
            var mockDTOConversionService = new Mock<IDTOConversionService>();
            mockDTOConversionService.Setup(m => m.ConvertChoiceToChoiceDTO(It.IsAny<Choice>()))
                .Returns(expectedChoiceDTO);
            var sut = new ChoiceService(mockChoiceRepository.Object, mockDTOConversionService.Object, mockLogger.Object);
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
            var choiceId = Guid.NewGuid();
            var mockLogger = new Mock<ILogger<ChoiceService>>();
            var mockDTOConversionService = new Mock<IDTOConversionService>();
            var mockChoiceRepository = new Mock<IChoiceRepository>();
            mockChoiceRepository.Setup(m => m.GetChoice(It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no choice with id: {choiceId}"));
            var sut = new ChoiceService(mockChoiceRepository.Object, mockDTOConversionService.Object, mockLogger.Object);
            //Act & Assert
            Assert.That(()=>sut.GetChoice(choiceId),Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void DeleteChoice_ExistingChoice_ChoiceDeletedFromRepository()
        {
            //Arragne
            var choiceId = Guid.NewGuid();
            var nextSceneId = Guid.NewGuid();
            const string choiceText = "Some choice text";
            var choice = new Choice()
            {
                Id = choiceId,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            var expectedChoiceDTO = new ChoiceDTO()
            {
                Id = choiceId,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            var mockLogger = new Mock<ILogger<ChoiceService>>();
            var mockChoiceRepository = new Mock<IChoiceRepository>();
            mockChoiceRepository.Setup(m => m.DeleteChoice(It.IsAny<Guid>()))
                .Returns(choice);
            var mockDTOConversionService = new Mock<IDTOConversionService>();
            mockDTOConversionService.Setup(m=>m.ConvertChoiceToChoiceDTO(It.IsAny<Choice>()))
                .Returns(expectedChoiceDTO);
            var sut = new ChoiceService(mockChoiceRepository.Object, mockDTOConversionService.Object, mockLogger.Object);
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
            var choiceId = Guid.NewGuid();
            var mockLogger = new Mock<ILogger<ChoiceService>>();
            var mockChoiceRepository = new Mock<IChoiceRepository>();
            mockChoiceRepository.Setup(m => m.DeleteChoice(It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no choice with id: {choiceId}"));
            var mockDTOConversionService = new Mock<IDTOConversionService>();
            var sut = new ChoiceService(mockChoiceRepository.Object, mockDTOConversionService.Object, mockLogger.Object);
            //Act & Assert
            Assert.That(()=> sut.DeleteChoice(choiceId),Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void GetChoices_NoChoiceExists_EmptyListReturned()
        {
            //Arrange
            var choices = new List<Choice>();
            var mockLogger = new Mock<ILogger<ChoiceService>>();
            var mockChoiceRepository = new Mock<IChoiceRepository>();
            mockChoiceRepository.Setup(m=>m.GetChoices())
                .Returns(choices);
            var mockDTOConversionService = new Mock<IDTOConversionService>();
            var sut = new ChoiceService(mockChoiceRepository.Object, mockDTOConversionService.Object, mockLogger.Object);
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
            var mockLogger = new Mock<ILogger<ChoiceService>>();
            var mockChoiceRepository = new Mock<IChoiceRepository>();
            mockChoiceRepository.Setup(m=>m.GetChoices())
                .Returns(choices);
            var mockDTOConversion = new Mock<IDTOConversionService>();
            var sut = new ChoiceService(mockChoiceRepository.Object, mockDTOConversion.Object, mockLogger.Object);
            //Act
            var numberOfChoice = sut.GetChoices().Count();
            //Assert
            Assert.That(numberOfChoice, Is.EqualTo(2));
        }
    }
}
