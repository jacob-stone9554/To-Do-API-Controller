using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using ToDoAPI_Controller.Models;

namespace ToDoAPI_Controller.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Item> Items { get; set; }
    }
}
