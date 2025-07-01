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
        [Test]
        public void CreateChoice_ValidChoice_CreatedReturned()
        {
            //Arrange
            var choiceId = Guid.NewGuid();
            const string choiceText = "Some choice text";
            var nextSceneId = Guid.NewGuid();
            var choiceDTO = new ChoiceDTO()
            {
                Id = choiceId,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            var createChoiceDTO = new CreateChoiceDTO()
            {
                SceneId = Guid.NewGuid(),
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            var mockLogger = new Mock<ILogger<ChoiceController>>();
            var mockChoiceService = new Mock<IChoiceService>();
            mockChoiceService.Setup(m => m.CreateChoice(It.IsAny<CreateChoiceDTO>()))
                .Returns(choiceDTO);
            var sut = new ChoiceController(mockChoiceService.Object, mockLogger.Object);
            //Act
            var result = sut.CreateChoice(createChoiceDTO);
            //Assert
            Assert.That(result, Is.InstanceOf<CreatedResult>());
        }

        [Test]
        public void CreateChoice_NonExistentScene_BadRequestReturned()
        {
            //Arrange
            var sceneId = Guid.NewGuid();
            var createChoiceDTO = new CreateChoiceDTO()
            {
                SceneId = sceneId,
                ChoiceText = "Some choice text",
                NextSceneId = Guid.NewGuid()
            };
            var mockLogger = new Mock<ILogger<ChoiceController>>();
            var mockChoiceService = new Mock<IChoiceService>();
            mockChoiceService.Setup(m => m.CreateChoice(It.IsAny<CreateChoiceDTO>()))
                .Throws(new RepositoryException($"There is no scene with id: {sceneId}"));
            var sut = new ChoiceController(mockChoiceService.Object, mockLogger.Object);
            //Act
            var result = sut.CreateChoice(createChoiceDTO);
            //Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetChoice_ValidChoiceId_OkReturned()
        {
            //Arrange
            var choiceId = Guid.NewGuid();
            var choiceDTO = new ChoiceDTO()
            {
                Id = choiceId,
                ChoiceText = "Some choice text",
                NextSceneId = Guid.NewGuid()
            };
            var mockLogger = new Mock<ILogger<ChoiceController>>();
            var mockChoiceService = new Mock<IChoiceService>();
            mockChoiceService.Setup(m => m.GetChoice(It.IsAny<Guid>()))
                .Returns(choiceDTO);
            var sut = new ChoiceController(mockChoiceService.Object, mockLogger.Object);
            //Act
            var result = sut.GetChoice(choiceId);
            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetChoice_NonExistentChoiceId_NotFoundReturned()
        {
            //Arrange
            var choiceId = Guid.NewGuid();
            var mockLogger = new Mock<ILogger<ChoiceController>>();
            var mockChoiceService = new Mock<IChoiceService>();
            mockChoiceService.Setup(m => m.GetChoice(It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no choice with id: {choiceId}"));
            var sut = new ChoiceController(mockChoiceService.Object, mockLogger.Object);
            //Act
            var result = sut.GetChoice(choiceId);
            //Assert
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public void DeleteChoice_ValidChoiceId_OkReturned()
        {
            //Arrange
            var choiceId = Guid.NewGuid();
            var choiceDTO = new ChoiceDTO()
            {
                Id = choiceId,
                ChoiceText = "Some choice text",
                NextSceneId = Guid.NewGuid()
            };
            var mockLogger = new Mock<ILogger<ChoiceController>>();
            var mockChoiceService = new Mock<IChoiceService>();
            mockChoiceService.Setup(m => m.DeleteChoice(It.IsAny<Guid>()))
                .Returns(choiceDTO);
            var sut = new ChoiceController(mockChoiceService.Object, mockLogger.Object);
            //Act
            var result = sut.DeleteChoice(choiceId);
            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void DeleteChoice_NonExistentChoiceId_NotFoundReturned()
        {
            //Arrange
            var choiceId = Guid.NewGuid();
            var mockLogger = new Mock<ILogger<ChoiceController>>();
            var mockChoiceService = new Mock<IChoiceService>();
            mockChoiceService.Setup(m => m.DeleteChoice(It.IsAny<Guid>()))
                .Throws(new RepositoryException($"There is no choice with id: {choiceId}"));
            var sut = new ChoiceController(mockChoiceService.Object, mockLogger.Object);
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
            var mockLogger = new Mock<ILogger<ChoiceController>>();
            var mockChoiceService = new Mock<IChoiceService>();
            mockChoiceService.Setup(m => m.GetChoices())
                .Returns(choiceDTOs);
            var sut = new ChoiceController(mockChoiceService.Object, mockLogger.Object);
            //Act
            var result = sut.GetChoices();
            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetChoices_NoChoiceExists_OkReturned()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ChoiceController>>();
            var mockChoiceService = new Mock<IChoiceService>();
            mockChoiceService.Setup(m => m.GetChoices())
                .Returns(new List<ChoiceDTO>());
            var sut = new ChoiceController(mockChoiceService.Object, mockLogger.Object);
            //Act
            var result = sut.GetChoices();
            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
    }
}
