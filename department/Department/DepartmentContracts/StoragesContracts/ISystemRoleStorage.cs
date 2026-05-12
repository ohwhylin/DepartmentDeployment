using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.StoragesContracts
{
    public interface ISystemRoleStorage
    {
        List<SystemRoleViewModel> GetFullList();
        List<SystemRoleViewModel> GetFilteredList(SystemRoleSearchModel model);
        SystemRoleViewModel? GetElement(SystemRoleSearchModel model);
        SystemRoleViewModel? Insert(SystemRoleBindingModel model);
        SystemRoleViewModel? Update(SystemRoleBindingModel model);
        SystemRoleViewModel? Delete(SystemRoleBindingModel model);
    }
}