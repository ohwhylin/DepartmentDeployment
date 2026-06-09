namespace DepartmentUserApp.ViewModels.Disciplines
{
    public class DisciplineCatalogItemViewModel
    {
        public int PrimaryId { get; set; }
        public string DisciplineName { get; set; } = string.Empty;
        public string DisciplineShortName { get; set; } = string.Empty;
        public string DisciplineDescription { get; set; } = string.Empty;
        public int VariantsCount { get; set; }
    }
}