using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.ViewModels.Disciplines
{
    public class DisciplineListPageViewModel
    {
        public string Search { get; set; } = string.Empty;
        public PagedResult<DisciplineCatalogItemViewModel> Result { get; set; }
            = new PagedResult<DisciplineCatalogItemViewModel>
            {
                Page = 1,
                PageSize = 10,
                TotalCount = 0
            };
    }
}