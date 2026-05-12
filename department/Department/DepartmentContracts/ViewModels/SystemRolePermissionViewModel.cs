using DepartmentContracts.Attributes;
using DepartmentDataModels.Models;

namespace DepartmentContracts.ViewModels
{
    public class SystemRolePermissionViewModel : ISystemRolePermissionModel
    {
        [Column(visible: false)]
        public int Id { get; set; }

        [Column(visible: false)]
        public int RoleId { get; set; }

        [Column(title: "Роль", width: 220)]
        public string RoleName { get; set; } = string.Empty;

        [Column(visible: false)]
        public int PermissionId { get; set; }

        [Column(title: "Право", width: 260)]
        public string PermissionName { get; set; } = string.Empty;
    }
}