using DepartmentDataModels.Models;

namespace DepartmentContracts.BindingModels
{
    public class SystemPermissionBindingModel : ISystemPermissionModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}