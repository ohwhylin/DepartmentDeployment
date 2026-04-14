using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Models;
using DepartmentDataModels.Enums;

namespace DepartmentDatabaseImplement.Models
{
    [DataContract]
    public class EducationDirection : IEducationDirectionModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public string Cipher { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string ShortName { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string Title { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public EducationDirectionQualification Qualification { get; private set; }

        [DataMember]
        [Required]
        public string Profile { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string Description { get; private set; } = string.Empty;

        [ForeignKey("EducationDirectionId")]
        public virtual List<AcademicPlan> AcademicPlans { get; set; }
        [ForeignKey("EducationDirectionId")]
        public virtual List<StudentGroup> StudentGroups { get; set; }
        [ForeignKey("EducationDirectionId")]
        public virtual List<StudentOrderBlock> StudentOrderBlocks { get; set; }
        public static EducationDirection? Create(EducationDirectionBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                Cipher = model.Cipher,
                ShortName = model.ShortName,
                Title = model.Title,
                Qualification = model.Qualification,
                Profile = model.Profile,
                Description = model.Description,
            };
        }

        public void Update(EducationDirectionBindingModel model)
        {
            if (model == null) return;
            Cipher = model.Cipher;
            ShortName = model.ShortName;
            Title = model.Title;
            Qualification = model.Qualification;
            Profile = model.Profile;
            Description = model.Description;
        }

        public EducationDirectionViewModel GetViewModel => new()
        {
            Id = Id,
            Cipher = Cipher,
            ShortName = ShortName,
            Title = Title,
            Qualification = Qualification,
            Profile = Profile,
            Description = Description,
        };
    }
}
