using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.BusinessLogicsContracts
{
    public interface ISystemUserRoleLogic
    {
        List<SystemUserRoleViewModel>? ReadList(SystemUserRoleSearchModel? model);
        SystemUserRoleViewModel? ReadElement(SystemUserRoleSearchModel model);
        bool Create(SystemUserRoleBindingModel model);
        bool Delete(SystemUserRoleBindingModel model);
    }
}