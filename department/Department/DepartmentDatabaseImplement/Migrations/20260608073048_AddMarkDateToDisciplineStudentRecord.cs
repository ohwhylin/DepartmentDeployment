using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepartmentDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class AddMarkDateToDisciplineStudentRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MarkDate",
                table: "DisciplineStudentRecords",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkDate",
                table: "DisciplineStudentRecords");
        }
    }
}
