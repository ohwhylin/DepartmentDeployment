using Microsoft.EntityFrameworkCore;
using DepartmentDatabaseImplement.Models;

namespace DepartmentDatabaseImplement
{
    public class DepartmentDatabase : DbContext
    {
        public DepartmentDatabase()
        {
        }

        public DepartmentDatabase(DbContextOptions<DepartmentDatabase> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    connectionString = "Host=localhost;Port=5543;Database=department_db;Username=department_user;Password=123456";
                }

                optionsBuilder.UseNpgsql(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentGroup>()
                .HasOne(x => x.Curator)
                .WithMany()
                .HasForeignKey(x => x.CuratorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentOrderBlockStudent>()
                .HasOne(x => x.StudentGroupFrom)
                .WithMany(x => x.StudentFromOrderBlockStudents)
                .HasForeignKey(x => x.StudentGroupFromId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentOrderBlockStudent>()
                .HasOne(x => x.StudentGroupTo)
                .WithMany(x => x.StudentToOrderBlockStudents)
                .HasForeignKey(x => x.StudentGroupToId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentOrderBlockStudent>()
                .HasOne(x => x.Student)
                .WithMany(x => x.StudentOrderBlockStudents)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentOrderBlockStudent>()
                .HasOne(x => x.StudentOrderBlock)
                .WithMany(x => x.StudentOrderBlockStudents)
                .HasForeignKey(x => x.StudentOrderBlockId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SystemUser>()
                .HasIndex(x => x.Login)
                .IsUnique();

            modelBuilder.Entity<SystemRole>()
                .HasIndex(x => x.Code)
                .IsUnique();

            modelBuilder.Entity<SystemPermission>()
                .HasIndex(x => x.Code)
                .IsUnique();

            modelBuilder.Entity<SystemUserRole>()
                .HasIndex(x => new { x.UserId, x.RoleId })
                .IsUnique();

            modelBuilder.Entity<SystemRolePermission>()
                .HasIndex(x => new { x.RoleId, x.PermissionId })
                .IsUnique();

            modelBuilder.Entity<SystemUserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SystemUserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SystemRolePermission>()
                .HasOne(x => x.Role)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SystemRolePermission>()
                .HasOne(x => x.Permission)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SystemRole>().HasData(
                new { Id = 1, Code = "LabHead", Name = "Зав. лабораторией" },
                new { Id = 2, Code = "Admin", Name = "Администратор" },
                new { Id = 3, Code = "DepartmentHead", Name = "Зав. кафедрой" },
                new { Id = 4, Code = "Teacher", Name = "Преподаватель" },
                new { Id = 5, Code = "Developer", Name = "Разработчик" }
            );

            modelBuilder.Entity<SystemPermission>().HasData(
                new { Id = 1, Code = "Core.Access", Name = "Доступ к основному модулю" },
                new { Id = 2, Code = "Load.Access", Name = "Доступ к модулю расчета нагрузки" },
                new { Id = 3, Code = "Lab.Inventory.Access", Name = "Доступ к МОЛ" },
                new { Id = 4, Code = "Lab.DutySchedule.Access", Name = "Доступ к графику дежурств" },
                new { Id = 5, Code = "Lab.Schedule.View", Name = "Просмотр расписания" },
                new { Id = 6, Code = "Lab.Schedule.BookConsultation", Name = "Добавление консультаций" }
            );

            modelBuilder.Entity<SystemRolePermission>().HasData(
                new { Id = 1, RoleId = 1, PermissionId = 3 },
                new { Id = 2, RoleId = 1, PermissionId = 4 },
                new { Id = 3, RoleId = 1, PermissionId = 6 },

                new { Id = 4, RoleId = 2, PermissionId = 4 },
                new { Id = 5, RoleId = 2, PermissionId = 6 },

                new { Id = 6, RoleId = 3, PermissionId = 2 },
                new { Id = 7, RoleId = 3, PermissionId = 4 },
                new { Id = 8, RoleId = 3, PermissionId = 6 },

                new { Id = 9, RoleId = 4, PermissionId = 6 },

                new { Id = 10, RoleId = 5, PermissionId = 1 },
                new { Id = 11, RoleId = 5, PermissionId = 2 },
                new { Id = 12, RoleId = 5, PermissionId = 3 },
                new { Id = 13, RoleId = 5, PermissionId = 4 },
                new { Id = 14, RoleId = 5, PermissionId = 5 },
                new { Id = 15, RoleId = 5, PermissionId = 6 }
            );
        }

        public virtual DbSet<AcademicPlan> AcademicPlans { get; set; }
        public virtual DbSet<AcademicPlanRecord> AcademicPlanRecords { get; set; }
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<Discipline> Disciplines { get; set; }
        public virtual DbSet<DisciplineBlock> DisciplineBlocks { get; set; }
        public virtual DbSet<DisciplineStudentRecord> DisciplineStudentRecords { get; set; }
        public virtual DbSet<EducationDirection> EducationDirections { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
        public virtual DbSet<LecturerDepartmentPost> LecturerDepartmentPosts { get; set; }
        public virtual DbSet<LecturerStudyPost> LecturerStudyPosts { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentGroup> StudentGroups { get; set; }
        public virtual DbSet<StudentOrder> StudentOrders { get; set; }
        public virtual DbSet<StudentOrderBlock> StudentOrderBlocks { get; set; }
        public virtual DbSet<StudentOrderBlockStudent> StudentOrderBlockStudents { get; set; }
        public virtual DbSet<SystemUser> SystemUsers { get; set; }
        public virtual DbSet<SystemRole> SystemRoles { get; set; }
        public virtual DbSet<SystemPermission> SystemPermissions { get; set; }
        public virtual DbSet<SystemUserRole> SystemUserRoles { get; set; }
        public virtual DbSet<SystemRolePermission> SystemRolePermissions { get; set; }
    }
}


