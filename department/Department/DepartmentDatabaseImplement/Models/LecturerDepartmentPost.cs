using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Models;

namespace DepartmentDatabaseImplement.Models
{
    [DataContract]
    public class LecturerDepartmentPost : ILecturerDepartmentPostModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public string DepartmentPostTitle { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public int Order { get; private set; }

        [ForeignKey("LecturerDepartmentPostId")]
        public virtual List<Lecturer> Lecturers { get; set; }

        public static LecturerDepartmentPost? Create(LecturerDepartmentPostBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                DepartmentPostTitle = model.DepartmentPostTitle,
                Order = model.Order,
            };
        }

        public void Update(LecturerDepartmentPostBindingModel model)
        {
            if (model == null) return;
            DepartmentPostTitle = model.DepartmentPostTitle;
            Order = model.Order;
        }

        public LecturerDepartmentPostViewModel GetViewModel => new()
        {
            Id = Id,
            DepartmentPostTitle = DepartmentPostTitle,
            Order = Order,
        };
    }
}
