using CitasApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitasApp.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

     public DbSet<AppUser> Users { get; set; }  
}

