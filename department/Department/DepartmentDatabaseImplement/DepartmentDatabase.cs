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
    }
}


