using DepartmentBusinessLogic.BusinessLogics.Sync;
using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.Dtos.OneC;
using DepartmentDataModels.Enums;
using DepartmentDatabaseImplement;
using DepartmentDatabaseImplement.Implements;
using DepartmentDatabaseImplement.Models;
using DepartmentRestApi.Tests.Infrastructure;
using Moq;
using NUnit.Framework;

namespace DepartmentRestApi.Tests.Integration;

[TestFixture]
public class StudentSyncLogicTests
{
    private Mock<IOneCApiService> _oneCApiServiceMock = null!;
    private StudentSyncLogic _logic = null!;

    [SetUp]
    public void SetUp()
    {
        TestDatabaseHelper.RecreateDatabase();
        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", TestEnvironment.ConnectionString);

        _oneCApiServiceMock = new Mock<IOneCApiService>();

        _logic = new StudentSyncLogic(
            _oneCApiServiceMock.Object,
            new StudentStorage(),
            new StudentGroupStorage());
    }

    private static EducationDirection CreateEducationDirection(
        int id,
        string cipher,
        string shortName,
        string title,
        EducationDirectionQualification qualification,
        string profile,
        string description)
    {
        return EducationDirection.Create(new EducationDirectionBindingModel
        {
            Id = id,
            Cipher = cipher,
            ShortName = shortName,
            Title = title,
            Qualification = qualification,
            Profile = profile,
            Description = description
        })!;
    }

    private static StudentGroup CreateStudentGroup(
        int id,
        int educationDirectionId,
        int? curatorId,
        string groupName,
        AcademicCourse course)
    {
        return StudentGroup.Create(new StudentGroupBindingModel
        {
            Id = id,
            EducationDirectionId = educationDirectionId,
            CuratorId = curatorId,
            GroupName = groupName,
            Course = course
        })!;
    }

    private static Student CreateStudent(
        int id,
        int? studentGroupId,
        string numberOfBook,
        string firstName,
        string lastName,
        string patronymic,
        string email,
        StudentState studentState,
        string description,
        byte[] photo,
        bool isSteward)
    {
        return Student.Create(new StudentBindingModel
        {
            Id = id,
            StudentGroupId = studentGroupId,
            NumberOfBook = numberOfBook,
            FirstName = firstName,
            LastName = lastName,
            Patronymic = patronymic,
            Email = email,
            StudentState = studentState,
            Description = description,
            Photo = photo,
            IsSteward = isSteward
        })!;
    }

    [Test]
    public async Task SyncStudentsAsync_ShouldInsertStudent_WhenGroupExists()
    {
        using (var db = new DepartmentDatabase())
        {
            db.EducationDirections.Add(CreateEducationDirection(
                id: 1,
                cipher: "09.03.04",
                shortName: "ПИ",
                title: "Программная инженерия",
                qualification: (EducationDirectionQualification)0,
                profile: "Разработка ПО",
                description: "Тест"));

            db.StudentGroups.Add(CreateStudentGroup(
                id: 10,
                educationDirectionId: 1,
                curatorId: null,
                groupName: "ПИбд-41",
                course: (AcademicCourse)1));

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetStudentsAsync())
            .ReturnsAsync(new List<StudentOneCDto>
            {
                new StudentOneCDto
                {
                    Id = 100,
                    StudentGroupId = 10,
                    NumberOfBook = "211004",
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Patronymic = "Иванович",
                    Email = "ivanov@test.local",
                    StudentState = (StudentState)0,
                    Description = "Описание",
                    Photo = new byte[] { 1, 2, 3 },
                    IsSteward = false
                }
            });

        await _logic.SyncStudentsAsync();

        using var checkDb = new DepartmentDatabase();

        Assert.That(checkDb.Students.Count(), Is.EqualTo(1));

        var student = checkDb.Students.Single();
        Assert.That(student.Id, Is.EqualTo(100));
        Assert.That(student.StudentGroupId, Is.EqualTo(10));
        Assert.That(student.NumberOfBook, Is.EqualTo("211004"));
        Assert.That(student.FirstName, Is.EqualTo("Иван"));
        Assert.That(student.LastName, Is.EqualTo("Иванов"));
        Assert.That(student.Email, Is.EqualTo("ivanov@test.local"));
        Assert.That(student.IsSteward, Is.False);
        Assert.That(student.Photo, Is.Not.Null);
        Assert.That(student.Photo!.Length, Is.EqualTo(3));
    }

    [Test]
    public async Task SyncStudentsAsync_ShouldUpdateExistingStudent_WhenDataChanged()
    {
        using (var db = new DepartmentDatabase())
        {
            db.Students.Add(CreateStudent(
                id: 100,
                studentGroupId: null,
                numberOfBook: "211004",
                firstName: "Иван",
                lastName: "Иванов",
                patronymic: "Иванович",
                email: "old@test.local",
                studentState: (StudentState)0,
                description: "Старое описание",
                photo: new byte[] { 1, 2, 3 },
                isSteward: false));

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetStudentsAsync())
            .ReturnsAsync(new List<StudentOneCDto>
            {
                new StudentOneCDto
                {
                    Id = 100,
                    StudentGroupId = null,
                    NumberOfBook = "211004",
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Patronymic = "Иванович",
                    Email = "new@test.local",
                    StudentState = (StudentState)0,
                    Description = "Новое описание",
                    Photo = new byte[] { 9, 9, 9, 9 },
                    IsSteward = true
                }
            });

        await _logic.SyncStudentsAsync();

        using var checkDb = new DepartmentDatabase();

        var student = checkDb.Students.Single(x => x.Id == 100);

        Assert.That(student.Email, Is.EqualTo("new@test.local"));
        Assert.That(student.Description, Is.EqualTo("Новое описание"));
        Assert.That(student.IsSteward, Is.True);
        Assert.That(student.Photo, Is.Not.Null);
        Assert.That(student.Photo!.Length, Is.EqualTo(4));
        Assert.That(student.Photo[0], Is.EqualTo(9));
    }

    [Test]
    public async Task SyncStudentsAsync_ShouldDeleteStudents_ThatAreMissingInOneC()
    {
        using (var db = new DepartmentDatabase())
        {
            db.Students.Add(CreateStudent(
                id: 100,
                studentGroupId: null,
                numberOfBook: "211004",
                firstName: "Иван",
                lastName: "Иванов",
                patronymic: "Иванович",
                email: "ivan@test.local",
                studentState: (StudentState)0,
                description: "",
                photo: Array.Empty<byte>(),
                isSteward: false));

            db.Students.Add(CreateStudent(
                id: 101,
                studentGroupId: null,
                numberOfBook: "211005",
                firstName: "Петр",
                lastName: "Петров",
                patronymic: "Петрович",
                email: "petr@test.local",
                studentState: (StudentState)0,
                description: "",
                photo: Array.Empty<byte>(),
                isSteward: false));

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetStudentsAsync())
            .ReturnsAsync(new List<StudentOneCDto>
            {
                new StudentOneCDto
                {
                    Id = 100,
                    StudentGroupId = null,
                    NumberOfBook = "211004",
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Patronymic = "Иванович",
                    Email = "ivan@test.local",
                    StudentState = (StudentState)0,
                    Description = "",
                    Photo = Array.Empty<byte>(),
                    IsSteward = false
                }
            });

        await _logic.SyncStudentsAsync();

        using var checkDb = new DepartmentDatabase();

        Assert.That(checkDb.Students.Count(), Is.EqualTo(1));
        Assert.That(checkDb.Students.Any(x => x.Id == 100), Is.True);
        Assert.That(checkDb.Students.Any(x => x.Id == 101), Is.False);
    }

    [Test]
    public void SyncStudentsAsync_ShouldThrow_WhenStudentGroupDoesNotExist()
    {
        _oneCApiServiceMock
            .Setup(x => x.GetStudentsAsync())
            .ReturnsAsync(new List<StudentOneCDto>
            {
                new StudentOneCDto
                {
                    Id = 100,
                    StudentGroupId = 999,
                    NumberOfBook = "211004",
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Patronymic = "Иванович",
                    Email = "ivan@test.local",
                    StudentState = (StudentState)0,
                    Description = "",
                    Photo = Array.Empty<byte>(),
                    IsSteward = false
                }
            });

        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _logic.SyncStudentsAsync());

        Assert.That(ex!.Message, Does.Contain("Не найдена учебная группа"));
    }

    [Test]
    public async Task SyncStudentsAsync_ShouldInsertStudent_WhenStudentGroupIdIsNull()
    {
        _oneCApiServiceMock
            .Setup(x => x.GetStudentsAsync())
            .ReturnsAsync(new List<StudentOneCDto>
            {
                new StudentOneCDto
                {
                    Id = 100,
                    StudentGroupId = null,
                    NumberOfBook = "211004",
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Patronymic = "Иванович",
                    Email = "ivan@test.local",
                    StudentState = (StudentState)0,
                    Description = "Без группы",
                    Photo = Array.Empty<byte>(),
                    IsSteward = false
                }
            });

        await _logic.SyncStudentsAsync();

        using var checkDb = new DepartmentDatabase();

        Assert.That(checkDb.Students.Count(), Is.EqualTo(1));

        var student = checkDb.Students.Single();
        Assert.That(student.StudentGroupId, Is.Null);
        Assert.That(student.Description, Is.EqualTo("Без группы"));
    }
}