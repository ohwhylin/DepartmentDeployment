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
    public class StudentOrder : IStudentOrderModel
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        [Required]
        public string OrderNumber { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public StudentOrderType StudentOrderType { get; private set; }
        [DataMember]
        [Required]
        public DateTime OrderDate { get; private set; }
        [ForeignKey("StudentOrderId")]
        public virtual List<StudentOrderBlock> StudentOrderBlocks { get; set; }

        public static StudentOrder? Create(StudentOrderBindingModel model)
        {
            if (model == null) return null;
            return new()
            {
                Id = model.Id,
                OrderNumber = model.OrderNumber,
                StudentOrderType = model.StudentOrderType,
                OrderDate = model.OrderDate,
            };
        }

        public void Update(StudentOrderBindingModel model)
        {
            if (model == null) return;
            OrderNumber = model.OrderNumber;
            StudentOrderType = model.StudentOrderType;
            OrderDate = model.OrderDate;
        }

        public StudentOrderViewModel GetViewModel => new()
        {
            Id = Id,
            OrderNumber = OrderNumber,
            StudentOrderType = StudentOrderType,
            OrderDate = OrderDate,
        };
    }
}
