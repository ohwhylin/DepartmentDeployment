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
    public class DisciplineStorage : IDisciplineStorage
    {
        public List<DisciplineViewModel> GetFullList()
        {
            using var context = new DepartmentDatabase();

            return context.Set<Discipline>()
                .Include(x => x.DisciplineBlock)
                .OrderBy(x => x.DisciplineName)
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public List<DisciplineViewModel> GetFilteredList(DisciplineSearchModel model)
        {
            using var context = new DepartmentDatabase();

            var query = context.Set<Discipline>()
                .Include(x => x.DisciplineBlock)
                .AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }

            if (model.DisciplineBlockId.HasValue)
            {
                query = query.Where(x => x.DisciplineBlockId == model.DisciplineBlockId.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.DisciplineName))
            {
                query = query.Where(x => x.DisciplineName == model.DisciplineName);
            }

            if (!string.IsNullOrWhiteSpace(model.DisciplineShortName))
            {
                query = query.Where(x => x.DisciplineShortName == model.DisciplineShortName);
            }

            if (!string.IsNullOrWhiteSpace(model.DisciplineDescription))
            {
                query = query.Where(x => x.DisciplineDescription == model.DisciplineDescription);
            }

            if (!string.IsNullOrWhiteSpace(model.DisciplineBlockBlueAsteriskName))
            {
                query = query.Where(x => x.DisciplineBlockBlueAsteriskName == model.DisciplineBlockBlueAsteriskName);
            }

            if (!string.IsNullOrWhiteSpace(model.Search))
            {
                var search = model.Search.Trim();

                query = query.Where(x =>
                    EF.Functions.ILike(x.DisciplineName, $"%{search}%") ||
                    EF.Functions.ILike(x.DisciplineShortName, $"%{search}%") ||
                    EF.Functions.ILike(x.DisciplineDescription, $"%{search}%") ||
                    (x.DisciplineBlock != null && EF.Functions.ILike(x.DisciplineBlock.Title, $"%{search}%")));
            }

            if (model.HasExam.HasValue)
            {
                query = query.Where(x => x.HasExam == model.HasExam.Value);
            }

            if (model.HasCredit.HasValue)
            {
                query = query.Where(x => x.HasCredit == model.HasCredit.Value);
            }

            if (model.HasCourseWork.HasValue)
            {
                query = query.Where(x => x.HasCourseWork == model.HasCourseWork.Value);
            }

            if (model.HasCourseProject.HasValue)
            {
                query = query.Where(x => x.HasCourseProject == model.HasCourseProject.Value);
            }

            return query
                .OrderBy(x => x.DisciplineName)
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public DisciplineViewModel? GetElement(DisciplineSearchModel model)
        {
            if (model == null) return null;

            using var context = new DepartmentDatabase();

            var query = context.Set<Discipline>()
                .Include(x => x.DisciplineBlock)
                .AsQueryable();

            var element = query.FirstOrDefault(x =>
                (!string.IsNullOrEmpty(model.DisciplineName) && x.DisciplineName == model.DisciplineName) ||
                (model.Id.HasValue && x.Id == model.Id.Value));

            return element == null ? null : MapToViewModel(element);
        }

        public DisciplineViewModel? Insert(DisciplineBindingModel model)
        {
            var newElement = Discipline.Create(model);
            if (newElement == null) return null;

            using var context = new DepartmentDatabase();
            context.Set<Discipline>().Add(newElement);
            context.SaveChanges();

            var saved = context.Set<Discipline>()
                .Include(x => x.DisciplineBlock)
                .FirstOrDefault(x => x.Id == newElement.Id);

            return saved == null ? null : MapToViewModel(saved);
        }

        public DisciplineViewModel? Update(DisciplineBindingModel model)
        {
            using var context = new DepartmentDatabase();

            var element = context.Set<Discipline>()
                .Include(x => x.DisciplineBlock)
                .AsQueryable();

            var dbElement = element.FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null) return null;

            dbElement.Update(model);
            context.SaveChanges();

            context.Entry(dbElement).Reload();
            context.Entry(dbElement).Reference(x => x.DisciplineBlock).Load();

            return MapToViewModel(dbElement);
        }

        public DisciplineViewModel? Delete(DisciplineBindingModel model)
        {
            using var context = new DepartmentDatabase();

            var element = context.Set<Discipline>()
                .Include(x => x.DisciplineBlock)
                .AsQueryable();

            var dbElement = element.FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null) return null;

            context.Set<Discipline>().Remove(dbElement);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        private static DisciplineViewModel MapToViewModel(Discipline entity)
        {
            var vm = entity.GetViewModel;
            vm.DisciplineBlock = entity.DisciplineBlock == null ? string.Empty : entity.DisciplineBlock.Title;
            return vm;
        }
    }
}