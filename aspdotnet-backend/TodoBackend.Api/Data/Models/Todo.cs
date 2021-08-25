namespace TodoBackend.Api.Data.Models
{
    public class Todo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Detail { get; set; }

        public bool IsDone { get; set; }
    }
}