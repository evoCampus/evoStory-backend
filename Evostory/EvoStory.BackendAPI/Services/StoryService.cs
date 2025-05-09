using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class StoryService(IStoryRepository storyRepository) : IStoryService
    {
        public void CreateStory(CreateStoryDTO story)
        {
            var newStory = new Story
            {
                Id = Guid.NewGuid(),
                Scenes = story.Scenes.Select(sceneDTO => new Scene()
                {
                    Choices = sceneDTO.Choices.Select(choiceDTO => new Choice()
                    {
                        ChoiceText = choiceDTO.ChoiceText,
                        Id = Guid.NewGuid(),
                        NextSceneId = choiceDTO.NextSceneId
                    }).ToList(),
                    Content = new Content
                    {
                        Id = Guid.NewGuid(),
                        Text = sceneDTO.Content.Text,
                        ImageId = sceneDTO.Content.ImageId,
                        SoundId = sceneDTO.Content.SoundId
                    },
                    Id = Guid.NewGuid()
                }),
                StartingSceneId = story.StartingSceneId ?? Guid.NewGuid(),
                Title = story.Title
            };
            storyRepository.CreateStory(newStory);
        }

        public void DeleteStory(Guid storyId)
        {
            storyRepository.DeleteStory(storyId);
        }

        public StoryDTO GetStory(Guid storyId)
        {
            var result = storyRepository.GetStory(storyId);
            if (result == null)
            {
                return null;
            }
            var storyDTO = new StoryDTO
            {
                Id = result.Id,
                Scenes = result.Scenes.Select(scene => new SceneDTO()
                {
                    Choices = scene.Choices.Select(choice => new ChoiceDTO()
                    {
                        ChoiceText = choice.ChoiceText,
                        Id = choice.Id,
                        NextSceneId = choice.NextSceneId
                    }).ToList(),
                    Content = new ContentDTO
                    {
                        Id = scene.Content.Id,
                        Text = scene.Content.Text,
                        ImageId = scene.Content.ImageId,
                        SoundId = scene.Content.SoundId
                    },
                    Id = scene.Id
                }),
                StartingSceneId = result.StartingSceneId,
                Title = result.Title
            };
            return storyDTO;
        }

        public IEnumerable<StoryDTO> GetStories()
        {
            var result = storyRepository.GetStories();
            if (result == null)
            {
                return null;
            }
            var storiesDTO = result.Select(story => new StoryDTO
            {
                Id = story.Id,
                Scenes = story.Scenes.Select(scene => new SceneDTO()
                {
                    Choices = scene.Choices.Select(choice => new ChoiceDTO()
                    {
                        ChoiceText = choice.ChoiceText,
                        Id = choice.Id,
                        NextSceneId = choice.NextSceneId
                    }).ToList(),
                    Content = new ContentDTO
                    {
                        Id = scene.Content.Id,
                        Text = scene.Content.Text,
                        ImageId = scene.Content.ImageId,
                        SoundId = scene.Content.SoundId
                    },
                    Id = scene.Id
                }),
                StartingSceneId = story.StartingSceneId,
                Title = story.Title
            });
            return storiesDTO;
        }

        public void EditStory(EditStoryDTO story)
        {
            var newStory = new Story
            {
                Id = story.Id,
                Scenes = story.Scenes.Select(sceneDTO => new Scene()
                {
                    Choices = sceneDTO.Choices.Select(choiceDTO => new Choice()
                    {
                        ChoiceText = choiceDTO.ChoiceText,
                        Id = choiceDTO.Id,
                        NextSceneId = choiceDTO.NextSceneId
                    }).ToList(),
                    Content = new Content
                    {
                        Id = sceneDTO.Content.Id,
                        Text = sceneDTO.Content.Text,
                        ImageId = sceneDTO.Content.ImageId,
                        SoundId = sceneDTO.Content.SoundId
                    },
                    Id = sceneDTO.Id
                }),
                StartingSceneId = story.StartingSceneId ?? Guid.NewGuid(),
                Title = story.Title
            };
            storyRepository.EditStory(newStory);
        }
    }
}
