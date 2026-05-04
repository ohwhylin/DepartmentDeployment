using DepartmentDataModels.Enums;

namespace DepartmentContracts.ViewModels
{
    public class StudentMovementHistoryViewModel
    {
        public int StudentOrderId { get; set; }

        public int StudentOrderBlockId { get; set; }

        public int StudentOrderBlockStudentId { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public StudentOrderType StudentOrderType { get; set; }

        public int? StudentGroupFromId { get; set; }

        public string? StudentGroupFromName { get; set; }

        public int? StudentGroupToId { get; set; }

        public string? StudentGroupToName { get; set; }

        public string MovementDescription { get; set; } = string.Empty;
    }
}