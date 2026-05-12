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
    public class SystemPermissionLogic : ISystemPermissionLogic
    {
        private readonly ILogger _logger;
        private readonly ISystemPermissionStorage _systemPermissionStorage;

        public SystemPermissionLogic(ILogger<SystemPermissionLogic> logger, ISystemPermissionStorage systemPermissionStorage)
        {
            _logger = logger;
            _systemPermissionStorage = systemPermissionStorage;
        }

        public List<SystemPermissionViewModel>? ReadList(SystemPermissionSearchModel? model)
        {
            _logger.LogInformation("ReadList. Id:{Id}", model?.Id);
            var list = model == null
                ? _systemPermissionStorage.GetFullList()
                : _systemPermissionStorage.GetFilteredList(model);

            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }

            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public SystemPermissionViewModel? ReadElement(SystemPermissionSearchModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            _logger.LogInformation("ReadElement. Id:{Id}", model.Id);
            var element = _systemPermissionStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }

            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(SystemPermissionBindingModel model)
        {
            CheckModel(model);

            if (_systemPermissionStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }

            return true;
        }

        public bool Update(SystemPermissionBindingModel model)
        {
            CheckModel(model);

            if (_systemPermissionStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }

            return true;
        }

        public bool Delete(SystemPermissionBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);

            if (_systemPermissionStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }

            return true;
        }

        private void CheckModel(SystemPermissionBindingModel model, bool withParams = true)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            if (!withParams)
            {
                if (model.Id <= 0)
                    throw new ArgumentNullException("", nameof(model.Id));
                return;
            }

            model.Code = (model.Code ?? string.Empty).Trim();
            model.Name = (model.Name ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(model.Code))
                throw new ArgumentNullException("", nameof(model.Code));

            if (string.IsNullOrWhiteSpace(model.Name))
                throw new ArgumentNullException("", nameof(model.Name));

            var element = _systemPermissionStorage.GetElement(new SystemPermissionSearchModel
            {
                Code = model.Code
            });

            if (element != null && element.Id != model.Id)
                throw new InvalidOperationException("Право с таким кодом уже существует");
        }
    }
}