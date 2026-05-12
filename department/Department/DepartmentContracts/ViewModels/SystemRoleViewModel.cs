using DepartmentContracts.Attributes;
using DepartmentDataModels.Models;

namespace DepartmentContracts.ViewModels
{
    public class SystemRoleViewModel : ISystemRoleModel
    {
        [Column(visible: false)]
        public int Id { get; set; }

        [Column(title: "Код", width: 180)]
        public string Code { get; set; } = string.Empty;

        [Column(title: "Название", width: 220)]
        public string Name { get; set; } = string.Empty;
    }
}