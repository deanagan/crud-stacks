using System;

namespace TodoBackend.Api.Data.ViewModels
{
    public class RoleViewModel
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
