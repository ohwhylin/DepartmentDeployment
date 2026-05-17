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
    public class LecturerStorage : ILecturerStorage
    {
        public List<LecturerViewModel> GetFullList()
        {
            using var context = new DepartmentDatabase();

            return context.Set<Lecturer>()
                .Include(x => x.LecturerStudyPost)
                .Include(x => x.LecturerDepartmentPost)
                .AsNoTracking()
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public List<LecturerViewModel> GetFilteredList(LecturerSearchModel model)
        {
            using var context = new DepartmentDatabase();

            var query = context.Set<Lecturer>()
                .Include(x => x.LecturerStudyPost)
                .Include(x => x.LecturerDepartmentPost)
                .AsQueryable();

            if (model.Id.HasValue)
                query = query.Where(x => x.Id == model.Id.Value);

            if (!string.IsNullOrWhiteSpace(model.FirstName))
                query = query.Where(x => x.FirstName.Contains(model.FirstName));

            if (!string.IsNullOrWhiteSpace(model.LastName))
                query = query.Where(x => x.LastName.Contains(model.LastName));

            if (!string.IsNullOrWhiteSpace(model.Patronymic))
                query = query.Where(x => x.Patronymic.Contains(model.Patronymic));

            if (model.LecturerStudyPostId.HasValue)
                query = query.Where(x => x.LecturerStudyPostId == model.LecturerStudyPostId.Value);

            if (model.LecturerDepartmentPostId.HasValue)
                query = query.Where(x => x.LecturerDepartmentPostId == model.LecturerDepartmentPostId.Value);

            return query
                .AsNoTracking()
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public LecturerViewModel? GetElement(LecturerSearchModel model)
        {
            if (model == null) return null;

            using var context = new DepartmentDatabase();

            var query = context.Set<Lecturer>()
                .Include(x => x.LecturerStudyPost)
                .Include(x => x.LecturerDepartmentPost)
                .AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(model.FirstName))
                    query = query.Where(x => x.FirstName == model.FirstName);

                if (!string.IsNullOrWhiteSpace(model.LastName))
                    query = query.Where(x => x.LastName == model.LastName);

                if (!string.IsNullOrWhiteSpace(model.Patronymic))
                    query = query.Where(x => x.Patronymic == model.Patronymic);
            }

            var entity = query.AsNoTracking().FirstOrDefault();
            return entity == null ? null : MapToViewModel(entity);
        }

        public LecturerViewModel? Insert(LecturerBindingModel model)
        {
            var newElement = Lecturer.Create(model);
            if (newElement == null) return null;

            using var context = new DepartmentDatabase();
            context.Set<Lecturer>().Add(newElement);
            context.SaveChanges();

            var saved = context.Set<Lecturer>()
                .Include(x => x.LecturerStudyPost)
                .Include(x => x.LecturerDepartmentPost)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == newElement.Id);

            return saved == null ? null : MapToViewModel(saved);
        }

        public LecturerViewModel? Update(LecturerBindingModel model)
        {
            using var context = new DepartmentDatabase();

            var dbElement = context.Set<Lecturer>()
                .Include(x => x.LecturerStudyPost)
                .Include(x => x.LecturerDepartmentPost)
                .FirstOrDefault(x => x.Id == model.Id);

            if (dbElement == null) return null;

            dbElement.Update(model);
            context.SaveChanges();

            context.Entry(dbElement).Reload();
            context.Entry(dbElement).Reference(x => x.LecturerStudyPost).Load();
            context.Entry(dbElement).Reference(x => x.LecturerDepartmentPost).Load();

            return MapToViewModel(dbElement);
        }

        public LecturerViewModel? Delete(LecturerBindingModel model)
        {
            using var context = new DepartmentDatabase();

            var dbElement = context.Set<Lecturer>()
                .Include(x => x.LecturerStudyPost)
                .Include(x => x.LecturerDepartmentPost)
                .FirstOrDefault(x => x.Id == model.Id);

            if (dbElement == null) return null;

            context.Set<Lecturer>().Remove(dbElement);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        private static LecturerViewModel MapToViewModel(Lecturer entity)
        {
            var vm = entity.GetViewModel;
            vm.LecturerStudyPost = entity.LecturerStudyPost == null
                ? string.Empty
                : entity.LecturerStudyPost.StudyPostTitle;
            vm.LecturerDepartmentPost = entity.LecturerDepartmentPost == null
                ? string.Empty
                : entity.LecturerDepartmentPost.DepartmentPostTitle;
            return vm;
        }
    }
}