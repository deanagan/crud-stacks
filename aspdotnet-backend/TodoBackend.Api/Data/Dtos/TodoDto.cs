using System;

namespace TodoBackend.Api.Data.Dtos
{
    public class TodoDto
    {
        public TodoDto Clone()
        {
            return (TodoDto) MemberwiseClone();
        }

        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public bool IsDone { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int RoleId { get; set; }
        public Guid RoleUniqueId { get; set; }
        public string RoleKind { get; set; }
        public string RoleDescription { get; set; }
        public DateTime RoleCreated { get; set; }
        public DateTime RoleUpdated { get; set; }
    }
}
