using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.BusinessLogicsContracts
{
    public interface IAuthProfileLogic
    {
        AuthProfileViewModel? ReadProfile(AuthProfileSearchModel model);
    }
}