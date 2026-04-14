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
    public class DisciplineStudentRecord : IDisciplineStudentRecordModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public int DisciplineId { get; private set; }
        public virtual Discipline Discipline { get; set; } = null!;

        [DataMember]
        [Required]
        public int StudentId { get; private set; }
        public virtual Student Student { get; set; } = null!;

        [DataMember]
        [Required]
        public Semesters Semester { get; private set; }

        [DataMember]
        [Required]
        public string Variant { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public int SubGroup { get; private set; }
        [DataMember]
        [Required]
        public MarkType MarkType { get; private set; }

        public static DisciplineStudentRecord? Create(DisciplineStudentRecordBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                DisciplineId = model.DisciplineId,
                StudentId = model.StudentId,
                Semester = model.Semester,
                Variant = model.Variant,
                SubGroup = model.SubGroup,
                MarkType = model.MarkType,
            };
        }

        public void Update(DisciplineStudentRecordBindingModel model)
        {
            if (model == null) return;
            DisciplineId = model.DisciplineId;
            StudentId = model.StudentId;
            Semester = model.Semester;
            Variant = model.Variant;
            SubGroup = model.SubGroup;
            MarkType = model.MarkType;
        }

        public DisciplineStudentRecordViewModel GetViewModel => new()
        {
            Id = Id,
            DisciplineId = DisciplineId,
            StudentId = StudentId,
            Semester = Semester,
            Variant = Variant,
            SubGroup = SubGroup,
            MarkType = MarkType,
        };
    }
}
