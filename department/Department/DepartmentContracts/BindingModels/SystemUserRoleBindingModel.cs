using DepartmentDataModels.Models;

namespace DepartmentContracts.BindingModels
{
    public class SystemUserRoleBindingModel : ISystemUserRoleModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}