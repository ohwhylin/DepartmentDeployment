using System.Collections.Generic;
using System.Linq;
using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;
using DepartmentDatabaseImplement.Models;

namespace DepartmentDatabaseImplement.Implements
{
    public class ClassroomStorage : IClassroomStorage
    {
        public List<ClassroomViewModel> GetFullList()
        {
            using var context = new DepartmentDatabase();
            return context.Set<Classroom>()
                .OrderBy(x => x.Number)
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public List<ClassroomViewModel> GetFilteredList(ClassroomSearchModel model)
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<Classroom>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(model.Number))
            {
                query = query.Where(x => x.Number.Contains(model.Number));
            }

            if (model.Type.HasValue)
            {
                query = query.Where(x => x.Type == model.Type.Value);
            }

            if (model.HasProjector.HasValue)
            {
                query = query.Where(x => x.HasProjector == model.HasProjector.Value);
            }

            if (model.NotUseInSchedule.HasValue)
            {
                query = query.Where(x => x.NotUseInSchedule == model.NotUseInSchedule.Value);
            }

            if (model.UseInSchedule.HasValue)
            {
                query = query.Where(x => x.NotUseInSchedule == !model.UseInSchedule.Value);
            }

            return query
                .OrderBy(x => x.Number)
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public ClassroomViewModel? GetElement(ClassroomSearchModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new DepartmentDatabase();
            var element = context.Set<Classroom>()
                .FirstOrDefault(x =>
                    (model.Id.HasValue && x.Id == model.Id) ||
                    (!string.IsNullOrWhiteSpace(model.Number) && x.Number == model.Number));

            return element == null ? null : MapToViewModel(element);
        }

        public ClassroomViewModel? Insert(ClassroomBindingModel model)
        {
            var newElement = Classroom.Create(model);
            if (newElement == null)
            {
                return null;
            }

            using var context = new DepartmentDatabase();
            context.Set<Classroom>().Add(newElement);
            context.SaveChanges();
            return MapToViewModel(newElement);
        }

        public ClassroomViewModel? Update(ClassroomBindingModel model)
        {
            using var context = new DepartmentDatabase();
            var dbElement = context.Set<Classroom>().FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null)
            {
                return null;
            }

            dbElement.Update(model);
            context.SaveChanges();
            return MapToViewModel(dbElement);
        }

        public ClassroomViewModel? Delete(ClassroomBindingModel model)
        {
            using var context = new DepartmentDatabase();
            var dbElement = context.Set<Classroom>().FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null)
            {
                return null;
            }

            context.Set<Classroom>().Remove(dbElement);
            context.SaveChanges();
            return MapToViewModel(dbElement);
        }

        private static ClassroomViewModel MapToViewModel(Classroom entity)
        {
            return entity.GetViewModel;
        }
    }
}