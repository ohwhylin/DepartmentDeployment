using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.StoragesContracts
{
    public interface ISystemPermissionStorage
    {
        List<SystemPermissionViewModel> GetFullList();
        List<SystemPermissionViewModel> GetFilteredList(SystemPermissionSearchModel model);
        SystemPermissionViewModel? GetElement(SystemPermissionSearchModel model);
        SystemPermissionViewModel? Insert(SystemPermissionBindingModel model);
        SystemPermissionViewModel? Update(SystemPermissionBindingModel model);
        SystemPermissionViewModel? Delete(SystemPermissionBindingModel model);
    }
}