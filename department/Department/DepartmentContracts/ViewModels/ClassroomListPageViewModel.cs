using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;

namespace DepartmentUserApp.ViewModels.Classrooms
{
    public class ClassroomListPageViewModel
    {
        public string Search { get; set; } = string.Empty;
        public ClassroomTypes? Type { get; set; }
        public bool? HasProjector { get; set; }
        public bool? UseInSchedule { get; set; }

        public PagedResult<ClassroomViewModel> Result { get; set; } = new();
    }
}