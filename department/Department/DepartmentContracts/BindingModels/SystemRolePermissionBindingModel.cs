using DepartmentDataModels.Models;

namespace DepartmentContracts.BindingModels
{
    public class SystemRolePermissionBindingModel : ISystemRolePermissionModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}