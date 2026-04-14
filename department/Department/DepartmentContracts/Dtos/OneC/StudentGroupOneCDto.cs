using DepartmentDataModels.Enums;

namespace DepartmentContracts.Dtos.OneC
{
    public class StudentGroupOneCDto
    {
        public int Id { get; set; }

        public int EducationDirectionId { get; set; }

        public int? CuratorId { get; set; }

        public string GroupName { get; set; } = string.Empty;

        public AcademicCourse Course { get; set; }
    }
}