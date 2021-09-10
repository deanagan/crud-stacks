using System;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Data.ViewModels
{
    public class TodoView
    {
         public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public int Detail { get; set; }
        public bool IsDone { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Role role { get; set; }
    }
}
