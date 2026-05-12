using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Models;

namespace DepartmentDatabaseImplement.Models
{
    [DataContract]
    public class SystemPermission : ISystemPermissionModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public string Code { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string Name { get; private set; } = string.Empty;

        [ForeignKey("PermissionId")]
        public virtual List<SystemRolePermission> RolePermissions { get; set; } = new();

        public static SystemPermission? Create(SystemPermissionBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                Code = model.Code,
                Name = model.Name
            };
        }

        public void Update(SystemPermissionBindingModel model)
        {
            if (model == null) return;
            Code = model.Code;
            Name = model.Name;
        }

        public SystemPermissionViewModel GetViewModel => new()
        {
            Id = Id,
            Code = Code,
            Name = Name
        };
    }
}