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
    public class Student : IStudentModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public int? StudentGroupId { get; private set; }
        public virtual StudentGroup? StudentGroup { get; set; } = null!;

        [DataMember]
        [Required]
        public string NumberOfBook { get; private set; } = string.Empty;

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
        public string Email { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public StudentState StudentState { get; private set; }

        [DataMember]
        [Required]
        public string Description { get; private set; } = string.Empty;

        [DataMember]
        public byte[]? Photo { get; private set; }

        [DataMember]
        [Required]
        public bool IsSteward { get; private set; }
        [ForeignKey("StudentId")]
        public virtual List<DisciplineStudentRecord> DisciplineStudentRecords { get; set; }
        [ForeignKey("StudentId")]
        public virtual List<StudentOrderBlockStudent> StudentOrderBlockStudents { get; set; }

        public static Student? Create(StudentBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                StudentGroupId = model.StudentGroupId,
                NumberOfBook = model.NumberOfBook,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
                Email = model.Email,
                StudentState = model.StudentState,
                Description = model.Description,
                Photo = model.Photo,
                IsSteward = model.IsSteward,
            };
        }

        public void Update(StudentBindingModel model)
        {
            if (model == null) return;
            StudentGroupId = model.StudentGroupId;
            NumberOfBook = model.NumberOfBook;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Patronymic = model.Patronymic;
            Email = model.Email;
            StudentState = model.StudentState;
            Description = model.Description;
            Photo = model.Photo;
            IsSteward = model.IsSteward;
        }

        public StudentViewModel GetViewModel => new()
        {
            Id = Id,
            StudentGroupId = StudentGroupId,
            NumberOfBook = NumberOfBook,
            FirstName = FirstName,
            LastName = LastName,
            Patronymic = Patronymic,
            Email = Email,
            StudentState = StudentState,
            Description = Description,
            Photo = Photo,
            IsSteward = IsSteward,
        };
    }
}
