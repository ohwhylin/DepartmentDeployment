using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Models;

namespace DepartmentDatabaseImplement.Models
{
    [DataContract]
    public class SystemRolePermission : ISystemRolePermissionModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public int RoleId { get; private set; }
        public virtual SystemRole Role { get; set; } = null!;

        [DataMember]
        [Required]
        public int PermissionId { get; private set; }
        public virtual SystemPermission Permission { get; set; } = null!;

        public static SystemRolePermission? Create(SystemRolePermissionBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                RoleId = model.RoleId,
                PermissionId = model.PermissionId
            };
        }

        public SystemRolePermissionViewModel GetViewModel => new()
        {
            Id = Id,
            RoleId = RoleId,
            PermissionId = PermissionId
        };
    }
}