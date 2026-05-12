using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.BusinessLogicsContracts
{
    public interface ISystemPermissionLogic
    {
        List<SystemPermissionViewModel>? ReadList(SystemPermissionSearchModel? model);
        SystemPermissionViewModel? ReadElement(SystemPermissionSearchModel model);
        bool Create(SystemPermissionBindingModel model);
        bool Update(SystemPermissionBindingModel model);
        bool Delete(SystemPermissionBindingModel model);
    }
}