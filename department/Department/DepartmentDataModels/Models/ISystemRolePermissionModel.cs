namespace DepartmentDataModels.Models
{
    public interface ISystemRolePermissionModel : IId
    {
        int Id { get; }
        int RoleId { get; }
        int PermissionId { get; }
    }
}