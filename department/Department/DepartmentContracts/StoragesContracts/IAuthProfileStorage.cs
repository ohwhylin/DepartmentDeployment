using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentContracts.StoragesContracts
{
    public interface IAuthProfileStorage
    {
        AuthProfileViewModel? GetProfile(AuthProfileSearchModel model);
    }
}