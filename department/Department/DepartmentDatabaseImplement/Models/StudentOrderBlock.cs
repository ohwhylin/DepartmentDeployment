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
    public class StudentOrderBlock : IStudentOrderBlockModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public int StudentOrderId { get; private set; }
        public virtual StudentOrder StudentOrder { get; set; } = null!;

        [DataMember]
        [Required]
        public int EducationDirectionId { get; private set; }
        public virtual EducationDirection EducationDirection { get; set; } = null!;

        [DataMember]
        [Required]
        public StudentOrderType StudentOrderType { get; private set; }
        [ForeignKey("StudentOrderBlockId")]
        public virtual List<StudentOrderBlockStudent> StudentOrderBlockStudents { get; set; }

        public static StudentOrderBlock? Create(StudentOrderBlockBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                StudentOrderId = model.StudentOrderId,
                EducationDirectionId = model.EducationDirectionId,
                StudentOrderType = model.StudentOrderType,
            };
        }

        public void Update(StudentOrderBlockBindingModel model)
        {
            if (model == null) return;
            StudentOrderId = model.StudentOrderId;
            EducationDirectionId = model.EducationDirectionId;
            StudentOrderType = model.StudentOrderType;
        }

        public StudentOrderBlockViewModel GetViewModel => new()
        {
            Id = Id,
            StudentOrderId = StudentOrderId,
            EducationDirectionId = EducationDirectionId,
            StudentOrderType = StudentOrderType,
        };
    }
}
