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
public class DisciplineStudentRecordSyncLogicTests
{
    private Mock<IOneCApiService> _oneCApiServiceMock = null!;
    private DisciplineStudentRecordSyncLogic _logic = null!;

    [SetUp]
    public void SetUp()
    {
        TestDatabaseHelper.RecreateDatabase();
        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", TestEnvironment.ConnectionString);

        _oneCApiServiceMock = new Mock<IOneCApiService>();

        _logic = new DisciplineStudentRecordSyncLogic(
            _oneCApiServiceMock.Object,
            new DisciplineStudentRecordStorage(),
            new DisciplineStorage(),
            new StudentStorage());
    }

    private static DisciplineBlock CreateDisciplineBlock(
        int id,
        string title,
        string blueName,
        bool useForGrouping,
        int order)
    {
        return DisciplineBlock.Create(new DisciplineBlockBindingModel
        {
            Id = id,
            Title = title,
            DisciplineBlockBlueAsteriskName = blueName,
            DisciplineBlockUseForGrouping = useForGrouping,
            DisciplineBlockOrder = order
        })!;
    }

    private static Discipline CreateDiscipline(
        int id,
        int blockId,
        string name,
        string shortName,
        string description,
        string blueName,
        bool hasExam,
        bool hasCredit,
        bool hasCourseWork,
        bool hasCourseProject)
    {
        return Discipline.Create(new DisciplineBindingModel
        {
            Id = id,
            DisciplineBlockId = blockId,
            DisciplineName = name,
            DisciplineShortName = shortName,
            DisciplineDescription = description,
            DisciplineBlockBlueAsteriskName = blueName,
            HasExam = hasExam,
            HasCredit = hasCredit,
            HasCourseWork = hasCourseWork,
            HasCourseProject = hasCourseProject
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

    private static DisciplineStudentRecord CreateDisciplineStudentRecord(
        int id,
        int disciplineId,
        int studentId,
        Semesters semester,
        string variant,
        int subGroup,
        MarkType markType)
    {
        return DisciplineStudentRecord.Create(new DisciplineStudentRecordBindingModel
        {
            Id = id,
            DisciplineId = disciplineId,
            StudentId = studentId,
            Semester = semester,
            Variant = variant,
            SubGroup = subGroup,
            MarkType = markType
        })!;
    }

    [Test]
    public async Task SyncDisciplineStudentRecordsAsync_ShouldInsertRecord_WhenStudentAndDisciplineExist()
    {
        using (var db = new DepartmentDatabase())
        {
            db.DisciplineBlocks.Add(CreateDisciplineBlock(
                id: 400,
                title: "Блок 1",
                blueName: "Блок 1",
                useForGrouping: false,
                order: 1));

            db.Disciplines.Add(CreateDiscipline(
                id: 300,
                blockId: 400,
                name: "Математика",
                shortName: "Математика",
                description: "Математика",
                blueName: "Блок 1",
                hasExam: true,
                hasCredit: false,
                hasCourseWork: false,
                hasCourseProject: false));

            db.Students.Add(CreateStudent(
                id: 200,
                studentGroupId: null,
                numberOfBook: "12345",
                firstName: "Иван",
                lastName: "Иванов",
                patronymic: "Иванович",
                email: "ivan@test.local",
                studentState: (StudentState)0,
                description: "",
                photo: Array.Empty<byte>(),
                isSteward: false));

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetDisciplineStudentRecordsAsync())
            .ReturnsAsync(new List<DisciplineStudentRecordOneCDto>
            {
                new DisciplineStudentRecordOneCDto
                {
                    Id = 100,
                    DisciplineId = 300,
                    StudentId = 200,
                    Semester = (Semesters)1,
                    Variant = "Основной",
                    SubGroup = 1,
                    MarkType = (MarkType)1
                }
            });

        await _logic.SyncDisciplineStudentRecordsAsync();

        using var checkDb = new DepartmentDatabase();

        Assert.That(checkDb.DisciplineStudentRecords.Count(), Is.EqualTo(1));

        var record = checkDb.DisciplineStudentRecords.Single();
        Assert.That(record.Id, Is.EqualTo(100));
        Assert.That(record.DisciplineId, Is.EqualTo(300));
        Assert.That(record.StudentId, Is.EqualTo(200));
        Assert.That(record.Variant, Is.EqualTo("Основной"));
        Assert.That(record.SubGroup, Is.EqualTo(1));
    }

    [Test]
    public async Task SyncDisciplineStudentRecordsAsync_ShouldUpdateExistingRecord()
    {
        using (var db = new DepartmentDatabase())
        {
            db.DisciplineBlocks.Add(CreateDisciplineBlock(
                id: 400,
                title: "Блок 1",
                blueName: "Блок 1",
                useForGrouping: false,
                order: 1));

            db.Disciplines.Add(CreateDiscipline(
                id: 300,
                blockId: 400,
                name: "Математика",
                shortName: "Математика",
                description: "Математика",
                blueName: "Блок 1",
                hasExam: true,
                hasCredit: false,
                hasCourseWork: false,
                hasCourseProject: false));

            db.Students.Add(CreateStudent(
                id: 200,
                studentGroupId: null,
                numberOfBook: "12345",
                firstName: "Иван",
                lastName: "Иванов",
                patronymic: "Иванович",
                email: "ivan@test.local",
                studentState: (StudentState)0,
                description: "",
                photo: Array.Empty<byte>(),
                isSteward: false));

            db.DisciplineStudentRecords.Add(CreateDisciplineStudentRecord(
                id: 100,
                disciplineId: 300,
                studentId: 200,
                semester: (Semesters)1,
                variant: "Старый вариант",
                subGroup: 1,
                markType: (MarkType)1));

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetDisciplineStudentRecordsAsync())
            .ReturnsAsync(new List<DisciplineStudentRecordOneCDto>
            {
                new DisciplineStudentRecordOneCDto
                {
                    Id = 100,
                    DisciplineId = 300,
                    StudentId = 200,
                    Semester = (Semesters)2,
                    Variant = "Новый вариант",
                    SubGroup = 2,
                    MarkType = (MarkType)2
                }
            });

        await _logic.SyncDisciplineStudentRecordsAsync();

        using var checkDb = new DepartmentDatabase();

        var record = checkDb.DisciplineStudentRecords.Single(x => x.Id == 100);

        Assert.That((int)record.Semester, Is.EqualTo(2));
        Assert.That(record.Variant, Is.EqualTo("Новый вариант"));
        Assert.That(record.SubGroup, Is.EqualTo(2));
        Assert.That((int)record.MarkType, Is.EqualTo(2));
    }

    [Test]
    public async Task SyncDisciplineStudentRecordsAsync_ShouldDeleteMissingRecords()
    {
        using (var db = new DepartmentDatabase())
        {
            db.DisciplineBlocks.Add(CreateDisciplineBlock(
                id: 400,
                title: "Блок 1",
                blueName: "Блок 1",
                useForGrouping: false,
                order: 1));

            db.Disciplines.Add(CreateDiscipline(
                id: 300,
                blockId: 400,
                name: "Математика",
                shortName: "Математика",
                description: "Математика",
                blueName: "Блок 1",
                hasExam: true,
                hasCredit: false,
                hasCourseWork: false,
                hasCourseProject: false));

            db.Students.Add(CreateStudent(
                id: 200,
                studentGroupId: null,
                numberOfBook: "12345",
                firstName: "Иван",
                lastName: "Иванов",
                patronymic: "Иванович",
                email: "ivan@test.local",
                studentState: (StudentState)0,
                description: "",
                photo: Array.Empty<byte>(),
                isSteward: false));

            db.DisciplineStudentRecords.Add(CreateDisciplineStudentRecord(
                id: 100,
                disciplineId: 300,
                studentId: 200,
                semester: (Semesters)1,
                variant: "Основной",
                subGroup: 1,
                markType: (MarkType)1));

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetDisciplineStudentRecordsAsync())
            .ReturnsAsync(new List<DisciplineStudentRecordOneCDto>());

        await _logic.SyncDisciplineStudentRecordsAsync();

        using var checkDb = new DepartmentDatabase();

        Assert.That(checkDb.DisciplineStudentRecords.Count(), Is.EqualTo(0));

        // Студент и дисциплина этой логикой не удаляются
        Assert.That(checkDb.Students.Count(), Is.EqualTo(1));
        Assert.That(checkDb.Disciplines.Count(), Is.EqualTo(1));
    }

    [Test]
    public void SyncDisciplineStudentRecordsAsync_ShouldThrow_WhenDisciplineDoesNotExist()
    {
        using (var db = new DepartmentDatabase())
        {
            db.Students.Add(CreateStudent(
                id: 200,
                studentGroupId: null,
                numberOfBook: "12345",
                firstName: "Иван",
                lastName: "Иванов",
                patronymic: "Иванович",
                email: "ivan@test.local",
                studentState: (StudentState)0,
                description: "",
                photo: Array.Empty<byte>(),
                isSteward: false));

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetDisciplineStudentRecordsAsync())
            .ReturnsAsync(new List<DisciplineStudentRecordOneCDto>
            {
                new DisciplineStudentRecordOneCDto
                {
                    Id = 100,
                    DisciplineId = 300,
                    StudentId = 200,
                    Semester = (Semesters)1,
                    Variant = "Основной",
                    SubGroup = 1,
                    MarkType = (MarkType)1
                }
            });

        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _logic.SyncDisciplineStudentRecordsAsync());

        Assert.That(ex!.Message, Does.Contain("Не найдена дисциплина"));
    }

    [Test]
    public void SyncDisciplineStudentRecordsAsync_ShouldThrow_WhenStudentDoesNotExist()
    {
        using (var db = new DepartmentDatabase())
        {
            db.DisciplineBlocks.Add(CreateDisciplineBlock(
                id: 400,
                title: "Блок 1",
                blueName: "Блок 1",
                useForGrouping: false,
                order: 1));

            db.Disciplines.Add(CreateDiscipline(
                id: 300,
                blockId: 400,
                name: "Математика",
                shortName: "Математика",
                description: "Математика",
                blueName: "Блок 1",
                hasExam: true,
                hasCredit: false,
                hasCourseWork: false,
                hasCourseProject: false));

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetDisciplineStudentRecordsAsync())
            .ReturnsAsync(new List<DisciplineStudentRecordOneCDto>
            {
                new DisciplineStudentRecordOneCDto
                {
                    Id = 100,
                    DisciplineId = 300,
                    StudentId = 200,
                    Semester = (Semesters)1,
                    Variant = "Основной",
                    SubGroup = 1,
                    MarkType = (MarkType)1
                }
            });

        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _logic.SyncDisciplineStudentRecordsAsync());

        Assert.That(ex!.Message, Does.Contain("Не найден студент"));
    }
}


