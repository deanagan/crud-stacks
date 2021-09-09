using System;
namespace TodoBackend.Api.Data.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public int Detail { get; set; }
        public bool IsDone { get; set; }
        public int AssigneeId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
