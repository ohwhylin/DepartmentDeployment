using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;
using DepartmentRestApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DepartmentRestApi.Tests.Functional;

[TestFixture]
public class StudentsControllerTests
{
    private Mock<IStudentLogic> _logicMock = null!;
    private StudentsController _controller = null!;

    [SetUp]
    public void SetUp()
    {
        _logicMock = new Mock<IStudentLogic>();

        _controller = new StudentsController(
            Mock.Of<ILogger<StudentsController>>(),
            _logicMock.Object);
    }

    [Test]
    public void GetStudentList_ShouldReturnOk()
    {
        _logicMock
            .Setup(x => x.ReadList(null))
            .Returns(new List<StudentViewModel> { new() });

        var result = _controller.GetStudentList();

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void GetStudentList_ShouldReturn500_WhenLogicThrows()
    {
        _logicMock
            .Setup(x => x.ReadList(null))
            .Throws(new Exception("test"));

        var result = _controller.GetStudentList();

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(500));
    }

    [Test]
    public void GetStudent_ShouldReturnOk_WhenElementExists()
    {
        _logicMock
            .Setup(x => x.ReadElement(It.IsAny<StudentSearchModel>()))
            .Returns(new StudentViewModel());

        var result = _controller.GetStudent(new StudentSearchModel { Id = 1 });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void GetStudent_ShouldReturnNotFound_WhenElementMissing()
    {
        _logicMock
            .Setup(x => x.ReadElement(It.IsAny<StudentSearchModel>()))
            .Returns((StudentViewModel?)null);

        var result = _controller.GetStudent(new StudentSearchModel { Id = 1 });

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void StudentCreate_ShouldReturnBadRequest_WhenModelIsNull()
    {
        var result = _controller.StudentCreate(null!);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void StudentCreate_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller.ModelState.AddModelError("FirstName", "Required");

        var result = _controller.StudentCreate(new StudentBindingModel());

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void StudentCreate_ShouldReturnOk_WhenModelIsValid()
    {
        _logicMock
            .Setup(x => x.Create(It.IsAny<StudentBindingModel>()))
            .Returns(true);

        var result = _controller.StudentCreate(new StudentBindingModel
        {
            Id = 1,
            StudentGroupId = 10,
            NumberOfBook = "211004",
            FirstName = "Иван",
            LastName = "Иванов",
            Patronymic = "Иванович",
            Email = "ivan@test.local",
            Description = "Тестовый студент",
            Photo = Array.Empty<byte>(),
            IsSteward = false
        });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void StudentCreate_ShouldReturnConflict_WhenLogicThrowsInvalidOperationException()
    {
        _logicMock
            .Setup(x => x.Create(It.IsAny<StudentBindingModel>()))
            .Throws(new InvalidOperationException("conflict"));

        var result = _controller.StudentCreate(new StudentBindingModel
        {
            StudentGroupId = 10,
            NumberOfBook = "211004",
            FirstName = "Иван",
            LastName = "Иванов",
            Patronymic = "Иванович",
            Email = "ivan@test.local",
            Description = "Тестовый студент",
            Photo = Array.Empty<byte>()
        });

        Assert.That(result, Is.InstanceOf<ConflictObjectResult>());
    }

    [Test]
    public void StudentUpdate_ShouldReturnOk_WhenModelIsValid()
    {
        _logicMock
            .Setup(x => x.Update(It.IsAny<StudentBindingModel>()))
            .Returns(true);

        var result = _controller.StudentUpdate(new StudentBindingModel
        {
            Id = 1,
            StudentGroupId = 10,
            NumberOfBook = "211004",
            FirstName = "Петр",
            LastName = "Петров",
            Patronymic = "Петрович",
            Email = "petr@test.local",
            Description = "Обновленный студент",
            Photo = Array.Empty<byte>(),
            IsSteward = true
        });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void StudentDelete_ShouldReturnBadRequest_WhenIdInvalid()
    {
        var result = _controller.StudentDelete(new StudentBindingModel
        {
            Id = 0
        });

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void StudentDelete_ShouldReturnOk_WhenDeleteSucceeded()
    {
        _logicMock
            .Setup(x => x.Delete(It.IsAny<StudentBindingModel>()))
            .Returns(true);

        var result = _controller.StudentDelete(new StudentBindingModel
        {
            Id = 1
        });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
}