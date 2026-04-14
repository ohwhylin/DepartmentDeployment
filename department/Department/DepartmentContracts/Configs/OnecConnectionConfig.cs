namespace DepartmentContracts.Configs
{
    public class OneCConnectionConfig
    {
        public string BaseUrl { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string AcademicPlansEndpoint { get; set; } = string.Empty;

        public string AcademicPlanRecordsEndpoint { get; set; } = string.Empty;

        public string StudentGroupsEndpoint { get; set; } = string.Empty;

        public string StudentsEndpoint { get; set; } = string.Empty;

        public string DisciplineStudentRecordsEndpoint { get; set; } = string.Empty;
        public string StudentOrdersEndpoint { get; set; } = string.Empty;
    }
}