using System;
namespace TodoBackend.Api.Data.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public bool IsDone { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Guid AssigneeGuid { get; set; }
    }
}
