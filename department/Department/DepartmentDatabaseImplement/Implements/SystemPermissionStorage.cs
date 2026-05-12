using System.Collections.Generic;
using System.Linq;
using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;
using DepartmentDatabaseImplement.Models;

namespace DepartmentDatabaseImplement.Implements
{
    public class SystemPermissionStorage : ISystemPermissionStorage
    {
        public List<SystemPermissionViewModel> GetFullList()
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<SystemPermission>().AsQueryable();

            return query
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public List<SystemPermissionViewModel> GetFilteredList(SystemPermissionSearchModel model)
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<SystemPermission>().AsQueryable();

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

        public SystemPermissionViewModel? GetElement(SystemPermissionSearchModel model)
        {
            if (model == null) return null;

            using var context = new DepartmentDatabase();
            var query = context.Set<SystemPermission>().AsQueryable();

            var element = query.FirstOrDefault(x =>
                (model.Id.HasValue && x.Id == model.Id.Value) ||
                (!string.IsNullOrWhiteSpace(model.Code) && x.Code == model.Code));

            return element == null ? null : MapToViewModel(element);
        }

        public SystemPermissionViewModel? Insert(SystemPermissionBindingModel model)
        {
            var newElement = SystemPermission.Create(model);
            if (newElement == null) return null;

            using var context = new DepartmentDatabase();
            context.Set<SystemPermission>().Add(newElement);
            context.SaveChanges();

            return MapToViewModel(newElement);
        }

        public SystemPermissionViewModel? Update(SystemPermissionBindingModel model)
        {
            using var context = new DepartmentDatabase();
            var dbElement = context.Set<SystemPermission>().FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null) return null;

            dbElement.Update(model);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        public SystemPermissionViewModel? Delete(SystemPermissionBindingModel model)
        {
            using var context = new DepartmentDatabase();
            var dbElement = context.Set<SystemPermission>().FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null) return null;

            context.Set<SystemPermission>().Remove(dbElement);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        private static SystemPermissionViewModel MapToViewModel(SystemPermission entity)
        {
            return entity.GetViewModel;
        }
    }
}