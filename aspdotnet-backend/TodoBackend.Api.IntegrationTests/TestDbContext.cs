using Microsoft.EntityFrameworkCore;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Data.Contexts
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
        {
        }

        public DbSet<Epic> Epics { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryDetail> StoryDetails { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            modelBuilder.Entity<User>()
                 .HasOne(a => a.Role);

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Scrum Master",
                    Description = "A scrum master has more administration rights."
                },
                new Role
                {
                    Id = 2,
                    Name = "Developer",
                    Description = "A developer can do basic operations."
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User {
                    Id = 1,
                    Name = "Jack Black",
                    RoleId = 1,
                    Email = "jack.black@todo.io",
                    Hash = "123456"
                },
                new User {
                    Id = 2,
                    Name = "John Smith",
                    RoleId = 2,
                    Email = "john.smith@todo.io",
                    Hash = "12345678"
                },
                new User {
                    Id = 3,
                    Name = "Jane Doe",
                    RoleId = 2,
                    Email = "jane.doe@todo.io",
                    Hash = "988767"
                }
            );
        }

    }
}
