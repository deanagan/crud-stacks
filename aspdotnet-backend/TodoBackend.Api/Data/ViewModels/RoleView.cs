using System;

namespace TodoBackend.Api.Data.ViewModels
{
    public class RoleView
    {
        public RoleView Clone()
        {
            return (RoleView) MemberwiseClone();
        }
        public Guid UniqueId { get; set; }
        public string Kind { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
