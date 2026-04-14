using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MolServiceDatabaseImplement
{
    public class MOLServiceDatabaseFactory : IDesignTimeDbContextFactory<MOLServiceDatabase>
    {
        public MOLServiceDatabase CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MOLServiceDatabase>();

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = "Host=localhost;Port=5544;Database=mol_db;Username=mol_user;Password=123456";
            }

            optionsBuilder.UseNpgsql(connectionString);

            return new MOLServiceDatabase(optionsBuilder.Options);
        }
    }
}
