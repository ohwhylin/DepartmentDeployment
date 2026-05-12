using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.StoragesContracts
{
    public interface ISystemUserStorage
    {
        List<SystemUserViewModel> GetFullList();
        List<SystemUserViewModel> GetFilteredList(SystemUserSearchModel model);
        SystemUserViewModel? GetElement(SystemUserSearchModel model);
        SystemUserViewModel? Insert(SystemUserBindingModel model);
        SystemUserViewModel? Update(SystemUserBindingModel model);
        SystemUserViewModel? Delete(SystemUserBindingModel model);
    }
}