namespace DepartmentDataModels.Models
{
    public interface ISystemUserModel : IId
    {
        int Id { get; }
        string Login { get; }
        bool IsActive { get; }
    }
}