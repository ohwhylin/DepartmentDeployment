using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Models;

namespace DepartmentDatabaseImplement.Models
{
    [DataContract]
    public class Discipline : IDisciplineModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public int DisciplineBlockId { get; private set; }
        public virtual DisciplineBlock DisciplineBlock { get; set; } = null!;

        [DataMember]
        [Required]
        public string DisciplineName { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string DisciplineShortName { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string DisciplineDescription { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public bool HasExam { get; private set; }

        [DataMember]
        [Required]
        public bool HasCredit { get; private set; }

        [DataMember]
        [Required]
        public bool HasCourseWork { get; private set; }

        [DataMember]
        [Required]
        public bool HasCourseProject { get; private set; }

        [DataMember]
        [Required]
        public string DisciplineBlockBlueAsteriskName { get; private set; } = string.Empty;
        [ForeignKey("DisciplineId")]
        public virtual List<DisciplineStudentRecord> DisciplineStudentRecords { get; set; }
        [ForeignKey("DisciplineId")]
        public virtual List<AcademicPlanRecord> AcademicPlanRecords { get; set; }

        public static Discipline? Create(DisciplineBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                DisciplineBlockId = model.DisciplineBlockId,
                DisciplineName = model.DisciplineName,
                DisciplineShortName = model.DisciplineShortName,
                DisciplineDescription = model.DisciplineDescription,
                DisciplineBlockBlueAsteriskName = model.DisciplineBlockBlueAsteriskName,
                HasExam = model.HasExam,
                HasCredit = model.HasCredit,
                HasCourseWork = model.HasCourseWork,
                HasCourseProject = model.HasCourseProject,
            };
        }

        public void Update(DisciplineBindingModel model)
        {
            if (model == null) return;
            DisciplineBlockId = model.DisciplineBlockId;
            DisciplineName = model.DisciplineName;
            DisciplineShortName = model.DisciplineShortName;
            DisciplineDescription = model.DisciplineDescription;
            DisciplineBlockBlueAsteriskName = model.DisciplineBlockBlueAsteriskName;
            HasExam = model.HasExam;
            HasCredit = model.HasCredit;
            HasCourseWork = model.HasCourseWork;
            HasCourseProject = model.HasCourseProject;
        }

        public DisciplineViewModel GetViewModel => new()
        {
            Id = Id,
            DisciplineBlockId = DisciplineBlockId,
            DisciplineName = DisciplineName,
            DisciplineShortName = DisciplineShortName,
            DisciplineDescription = DisciplineDescription,
            DisciplineBlockBlueAsteriskName = DisciplineBlockBlueAsteriskName,
            HasExam = HasExam,
            HasCredit = HasCredit,
            HasCourseWork = HasCourseWork,
            HasCourseProject = HasCourseProject,
        };
    }
}
