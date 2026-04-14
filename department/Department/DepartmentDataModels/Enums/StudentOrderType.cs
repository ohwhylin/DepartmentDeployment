using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDataModels.Enums
{
    public enum StudentOrderType
    {
        Зачисление = 0,
        Распределение = 1,
        Движение = 2,
        ПереводНаКурс = 3,
        ПереводВГруппу = 4,
        ИзАкадема = 5,
        ВАкадем = 6,
        ОтчислитьЗаНеуспевамость = 7,
        ОтчислитьВСвязиСПереводом = 8,
        ОтчислитьПоСобственному = 9,
        ЗачислитьПоПереводу = 10,
        Восстановить = 11,
        ОтчислитьПоЗавершению = 12,
        ПродлАкадем = 13,
        ОтчислитьЗаНевыходСАкадема = 14
    }
}
