using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.SearchModels;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;

namespace DepartmentBusinessLogic.BusinessLogics
{
    public class LecturerLogic : ILecturerLogic
    {
        private readonly ILogger _logger;
        private readonly ILecturerStorage _LecturerStorage;

        public LecturerLogic(ILogger<LecturerLogic> logger, ILecturerStorage LecturerStorage)
        {
            _logger = logger;
            _LecturerStorage = LecturerStorage;
        }

        public List<LecturerViewModel>? ReadList(LecturerSearchModel? model)
        {
            _logger.LogInformation("ReadList. Id:{Id}", model?.Id);
            var list = model == null ? _LecturerStorage.GetFullList() : _LecturerStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public LecturerViewModel? ReadElement(LecturerSearchModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _logger.LogInformation("ReadElement. Id:{Id}", model.Id);
            var element = _LecturerStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(LecturerBindingModel model)
        {
            model.Abbreviation = BuildAbbreviation(model.LastName, model.FirstName, model.Patronymic);
            model.Rank = Rank.Отсуствует;
            model.DateBirth = DateTime.SpecifyKind(model.DateBirth.Date, DateTimeKind.Utc);

            CheckModel(model);
            if (_LecturerStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }

        public bool Update(LecturerBindingModel model)
        {
            model.Abbreviation = BuildAbbreviation(model.LastName, model.FirstName, model.Patronymic);
            model.Rank = Rank.Отсуствует;
            model.DateBirth = DateTime.SpecifyKind(model.DateBirth.Date, DateTimeKind.Utc);

            CheckModel(model);
            if (_LecturerStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }

        public bool Delete(LecturerBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_LecturerStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }

        private static string BuildAbbreviation(string? lastName, string? firstName, string? patronymic)
        {
            lastName = (lastName ?? string.Empty).Trim();
            firstName = (firstName ?? string.Empty).Trim();
            patronymic = (patronymic ?? string.Empty).Trim();

            var firstInitial = string.IsNullOrWhiteSpace(firstName) ? "" : $"{char.ToUpper(firstName[0])}.";
            var patronymicInitial = string.IsNullOrWhiteSpace(patronymic) ? "" : $"{char.ToUpper(patronymic[0])}.";

            return string.IsNullOrWhiteSpace(lastName)
                ? string.Empty
                : $"{lastName} {firstInitial}{patronymicInitial}".Trim();
        }

        private void CheckModel(LecturerBindingModel model, bool withParams = true)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (!withParams) return;

            if (string.IsNullOrEmpty(model.FirstName))
                throw new ArgumentNullException("", nameof(model.FirstName));
            if (string.IsNullOrEmpty(model.LastName))
                throw new ArgumentNullException("", nameof(model.LastName));
            if (string.IsNullOrEmpty(model.Patronymic))
                throw new ArgumentNullException("", nameof(model.Patronymic));

            if (string.IsNullOrEmpty(model.Address))
                throw new ArgumentNullException("", nameof(model.Address));
            if (string.IsNullOrEmpty(model.Email))
                throw new ArgumentNullException("", nameof(model.Email));
            if (string.IsNullOrEmpty(model.MobileNumber))
                throw new ArgumentNullException("", nameof(model.MobileNumber));
            if (string.IsNullOrEmpty(model.HomeNumber))
                throw new ArgumentNullException("", nameof(model.HomeNumber));
            if (string.IsNullOrEmpty(model.Description))
                throw new ArgumentNullException("", nameof(model.Description));
            if (model.LecturerStudyPostId <= 0)
                throw new ArgumentNullException("", nameof(model.LecturerStudyPostId));
            if (model.LecturerDepartmentPostId <= 0)
                throw new ArgumentNullException("", nameof(model.LecturerDepartmentPostId));

            var element = _LecturerStorage.GetElement(new LecturerSearchModel
            {
                FirstName = model.FirstName
            });
            if (element != null && element.Id != model.Id)
                throw new InvalidOperationException("");
        }
    }
}