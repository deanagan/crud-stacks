using System.Collections.Generic;

namespace TodoBackend.Api.Data.Models
{
    public class Epic
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set;}

        public ICollection<Story> Stories { get; set;}
    }
}
