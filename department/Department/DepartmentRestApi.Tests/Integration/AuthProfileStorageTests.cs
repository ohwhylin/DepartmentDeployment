using DepartmentContracts.BindingModels;
using DepartmentContracts.SearchModels;
using DepartmentDatabaseImplement;
using DepartmentDatabaseImplement.Implements;
using DepartmentDatabaseImplement.Models;
using DepartmentRestApi.Tests.Infrastructure;
using NUnit.Framework;

namespace DepartmentRestApi.Tests.Integration;

[TestFixture]
public class AuthProfileStorageTests
{
    private AuthProfileStorage _storage = null!;

    [SetUp]
    public void SetUp()
    {
        TestDatabaseHelper.RecreateDatabase();
        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", TestEnvironment.ConnectionString);

        _storage = new AuthProfileStorage();
    }

    private static SystemUser CreateSystemUser(int id, string login, bool isActive)
    {
        return SystemUser.Create(new SystemUserBindingModel
        {
            Id = id,
            Login = login,
            IsActive = isActive
        })!;
    }

    private static SystemUserRole CreateSystemUserRole(int id, int userId, int roleId)
    {
        return SystemUserRole.Create(new SystemUserRoleBindingModel
        {
            Id = id,
            UserId = userId,
            RoleId = roleId
        })!;
    }

    [Test]
    public void GetProfile_ShouldReturnNull_WhenModelIsNull()
    {
        var result = _storage.GetProfile(null!);

        Assert.That(result, Is.Null);
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("   ")]
    [TestCase("\t")]
    public void GetProfile_ShouldReturnNull_WhenLoginIsEmptyOrWhitespace(string login)
    {
        var result = _storage.GetProfile(new AuthProfileSearchModel
        {
            Login = login
        });

        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetProfile_ShouldReturnExistsFalse_WhenUserIsNotFound()
    {
        var result = _storage.GetProfile(new AuthProfileSearchModel
        {
            Login = "unknown.user"
        });

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Exists, Is.False);
        Assert.That(result.IsActive, Is.False);
        Assert.That(result.Login, Is.EqualTo("unknown.user"));
        Assert.That(result.Roles, Is.Empty);
        Assert.That(result.Permissions, Is.Empty);
    }

    [Test]
    public void GetProfile_ShouldReturnBasePermissions_WhenUserIsActiveAndHasNoRoles()
    {
        using (var db = new DepartmentDatabase())
        {
            db.SystemUsers.Add(CreateSystemUser(
                id: 100,
                login: "p.chubykina",
                isActive: true));

            db.SaveChanges();
        }

        var result = _storage.GetProfile(new AuthProfileSearchModel
        {
            Login = "p.chubykina"
        });

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Exists, Is.True);
        Assert.That(result.IsActive, Is.True);
        Assert.That(result.Login, Is.EqualTo("p.chubykina"));
        Assert.That(result.Roles, Is.Empty);

        Assert.That(result.Permissions, Is.EqualTo(new[]
        {
            "Core.Access",
            "Lab.Schedule.View"
        }));
    }

    [Test]
    public void GetProfile_ShouldReturnRolesAndMergedPermissions_WhenUserIsActive()
    {
        using (var db = new DepartmentDatabase())
        {
            db.SystemUsers.Add(CreateSystemUser(
                id: 100,
                login: "p.chubykina",
                isActive: true));

            db.SystemUserRoles.Add(CreateSystemUserRole(
                id: 1000,
                userId: 100,
                roleId: 2));

            db.SystemUserRoles.Add(CreateSystemUserRole(
                id: 1001,
                userId: 100,
                roleId: 5));

            db.SaveChanges();
        }

        var result = _storage.GetProfile(new AuthProfileSearchModel
        {
            Login = "p.chubykina"
        });

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Exists, Is.True);
        Assert.That(result.IsActive, Is.True);
        Assert.That(result.Login, Is.EqualTo("p.chubykina"));

        Assert.That(result.Roles, Is.EqualTo(new[]
        {
            "Admin",
            "Developer"
        }));

        Assert.That(result.Permissions, Is.EqualTo(new[]
        {
            "Core.Access",
            "Lab.DutySchedule.Access",
            "Lab.Inventory.Access",
            "Lab.Schedule.BookConsultation",
            "Lab.Schedule.View",
            "Load.Access"
        }));
    }

    [Test]
    public void GetProfile_ShouldReturnRolePermissionsButNotBasePermissions_WhenUserIsInactive()
    {
        using (var db = new DepartmentDatabase())
        {
            db.SystemUsers.Add(CreateSystemUser(
                id: 100,
                login: "inactive.user",
                isActive: false));

            db.SystemUserRoles.Add(CreateSystemUserRole(
                id: 1000,
                userId: 100,
                roleId: 4));

            db.SaveChanges();
        }

        var result = _storage.GetProfile(new AuthProfileSearchModel
        {
            Login = "inactive.user"
        });

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Exists, Is.True);
        Assert.That(result.IsActive, Is.False);
        Assert.That(result.Login, Is.EqualTo("inactive.user"));

        Assert.That(result.Roles, Is.EqualTo(new[]
        {
            "Teacher"
        }));

        Assert.That(result.Permissions, Is.EqualTo(new[]
        {
            "Lab.Schedule.BookConsultation"
        }));
    }
}