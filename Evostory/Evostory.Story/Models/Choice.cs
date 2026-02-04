using System.ComponentModel.DataAnnotations.Schema;

namespace EvoStory.Database.Models
{
    public class Choice
    {
        public required Guid Id { get; set; }
        public string ChoiceText { get; set; }
        public Guid? SceneId { get; set; }

        [ForeignKey("SceneId")]
        public Scene Scene { get; set; }
        public Guid? NextSceneId { get; set; }

        [ForeignKey("NextSceneId")]
        public Scene NextScene { get; set; }

        public Guid? RequiredItemId { get; set; }
        public Guid? RewardItemId { get; set; }

        [ForeignKey("RewardItemId")]
        public virtual Item? RewardItem { get; set; }

        [ForeignKey("RequiredItemId")]
        public virtual Item? RequiredItem { get; set; }
    }
    }
