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
    }
}
