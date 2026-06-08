using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.ViewModels.Disciplines
{
    public class DisciplineListPageViewModel
    {
        public string Search { get; set; } = string.Empty;

        public PagedResult<DisciplineViewModel> Result { get; set; } =
            new PagedResult<DisciplineViewModel>();
    }
}