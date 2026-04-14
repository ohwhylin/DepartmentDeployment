using DepartmentDataModels.Enums;

namespace DepartmentOneCMockApi.Models
{
    public class StudentOrderBlockMockModel
    {
        public int Id { get; set; }

        public int StudentOrderId { get; set; }

        public int EducationDirectionId { get; set; }

        public StudentOrderType StudentOrderType { get; set; }

        public List<StudentOrderBlockStudentMockModel> Students { get; set; } = new();
    }
}