using DepartmentDataModels.Enums;

namespace DepartmentOneCMockApi.Models
{
    public class AcademicPlanRecordMockModel
    {
        public int Id { get; set; }

        public int AcademicPlanId { get; set; }

        public int? DisciplineId { get; set; }

        public int DisciplineBlockId { get; set; }

        public string DisciplineBlockTitle { get; set; } = string.Empty;

        public string DisciplineBlockBlueAsteriskName { get; set; } = string.Empty;

        public bool DisciplineBlockUseForGrouping { get; set; }

        public int DisciplineBlockOrder { get; set; }

        public string DisciplineShortName { get; set; } = string.Empty;

        public string DisciplineDescription { get; set; } = string.Empty;

        public bool HasExam { get; set; }

        public bool HasCredit { get; set; }

        public bool HasCourseWork { get; set; }

        public bool HasCourseProject { get; set; }

        public string Index { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int Semester { get; set; }

        public int Zet { get; set; }

        public int AcademicHours { get; set; }

        public int? Exam { get; set; }

        public int? Pass { get; set; }

        public int? GradedPass { get; set; }

        public int? CourseWork { get; set; }

        public int? CourseProject { get; set; }

        public int? Rgr { get; set; }

        public int? Lectures { get; set; }

        public int? LaboratoryHours { get; set; }

        public int? PracticalHours { get; set; }
    }
}
