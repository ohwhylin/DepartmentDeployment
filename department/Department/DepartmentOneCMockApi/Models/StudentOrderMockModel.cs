using DepartmentDataModels.Enums;

namespace DepartmentOneCMockApi.Models
{
    public class StudentOrderMockModel
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public StudentOrderType StudentOrderType { get; set; }

        public List<StudentOrderBlockMockModel> Blocks { get; set; } = new();
    }
}