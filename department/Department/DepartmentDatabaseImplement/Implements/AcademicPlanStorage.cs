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
    public class AcademicPlanStorage : IAcademicPlanStorage
    {
        public List<AcademicPlanViewModel> GetFullList()
        {
            using var context = new DepartmentDatabase();

            return context.Set<AcademicPlan>()
                .Include(x => x.EducationDirection)
                .OrderByDescending(x => x.Year)
                .ThenBy(x => x.AcademicCourses)
                .ThenBy(x => x.EducationDirection!.Title)
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public List<AcademicPlanViewModel> GetFilteredList(AcademicPlanSearchModel model)
        {
            using var context = new DepartmentDatabase();

            var query = context.Set<AcademicPlan>()
                .Include(x => x.EducationDirection)
                .AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(x => x.Id == model.Id.Value);
            }

            if (model.EducationDirectionId.HasValue)
            {
                query = query.Where(x => x.EducationDirectionId == model.EducationDirectionId.Value);
            }

            if (model.AcademicCourses.HasValue)
            {
                query = query.Where(x => x.AcademicCourses == model.AcademicCourses.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.Year))
            {
                var year = model.Year.Trim();
                query = query.Where(x => EF.Functions.ILike(x.Year, $"%{year}%"));
            }

            if (model.Qualification.HasValue)
            {
                query = query.Where(x =>
                    x.EducationDirection != null &&
                    x.EducationDirection.Qualification == model.Qualification.Value);
            }

            return query
                .OrderByDescending(x => x.Year)
                .ThenBy(x => x.AcademicCourses)
                .ThenBy(x => x.EducationDirection!.Title)
                .ToList()
                .Select(MapToViewModel)
                .ToList();
        }

        public AcademicPlanViewModel? GetElement(AcademicPlanSearchModel model)
        {
            if (model == null) return null;

            using var context = new DepartmentDatabase();

            var query = context.Set<AcademicPlan>()
                .Include(x => x.EducationDirection)
                .AsQueryable();

            var element = query.FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id.Value);

            return element == null ? null : MapToViewModel(element);
        }

        public AcademicPlanViewModel? Insert(AcademicPlanBindingModel model)
        {
            var newElement = AcademicPlan.Create(model);
            if (newElement == null) return null;

            using var context = new DepartmentDatabase();
            context.Set<AcademicPlan>().Add(newElement);
            context.SaveChanges();

            var saved = context.Set<AcademicPlan>()
                .Include(x => x.EducationDirection)
                .FirstOrDefault(x => x.Id == newElement.Id);

            return saved == null ? null : MapToViewModel(saved);
        }

        public AcademicPlanViewModel? Update(AcademicPlanBindingModel model)
        {
            using var context = new DepartmentDatabase();

            var element = context.Set<AcademicPlan>()
                .Include(x => x.EducationDirection)
                .AsQueryable();

            var dbElement = element.FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null) return null;

            dbElement.Update(model);
            context.SaveChanges();

            context.Entry(dbElement).Reload();
            context.Entry(dbElement).Reference(x => x.EducationDirection).Load();

            return MapToViewModel(dbElement);
        }

        public AcademicPlanViewModel? Delete(AcademicPlanBindingModel model)
        {
            using var context = new DepartmentDatabase();

            var element = context.Set<AcademicPlan>()
                .Include(x => x.EducationDirection)
                .AsQueryable();

            var dbElement = element.FirstOrDefault(x => x.Id == model.Id);
            if (dbElement == null) return null;

            context.Set<AcademicPlan>().Remove(dbElement);
            context.SaveChanges();

            return MapToViewModel(dbElement);
        }

        private static AcademicPlanViewModel MapToViewModel(AcademicPlan entity)
        {
            var vm = entity.GetViewModel;
            vm.EducationDirection = entity.EducationDirection == null
                ? string.Empty
                : entity.EducationDirection.Title;

            return vm;
        }
    }
}