using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;

namespace DepartmentUserApp.ViewModels.StudentGroups
{
    public class StudentGroupListPageViewModel
    {
        public string GroupSearch { get; set; } = string.Empty;

        public string StudentSearch { get; set; } = string.Empty;

        public AcademicCourse? Course { get; set; }

        public bool OnlyGroupsWithDebts { get; set; }

        public bool OnlyStudentsWithDebts { get; set; }

        public bool OnlyHighRisk { get; set; }

        public PagedResult<StudentGroupListItemViewModel> Result { get; set; } = new();
    }
}