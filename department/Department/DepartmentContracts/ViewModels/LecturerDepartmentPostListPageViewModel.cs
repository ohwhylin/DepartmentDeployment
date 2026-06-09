using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.ViewModels.LecturerDepartmentPosts
{
    public class LecturerDepartmentPostListPageViewModel
    {
        public string Search { get; set; } = string.Empty;

        public PagedResult<LecturerDepartmentPostViewModel> Result { get; set; } = new();
    }
}