using DepartmentDataModels.Models;

namespace DepartmentContracts.BindingModels
{
    public class SystemUserBindingModel : ISystemUserModel
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}