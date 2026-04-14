using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Models;

namespace DepartmentDatabaseImplement.Models
{
    [DataContract]
    public class StudentOrderBlockStudent : IStudentOrderBlockStudentModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public int StudentOrderBlockId { get; private set; }
        public virtual StudentOrderBlock StudentOrderBlock { get; set; } = null!;

        [DataMember]
        [Required]
        public int StudentId { get; private set; }
        public virtual Student Student { get; set; } = null!;

        [DataMember]
        public int? StudentGroupFromId { get; private set; }
        public virtual StudentGroup? StudentGroupFrom { get; set; } = null!;

        [DataMember]
        public int? StudentGroupToId { get; private set; }
        public virtual StudentGroup? StudentGroupTo { get; set; } = null!;

        public static StudentOrderBlockStudent? Create(StudentOrderBlockStudentBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                StudentOrderBlockId = model.StudentOrderBlockId,
                StudentId = model.StudentId,
                StudentGroupFromId = model.StudentGroupFromId,
                StudentGroupToId = model.StudentGroupToId,
            };
        }

        public void Update(StudentOrderBlockStudentBindingModel model)
        {
            if (model == null) return;
            StudentOrderBlockId = model.StudentOrderBlockId;
            StudentId = model.StudentId;
            StudentGroupFromId = model.StudentGroupFromId;
            StudentGroupToId = model.StudentGroupToId;
        }

        public StudentOrderBlockStudentViewModel GetViewModel => new()
        {
            Id = Id,
            StudentOrderBlockId = StudentOrderBlockId,
            StudentId = StudentId,
            StudentGroupFromId = StudentGroupFromId,
            StudentGroupToId = StudentGroupToId,
        };
    }
}
