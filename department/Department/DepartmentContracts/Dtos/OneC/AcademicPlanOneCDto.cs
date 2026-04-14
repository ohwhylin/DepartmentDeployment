using DepartmentDataModels.Enums;

namespace DepartmentContracts.Dtos.OneC
{
    public class AcademicPlanOneCDto
    {
        public int Id { get; set; }

        public int? EducationDirectionId { get; set; }

        public AcademicCourse AcademicCourses { get; set; }
        public EducationForm EducationForm { get; set; }

        public string Year { get; set; }

        public List<AcademicPlanRecordOneCDto> AcademicPlanRecords { get; set; } = new();
    }
}