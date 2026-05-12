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
    public class SystemUserRoleStorage : ISystemUserRoleStorage
    {
        public List<SystemUserRoleViewModel> GetFullList()
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<SystemUserRole>().AsQueryable();

            query = query.Include(x => x.User);
            query = query.Include(x => x.Role);

            return query
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public List<SystemUserRoleViewModel> GetFilteredList(SystemUserRoleSearchModel model)
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<SystemUserRole>().AsQueryable();

            query = query.Include(x => x.User);
            query = query.Include(x => x.Role);

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }

            if (model.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == model.UserId.Value);
            }

            if (model.RoleId.HasValue)
            {
                query = query.Where(x => x.RoleId == model.RoleId.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.UserLogin))
            {
                query = query.Where(x => x.User.Login.Contains(model.UserLogin));
            }

            if (!string.IsNullOrWhiteSpace(model.RoleCode))
            {
                query = query.Where(x => x.Role.Code == model.RoleCode);
            }

            return query
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public SystemUserRoleViewModel? GetElement(SystemUserRoleSearchModel model)
        {
            if (model == null) return null;

            using var context = new DepartmentDatabase();
            var query = context.Set<SystemUserRole>().AsQueryable();

            query = query.Include(x => x.User);
            query = query.Include(x => x.Role);

            var element = query.FirstOrDefault(x =>
                (model.Id.HasValue && x.Id == model.Id.Value) ||
                (model.UserId.HasValue && model.RoleId.HasValue &&
                 x.UserId == model.UserId.Value && x.RoleId == model.RoleId.Value));

            return element == null ? null : MapToViewModel(element);
        }

        public SystemUserRoleViewModel? Insert(SystemUserRoleBindingModel model)
        {
            var newElement = SystemUserRole.Create(model);
            if (newElement == null) return null;

            using var context = new DepartmentDatabase();
            context.Set<SystemUserRole>().Add(newElement);
            context.SaveChanges();

            var saved = context.Set<SystemUserRole>()
                .Include(x => x.User)
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Id == newElement.Id);

            return saved == null ? null : MapToViewModel(saved);
        }

        public SystemUserRoleViewModel? Delete(SystemUserRoleBindingModel model)
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<SystemUserRole>().AsQueryable();

            query = query.Include(x => x.User);
            query = query.Include(x => x.Role);

            var dbElement = query.FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null) return null;

            context.Set<SystemUserRole>().Remove(dbElement);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        private static SystemUserRoleViewModel MapToViewModel(SystemUserRole entity)
        {
            var vm = entity.GetViewModel;
            vm.UserLogin = entity.User == null ? string.Empty : entity.User.Login;
            vm.RoleName = entity.Role == null ? string.Empty : entity.Role.Name;
            return vm;
        }
    }
}