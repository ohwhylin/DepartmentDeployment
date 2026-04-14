using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleServiceDatabaseImplement
{
    public class ScheduleServiceDatabaseFactory : IDesignTimeDbContextFactory<ScheduleServiceDatabase>
    {
        public ScheduleServiceDatabase CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ScheduleServiceDatabase>();

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = "Host=localhost;Port=5545;Database=schedule_db;Username=schedule_user;Password=h3";
            }

            optionsBuilder.UseNpgsql(connectionString);

            return new ScheduleServiceDatabase(optionsBuilder.Options);
        }
    }
}
