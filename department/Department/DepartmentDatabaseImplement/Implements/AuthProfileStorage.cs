using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DepartmentContracts.SearchModels;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;
using DepartmentDatabaseImplement.Models;

namespace DepartmentDatabaseImplement.Implements
{
    public class AuthProfileStorage : IAuthProfileStorage
    {
        public AuthProfileViewModel? GetProfile(AuthProfileSearchModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Login))
            {
                return null;
            }

            using var context = new DepartmentDatabase();

            var user = context.Set<SystemUser>()
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.RolePermissions)
                            .ThenInclude(x => x.Permission)
                .FirstOrDefault(x => x.Login == model.Login);

            if (user == null)
            {
                return new AuthProfileViewModel
                {
                    Exists = false,
                    IsActive = false,
                    Login = model.Login
                };
            }

            var roles = user.UserRoles
                .Select(x => x.Role.Code)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            var permissions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (user.IsActive)
            {
                permissions.Add("Core.Access");
                permissions.Add("Lab.Schedule.View");
            }

            foreach (var permission in user.UserRoles
                         .SelectMany(x => x.Role.RolePermissions)
                         .Select(x => x.Permission.Code))
            {
                permissions.Add(permission);
            }

            return new AuthProfileViewModel
            {
                Exists = true,
                IsActive = user.IsActive,
                Login = user.Login,
                Roles = roles,
                Permissions = permissions.OrderBy(x => x).ToList()
            };
        }
    }
}