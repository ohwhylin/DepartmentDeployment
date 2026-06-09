using DepartmentContracts.ViewModels;
using DepartmentContracts.ViewModels.StudentGroups;
using System.Collections.Generic;

namespace DepartmentUserApp.ViewModels.StudentGroups
{
    public class StudentGroupListItemViewModel
    {
        public StudentGroupViewModel Group { get; set; } = new();

        public int StudentCount { get; set; }

        public int StudentsWithDebtsCount { get; set; }

        public int HighRiskStudentsCount { get; set; }

        public List<StudentGroupStudentListItemViewModel> Students { get; set; } = new();
    }
}