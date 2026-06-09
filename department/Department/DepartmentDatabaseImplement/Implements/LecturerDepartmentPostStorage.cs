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
    public class LecturerDepartmentPostStorage : ILecturerDepartmentPostStorage
    {
        public List<LecturerDepartmentPostViewModel> GetFullList()
        {
            using var context = new DepartmentDatabase();

            return context.Set<LecturerDepartmentPost>()
                .AsNoTracking()
                .OrderBy(x => x.Order)
                .ThenBy(x => x.DepartmentPostTitle)
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public List<LecturerDepartmentPostViewModel> GetFilteredList(LecturerDepartmentPostSearchModel model)
        {
            using var context = new DepartmentDatabase();

            var query = context.Set<LecturerDepartmentPost>().AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.Search))
            {
                var pattern = $"%{model.Search.Trim()}%";
                query = query.Where(x => EF.Functions.ILike(x.DepartmentPostTitle, pattern));
            }

            if (!string.IsNullOrWhiteSpace(model.DepartmentPostTitle))
            {
                query = query.Where(x => x.DepartmentPostTitle.Contains(model.DepartmentPostTitle));
            }

            if (model.Order.HasValue)
            {
                query = query.Where(x => x.Order == model.Order.Value);
            }

            return query
                .AsNoTracking()
                .OrderBy(x => x.Order)
                .ThenBy(x => x.DepartmentPostTitle)
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public LecturerDepartmentPostViewModel? GetElement(LecturerDepartmentPostSearchModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new DepartmentDatabase();

            var entity = context.Set<LecturerDepartmentPost>()
                .AsNoTracking()
                .FirstOrDefault(x =>
                    (model.Id.HasValue && x.Id == model.Id.Value) ||
                    (!string.IsNullOrWhiteSpace(model.DepartmentPostTitle) && x.DepartmentPostTitle == model.DepartmentPostTitle));

            return entity == null ? null : MapToViewModel(entity);
        }

        public LecturerDepartmentPostViewModel? Insert(LecturerDepartmentPostBindingModel model)
        {
            var newElement = LecturerDepartmentPost.Create(model);
            if (newElement == null)
            {
                return null;
            }

            using var context = new DepartmentDatabase();
            context.Set<LecturerDepartmentPost>().Add(newElement);
            context.SaveChanges();

            return MapToViewModel(newElement);
        }

        public LecturerDepartmentPostViewModel? Update(LecturerDepartmentPostBindingModel model)
        {
            using var context = new DepartmentDatabase();

            var dbElement = context.Set<LecturerDepartmentPost>()
                .FirstOrDefault(x => x.Id == model.Id);

            if (dbElement == null)
            {
                return null;
            }

            dbElement.Update(model);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        public LecturerDepartmentPostViewModel? Delete(LecturerDepartmentPostBindingModel model)
        {
            using var context = new DepartmentDatabase();

            var dbElement = context.Set<LecturerDepartmentPost>()
                .FirstOrDefault(x => x.Id == model.Id);

            if (dbElement == null)
            {
                return null;
            }

            context.Set<LecturerDepartmentPost>().Remove(dbElement);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        private static LecturerDepartmentPostViewModel MapToViewModel(LecturerDepartmentPost entity)
        {
            return entity.GetViewModel;
        }
    }
}