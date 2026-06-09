using System;
using DepartmentDataModels.Enums;

namespace DepartmentContracts.SearchModels
{
    public class DisciplineStudentRecordSearchModel
    {
        public int? Id { get; set; }
        public int? DisciplineId { get; set; }
        public int? StudentId { get; set; }
        public Semesters? Semester { get; set; }
        public string? Variant { get; set; }
        public int? SubGroup { get; set; }
        public MarkType? MarkType { get; set; }
        public DateTime? MarkDate { get; set; }

        public string? GroupSearch { get; set; }
        public string? StudentSearch { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}