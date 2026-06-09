using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.ViewModels.LecturerStudyPosts
{
    public class LecturerStudyPostListPageViewModel
    {
        public string Search { get; set; } = string.Empty;

        public PagedResult<LecturerStudyPostViewModel> Result { get; set; } = new();
    }
}