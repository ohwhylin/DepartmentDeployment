using DepartmentContracts.Dtos.OneC;

namespace DepartmentContracts.BusinessLogicsContracts
{
    public interface IOneCApiService
    {
        Task<List<AcademicPlanOneCDto>> GetAcademicPlansAsync();
        Task<List<AcademicPlanRecordOneCDto>> GetAcademicPlanRecordsAsync();
        Task<List<StudentGroupOneCDto>> GetStudentGroupsAsync();
        Task<List<StudentOneCDto>> GetStudentsAsync();
        Task<List<DisciplineStudentRecordOneCDto>> GetDisciplineStudentRecordsAsync();
        Task<List<StudentOrderOneCDto>> GetStudentOrdersAsync();

    }
}
