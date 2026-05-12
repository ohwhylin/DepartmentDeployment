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
    public class SystemRoleLogic : ISystemRoleLogic
    {
        private readonly ILogger _logger;
        private readonly ISystemRoleStorage _systemRoleStorage;

        public SystemRoleLogic(ILogger<SystemRoleLogic> logger, ISystemRoleStorage systemRoleStorage)
        {
            _logger = logger;
            _systemRoleStorage = systemRoleStorage;
        }

        public List<SystemRoleViewModel>? ReadList(SystemRoleSearchModel? model)
        {
            _logger.LogInformation("ReadList. Id:{Id}", model?.Id);
            var list = model == null
                ? _systemRoleStorage.GetFullList()
                : _systemRoleStorage.GetFilteredList(model);

            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }

            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public SystemRoleViewModel? ReadElement(SystemRoleSearchModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            _logger.LogInformation("ReadElement. Id:{Id}", model.Id);
            var element = _systemRoleStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }

            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(SystemRoleBindingModel model)
        {
            CheckModel(model);

            if (_systemRoleStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }

            return true;
        }

        public bool Update(SystemRoleBindingModel model)
        {
            CheckModel(model);

            if (_systemRoleStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }

            return true;
        }

        public bool Delete(SystemRoleBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);

            if (_systemRoleStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }

            return true;
        }

        private void CheckModel(SystemRoleBindingModel model, bool withParams = true)
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

            var element = _systemRoleStorage.GetElement(new SystemRoleSearchModel
            {
                Code = model.Code
            });

            if (element != null && element.Id != model.Id)
                throw new InvalidOperationException("Роль с таким кодом уже существует");
        }
    }
}