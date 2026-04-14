using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Models;
using DepartmentDataModels.Enums;

namespace DepartmentDatabaseImplement.Models
{
    [DataContract]
    public class StudentGroup : IStudentGroupModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public int EducationDirectionId { get; private set; }
        public virtual EducationDirection EducationDirection { get; set; } = null!;

        [DataMember]
        public int? CuratorId { get; private set; }
        public virtual Lecturer? Curator { get; set; }

        [DataMember]
        [Required]
        public string GroupName { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public AcademicCourse Course { get; private set; }
        [ForeignKey("StudentGroupId")]
        public virtual List<Student> Students { get; set; }
        [ForeignKey("StudentGroupFromId")]
        public virtual List<StudentOrderBlockStudent> StudentFromOrderBlockStudents { get; set; }
        [ForeignKey("StudentGroupToId")]
        public virtual List<StudentOrderBlockStudent> StudentToOrderBlockStudents { get; set; }

        public static StudentGroup? Create(StudentGroupBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                EducationDirectionId = model.EducationDirectionId,
                CuratorId = model.CuratorId,
                GroupName = model.GroupName,
                Course = model.Course,
            };
        }

        public void Update(StudentGroupBindingModel model)
        {
            if (model == null) return;
            EducationDirectionId = model.EducationDirectionId;
            CuratorId = model.CuratorId;
            GroupName = model.GroupName;
            Course = model.Course;
        }

        public StudentGroupViewModel GetViewModel => new()
        {
            Id = Id,
            EducationDirectionId = EducationDirectionId,
            CuratorId = CuratorId,
            GroupName = GroupName,
            Course = Course,
        };
    }
}
