using DepartmentContracts.Attributes;
using DepartmentDataModels.Models;

namespace DepartmentContracts.ViewModels
{
    public class SystemPermissionViewModel : ISystemPermissionModel
    {
        [Column(visible: false)]
        public int Id { get; set; }

        [Column(title: "Код", width: 220)]
        public string Code { get; set; } = string.Empty;

        [Column(title: "Название", width: 260)]
        public string Name { get; set; } = string.Empty;
    }
}