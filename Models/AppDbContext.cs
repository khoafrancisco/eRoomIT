using eRoomIT.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace eRoomIT.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public static object? Room { get; internal set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; } 
    public DbSet<Computers> Computer { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<User>().ToTable("NguoiDung"); 
    }
}