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
    public class AcademicPlanRecord : IAcademicPlanRecordModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public int AcademicPlanId { get; private set; }
        public virtual AcademicPlan AcademicPlan { get; set; } = null!;

        [DataMember]
        public int? DisciplineId { get; private set; }
        public virtual Discipline? Discipline { get; set; } = null!;
        [DataMember]
        [Required]
        public virtual string Index { get; private set; } = string.Empty;
        [DataMember]
        [Required]
        public virtual string Name { get; private set; } = string.Empty;
        [DataMember]
        [Required]
        public virtual int Semester { get; private set; }
        [DataMember]
        [Required]
        public virtual int Zet { get; private set; }
        [DataMember]
        [Required]
        public virtual int AcademicHours { get; private set; }
        [DataMember]
        public virtual int? Exam { get; private set; }
        [DataMember]
        public virtual int? Pass { get; private set; }
        [DataMember]
        public virtual int? GradedPass { get; private set; }
        [DataMember]
        public virtual int? CourseWork { get; private set; }
        [DataMember]
        public virtual int? CourseProject { get; private set; }
        [DataMember]
        public virtual int? Rgr { get; private set; }
        [DataMember]
        public virtual int? Lectures { get; private set; }
        [DataMember]
        public virtual int? LaboratoryHours { get; private set; }
        [DataMember]
        public virtual int? PracticalHours { get; private set; }
        [ForeignKey("AcademicPlanRecordParentId")]
        public virtual List<AcademicPlanRecord> AcademicPlanRecords { get; set; }

        public static AcademicPlanRecord? Create(AcademicPlanRecordBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                AcademicPlanId = model.AcademicPlanId,
                Index = model.Index,
                Name = model.Name,
                Semester = model.Semester,
                Zet = model.Zet,
                AcademicHours = model.AcademicHours,
                Exam = model.Exam,
                Pass = model.Pass,
                GradedPass = model.GradedPass,
                CourseWork = model.CourseWork,
                CourseProject = model.CourseProject,
                Rgr = model.Rgr,
                Lectures = model.Lectures,
                LaboratoryHours = model.LaboratoryHours,
                PracticalHours = model.PracticalHours,
                DisciplineId = model.DisciplineId,
            };
        }

        public void Update(AcademicPlanRecordBindingModel model)
        {
            if (model == null) return;
            AcademicPlanId = model.AcademicPlanId;
            Index = model.Index;
            Name = model.Name;
            Semester = model.Semester;
            Zet = model.Zet;
            AcademicHours = model.AcademicHours;
            Exam = model.Exam;
            Pass = model.Pass;
            GradedPass = model.GradedPass;
            CourseWork = model.CourseWork;
            CourseProject = model.CourseProject;
            Rgr = model.Rgr;
            Lectures = model.Lectures;
            LaboratoryHours = model.LaboratoryHours;
            PracticalHours = model.PracticalHours;
            DisciplineId = model.DisciplineId;
        }

        public AcademicPlanRecordViewModel GetViewModel => new()
        {
            Id = Id,
            AcademicPlanId = AcademicPlanId,
            Index = Index,
            Name = Name,
            Semester = Semester,
            Zet = Zet,
            AcademicHours = AcademicHours,
            Exam = Exam,
            Pass = Pass,
            GradedPass = GradedPass,
            CourseWork = CourseWork,
            CourseProject = CourseProject,
            Rgr = Rgr,
            Lectures = Lectures,
            LaboratoryHours = LaboratoryHours,
            PracticalHours = PracticalHours,
            DisciplineId = DisciplineId,
        };
    }
}