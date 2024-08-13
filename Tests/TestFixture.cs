using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using ToDoAPI_Controller.Data;
using Xunit;

namespace ToDoAPI_Controller.Tests
{
    public class TestFixture : IDisposable
    {
        public AppDbContext ctx { get; set; }
        
        public TestFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            ctx = new AppDbContext(options);
        }

        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}
