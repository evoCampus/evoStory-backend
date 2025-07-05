using Evostory.Story.Models;
using EvoStory.BackendAPI.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace EvoStory.BackendAPI.uTest.Services
{
    class DTOConversionServiceTest
    {
        [Test]
        public void ConvertChoiceToChoiceDTO_CompleteChoice_ChoiceDTOCreatedWithEqualValues()
        {
            // Arrange = Given
            var mockLogger = new Mock<ILogger<DTOConversionService>>();
            Guid id = Guid.NewGuid();
            const string choiceText = "Some choice text.";
            Guid nextSceneId = Guid.NewGuid();
            var choice = new Choice()
            {
                Id = id,
                ChoiceText = choiceText,
                NextSceneId = nextSceneId
            };
            var sut = new DTOConversionService(mockLogger.Object);

            // Act = When
            var choiceDTO = sut.ConvertChoiceToChoiceDTO(choice);

            // Assert = Then
            Assert.That(choiceDTO.Id, Is.EqualTo(id));
            Assert.That(choiceDTO.ChoiceText, Is.EqualTo(choiceText));
            Assert.That(choiceDTO.NextSceneId, Is.EqualTo(nextSceneId));
        }
    }
}
