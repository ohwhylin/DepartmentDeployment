using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.SearchModels;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;

namespace DepartmentBusinessLogic.BusinessLogics
{
    public class LecturerDepartmentPostLogic : ILecturerDepartmentPostLogic
    {
        private readonly ILogger _logger;
        private readonly ILecturerDepartmentPostStorage _LecturerDepartmentPostStorage;

        public LecturerDepartmentPostLogic(ILogger<LecturerDepartmentPostLogic> logger, ILecturerDepartmentPostStorage LecturerDepartmentPostStorage)
        {
            _logger = logger;
            _LecturerDepartmentPostStorage = LecturerDepartmentPostStorage;
        }

        public List<LecturerDepartmentPostViewModel>? ReadList(LecturerDepartmentPostSearchModel? model)
        {
            _logger.LogInformation("ReadList. Id:{Id}", model?.Id);
            var list = model == null ? _LecturerDepartmentPostStorage.GetFullList() : _LecturerDepartmentPostStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public LecturerDepartmentPostViewModel? ReadElement(LecturerDepartmentPostSearchModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _logger.LogInformation("ReadElement. Id:{Id}", model.Id);
            var element = _LecturerDepartmentPostStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(LecturerDepartmentPostBindingModel model)
        {
            CheckModel(model);

            model.Order = 1;

            if (_LecturerDepartmentPostStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }

        public bool Update(LecturerDepartmentPostBindingModel model)
        {
            CheckModel(model);

            model.Order = 1;

            if (_LecturerDepartmentPostStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }

        public bool Delete(LecturerDepartmentPostBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_LecturerDepartmentPostStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(LecturerDepartmentPostBindingModel model, bool withParams = true)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (!withParams) return;
            if (string.IsNullOrEmpty(model.DepartmentPostTitle))
                throw new ArgumentNullException("", nameof(model.DepartmentPostTitle));
        }
    }
}