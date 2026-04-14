using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DepartmentDatabaseImplement
{
    public class DepartmentDatabaseFactory : IDesignTimeDbContextFactory<DepartmentDatabase>
    {
        public DepartmentDatabase CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DepartmentDatabase>();

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = "Host=localhost;Port=5543;Database=department_db;Username=department_user;Password=123456";
            }

            optionsBuilder.UseNpgsql(connectionString);

            return new DepartmentDatabase(optionsBuilder.Options);
        }
    }
}


