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
    public class SystemUserRoleLogic : ISystemUserRoleLogic
    {
        private readonly ILogger _logger;
        private readonly ISystemUserRoleStorage _systemUserRoleStorage;

        public SystemUserRoleLogic(ILogger<SystemUserRoleLogic> logger, ISystemUserRoleStorage systemUserRoleStorage)
        {
            _logger = logger;
            _systemUserRoleStorage = systemUserRoleStorage;
        }

        public List<SystemUserRoleViewModel>? ReadList(SystemUserRoleSearchModel? model)
        {
            _logger.LogInformation("ReadList. Id:{Id}", model?.Id);
            var list = model == null
                ? _systemUserRoleStorage.GetFullList()
                : _systemUserRoleStorage.GetFilteredList(model);

            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }

            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public SystemUserRoleViewModel? ReadElement(SystemUserRoleSearchModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            _logger.LogInformation("ReadElement. Id:{Id}", model.Id);
            var element = _systemUserRoleStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }

            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(SystemUserRoleBindingModel model)
        {
            CheckModel(model);

            if (_systemUserRoleStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }

            return true;
        }

        public bool Delete(SystemUserRoleBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);

            if (_systemUserRoleStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }

            return true;
        }

        private void CheckModel(SystemUserRoleBindingModel model, bool withParams = true)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            if (!withParams)
            {
                if (model.Id <= 0)
                    throw new ArgumentNullException("", nameof(model.Id));
                return;
            }

            if (model.UserId <= 0)
                throw new ArgumentNullException("", nameof(model.UserId));

            if (model.RoleId <= 0)
                throw new ArgumentNullException("", nameof(model.RoleId));

            var element = _systemUserRoleStorage.GetElement(new SystemUserRoleSearchModel
            {
                UserId = model.UserId,
                RoleId = model.RoleId
            });

            if (element != null && element.Id != model.Id)
                throw new InvalidOperationException("Такая роль у пользователя уже назначена");
        }
    }
}