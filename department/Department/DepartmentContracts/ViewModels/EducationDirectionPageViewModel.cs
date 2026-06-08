using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;

namespace DepartmentUserApp.ViewModels.EducationDirections
{
    public class EducationDirectionListPageViewModel
    {
        public string Search { get; set; } = string.Empty;
        public EducationDirectionQualification? Qualification { get; set; }

        public PagedResult<EducationDirectionViewModel> Result { get; set; } =
            new PagedResult<EducationDirectionViewModel>();
    }
}