using DepartmentContracts.Attributes;
using DepartmentDataModels.Models;

namespace DepartmentContracts.ViewModels
{
    public class SystemUserRoleViewModel : ISystemUserRoleModel
    {
        [Column(visible: false)]
        public int Id { get; set; }

        [Column(visible: false)]
        public int UserId { get; set; }

        [Column(title: "Пользователь", width: 220)]
        public string UserLogin { get; set; } = string.Empty;

        [Column(visible: false)]
        public int RoleId { get; set; }

        [Column(title: "Роль", width: 220)]
        public string RoleName { get; set; } = string.Empty;
    }
}