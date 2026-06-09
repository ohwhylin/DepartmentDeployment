using DepartmentDataModels.Enums;

namespace DepartmentContracts.SearchModels
{
    public class ClassroomSearchModel
    {
        public int? Id { get; set; }
        public string? Number { get; set; }
        public ClassroomTypes? Type { get; set; }
        public int? Capacity { get; set; }
        public bool? NotUseInSchedule { get; set; }
        public bool? HasProjector { get; set; }

        public bool? UseInSchedule { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}