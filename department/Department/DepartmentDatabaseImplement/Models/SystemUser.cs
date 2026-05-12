using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Models;

namespace DepartmentDatabaseImplement.Models
{
    [DataContract]
    public class SystemUser : ISystemUserModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public string Login { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public bool IsActive { get; private set; }

        [ForeignKey("UserId")]
        public virtual List<SystemUserRole> UserRoles { get; set; } = new();

        public static SystemUser? Create(SystemUserBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                Login = model.Login,
                IsActive = model.IsActive
            };
        }

        public void Update(SystemUserBindingModel model)
        {
            if (model == null) return;
            Login = model.Login;
            IsActive = model.IsActive;
        }

        public SystemUserViewModel GetViewModel => new()
        {
            Id = Id,
            Login = Login,
            IsActive = IsActive
        };
    }
}