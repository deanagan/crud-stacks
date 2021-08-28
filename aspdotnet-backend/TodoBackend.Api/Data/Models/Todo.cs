using System;
namespace TodoBackend.Api.Data.Models
{
    public class Todo
    {
        public int TodoId { get; set; }
        public Guid TodoUniqueId { get; set; }
        public string Name { get; set; }
        public int Detail { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
    }
}
