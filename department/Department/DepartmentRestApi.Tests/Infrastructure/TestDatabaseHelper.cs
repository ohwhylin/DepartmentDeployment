using DepartmentDatabaseImplement;

namespace DepartmentRestApi.Tests.Infrastructure;

public static class TestDatabaseHelper
{
    public static void RecreateDatabase()
    {
        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", TestEnvironment.ConnectionString);

        using var context = new DepartmentDatabase();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    public static void ClearDatabase()
    {
        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", TestEnvironment.ConnectionString);

        using var context = new DepartmentDatabase();

        context.StudentOrderBlockStudents.RemoveRange(context.StudentOrderBlockStudents);
        context.StudentOrderBlocks.RemoveRange(context.StudentOrderBlocks);
        context.StudentOrders.RemoveRange(context.StudentOrders);

        context.DisciplineStudentRecords.RemoveRange(context.DisciplineStudentRecords);
        context.Students.RemoveRange(context.Students);
        context.StudentGroups.RemoveRange(context.StudentGroups);

        context.AcademicPlanRecords.RemoveRange(context.AcademicPlanRecords);
        context.Disciplines.RemoveRange(context.Disciplines);
        context.DisciplineBlocks.RemoveRange(context.DisciplineBlocks);
        context.AcademicPlans.RemoveRange(context.AcademicPlans);

        context.SaveChanges();
    }
}