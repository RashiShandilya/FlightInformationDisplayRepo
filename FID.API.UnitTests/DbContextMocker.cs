using Microsoft.EntityFrameworkCore;
using FID.API.Models;

namespace FID.API.UnitTests
{
    public static class DbContextMocker
    {
        public static FIDContext GetFIDDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<FIDContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new FIDContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
