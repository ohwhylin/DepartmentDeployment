using System.Collections.Generic;

namespace DepartmentContracts.ViewModels
{
    public class DisciplineStudentRecordGroupPageViewModel
    {
        public string GroupSearch { get; set; } = string.Empty;
        public string StudentSearch { get; set; } = string.Empty;

        public PagedResult<StudentGroupViewModel> Groups { get; set; } = new();
        public List<StudentViewModel> Students { get; set; } = new();
        public List<DisciplineViewModel> Disciplines { get; set; } = new();
        public List<DisciplineStudentRecordViewModel> Records { get; set; } = new();
    }
}