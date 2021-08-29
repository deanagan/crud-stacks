using System;

namespace TodoBackend.Api.Data.Models
{
    public class Role
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Kind { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}
