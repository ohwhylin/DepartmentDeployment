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
    public class SystemUserLogic : ISystemUserLogic
    {
        private readonly ILogger _logger;
        private readonly ISystemUserStorage _systemUserStorage;

        public SystemUserLogic(ILogger<SystemUserLogic> logger, ISystemUserStorage systemUserStorage)
        {
            _logger = logger;
            _systemUserStorage = systemUserStorage;
        }

        public List<SystemUserViewModel>? ReadList(SystemUserSearchModel? model)
        {
            _logger.LogInformation("ReadList. Id:{Id}", model?.Id);
            var list = model == null
                ? _systemUserStorage.GetFullList()
                : _systemUserStorage.GetFilteredList(model);

            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }

            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public SystemUserViewModel? ReadElement(SystemUserSearchModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            _logger.LogInformation("ReadElement. Id:{Id}", model.Id);
            var element = _systemUserStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }

            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(SystemUserBindingModel model)
        {
            CheckModel(model);

            if (_systemUserStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }

            return true;
        }

        public bool Update(SystemUserBindingModel model)
        {
            CheckModel(model);

            if (_systemUserStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }

            return true;
        }

        public bool Delete(SystemUserBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);

            if (_systemUserStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }

            return true;
        }

        private void CheckModel(SystemUserBindingModel model, bool withParams = true)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            if (!withParams)
            {
                if (model.Id <= 0)
                    throw new ArgumentNullException("", nameof(model.Id));
                return;
            }

            model.Login = (model.Login ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(model.Login))
                throw new ArgumentNullException("", nameof(model.Login));

            var element = _systemUserStorage.GetElement(new SystemUserSearchModel
            {
                Login = model.Login
            });

            if (element != null && element.Id != model.Id)
                throw new InvalidOperationException("Пользователь с таким логином уже существует");
        }
    }
}