using DepartmentDataModels.Enums;

namespace DepartmentContracts.SearchModels
{
    public class EducationDirectionSearchModel
    {
        public int? Id { get; set; }

        public string? Search { get; set; }

        public string? Cipher { get; set; }
        public string? ShortName { get; set; }
        public string? Title { get; set; }
        public EducationDirectionQualification? Qualification { get; set; }
        public string? Profile { get; set; }
        public string? Description { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}