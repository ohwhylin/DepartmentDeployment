using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace DepartmentRestApi.Tests.Infrastructure;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", TestEnvironment.ConnectionString);

        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            var testSettings = new Dictionary<string, string?>
            {
                ["AcademicPlanSyncSchedule:Enabled"] = "false",
                ["AcademicPlanSyncSchedule:DayOfWeek"] = "Sunday",
                ["AcademicPlanSyncSchedule:Hour"] = "3",
                ["AcademicPlanSyncSchedule:Minute"] = "0",

                ["OneCConnection:BaseUrl"] = TestEnvironment.OneCBaseUrl,
                ["OneCConnection:Login"] = "",
                ["OneCConnection:Password"] = "",
                ["OneCConnection:AcademicPlansEndpoint"] = "/academic-plans",
                ["OneCConnection:AcademicPlanRecordsEndpoint"] = "",
                ["OneCConnection:StudentGroupsEndpoint"] = "/students/groups",
                ["OneCConnection:StudentsEndpoint"] = "/students",
                ["OneCConnection:DisciplineStudentRecordsEndpoint"] = "/students/marks",
                ["OneCConnection:StudentOrdersEndpoint"] = "/student-orders"
            };

            config.AddInMemoryCollection(testSettings);
        });
    }
}