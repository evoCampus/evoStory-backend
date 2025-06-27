﻿using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;
using EvoStory.BackendAPI.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
