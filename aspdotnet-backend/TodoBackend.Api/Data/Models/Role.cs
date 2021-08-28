using System;

namespace TodoBackend.Api.Data.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        public string RoleKind { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

    }
}
