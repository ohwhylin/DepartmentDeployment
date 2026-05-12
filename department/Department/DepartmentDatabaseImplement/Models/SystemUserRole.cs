using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Models;

namespace DepartmentDatabaseImplement.Models
{
    [DataContract]
    public class SystemUserRole : ISystemUserRoleModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public int UserId { get; private set; }
        public virtual SystemUser User { get; set; } = null!;

        [DataMember]
        [Required]
        public int RoleId { get; private set; }
        public virtual SystemRole Role { get; set; } = null!;

        public static SystemUserRole? Create(SystemUserRoleBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                UserId = model.UserId,
                RoleId = model.RoleId
            };
        }

        public SystemUserRoleViewModel GetViewModel => new()
        {
            Id = Id,
            UserId = UserId,
            RoleId = RoleId
        };
    }
}