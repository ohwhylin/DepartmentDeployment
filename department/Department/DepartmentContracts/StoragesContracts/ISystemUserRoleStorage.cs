using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.StoragesContracts
{
    public interface ISystemUserRoleStorage
    {
        List<SystemUserRoleViewModel> GetFullList();
        List<SystemUserRoleViewModel> GetFilteredList(SystemUserRoleSearchModel model);
        SystemUserRoleViewModel? GetElement(SystemUserRoleSearchModel model);
        SystemUserRoleViewModel? Insert(SystemUserRoleBindingModel model);
        SystemUserRoleViewModel? Delete(SystemUserRoleBindingModel model);
    }
}