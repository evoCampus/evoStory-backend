using EvoStory.BackendAPI.Controllers;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Exceptions;
using EvoStory.BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace EvoStory.BackendAPI.uTest.Controller
{
    class ChoiceControllerTest
    {
        private Guid _choiceId;
        private Guid _nextsceneId;
        private Guid _sceneId;
        private ChoiceDTO _choiceDTO;
        private CreateChoiceDTO _createChoiceDTO;
        private Mock<ILogger<ChoiceController>> _mockLogger;
        private Mock<IChoiceService> _mockChoiceService;
        private ChoiceController _sut;

        [SetUp]
        public void Setup()
        {
            _choiceId = Guid.NewGuid();
            _nextsceneId = Guid.NewGuid();
            _sceneId = Guid.NewGuid();
            _choiceDTO = new ChoiceDTO()
            {
                Id = _choiceId,
                ChoiceText = "Some choice text",
                NextSceneId = _nextsceneId
            };
            _createChoiceDTO = new CreateChoiceDTO()
            {
                SceneId = _sceneId,
                ChoiceText = "Some choice text",
                NextSceneId = _nextsceneId
            };
            _mockLogger = new Mock<ILogger<ChoiceController>>();
            _mockChoiceService = new Mock<IChoiceService>();
            _sut = new ChoiceController(_mockChoiceService.Object, _mockLogger.Object);
        }

        [Test]
        public void CreateChoice_ValidChoice_CreatedReturned()
        {
            //Arrange
            _mockChoiceService.Setup(m => m.CreateChoice(It.IsAny<CreateChoiceDTO>()))
                .Returns(_choiceDTO);

            //Act
            var result = _sut.CreateChoice(_createChoiceDTO);

            //Assert
            Assert.That(result, Is.InstanceOf<CreatedResult>());
            var createdResult = result as CreatedResult;
            Assert.That(createdResult!.Value, Is.InstanceOf<ChoiceDTO>());
            var createdChoice = createdResult.Value as ChoiceDTO;
            Assert.That(createdChoice!.Id, Is.EqualTo(_choiceId));
            Assert.That(createdChoice.ChoiceText, Is.EqualTo(_createChoiceDTO.ChoiceText));
            Assert.That(createdChoice.NextSceneId, Is.EqualTo(_nextsceneId));
        }

        [Test]
        public void CreateChoice_NonExistentScene_BadRequestReturned()
        {
            //Arrange
            _mockChoiceService.Setup(m => m.CreateChoice(It.IsAny<CreateChoiceDTO>()))
                .Throws(new RepositoryException($"There is no scene with id: {_sceneId}"));

            //Act
            var result = _sut.CreateChoice(_createChoiceDTO);

            //Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult!.Value, Is.EqualTo($"There is no scene with id: {_sceneId}"));
        }

        [Test]
        public void GetChoice_ValidChoiceId_OkReturned()
        {
            //Arrange
            _mockChoiceService.Setup(m => m.GetChoice(It.IsAny<Guid>()))
                .Returns(_choiceDTO);

            //Act
            var result = _sut.GetChoice(_choiceId);

            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult!.Value, Is.InstanceOf<ChoiceDTO>());
            var choice = okResult.Value as ChoiceDTO;
            Assert.That(choice!.Id, Is.EqualTo(_choiceId));
            Assert.That(choice.ChoiceText, Is.EqualTo(_choiceDTO.ChoiceText));
            Assert.That(choice.NextSceneId, Is.EqualTo(_choiceDTO.NextSceneId));
        }

        [Test]
        public void GetChoice_NonExistentChoiceId_NotFoundReturned()
        {
            //Arrange
            _mockChoiceService.Setup(m => m.GetChoice(It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no choice with id: {_choiceId}"));

            //Act
            var result = _sut.GetChoice(_choiceId);

            //Assert
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult!.Value, Is.EqualTo($"There is no choice with id: {_choiceId}"));
        }

        [Test]
        public void DeleteChoice_ValidChoiceId_OkReturned()
        {
            //Arrange
            _mockChoiceService.Setup(m => m.DeleteChoice(It.IsAny<Guid>()))
                .Returns(_choiceDTO);

            //Act
            var result = _sut.DeleteChoice(_choiceId);

            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult!.Value, Is.InstanceOf<ChoiceDTO>());
            var deletedChoice = okResult.Value as ChoiceDTO;
            Assert.That(deletedChoice!.Id, Is.EqualTo(_choiceId));
            Assert.That(deletedChoice.ChoiceText, Is.EqualTo(_choiceDTO.ChoiceText));
            Assert.That(deletedChoice.NextSceneId, Is.EqualTo(_choiceDTO.NextSceneId));
        }

        [Test]
        public void DeleteChoice_NonExistentChoiceId_NotFoundReturned()
        {
            //Arrange
            _mockChoiceService.Setup(m => m.DeleteChoice(It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no choice with id: {_choiceId}"));

            //Act
            var result = _sut.DeleteChoice(_choiceId);

            //Assert
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult!.Value, Is.EqualTo($"There is no choice with id: {_choiceId}"));
        }

        [Test]
        public void GetChoices_ChoicesExist_OkReturned()
        {
            //Arrange
            var choiceDTOs = new List<ChoiceDTO>()
            {
                new ChoiceDTO()
                {
                    Id = Guid.NewGuid(),
                    ChoiceText = "Some choice text",
                    NextSceneId = Guid.NewGuid()
                }
            };
            _mockChoiceService.Setup(m => m.GetChoices())
                .Returns(choiceDTOs);

            //Act
            var result = _sut.GetChoices();

            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult!.Value, Is.InstanceOf<List<ChoiceDTO>>());
            var choices = okResult.Value as List<ChoiceDTO>;
            Assert.That(choices!.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetChoices_NoChoiceExists_OkReturned()
        {
            //Arrange
            _mockChoiceService.Setup(m => m.GetChoices())
                .Returns(new List<ChoiceDTO>());

            //Act
            var result = _sut.GetChoices();

            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult!.Value, Is.InstanceOf<List<ChoiceDTO>>());
            var choices = okResult.Value as List<ChoiceDTO>;
            Assert.That(choices!.Count, Is.EqualTo(0));
        }
    }
}
