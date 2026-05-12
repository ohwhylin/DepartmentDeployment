namespace DepartmentDataModels.Models
{
    public interface ISystemPermissionModel : IId
    {
        int Id { get; }
        string Code { get; }
        string Name { get; }
    }
}