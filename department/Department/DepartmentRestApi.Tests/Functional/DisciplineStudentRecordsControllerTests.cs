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
public class DisciplineStudentRecordsControllerTests
{
    private Mock<IDisciplineStudentRecordLogic> _logicMock = null!;
    private DisciplineStudentRecordsController _controller = null!;

    [SetUp]
    public void SetUp()
    {
        _logicMock = new Mock<IDisciplineStudentRecordLogic>();

        _controller = new DisciplineStudentRecordsController(
            Mock.Of<ILogger<DisciplineStudentRecordsController>>(),
            _logicMock.Object);
    }

    [Test]
    public void GetDisciplineStudentRecordList_ShouldReturnOk()
    {
        _logicMock
            .Setup(x => x.ReadList(null))
            .Returns(new List<DisciplineStudentRecordViewModel> { new() });

        var result = _controller.GetDisciplineStudentRecordList();

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void GetDisciplineStudentRecordList_ShouldReturn500_WhenLogicThrows()
    {
        _logicMock
            .Setup(x => x.ReadList(null))
            .Throws(new Exception("test"));

        var result = _controller.GetDisciplineStudentRecordList();

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(500));
    }

    [Test]
    public void GetDisciplineStudentRecord_ShouldReturnOk_WhenElementExists()
    {
        _logicMock
            .Setup(x => x.ReadElement(It.IsAny<DisciplineStudentRecordSearchModel>()))
            .Returns(new DisciplineStudentRecordViewModel());

        var result = _controller.GetDisciplineStudentRecord(new DisciplineStudentRecordSearchModel { Id = 1 });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void GetDisciplineStudentRecord_ShouldReturnNotFound_WhenElementMissing()
    {
        _logicMock
            .Setup(x => x.ReadElement(It.IsAny<DisciplineStudentRecordSearchModel>()))
            .Returns((DisciplineStudentRecordViewModel?)null);

        var result = _controller.GetDisciplineStudentRecord(new DisciplineStudentRecordSearchModel { Id = 1 });

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void DisciplineStudentRecordCreate_ShouldReturnBadRequest_WhenModelIsNull()
    {
        var result = _controller.DisciplineStudentRecordCreate(null!);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void DisciplineStudentRecordCreate_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller.ModelState.AddModelError("Variant", "Required");

        var result = _controller.DisciplineStudentRecordCreate(new DisciplineStudentRecordBindingModel());

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void DisciplineStudentRecordCreate_ShouldReturnOk_WhenModelIsValid()
    {
        _logicMock
            .Setup(x => x.Create(It.IsAny<DisciplineStudentRecordBindingModel>()))
            .Returns(true);

        var result = _controller.DisciplineStudentRecordCreate(new DisciplineStudentRecordBindingModel
        {
            Id = 1,
            DisciplineId = 10,
            StudentId = 20,
            Variant = "Основной",
            SubGroup = 1
        });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void DisciplineStudentRecordCreate_ShouldReturnConflict_WhenLogicThrowsInvalidOperationException()
    {
        _logicMock
            .Setup(x => x.Create(It.IsAny<DisciplineStudentRecordBindingModel>()))
            .Throws(new InvalidOperationException("conflict"));

        var result = _controller.DisciplineStudentRecordCreate(new DisciplineStudentRecordBindingModel
        {
            DisciplineId = 10,
            StudentId = 20,
            Variant = "Основной",
            SubGroup = 1
        });

        Assert.That(result, Is.InstanceOf<ConflictObjectResult>());
    }

    [Test]
    public void DisciplineStudentRecordUpdate_ShouldReturnOk_WhenModelIsValid()
    {
        _logicMock
            .Setup(x => x.Update(It.IsAny<DisciplineStudentRecordBindingModel>()))
            .Returns(true);

        var result = _controller.DisciplineStudentRecordUpdate(new DisciplineStudentRecordBindingModel
        {
            Id = 1,
            DisciplineId = 10,
            StudentId = 20,
            Variant = "Обновленный",
            SubGroup = 2
        });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void DisciplineStudentRecordDelete_ShouldReturnBadRequest_WhenIdInvalid()
    {
        var result = _controller.DisciplineStudentRecordDelete(new DisciplineStudentRecordBindingModel
        {
            Id = 0
        });

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void DisciplineStudentRecordDelete_ShouldReturnOk_WhenDeleteSucceeded()
    {
        _logicMock
            .Setup(x => x.Delete(It.IsAny<DisciplineStudentRecordBindingModel>()))
            .Returns(true);

        var result = _controller.DisciplineStudentRecordDelete(new DisciplineStudentRecordBindingModel
        {
            Id = 1
        });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
}