using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepartmentDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class MarkAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarkType",
                table: "DisciplineStudentRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkType",
                table: "DisciplineStudentRecords");
        }
    }
}
