using DepartmentDataModels.Enums;

namespace DepartmentOneCMockApi.Models
{
    public class AcademicPlanMockModel
    {
        public int Id { get; set; }

        public int? EducationDirectionId { get; set; }

        public EducationForm EducationForm { get; set; }

        public AcademicCourse AcademicCourses { get; set; }

        public string Year { get; set; } = string.Empty;

        public List<AcademicPlanRecordMockModel> AcademicPlanRecords { get; set; } = new();
    }
}