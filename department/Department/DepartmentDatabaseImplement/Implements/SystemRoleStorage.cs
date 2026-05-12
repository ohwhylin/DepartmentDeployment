using System.Collections.Generic;
using System.Linq;
using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;
using DepartmentDatabaseImplement.Models;

namespace DepartmentDatabaseImplement.Implements
{
    public class SystemRoleStorage : ISystemRoleStorage
    {
        public List<SystemRoleViewModel> GetFullList()
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<SystemRole>().AsQueryable();

            return query
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public List<SystemRoleViewModel> GetFilteredList(SystemRoleSearchModel model)
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<SystemRole>().AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.Code))
            {
                query = query.Where(x => x.Code.Contains(model.Code));
            }

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                query = query.Where(x => x.Name.Contains(model.Name));
            }

            return query
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public SystemRoleViewModel? GetElement(SystemRoleSearchModel model)
        {
            if (model == null) return null;

            using var context = new DepartmentDatabase();
            var query = context.Set<SystemRole>().AsQueryable();

            var element = query.FirstOrDefault(x =>
                (model.Id.HasValue && x.Id == model.Id.Value) ||
                (!string.IsNullOrWhiteSpace(model.Code) && x.Code == model.Code));

            return element == null ? null : MapToViewModel(element);
        }

        public SystemRoleViewModel? Insert(SystemRoleBindingModel model)
        {
            var newElement = SystemRole.Create(model);
            if (newElement == null) return null;

            using var context = new DepartmentDatabase();
            context.Set<SystemRole>().Add(newElement);
            context.SaveChanges();

            return MapToViewModel(newElement);
        }

        public SystemRoleViewModel? Update(SystemRoleBindingModel model)
        {
            using var context = new DepartmentDatabase();
            var dbElement = context.Set<SystemRole>().FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null) return null;

            dbElement.Update(model);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        public SystemRoleViewModel? Delete(SystemRoleBindingModel model)
        {
            using var context = new DepartmentDatabase();
            var dbElement = context.Set<SystemRole>().FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null) return null;

            context.Set<SystemRole>().Remove(dbElement);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        private static SystemRoleViewModel MapToViewModel(SystemRole entity)
        {
            return entity.GetViewModel;
        }
    }
}