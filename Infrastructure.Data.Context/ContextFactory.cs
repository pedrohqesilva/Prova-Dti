using Application.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connString = "Data Source=DTI.db";

            var dbContext = new DbContextOptionsBuilder()
                .UseSqlite(connString)
                .UseLazyLoadingProxies();

            return new MyContext(dbContext.Options);
        }
    }
}