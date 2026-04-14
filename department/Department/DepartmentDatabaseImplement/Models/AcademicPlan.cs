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
    public class AcademicPlan : IAcademicPlanModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public int? EducationDirectionId { get; private set; }
        public virtual EducationDirection? EducationDirection { get; set; } = null!;

        [DataMember]
        [Required]
        public AcademicCourse AcademicCourses { get; private set; }

        [DataMember]
        [Required]
        public EducationForm EducationForm { get; private set; }

        [DataMember]
        [Required]
        public string Year { get; private set; } = string.Empty;
        [ForeignKey("AcademicPlanId")]
        public virtual List<AcademicPlanRecord> AcademicPlanRecords { get; set; }

        public static AcademicPlan? Create(AcademicPlanBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                EducationDirectionId = model.EducationDirectionId,
                AcademicCourses = model.AcademicCourses,
                EducationForm = model.EducationForm,
                Year = model.Year,
            };
        }

        public void Update(AcademicPlanBindingModel model)
        {
            if (model == null) return;
            EducationDirectionId = model.EducationDirectionId;
            AcademicCourses = model.AcademicCourses;
            EducationForm = model.EducationForm;
            Year = model.Year;
        }

        public AcademicPlanViewModel GetViewModel => new()
        {
            Id = Id,
            EducationDirectionId = EducationDirectionId,
            AcademicCourses = AcademicCourses,
            EducationForm = EducationForm,
            Year = Year,
        };

    }
}
