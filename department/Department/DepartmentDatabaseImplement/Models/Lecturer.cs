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
    public class Lecturer : ILecturerModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public int? LecturerStudyPostId { get; private set; }
        public virtual LecturerStudyPost? LecturerStudyPost { get; set; } = null!;

        [DataMember]
        [Required]
        public int LecturerDepartmentPostId { get; private set; }
        public virtual LecturerDepartmentPost LecturerDepartmentPost { get; set; } = null!;

        [DataMember]
        [Required]
        public string FirstName { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string LastName { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string Patronymic { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string Abbreviation { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public DateTime DateBirth { get; private set; }

        [DataMember]
        [Required]
        public string Address { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string Email { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string MobileNumber { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string HomeNumber { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public Rank Rank { get; private set; }

        [DataMember]
        [Required]
        public Rank2 Rank2 { get; private set; }

        [DataMember]
        [Required]
        public string Description { get; private set; } = string.Empty;

        [DataMember]
        public byte[]? Photo { get; private set; }

        [DataMember]
        [Required]
        public bool OnlyForPrivate { get; private set; }

        public static Lecturer? Create(LecturerBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                LecturerStudyPostId = model.LecturerStudyPostId,
                LecturerDepartmentPostId = model.LecturerDepartmentPostId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
                Abbreviation = model.Abbreviation,
                DateBirth = model.DateBirth,
                Address = model.Address,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                HomeNumber = model.HomeNumber,
                Rank = model.Rank,
                Rank2 = model.Rank2,
                Description = model.Description,
                Photo = model.Photo,
                OnlyForPrivate = model.OnlyForPrivate,
            };
        }

        public void Update(LecturerBindingModel model)
        {
            if (model == null) return;
            LecturerStudyPostId = model.LecturerStudyPostId;
            LecturerDepartmentPostId = model.LecturerDepartmentPostId;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Patronymic = model.Patronymic;
            Abbreviation = model.Abbreviation;
            DateBirth = model.DateBirth;
            Address = model.Address;
            Email = model.Email;
            MobileNumber = model.MobileNumber;
            HomeNumber = model.HomeNumber;
            Rank = model.Rank;
            Rank2 = model.Rank2;
            Description = model.Description;
            Photo = model.Photo;
            OnlyForPrivate = model.OnlyForPrivate;
        }

        public LecturerViewModel GetViewModel => new()
        {
            Id = Id,
            LecturerStudyPostId = LecturerStudyPostId,
            LecturerDepartmentPostId = LecturerDepartmentPostId,
            FirstName = FirstName,
            LastName = LastName,
            Patronymic = Patronymic,
            Abbreviation = Abbreviation,
            DateBirth = DateBirth,
            Address = Address,
            Email = Email,
            MobileNumber = MobileNumber,
            HomeNumber = HomeNumber,
            Rank = Rank,
            Rank2 = Rank2,
            Description = Description,
            Photo = Photo,
            OnlyForPrivate = OnlyForPrivate,
        };
    }
}
