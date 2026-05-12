using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.BusinessLogicsContracts
{
    public interface ISystemUserLogic
    {
        List<SystemUserViewModel>? ReadList(SystemUserSearchModel? model);
        SystemUserViewModel? ReadElement(SystemUserSearchModel model);
        bool Create(SystemUserBindingModel model);
        bool Update(SystemUserBindingModel model);
        bool Delete(SystemUserBindingModel model);
    }
}