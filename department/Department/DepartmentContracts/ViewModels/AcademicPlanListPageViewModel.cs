using System.Collections.Generic;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;

namespace DepartmentUserApp.ViewModels.AcademicPlans
{
    public class AcademicPlanListPageViewModel
    {
        public AcademicCourse? Course { get; set; }
        public string Year { get; set; } = string.Empty;
        public EducationDirectionQualification? Qualification { get; set; }

        public PagedResult<AcademicPlanViewModel> Result { get; set; } =
            new PagedResult<AcademicPlanViewModel>();

        public List<AcademicPlanRecordViewModel> Records { get; set; } = new();
    }
}