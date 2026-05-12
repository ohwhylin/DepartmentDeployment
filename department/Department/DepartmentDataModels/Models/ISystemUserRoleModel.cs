namespace DepartmentDataModels.Models
{
    public interface ISystemUserRoleModel : IId
    {
        int Id { get; }
        int UserId { get; }
        int RoleId { get; }
    }
}