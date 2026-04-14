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
    public class Classroom : IClassroomModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public string Number { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public ClassroomTypes Type { get; private set; }

        [DataMember]
        [Required]
        public int Capacity { get; private set; }

        [DataMember]
        [Required]
        public bool NotUseInSchedule { get; private set; }
        [DataMember]
        [Required]
        public bool HasProjector { get; private set; }

        public static Classroom? Create(ClassroomBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                Number = model.Number,
                Type = model.Type,
                Capacity = model.Capacity,
                NotUseInSchedule = model.NotUseInSchedule,
                HasProjector = model.HasProjector,
            };
        }

        public void Update(ClassroomBindingModel model)
        {
            if (model == null) return;
            Number = model.Number;
            Type = model.Type;
            Capacity = model.Capacity;
            NotUseInSchedule = model.NotUseInSchedule;
            HasProjector = model.HasProjector;
        }

        public ClassroomViewModel GetViewModel => new()
        {
            Id = Id,
            Number = Number,
            Type = Type,
            Capacity = Capacity,
            NotUseInSchedule = NotUseInSchedule,
            HasProjector = HasProjector,
        };
    }
}
