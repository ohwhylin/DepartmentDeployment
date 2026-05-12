namespace DepartmentDataModels.Models
{
    public interface ISystemRoleModel : IId
    {
        int Id { get; }
        string Code { get; }
        string Name { get; }
    }
}