using System.Collections.Generic;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;

namespace DepartmentUserApp.ViewModels.StudentOrders
{
    public class StudentOrderBlockListItemViewModel
    {
        public StudentOrderBlockViewModel Block { get; set; } = new();
        public List<StudentOrderBlockStudentViewModel> Students { get; set; } = new();
    }

    public class StudentOrderListItemViewModel
    {
        public StudentOrderViewModel Order { get; set; } = new();
        public List<StudentOrderBlockListItemViewModel> Blocks { get; set; } = new();
    }

    public class StudentOrderListPageViewModel
    {
        public string? StudentSearch { get; set; }
        public string? GroupSearch { get; set; }
        public StudentOrderType? OrderType { get; set; }

        public List<StudentOrderType> OrderTypes { get; set; } = new();

        public PagedResult<StudentOrderListItemViewModel> Result { get; set; } = new();
    }
}