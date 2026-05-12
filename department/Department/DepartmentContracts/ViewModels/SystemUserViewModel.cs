using DepartmentContracts.Attributes;
using DepartmentDataModels.Models;

namespace DepartmentContracts.ViewModels
{
    public class SystemUserViewModel : ISystemUserModel
    {
        [Column(visible: false)]
        public int Id { get; set; }

        [Column(title: "Логин", width: 200)]
        public string Login { get; set; } = string.Empty;

        [Column(title: "Активен", width: 100)]
        public bool IsActive { get; set; }
    }
}