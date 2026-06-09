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
    public class LecturerStudyPostStorage : ILecturerStudyPostStorage
    {
        public List<LecturerStudyPostViewModel> GetFullList()
        {
            using var context = new DepartmentDatabase();

            return context.Set<LecturerStudyPost>()
                .AsNoTracking()
                .OrderBy(x => x.StudyPostTitle)
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public List<LecturerStudyPostViewModel> GetFilteredList(LecturerStudyPostSearchModel model)
        {
            using var context = new DepartmentDatabase();

            var query = context.Set<LecturerStudyPost>().AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.Search))
            {
                var pattern = $"%{model.Search.Trim()}%";
                query = query.Where(x => EF.Functions.ILike(x.StudyPostTitle, pattern));
            }

            if (!string.IsNullOrWhiteSpace(model.StudyPostTitle))
            {
                query = query.Where(x => x.StudyPostTitle.Contains(model.StudyPostTitle));
            }

            if (model.Hours.HasValue)
            {
                query = query.Where(x => x.Hours == model.Hours.Value);
            }

            return query
                .AsNoTracking()
                .OrderBy(x => x.StudyPostTitle)
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public LecturerStudyPostViewModel? GetElement(LecturerStudyPostSearchModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new DepartmentDatabase();

            var entity = context.Set<LecturerStudyPost>()
                .AsNoTracking()
                .FirstOrDefault(x =>
                    (model.Id.HasValue && x.Id == model.Id.Value) ||
                    (!string.IsNullOrWhiteSpace(model.StudyPostTitle) && x.StudyPostTitle == model.StudyPostTitle));

            return entity == null ? null : MapToViewModel(entity);
        }

        public LecturerStudyPostViewModel? Insert(LecturerStudyPostBindingModel model)
        {
            var newElement = LecturerStudyPost.Create(model);
            if (newElement == null)
            {
                return null;
            }

            using var context = new DepartmentDatabase();
            context.Set<LecturerStudyPost>().Add(newElement);
            context.SaveChanges();

            return MapToViewModel(newElement);
        }

        public LecturerStudyPostViewModel? Update(LecturerStudyPostBindingModel model)
        {
            using var context = new DepartmentDatabase();

            var dbElement = context.Set<LecturerStudyPost>()
                .FirstOrDefault(x => x.Id == model.Id);

            if (dbElement == null)
            {
                return null;
            }

            dbElement.Update(model);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        public LecturerStudyPostViewModel? Delete(LecturerStudyPostBindingModel model)
        {
            using var context = new DepartmentDatabase();

            var dbElement = context.Set<LecturerStudyPost>()
                .FirstOrDefault(x => x.Id == model.Id);

            if (dbElement == null)
            {
                return null;
            }

            context.Set<LecturerStudyPost>().Remove(dbElement);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        private static LecturerStudyPostViewModel MapToViewModel(LecturerStudyPost entity)
        {
            return entity.GetViewModel;
        }
    }
}