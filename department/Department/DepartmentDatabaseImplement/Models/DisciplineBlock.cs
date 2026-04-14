using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Models;

namespace DepartmentDatabaseImplement.Models
{
    [DataContract]
    public class DisciplineBlock : IDisciplineBlockModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public string Title { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string DisciplineBlockBlueAsteriskName { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public bool DisciplineBlockUseForGrouping { get; private set; }

        [DataMember]
        [Required]
        public int DisciplineBlockOrder { get; private set; }
        [ForeignKey("DisciplineBlockId")]
        public virtual List<Discipline> Disciplines { get; set; }

        public static DisciplineBlock? Create(DisciplineBlockBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                Title = model.Title,
                DisciplineBlockBlueAsteriskName = model.DisciplineBlockBlueAsteriskName,
                DisciplineBlockUseForGrouping = model.DisciplineBlockUseForGrouping,
                DisciplineBlockOrder = model.DisciplineBlockOrder,
            };
        }

        public void Update(DisciplineBlockBindingModel model)
        {
            if (model == null) return;
            Title = model.Title;
            DisciplineBlockBlueAsteriskName = model.DisciplineBlockBlueAsteriskName;
            DisciplineBlockUseForGrouping = model.DisciplineBlockUseForGrouping;
            DisciplineBlockOrder = model.DisciplineBlockOrder;
        }

        public DisciplineBlockViewModel GetViewModel => new()
        {
            Id = Id,
            Title = Title,
            DisciplineBlockBlueAsteriskName = DisciplineBlockBlueAsteriskName,
            DisciplineBlockUseForGrouping = DisciplineBlockUseForGrouping,
            DisciplineBlockOrder = DisciplineBlockOrder,
        };
    }
}
