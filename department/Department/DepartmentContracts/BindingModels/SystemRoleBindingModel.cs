using DepartmentDataModels.Models;

namespace DepartmentContracts.BindingModels
{
    public class SystemRoleBindingModel : ISystemRoleModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}