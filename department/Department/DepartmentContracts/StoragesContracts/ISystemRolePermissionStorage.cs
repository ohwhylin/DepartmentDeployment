using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.StoragesContracts
{
    public interface ISystemRolePermissionStorage
    {
        List<SystemRolePermissionViewModel> GetFullList();
        List<SystemRolePermissionViewModel> GetFilteredList(SystemRolePermissionSearchModel model);
        SystemRolePermissionViewModel? GetElement(SystemRolePermissionSearchModel model);
        SystemRolePermissionViewModel? Insert(SystemRolePermissionBindingModel model);
        SystemRolePermissionViewModel? Delete(SystemRolePermissionBindingModel model);
    }
}