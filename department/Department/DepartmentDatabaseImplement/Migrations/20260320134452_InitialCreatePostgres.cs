using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DepartmentDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatePostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "text", nullable: false),
                    ClassroomType = table.Column<int>(type: "integer", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    NotUseInSchedule = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    DisciplineBlockBlueAsteriskName = table.Column<string>(type: "text", nullable: false),
                    DisciplineBlockUseForGrouping = table.Column<bool>(type: "boolean", nullable: false),
                    DisciplineBlockOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationDirections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cipher = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Qualification = table.Column<int>(type: "integer", nullable: false),
                    Profile = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDirections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LecturerDepartmentPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentPostTitle = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerDepartmentPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LecturerStudyPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudyPostTitle = table.Column<string>(type: "text", nullable: false),
                    Hours = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerStudyPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderNumber = table.Column<string>(type: "text", nullable: false),
                    StudentOrderType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisciplineBlockId = table.Column<int>(type: "integer", nullable: false),
                    DisciplineName = table.Column<string>(type: "text", nullable: false),
                    DisciplineShortName = table.Column<string>(type: "text", nullable: false),
                    DisciplineDescription = table.Column<string>(type: "text", nullable: false),
                    DisciplineBlockBlueAsteriskName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplines_DisciplineBlocks_DisciplineBlockId",
                        column: x => x.DisciplineBlockId,
                        principalTable: "DisciplineBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EducationDirectionId = table.Column<int>(type: "integer", nullable: true),
                    AcademicCourses = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicPlans_EducationDirections_EducationDirectionId",
                        column: x => x.EducationDirectionId,
                        principalTable: "EducationDirections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LecturerStudyPostId = table.Column<int>(type: "integer", nullable: true),
                    LecturerDepartmentPostId = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: false),
                    Abbreviation = table.Column<string>(type: "text", nullable: false),
                    DateBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    MobileNumber = table.Column<string>(type: "text", nullable: false),
                    HomeNumber = table.Column<string>(type: "text", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    Rank2 = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Photo = table.Column<byte[]>(type: "bytea", nullable: true),
                    OnlyForPrivate = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecturers_LecturerDepartmentPosts_LecturerDepartmentPostId",
                        column: x => x.LecturerDepartmentPostId,
                        principalTable: "LecturerDepartmentPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lecturers_LecturerStudyPosts_LecturerStudyPostId",
                        column: x => x.LecturerStudyPostId,
                        principalTable: "LecturerStudyPosts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentOrderBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentOrderId = table.Column<int>(type: "integer", nullable: false),
                    EducationDirectionId = table.Column<int>(type: "integer", nullable: false),
                    StudentOrderType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentOrderBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentOrderBlocks_EducationDirections_EducationDirectionId",
                        column: x => x.EducationDirectionId,
                        principalTable: "EducationDirections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentOrderBlocks_StudentOrders_StudentOrderId",
                        column: x => x.StudentOrderId,
                        principalTable: "StudentOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicPlanRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AcademicPlanId = table.Column<int>(type: "integer", nullable: false),
                    DisciplineId = table.Column<int>(type: "integer", nullable: false),
                    AcademicPlanRecordParentId = table.Column<int>(type: "integer", nullable: true),
                    InDepartment = table.Column<bool>(type: "boolean", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: false),
                    Zet = table.Column<int>(type: "integer", nullable: false),
                    IsParent = table.Column<bool>(type: "boolean", nullable: false),
                    IsChild = table.Column<bool>(type: "boolean", nullable: false),
                    IsFacultative = table.Column<bool>(type: "boolean", nullable: false),
                    IsUseInWorkload = table.Column<bool>(type: "boolean", nullable: false),
                    IsActiveSemester = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicPlanRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicPlanRecords_AcademicPlanRecords_AcademicPlanRecordP~",
                        column: x => x.AcademicPlanRecordParentId,
                        principalTable: "AcademicPlanRecords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AcademicPlanRecords_AcademicPlans_AcademicPlanId",
                        column: x => x.AcademicPlanId,
                        principalTable: "AcademicPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicPlanRecords_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EducationDirectionId = table.Column<int>(type: "integer", nullable: false),
                    CuratorId = table.Column<int>(type: "integer", nullable: true),
                    GroupName = table.Column<string>(type: "text", nullable: false),
                    Course = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGroups_EducationDirections_EducationDirectionId",
                        column: x => x.EducationDirectionId,
                        principalTable: "EducationDirections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentGroups_Lecturers_CuratorId",
                        column: x => x.CuratorId,
                        principalTable: "Lecturers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentGroupId = table.Column<int>(type: "integer", nullable: true),
                    NumberOfBook = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    StudentState = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Photo = table.Column<byte[]>(type: "bytea", nullable: true),
                    IsSteward = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DisciplineStudentRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisciplineId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: false),
                    Variant = table.Column<string>(type: "text", nullable: false),
                    SubGroup = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineStudentRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineStudentRecords_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineStudentRecords_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentOrderBlockStudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentOrderBlockId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    StudentGroupFromId = table.Column<int>(type: "integer", nullable: false),
                    StudentGroupToId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentOrderBlockStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentOrderBlockStudents_StudentGroups_StudentGroupFromId",
                        column: x => x.StudentGroupFromId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentOrderBlockStudents_StudentGroups_StudentGroupToId",
                        column: x => x.StudentGroupToId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentOrderBlockStudents_StudentOrderBlocks_StudentOrderBl~",
                        column: x => x.StudentOrderBlockId,
                        principalTable: "StudentOrderBlocks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentOrderBlockStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlanRecords_AcademicPlanId",
                table: "AcademicPlanRecords",
                column: "AcademicPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlanRecords_AcademicPlanRecordParentId",
                table: "AcademicPlanRecords",
                column: "AcademicPlanRecordParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlanRecords_DisciplineId",
                table: "AcademicPlanRecords",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPlans_EducationDirectionId",
                table: "AcademicPlans",
                column: "EducationDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_DisciplineBlockId",
                table: "Disciplines",
                column: "DisciplineBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineStudentRecords_DisciplineId",
                table: "DisciplineStudentRecords",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineStudentRecords_StudentId",
                table: "DisciplineStudentRecords",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_LecturerDepartmentPostId",
                table: "Lecturers",
                column: "LecturerDepartmentPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_LecturerStudyPostId",
                table: "Lecturers",
                column: "LecturerStudyPostId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_CuratorId",
                table: "StudentGroups",
                column: "CuratorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_EducationDirectionId",
                table: "StudentGroups",
                column: "EducationDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOrderBlocks_EducationDirectionId",
                table: "StudentOrderBlocks",
                column: "EducationDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOrderBlocks_StudentOrderId",
                table: "StudentOrderBlocks",
                column: "StudentOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOrderBlockStudents_StudentGroupFromId",
                table: "StudentOrderBlockStudents",
                column: "StudentGroupFromId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOrderBlockStudents_StudentGroupToId",
                table: "StudentOrderBlockStudents",
                column: "StudentGroupToId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOrderBlockStudents_StudentId",
                table: "StudentOrderBlockStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOrderBlockStudents_StudentOrderBlockId",
                table: "StudentOrderBlockStudents",
                column: "StudentOrderBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentGroupId",
                table: "Students",
                column: "StudentGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicPlanRecords");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "DisciplineStudentRecords");

            migrationBuilder.DropTable(
                name: "StudentOrderBlockStudents");

            migrationBuilder.DropTable(
                name: "AcademicPlans");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "StudentOrderBlocks");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "DisciplineBlocks");

            migrationBuilder.DropTable(
                name: "StudentOrders");

            migrationBuilder.DropTable(
                name: "StudentGroups");

            migrationBuilder.DropTable(
                name: "EducationDirections");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "LecturerDepartmentPosts");

            migrationBuilder.DropTable(
                name: "LecturerStudyPosts");
        }
    }
}
