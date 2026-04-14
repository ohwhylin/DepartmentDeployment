using DepartmentDataModels.Enums;

namespace DepartmentContracts.Dtos.OneC
{
    public class StudentOrderBlockOneCDto
    {
        public int Id { get; set; }
        public int StudentOrderId { get; set; }
        public int EducationDirectionId { get; set; }
        public StudentOrderType StudentOrderType { get; set; }

        public List<StudentOrderBlockStudentOneCDto> Students { get; set; } = new();
    }
}