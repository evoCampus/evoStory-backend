using EvoStory.Database.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.Database.Repository;
using EvoStory.BackendAPI.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace EvoStory.BackendAPI.uTest.Services
{
    class ChoiceServiceTest
    {
        private Guid _sceneId;
        private Guid _nextSceneId;
        private const string _choiceText = "Some choice text.";
        private Guid _choiceId;
        private Choice _choice;
        private ChoiceDTO _expectedChoiceDTO;
        private Mock<ILogger<ChoiceService>> _mockLogger;
        private Mock<IChoiceRepository> _mockChoiceRepository;
        private Mock<IDTOConversionService> _mockDTOConversionService;
        private ChoiceService _sut;

        [SetUp]
        public void Setup()
        {
            _sceneId = Guid.NewGuid();
            _nextSceneId = Guid.NewGuid();
            _choiceId = Guid.NewGuid();
            _choice = new Choice()
            {
                Id = _choiceId,
                ChoiceText = _choiceText,
                NextSceneId = _nextSceneId
            };
            _expectedChoiceDTO = new ChoiceDTO()
            {
                Id = _choiceId,
                ChoiceText = _choiceText,
                NextSceneId = _nextSceneId
            };
            _mockLogger = new Mock<ILogger<ChoiceService>>();
            _mockChoiceRepository = new Mock<IChoiceRepository>();
            _mockDTOConversionService = new Mock<IDTOConversionService>();
            _sut = new ChoiceService(_mockChoiceRepository.Object, _mockDTOConversionService.Object, _mockLogger.Object);
        }
        [Test]
        public async Task CreateChoice_ValidCreateChoiceDTO_ChoiceIsAddToRepository()
        {
            // Arrange
            var createChoiceDTO = new CreateChoiceDTO()
            {
                SceneId = _sceneId,
                ChoiceText = _choiceText,
                NextSceneId = _nextSceneId
            };
            _mockChoiceRepository.Setup(m => m.CreateChoice(It.IsAny<Choice>(), It.IsAny<Guid>()))
                .Returns(_choice);
            _mockDTOConversionService.Setup(m => m.ConvertChoiceToChoiceDTO(It.IsAny<Choice>()))
                .Returns(_expectedChoiceDTO);

            // Act
            var actualChoiceDTO = _sut.CreateChoice(createChoiceDTO);

            // Assert
            Assert.That(actualChoiceDTO.Id, Is.EqualTo(_expectedChoiceDTO.Id));
            Assert.That(actualChoiceDTO.ChoiceText, Is.EqualTo(_expectedChoiceDTO.ChoiceText));
            Assert.That(actualChoiceDTO.NextSceneId, Is.EqualTo(_expectedChoiceDTO.NextSceneId));
        }

        [Test]
        public void CreateChoice_NonExistingScene_ExceptionThrown()
        {
            //Arrange
            var createChoiceDTO = new CreateChoiceDTO()
            {
                SceneId = _sceneId,
                NextSceneId = _nextSceneId,
                ChoiceText = _choiceText
            };
            _mockChoiceRepository.Setup(m => m.CreateChoice(It.IsAny<Choice>(), It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no existent scene with id: {_sceneId}"));

            //Act & Assert
            Assert.That(()=> _sut.CreateChoice(createChoiceDTO), Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void GetChoice_ValidChoice_ChoiceDTOReturned()
        {
            //Arrange
            _mockChoiceRepository.Setup(m => m.GetChoice(It.IsAny<Guid>()))
                .Returns(_choice);
            _mockDTOConversionService.Setup(m => m.ConvertChoiceToChoiceDTO(It.IsAny<Choice>()))
                .Returns(_expectedChoiceDTO);

            //Act
            var actualChoiceDTO = _sut.GetChoice(_choiceId);

            //Assert
            Assert.That(actualChoiceDTO.Id, Is.EqualTo(_expectedChoiceDTO.Id));
            Assert.That(actualChoiceDTO.ChoiceText, Is.EqualTo(_expectedChoiceDTO.ChoiceText));
            Assert.That(actualChoiceDTO.NextSceneId, Is.EqualTo(_expectedChoiceDTO.NextSceneId));
        }

        [Test]
        public void GetChoice_NonExistentChoice_ExceptionThrown()
        {
            //Arrange
            _mockChoiceRepository.Setup(m => m.GetChoice(It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no choice with id: {_choiceId}"));

            //Act & Assert
            Assert.That(()=>_sut.GetChoice(_choiceId),Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void DeleteChoice_ExistingChoice_ChoiceDeletedFromRepository()
        {
            //Arragne
            _mockChoiceRepository.Setup(m => m.DeleteChoice(It.IsAny<Guid>()))
                .Returns(_choice);
            _mockDTOConversionService.Setup(m=>m.ConvertChoiceToChoiceDTO(It.IsAny<Choice>()))
                .Returns(_expectedChoiceDTO);

            //Act
            var actualChoiceDTO = _sut.DeleteChoice(_choiceId);

            //Assert
            Assert.That(actualChoiceDTO.Id, Is.EqualTo(_expectedChoiceDTO.Id));
            Assert.That(actualChoiceDTO.ChoiceText, Is.EqualTo(_expectedChoiceDTO.ChoiceText));
            Assert.That(actualChoiceDTO.NextSceneId, Is.EqualTo(_expectedChoiceDTO.NextSceneId));
        }

        [Test]
        public void Delete_NonExistentChoice_ExceptionThrown()
        {
            //Arrange
            _mockChoiceRepository.Setup(m => m.DeleteChoice(It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no choice with id: {_choiceId}"));

            //Act & Assert
            Assert.That(()=> _sut.DeleteChoice(_choiceId),Throws.InstanceOf<RepositoryException>());
        }

        [Test]
        public void GetChoices_NoChoiceExists_EmptyListReturned()
        {
            //Arrange
            var choices = new List<Choice>();
            _mockChoiceRepository.Setup(m=>m.GetChoices())
                .Returns(choices);

            //Act
            var resultChoices = _sut.GetChoices();

            //Assert
            Assert.That(resultChoices.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetChoices_ChoiceExists_ListReturned()
        {
            //Arrange
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
            var loggerMock =  new Mock<ILogger<ChoiceService>>();
            var choiceRepositoryMock =  new Mock<IChoiceRepository>();
            choiceRepositoryMock.Setup(m => m.CreateChoice(It.IsAny<Choice>(), It.IsAny<Guid>()))
                .ReturnsAsync(choice);
            var dtoConversionServiceMock = new Mock<IDTOConversionService>();
            dtoConversionServiceMock.Setup(m => m.ConvertChoiceToChoiceDTO(It.IsAny<Choice>()))
                .Returns(expectedChoiceDTO);

            //Act
            var resultChoices = _sut.GetChoices();

            // Act
            var actualChoiceDTO = await sut.CreateChoice(createChoiceDTO);

            // Assert
            Assert.That(actualChoiceDTO.Id, Is.EqualTo(expectedChoiceDTO.Id));
            Assert.That(actualChoiceDTO.ChoiceText, Is.EqualTo(expectedChoiceDTO.ChoiceText));
            Assert.That(actualChoiceDTO.NextSceneId, Is.EqualTo(expectedChoiceDTO.NextSceneId));
        }
    }
}
