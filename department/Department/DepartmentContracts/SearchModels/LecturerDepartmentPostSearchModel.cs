namespace DepartmentContracts.SearchModels
{
    public class LecturerDepartmentPostSearchModel
    {
        public int? Id { get; set; }
        public string? DepartmentPostTitle { get; set; }
        public int? Order { get; set; }

        public string? Search { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}