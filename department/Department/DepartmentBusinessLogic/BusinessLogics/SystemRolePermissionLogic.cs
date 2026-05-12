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
    public class SystemRolePermissionLogic : ISystemRolePermissionLogic
    {
        private readonly ILogger _logger;
        private readonly ISystemRolePermissionStorage _systemRolePermissionStorage;

        public SystemRolePermissionLogic(ILogger<SystemRolePermissionLogic> logger, ISystemRolePermissionStorage systemRolePermissionStorage)
        {
            _logger = logger;
            _systemRolePermissionStorage = systemRolePermissionStorage;
        }

        public List<SystemRolePermissionViewModel>? ReadList(SystemRolePermissionSearchModel? model)
        {
            _logger.LogInformation("ReadList. Id:{Id}", model?.Id);
            var list = model == null
                ? _systemRolePermissionStorage.GetFullList()
                : _systemRolePermissionStorage.GetFilteredList(model);

            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }

            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public SystemRolePermissionViewModel? ReadElement(SystemRolePermissionSearchModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            _logger.LogInformation("ReadElement. Id:{Id}", model.Id);
            var element = _systemRolePermissionStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }

            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(SystemRolePermissionBindingModel model)
        {
            CheckModel(model);

            if (_systemRolePermissionStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }

            return true;
        }

        public bool Delete(SystemRolePermissionBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);

            if (_systemRolePermissionStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }

            return true;
        }

        private void CheckModel(SystemRolePermissionBindingModel model, bool withParams = true)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            if (!withParams)
            {
                if (model.Id <= 0)
                    throw new ArgumentNullException("", nameof(model.Id));
                return;
            }

            if (model.RoleId <= 0)
                throw new ArgumentNullException("", nameof(model.RoleId));

            if (model.PermissionId <= 0)
                throw new ArgumentNullException("", nameof(model.PermissionId));

            var element = _systemRolePermissionStorage.GetElement(new SystemRolePermissionSearchModel
            {
                RoleId = model.RoleId,
                PermissionId = model.PermissionId
            });

            if (element != null && element.Id != model.Id)
                throw new InvalidOperationException("Такое право уже назначено роли");
        }
    }
}