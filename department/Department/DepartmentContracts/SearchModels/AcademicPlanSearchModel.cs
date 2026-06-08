using DepartmentDataModels.Enums;

namespace DepartmentContracts.SearchModels
{
    public class AcademicPlanSearchModel
    {
        public int? Id { get; set; }
        public int? EducationDirectionId { get; set; }
        public AcademicCourse? AcademicCourses { get; set; }
        public string? Year { get; set; }
        public EducationDirectionQualification? Qualification { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}