using System.Collections.Generic;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.ViewModels.StudentGroups
{
    public class StudentGroupStudentListItemViewModel
    {
        public StudentViewModel Student { get; set; } = new();

        public List<DisciplineStudentRecordViewModel> Debts { get; set; } = new();

        public bool HasHighRiskDebt { get; set; }
    }
}