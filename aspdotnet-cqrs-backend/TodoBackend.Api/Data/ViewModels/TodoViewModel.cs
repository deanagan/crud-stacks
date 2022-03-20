using System;

namespace TodoBackend.Api.Data.ViewModels
{
    public class TodoViewModel
    {
        public Guid UniqueId { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public bool? IsDone { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public AssigneeViewModel Assignee { get; set; }
    }
}
