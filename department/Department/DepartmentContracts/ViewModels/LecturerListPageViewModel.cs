using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.ViewModels.Lecturers
{
    public class LecturerListPageViewModel
    {
        public string Search { get; set; } = string.Empty;

        public PagedResult<LecturerViewModel> Result { get; set; } = new();
    }
}