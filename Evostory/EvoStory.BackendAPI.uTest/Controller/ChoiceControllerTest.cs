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
        private Guid choiceId;
        private Guid nextsceneId;
        private Guid sceneId;
        private ChoiceDTO choiceDTO;
        private CreateChoiceDTO createChoiceDTO;
        private Mock<ILogger<ChoiceController>> mockLogger;
        private Mock<IChoiceService> mockChoiceService;
        private ChoiceController sut;

        [SetUp]
        public void Setup()
        {
            choiceId = Guid.NewGuid();
            nextsceneId = Guid.NewGuid();
            sceneId = Guid.NewGuid();
            choiceDTO = new ChoiceDTO()
            {
                Id = choiceId,
                ChoiceText = "Some choice text",
                NextSceneId = nextsceneId
            };
            createChoiceDTO = new CreateChoiceDTO()
            {
                SceneId = sceneId,
                ChoiceText = "Some choice text",
                NextSceneId = nextsceneId
            };
            mockLogger = new Mock<ILogger<ChoiceController>>();
            mockChoiceService = new Mock<IChoiceService>();
            sut = new ChoiceController(mockChoiceService.Object, mockLogger.Object);
        }

        [Test]
        public void CreateChoice_ValidChoice_CreatedReturned()
        {
            //Arrange
            mockChoiceService.Setup(m => m.CreateChoice(It.IsAny<CreateChoiceDTO>()))
                .Returns(choiceDTO);
            //Act
            var result = sut.CreateChoice(createChoiceDTO);
            //Assert
            Assert.That(result, Is.InstanceOf<CreatedResult>());
        }

        [Test]
        public void CreateChoice_NonExistentScene_BadRequestReturned()
        {
            //Arrange
            mockChoiceService.Setup(m => m.CreateChoice(It.IsAny<CreateChoiceDTO>()))
                .Throws(new RepositoryException($"There is no scene with id: {sceneId}"));
            //Act
            var result = sut.CreateChoice(createChoiceDTO);
            //Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetChoice_ValidChoiceId_OkReturned()
        {
            //Arrange
            mockChoiceService.Setup(m => m.GetChoice(It.IsAny<Guid>()))
                .Returns(choiceDTO);
            //Act
            var result = sut.GetChoice(choiceId);
            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetChoice_NonExistentChoiceId_NotFoundReturned()
        {
            //Arrange
            mockChoiceService.Setup(m => m.GetChoice(It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no choice with id: {choiceId}"));
            //Act
            var result = sut.GetChoice(choiceId);
            //Assert
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public void DeleteChoice_ValidChoiceId_OkReturned()
        {
            //Arrange
            mockChoiceService.Setup(m => m.DeleteChoice(It.IsAny<Guid>()))
                .Returns(choiceDTO);
            //Act
            var result = sut.DeleteChoice(choiceId);
            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void DeleteChoice_NonExistentChoiceId_NotFoundReturned()
        {
            //Arrange
            mockChoiceService.Setup(m => m.DeleteChoice(It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no choice with id: {choiceId}"));
            //Act
            var result = sut.DeleteChoice(choiceId);
            //Assert
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
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
            mockChoiceService.Setup(m => m.GetChoices())
                .Returns(choiceDTOs);
            //Act
            var result = sut.GetChoices();
            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetChoices_NoChoiceExists_OkReturned()
        {
            //Arrange
            mockChoiceService.Setup(m => m.GetChoices())
                .Returns(new List<ChoiceDTO>());
            //Act
            var result = sut.GetChoices();
            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
    }
}
