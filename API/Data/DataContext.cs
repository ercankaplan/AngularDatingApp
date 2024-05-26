using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

/// <summary>
/// Represents the database context for the application.
/// </summary>
public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }

    //It is not added this DbSet because it isn't going to be used on its own in the application
    //public DbSet<Photo> Photos { get; set; }


}



