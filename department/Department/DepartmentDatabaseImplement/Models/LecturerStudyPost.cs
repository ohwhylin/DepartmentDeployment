using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Models;

namespace DepartmentDatabaseImplement.Models
{
    [DataContract]
    public class LecturerStudyPost : ILecturerStudyPostModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public string StudyPostTitle { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public int Hours { get; private set; }
        [ForeignKey("LecturerStudyPostId")]
        public virtual List<Lecturer> Lecturers { get; set; }

        public static LecturerStudyPost? Create(LecturerStudyPostBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                StudyPostTitle = model.StudyPostTitle,
                Hours = model.Hours,
            };
        }

        public void Update(LecturerStudyPostBindingModel model)
        {
            if (model == null) return;
            StudyPostTitle = model.StudyPostTitle;
            Hours = model.Hours;
        }

        public LecturerStudyPostViewModel GetViewModel => new()
        {
            Id = Id,
            StudyPostTitle = StudyPostTitle,
            Hours = Hours,
        };
    }
}
