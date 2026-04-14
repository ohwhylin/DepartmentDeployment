using DepartmentDataModels.Enums;

namespace DepartmentOneCMockApi.Models
{
    public class DisciplineStudentRecordMockModel
    {
        public int Id { get; set; }

        public int DisciplineId { get; set; }

        public int StudentId { get; set; }

        public Semesters Semester { get; set; }

        public string Variant { get; set; } = string.Empty;

        public int SubGroup { get; set; }
        public MarkType MarkType { get; set; }
    }
}