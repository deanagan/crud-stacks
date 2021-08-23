using System.Collections.Generic;

namespace PlanningBoard.Api.Data.Models
{
    public class Story
    {
        public int Id { get; set; }

        public string Summary { get; set; }

        public State State { get; set; }

        public Epic Epic { get; set; }

        public User User { get; set; }

        public ICollection<StoryDetail> StoryDetails { get; set; }

    }
}
