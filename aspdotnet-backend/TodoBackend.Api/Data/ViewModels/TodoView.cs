using System;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Data.ViewModels
{
    public class TodoView
    {
        public Guid UniqueId { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public bool IsDone { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public AssigneeView Assignee { get; set; }
    }
}