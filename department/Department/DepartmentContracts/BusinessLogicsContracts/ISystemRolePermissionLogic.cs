using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.BusinessLogicsContracts
{
    public interface ISystemRolePermissionLogic
    {
        List<SystemRolePermissionViewModel>? ReadList(SystemRolePermissionSearchModel? model);
        SystemRolePermissionViewModel? ReadElement(SystemRolePermissionSearchModel model);
        bool Create(SystemRolePermissionBindingModel model);
        bool Delete(SystemRolePermissionBindingModel model);
    }
}