using System.Collections.Generic;
using System.Linq;
using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;
using DepartmentDatabaseImplement.Models;

namespace DepartmentDatabaseImplement.Implements
{
    public class SystemUserStorage : ISystemUserStorage
    {
        public List<SystemUserViewModel> GetFullList()
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<SystemUser>().AsQueryable();

            return query
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public List<SystemUserViewModel> GetFilteredList(SystemUserSearchModel model)
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<SystemUser>().AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.Login))
            {
                query = query.Where(x => x.Login.Contains(model.Login));
            }

            if (model.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == model.IsActive.Value);
            }

            return query
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public SystemUserViewModel? GetElement(SystemUserSearchModel model)
        {
            if (model == null) return null;

            using var context = new DepartmentDatabase();
            var query = context.Set<SystemUser>().AsQueryable();

            var element = query.FirstOrDefault(x =>
                (model.Id.HasValue && x.Id == model.Id.Value) ||
                (!string.IsNullOrWhiteSpace(model.Login) && x.Login == model.Login));

            return element == null ? null : MapToViewModel(element);
        }

        public SystemUserViewModel? Insert(SystemUserBindingModel model)
        {
            var newElement = SystemUser.Create(model);
            if (newElement == null) return null;

            using var context = new DepartmentDatabase();
            context.Set<SystemUser>().Add(newElement);
            context.SaveChanges();

            return MapToViewModel(newElement);
        }

        public SystemUserViewModel? Update(SystemUserBindingModel model)
        {
            using var context = new DepartmentDatabase();
            var dbElement = context.Set<SystemUser>().FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null) return null;

            dbElement.Update(model);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        public SystemUserViewModel? Delete(SystemUserBindingModel model)
        {
            using var context = new DepartmentDatabase();
            var dbElement = context.Set<SystemUser>().FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null) return null;

            context.Set<SystemUser>().Remove(dbElement);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        private static SystemUserViewModel MapToViewModel(SystemUser entity)
        {
            return entity.GetViewModel;
        }
    }
}