
namespace TodoBackend.Api.Data.Models
{
    public class StoryDetail
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool isDone { get; set; }

        public Story Story { get; set; }
    }
}
