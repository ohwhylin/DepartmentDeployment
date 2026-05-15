using System;
using Microsoft.Extensions.Logging;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.SearchModels;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;

namespace DepartmentBusinessLogic.BusinessLogics
{
    public class AuthProfileLogic : IAuthProfileLogic
    {
        private readonly ILogger _logger;
        private readonly IAuthProfileStorage _authProfileStorage;

        public AuthProfileLogic(ILogger<AuthProfileLogic> logger, IAuthProfileStorage authProfileStorage)
        {
            _logger = logger;
            _authProfileStorage = authProfileStorage;
        }

        public AuthProfileViewModel? ReadProfile(AuthProfileSearchModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrWhiteSpace(model.Login))
                throw new ArgumentNullException(nameof(model.Login));

            model.Login = model.Login.Trim();

            _logger.LogInformation("ReadProfile. Login:{Login}", model.Login);

            var profile = _authProfileStorage.GetProfile(model);

            if (profile == null)
            {
                _logger.LogWarning("ReadProfile profile not found");
                return null;
            }

            _logger.LogInformation("ReadProfile completed. Exists:{Exists}, IsActive:{IsActive}", profile.Exists, profile.IsActive);
            return profile;
        }
    }
}