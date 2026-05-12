using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.BusinessLogicsContracts
{
    public interface ISystemRoleLogic
    {
        List<SystemRoleViewModel>? ReadList(SystemRoleSearchModel? model);
        SystemRoleViewModel? ReadElement(SystemRoleSearchModel model);
        bool Create(SystemRoleBindingModel model);
        bool Update(SystemRoleBindingModel model);
        bool Delete(SystemRoleBindingModel model);
    }
}