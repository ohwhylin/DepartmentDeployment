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
    public class EducationDirectionStorage : IEducationDirectionStorage
    {
        public List<EducationDirectionViewModel> GetFullList()
        {
            using var context = new DepartmentDatabase();

            return context.Set<EducationDirection>()
                .OrderBy(x => x.Cipher)
                .ThenBy(x => x.Title)
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public List<EducationDirectionViewModel> GetFilteredList(EducationDirectionSearchModel model)
        {
            using var context = new DepartmentDatabase();
            var query = context.Set<EducationDirection>().AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.ShortName))
            {
                query = query.Where(x => x.ShortName == model.ShortName);
            }

            if (!string.IsNullOrWhiteSpace(model.Cipher))
            {
                query = query.Where(x => x.Cipher == model.Cipher);
            }

            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                query = query.Where(x => x.Title == model.Title);
            }

            if (!string.IsNullOrWhiteSpace(model.Search))
            {
                var search = model.Search.Trim();

                query = query.Where(x =>
                    EF.Functions.ILike(x.Title, $"%{search}%") ||
                    EF.Functions.ILike(x.Cipher, $"%{search}%") ||
                    EF.Functions.ILike(x.ShortName, $"%{search}%"));
            }

            if (model.Qualification.HasValue)
            {
                query = query.Where(x => x.Qualification == model.Qualification.Value);
            }

            return query
                .OrderBy(x => x.Cipher)
                .ThenBy(x => x.Title)
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public EducationDirectionViewModel? GetElement(EducationDirectionSearchModel model)
        {
            if (model == null) return null;

            using var context = new DepartmentDatabase();
            var query = context.Set<EducationDirection>().AsQueryable();

            var element = query.FirstOrDefault(x =>
                (!string.IsNullOrEmpty(model.ShortName) && x.ShortName == model.ShortName) ||
                (model.Id.HasValue && x.Id == model.Id.Value));

            return element == null ? null : MapToViewModel(element);
        }

        public EducationDirectionViewModel? Insert(EducationDirectionBindingModel model)
        {
            var newElement = EducationDirection.Create(model);
            if (newElement == null) return null;

            using var context = new DepartmentDatabase();
            context.Set<EducationDirection>().Add(newElement);
            context.SaveChanges();

            return MapToViewModel(newElement);
        }

        public EducationDirectionViewModel? Update(EducationDirectionBindingModel model)
        {
            using var context = new DepartmentDatabase();
            var element = context.Set<EducationDirection>().AsQueryable();
            var dbElement = element.FirstOrDefault(x => x.Id == model.Id);

            if (dbElement == null) return null;

            dbElement.Update(model);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        public EducationDirectionViewModel? Delete(EducationDirectionBindingModel model)
        {
            using var context = new DepartmentDatabase();
            var element = context.Set<EducationDirection>().AsQueryable();
            var dbElement = element.FirstOrDefault(x => x.Id == model.Id);

            if (dbElement == null) return null;

            context.Set<EducationDirection>().Remove(dbElement);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        private static EducationDirectionViewModel MapToViewModel(EducationDirection entity)
        {
            return entity.GetViewModel;
        }
    }
}