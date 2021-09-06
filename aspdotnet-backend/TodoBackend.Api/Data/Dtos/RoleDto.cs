using System;

namespace TodoBackend.Api.Data.Dtos
{
    public class RoleDto
    {
        public RoleDto Clone()
        {
            return (RoleDto) MemberwiseClone();
        }

        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Kind { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
