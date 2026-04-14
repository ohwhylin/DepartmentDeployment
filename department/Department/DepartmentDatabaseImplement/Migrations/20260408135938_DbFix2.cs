using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepartmentDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class DbFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicPlanRecords_Disciplines_DisciplineId",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "InDepartment",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "IsActiveSemester",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "IsChild",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "IsFacultative",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "IsParent",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "IsUseInWorkload",
                table: "AcademicPlanRecords");

            migrationBuilder.AddColumn<int>(
                name: "EducationForm",
                table: "AcademicPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DisciplineId",
                table: "AcademicPlanRecords",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "AcademicHours",
                table: "AcademicPlanRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseProject",
                table: "AcademicPlanRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseWork",
                table: "AcademicPlanRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Exam",
                table: "AcademicPlanRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GradedPass",
                table: "AcademicPlanRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Index",
                table: "AcademicPlanRecords",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LaboratoryHours",
                table: "AcademicPlanRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Lectures",
                table: "AcademicPlanRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AcademicPlanRecords",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Pass",
                table: "AcademicPlanRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PracticalHours",
                table: "AcademicPlanRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rgr",
                table: "AcademicPlanRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicPlanRecords_Disciplines_DisciplineId",
                table: "AcademicPlanRecords",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicPlanRecords_Disciplines_DisciplineId",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "EducationForm",
                table: "AcademicPlans");

            migrationBuilder.DropColumn(
                name: "AcademicHours",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "CourseProject",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "CourseWork",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "Exam",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "GradedPass",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "LaboratoryHours",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "Lectures",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "Pass",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "PracticalHours",
                table: "AcademicPlanRecords");

            migrationBuilder.DropColumn(
                name: "Rgr",
                table: "AcademicPlanRecords");

            migrationBuilder.AlterColumn<int>(
                name: "DisciplineId",
                table: "AcademicPlanRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InDepartment",
                table: "AcademicPlanRecords",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActiveSemester",
                table: "AcademicPlanRecords",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsChild",
                table: "AcademicPlanRecords",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFacultative",
                table: "AcademicPlanRecords",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsParent",
                table: "AcademicPlanRecords",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUseInWorkload",
                table: "AcademicPlanRecords",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicPlanRecords_Disciplines_DisciplineId",
                table: "AcademicPlanRecords",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
