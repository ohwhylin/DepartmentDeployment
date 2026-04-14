using DepartmentDataModels.Enums;

namespace DepartmentOneCMockApi.Models
{
    public class StudentGroupMockModel
    {
        public int Id { get; set; }

        public int EducationDirectionId { get; set; }

        public int? CuratorId { get; set; }

        public string GroupName { get; set; } = string.Empty;

        public AcademicCourse Course { get; set; }
    }
}