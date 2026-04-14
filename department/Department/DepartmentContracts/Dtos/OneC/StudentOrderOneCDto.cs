using DepartmentDataModels.Enums;

namespace DepartmentContracts.Dtos.OneC
{
    public class StudentOrderOneCDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public StudentOrderType StudentOrderType { get; set; }

        public List<StudentOrderBlockOneCDto> Blocks { get; set; } = new();
    }
}