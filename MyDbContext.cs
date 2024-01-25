using Microsoft.EntityFrameworkCore;

namespace EFCore0004;

public class MyDbContext : DbContext
{
    public DbSet<User> Users {get; set;}
    public DbSet<Group> Groups {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Database004");
    }

}
