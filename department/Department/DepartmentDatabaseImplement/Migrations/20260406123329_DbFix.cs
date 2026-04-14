using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepartmentDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class DbFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassroomType",
                table: "Classrooms",
                newName: "Type");

            migrationBuilder.AlterColumn<int>(
                name: "StudentGroupToId",
                table: "StudentOrderBlockStudents",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "StudentGroupFromId",
                table: "StudentOrderBlockStudents",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "HasCourseProject",
                table: "Disciplines",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCourseWork",
                table: "Disciplines",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCredit",
                table: "Disciplines",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasExam",
                table: "Disciplines",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasProjector",
                table: "Classrooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "AcademicPlans",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasCourseProject",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "HasCourseWork",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "HasCredit",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "HasExam",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "HasProjector",
                table: "Classrooms");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Classrooms",
                newName: "ClassroomType");

            migrationBuilder.AlterColumn<int>(
                name: "StudentGroupToId",
                table: "StudentOrderBlockStudents",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentGroupFromId",
                table: "StudentOrderBlockStudents",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "AcademicPlans",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
