namespace DepartmentContracts.SearchModels
{
    public class LecturerStudyPostSearchModel
    {
        public int? Id { get; set; }
        public string? StudyPostTitle { get; set; }
        public int? Hours { get; set; }

        public string? Search { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}