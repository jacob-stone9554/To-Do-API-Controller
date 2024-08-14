using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using ToDoAPI_Controller.Models;

namespace ToDoAPI_Controller.Data
{
    /*
     * AppDbContext is a necessary class for EF Core to interact with the database. (using base options)
     * It is used when EF is creating the database to help it map out tables.
     */
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Item> Items { get; set; }
    }
}
