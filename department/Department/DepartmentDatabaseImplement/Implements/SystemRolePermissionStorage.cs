using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;
using DepartmentDatabaseImplement.Models;

namespace DepartmentDatabaseImplement.Implements
{
    public class SystemRolePermissionStorage : ISystemRolePermissionStorage
    {
        public List<SystemRolePermissionViewModel> GetFullList()
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<SystemRolePermission>().AsQueryable();

            query = query.Include(x => x.Role);
            query = query.Include(x => x.Permission);

            return query
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public List<SystemRolePermissionViewModel> GetFilteredList(SystemRolePermissionSearchModel model)
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<SystemRolePermission>().AsQueryable();

            query = query.Include(x => x.Role);
            query = query.Include(x => x.Permission);

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }

            if (model.RoleId.HasValue)
            {
                query = query.Where(x => x.RoleId == model.RoleId.Value);
            }

            if (model.PermissionId.HasValue)
            {
                query = query.Where(x => x.PermissionId == model.PermissionId.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.RoleCode))
            {
                query = query.Where(x => x.Role.Code == model.RoleCode);
            }

            if (!string.IsNullOrWhiteSpace(model.PermissionCode))
            {
                query = query.Where(x => x.Permission.Code == model.PermissionCode);
            }

            return query
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public SystemRolePermissionViewModel? GetElement(SystemRolePermissionSearchModel model)
        {
            if (model == null) return null;

            using var context = new DepartmentDatabase();
            var query = context.Set<SystemRolePermission>().AsQueryable();

            query = query.Include(x => x.Role);
            query = query.Include(x => x.Permission);

            var element = query.FirstOrDefault(x =>
                (model.Id.HasValue && x.Id == model.Id.Value) ||
                (model.RoleId.HasValue && model.PermissionId.HasValue &&
                 x.RoleId == model.RoleId.Value && x.PermissionId == model.PermissionId.Value));

            return element == null ? null : MapToViewModel(element);
        }

        public SystemRolePermissionViewModel? Insert(SystemRolePermissionBindingModel model)
        {
            var newElement = SystemRolePermission.Create(model);
            if (newElement == null) return null;

            using var context = new DepartmentDatabase();
            context.Set<SystemRolePermission>().Add(newElement);
            context.SaveChanges();

            var saved = context.Set<SystemRolePermission>()
                .Include(x => x.Role)
                .Include(x => x.Permission)
                .FirstOrDefault(x => x.Id == newElement.Id);

            return saved == null ? null : MapToViewModel(saved);
        }

        public SystemRolePermissionViewModel? Delete(SystemRolePermissionBindingModel model)
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<SystemRolePermission>().AsQueryable();

            query = query.Include(x => x.Role);
            query = query.Include(x => x.Permission);

            var dbElement = query.FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null) return null;

            context.Set<SystemRolePermission>().Remove(dbElement);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        private static SystemRolePermissionViewModel MapToViewModel(SystemRolePermission entity)
        {
            var vm = entity.GetViewModel;
            vm.RoleName = entity.Role == null ? string.Empty : entity.Role.Name;
            vm.PermissionName = entity.Permission == null ? string.Empty : entity.Permission.Name;
            return vm;
        }
    }
}