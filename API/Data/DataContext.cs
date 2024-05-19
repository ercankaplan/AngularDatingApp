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
}



