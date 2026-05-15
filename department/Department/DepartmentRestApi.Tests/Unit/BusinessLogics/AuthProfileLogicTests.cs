using DepartmentBusinessLogic.BusinessLogics;
using DepartmentContracts.SearchModels;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DepartmentRestApi.Tests.Unit.BusinessLogics;

[TestFixture]
public class AuthProfileLogicTests
{
    private Mock<IAuthProfileStorage> _authProfileStorageMock = null!;
    private Mock<ILogger<AuthProfileLogic>> _loggerMock = null!;

    private AuthProfileLogic _logic = null!;

    [SetUp]
    public void SetUp()
    {
        _authProfileStorageMock = new Mock<IAuthProfileStorage>();
        _loggerMock = new Mock<ILogger<AuthProfileLogic>>();

        _logic = new AuthProfileLogic(
            _loggerMock.Object,
            _authProfileStorageMock.Object);
    }

    [Test]
    public void ReadProfile_ShouldThrowArgumentNullException_WhenModelIsNull()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
            _logic.ReadProfile(null!));

        Assert.That(ex!.ParamName, Is.EqualTo("model"));

        _authProfileStorageMock.Verify(
            x => x.GetProfile(It.IsAny<AuthProfileSearchModel>()),
            Times.Never);
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("   ")]
    [TestCase("\t")]
    public void ReadProfile_ShouldThrowArgumentNullException_WhenLoginIsEmptyOrWhitespace(string login)
    {
        var model = new AuthProfileSearchModel
        {
            Login = login
        };

        var ex = Assert.Throws<ArgumentNullException>(() =>
            _logic.ReadProfile(model));

        Assert.That(ex!.ParamName, Is.EqualTo("Login"));

        _authProfileStorageMock.Verify(
            x => x.GetProfile(It.IsAny<AuthProfileSearchModel>()),
            Times.Never);
    }

    [Test]
    public void ReadProfile_ShouldThrowArgumentNullException_WhenLoginIsNull()
    {
        var model = new AuthProfileSearchModel
        {
            Login = null!
        };

        var ex = Assert.Throws<ArgumentNullException>(() =>
            _logic.ReadProfile(model));

        Assert.That(ex!.ParamName, Is.EqualTo("Login"));

        _authProfileStorageMock.Verify(
            x => x.GetProfile(It.IsAny<AuthProfileSearchModel>()),
            Times.Never);
    }

    [Test]
    public void ReadProfile_ShouldTrimLogin_BeforePassingModelToStorage()
    {
        AuthProfileSearchModel? capturedModel = null;

        var expectedProfile = new AuthProfileViewModel
        {
            Exists = true,
            IsActive = true,
            Login = "p.chubykina",
            Roles = new List<string> { "Developer" },
            Permissions = new List<string> { "Core.Access" }
        };

        _authProfileStorageMock
            .Setup(x => x.GetProfile(It.IsAny<AuthProfileSearchModel>()))
            .Callback<AuthProfileSearchModel>(model => capturedModel = model)
            .Returns(expectedProfile);

        var result = _logic.ReadProfile(new AuthProfileSearchModel
        {
            Login = "   p.chubykina   "
        });

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.SameAs(expectedProfile));

        Assert.That(capturedModel, Is.Not.Null);
        Assert.That(capturedModel!.Login, Is.EqualTo("p.chubykina"));

        _authProfileStorageMock.Verify(
            x => x.GetProfile(It.IsAny<AuthProfileSearchModel>()),
            Times.Once);
    }

    [Test]
    public void ReadProfile_ShouldReturnNull_WhenStorageReturnsNull()
    {
        _authProfileStorageMock
            .Setup(x => x.GetProfile(It.IsAny<AuthProfileSearchModel>()))
            .Returns((AuthProfileViewModel?)null);

        var result = _logic.ReadProfile(new AuthProfileSearchModel
        {
            Login = "p.chubykina"
        });

        Assert.That(result, Is.Null);

        _authProfileStorageMock.Verify(
            x => x.GetProfile(It.Is<AuthProfileSearchModel>(m => m.Login == "p.chubykina")),
            Times.Once);
    }

    [Test]
    public void ReadProfile_ShouldReturnProfile_WhenStorageReturnsProfile()
    {
        var expectedProfile = new AuthProfileViewModel
        {
            Exists = true,
            IsActive = true,
            Login = "p.chubykina",
            Roles = new List<string> { "Developer", "Teacher" },
            Permissions = new List<string> { "Core.Access", "Lab.Schedule.View" }
        };

        _authProfileStorageMock
            .Setup(x => x.GetProfile(It.IsAny<AuthProfileSearchModel>()))
            .Returns(expectedProfile);

        var result = _logic.ReadProfile(new AuthProfileSearchModel
        {
            Login = "p.chubykina"
        });

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Exists, Is.True);
        Assert.That(result.IsActive, Is.True);
        Assert.That(result.Login, Is.EqualTo("p.chubykina"));
        Assert.That(result.Roles, Is.EqualTo(new[] { "Developer", "Teacher" }));
        Assert.That(result.Permissions, Is.EqualTo(new[] { "Core.Access", "Lab.Schedule.View" }));

        _authProfileStorageMock.Verify(
            x => x.GetProfile(It.Is<AuthProfileSearchModel>(m => m.Login == "p.chubykina")),
            Times.Once);
    }
}