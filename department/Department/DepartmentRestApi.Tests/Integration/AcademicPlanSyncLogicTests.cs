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
public class AcademicPlanSyncLogicTests
{
    private Mock<IOneCApiService> _oneCApiServiceMock = null!;
    private AcademicPlanSyncLogic _logic = null!;

    [SetUp]
    public void SetUp()
    {
        TestDatabaseHelper.RecreateDatabase();
        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", TestEnvironment.ConnectionString);

        _oneCApiServiceMock = new Mock<IOneCApiService>();

        _logic = new AcademicPlanSyncLogic(
            _oneCApiServiceMock.Object,
            new AcademicPlanStorage(),
            new DisciplineStorage(),
            new AcademicPlanRecordStorage(),
            new DisciplineBlockStorage());
    }

    private static AcademicPlan CreateAcademicPlan(
        int id,
        int? educationDirectionId,
        AcademicCourse course,
        EducationForm educationForm,
        string year)
    {
        return AcademicPlan.Create(new AcademicPlanBindingModel
        {
            Id = id,
            EducationDirectionId = educationDirectionId,
            AcademicCourses = course,
            EducationForm = educationForm,
            Year = year
        })!;
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

    private static AcademicPlanRecord CreateAcademicPlanRecord(
        int id,
        int academicPlanId,
        int? disciplineId,
        string index,
        string name,
        int semester,
        int zet,
        int academicHours,
        int? exam,
        int? pass,
        int? gradedPass,
        int? courseWork,
        int? courseProject,
        int? rgr,
        int? lectures,
        int? laboratoryHours,
        int? practicalHours)
    {
        return AcademicPlanRecord.Create(new AcademicPlanRecordBindingModel
        {
            Id = id,
            AcademicPlanId = academicPlanId,
            DisciplineId = disciplineId,
            Index = index,
            Name = name,
            Semester = semester,
            Zet = zet,
            AcademicHours = academicHours,
            Exam = exam,
            Pass = pass,
            GradedPass = gradedPass,
            CourseWork = courseWork,
            CourseProject = courseProject,
            Rgr = rgr,
            Lectures = lectures,
            LaboratoryHours = laboratoryHours,
            PracticalHours = practicalHours
        })!;
    }

    [Test]
    public async Task SyncAcademicPlansAsync_ShouldInsertPlanBlockDisciplineAndRecord()
    {
        _oneCApiServiceMock
            .Setup(x => x.GetAcademicPlansAsync())
            .ReturnsAsync(new List<AcademicPlanOneCDto>
            {
                new AcademicPlanOneCDto
                {
                    Id = 100,
                    EducationDirectionId = null,
                    AcademicCourses = AcademicCourse.Course_1,
                    EducationForm = EducationForm.Очная,
                    Year = "2024",
                    AcademicPlanRecords = new List<AcademicPlanRecordOneCDto>
                    {
                        new AcademicPlanRecordOneCDto
                        {
                            Id = 200,
                            AcademicPlanId = 100,
                            DisciplineId = 300,
                            DisciplineBlockId = 400,
                            DisciplineBlockTitle = "Блок 1",
                            DisciplineBlockBlueAsteriskName = "Блок 1",
                            DisciplineBlockUseForGrouping = false,
                            DisciplineBlockOrder = 1,

                            DisciplineShortName = "Математика",
                            DisciplineDescription = "Математика",

                            HasExam = true,
                            HasCredit = false,
                            HasCourseWork = false,
                            HasCourseProject = false,

                            Index = "Б1.О.01",
                            Name = "Математика",
                            Semester = 1,
                            Zet = 3,
                            AcademicHours = 108,
                            Exam = 1,
                            Pass = 0,
                            GradedPass = 0,
                            CourseWork = 0,
                            CourseProject = 0,
                            Rgr = 0,
                            Lectures = 36,
                            LaboratoryHours = 18,
                            PracticalHours = 18
                        }
                    }
                }
            });

        await _logic.SyncAcademicPlansAsync();

        using var db = new DepartmentDatabase();

        Assert.That(db.AcademicPlans.Count(), Is.EqualTo(1));
        Assert.That(db.DisciplineBlocks.Count(), Is.EqualTo(1));
        Assert.That(db.Disciplines.Count(), Is.EqualTo(1));
        Assert.That(db.AcademicPlanRecords.Count(), Is.EqualTo(1));

        var plan = db.AcademicPlans.Single();
        var block = db.DisciplineBlocks.Single();
        var discipline = db.Disciplines.Single();
        var record = db.AcademicPlanRecords.Single();

        Assert.That(plan.Id, Is.EqualTo(100));
        Assert.That(plan.Year, Is.EqualTo("2024"));
        Assert.That(plan.AcademicCourses, Is.EqualTo(AcademicCourse.Course_1));

        Assert.That(block.Id, Is.EqualTo(400));
        Assert.That(block.Title, Is.EqualTo("Блок 1"));

        Assert.That(discipline.Id, Is.EqualTo(300));
        Assert.That(discipline.DisciplineBlockId, Is.EqualTo(400));
        Assert.That(discipline.DisciplineName, Is.EqualTo("Математика"));

        Assert.That(record.Id, Is.EqualTo(200));
        Assert.That(record.AcademicPlanId, Is.EqualTo(100));
        Assert.That(record.DisciplineId, Is.EqualTo(300));
        Assert.That(record.Index, Is.EqualTo("Б1.О.01"));
    }

    [Test]
    public async Task SyncAcademicPlansAsync_ShouldUpdateExistingEntities()
    {
        using (var checkdb = new DepartmentDatabase())
        {
            checkdb.AcademicPlans.Add(CreateAcademicPlan(
                id: 100,
                educationDirectionId: null,
                course: AcademicCourse.Course_1,
                educationForm: EducationForm.Очная,
                year: "2023"));

            checkdb.DisciplineBlocks.Add(CreateDisciplineBlock(
                id: 400,
                title: "Старый блок",
                blueName: "Старый блок",
                useForGrouping: false,
                order: 1));

            checkdb.Disciplines.Add(CreateDiscipline(
                id: 300,
                blockId: 400,
                name: "Старая дисциплина",
                shortName: "Старая дисциплина",
                description: "Старая дисциплина",
                blueName: "Старый блок",
                hasExam: false,
                hasCredit: true,
                hasCourseWork: false,
                hasCourseProject: false));

            checkdb.AcademicPlanRecords.Add(CreateAcademicPlanRecord(
                id: 200,
                academicPlanId: 100,
                disciplineId: 300,
                index: "OLD",
                name: "Старое имя",
                semester: 1,
                zet: 2,
                academicHours: 72,
                exam: 0,
                pass: 1,
                gradedPass: 0,
                courseWork: 0,
                courseProject: 0,
                rgr: 0,
                lectures: 10,
                laboratoryHours: 10,
                practicalHours: 10));

            checkdb.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetAcademicPlansAsync())
            .ReturnsAsync(new List<AcademicPlanOneCDto>
            {
                new AcademicPlanOneCDto
                {
                    Id = 100,
                    EducationDirectionId = null,
                    AcademicCourses = AcademicCourse.Course_2,
                    EducationForm = EducationForm.Очная,
                    Year = "2024",
                    AcademicPlanRecords = new List<AcademicPlanRecordOneCDto>
                    {
                        new AcademicPlanRecordOneCDto
                        {
                            Id = 200,
                            AcademicPlanId = 100,
                            DisciplineId = 300,
                            DisciplineBlockId = 400,
                            DisciplineBlockTitle = "Новый блок",
                            DisciplineBlockBlueAsteriskName = "Новый блок",
                            DisciplineBlockUseForGrouping = true,
                            DisciplineBlockOrder = 2,

                            DisciplineShortName = "Мат.нов",
                            DisciplineDescription = "Обновленное описание",

                            HasExam = true,
                            HasCredit = false,
                            HasCourseWork = true,
                            HasCourseProject = false,

                            Index = "Б1.О.99",
                            Name = "Новая математика",
                            Semester = 2,
                            Zet = 4,
                            AcademicHours = 144,
                            Exam = 1,
                            Pass = 0,
                            GradedPass = 1,
                            CourseWork = 1,
                            CourseProject = 0,
                            Rgr = 1,
                            Lectures = 40,
                            LaboratoryHours = 20,
                            PracticalHours = 30
                        }
                    }
                }
            });

        await _logic.SyncAcademicPlansAsync();

        using var db = new DepartmentDatabase();

        var plan = db.AcademicPlans.Single(x => x.Id == 100);
        var block = db.DisciplineBlocks.Single(x => x.Id == 400);
        var discipline = db.Disciplines.Single(x => x.Id == 300);
        var record = db.AcademicPlanRecords.Single(x => x.Id == 200);

        Assert.That(plan.Year, Is.EqualTo("2024"));
        Assert.That(plan.AcademicCourses, Is.EqualTo(AcademicCourse.Course_2));

        Assert.That(block.Title, Is.EqualTo("Новый блок"));
        Assert.That(block.DisciplineBlockUseForGrouping, Is.True);
        Assert.That(block.DisciplineBlockOrder, Is.EqualTo(2));

        Assert.That(discipline.DisciplineName, Is.EqualTo("Новая математика"));
        Assert.That(discipline.DisciplineShortName, Is.EqualTo("Мат.нов"));
        Assert.That(discipline.DisciplineDescription, Is.EqualTo("Обновленное описание"));
        Assert.That(discipline.HasExam, Is.True);
        Assert.That(discipline.HasCredit, Is.False);
        Assert.That(discipline.HasCourseWork, Is.True);

        Assert.That(record.Index, Is.EqualTo("Б1.О.99"));
        Assert.That(record.Name, Is.EqualTo("Новая математика"));
        Assert.That(record.Semester, Is.EqualTo(2));
        Assert.That(record.Zet, Is.EqualTo(4));
        Assert.That(record.AcademicHours, Is.EqualTo(144));
        Assert.That(record.GradedPass, Is.EqualTo(1));
        Assert.That(record.CourseWork, Is.EqualTo(1));
        Assert.That(record.Rgr, Is.EqualTo(1));
    }

    [Test]
    public async Task SyncAcademicPlansAsync_ShouldDeleteMissingPlansAndRecords()
    {
        using (var checkdb = new DepartmentDatabase())
        {
            checkdb.AcademicPlans.Add(CreateAcademicPlan(
                id: 100,
                educationDirectionId: null,
                course: AcademicCourse.Course_1,
                educationForm: EducationForm.Очная,
                year: "2023"));

            checkdb.DisciplineBlocks.Add(CreateDisciplineBlock(
                id: 400,
                title: "Блок 1",
                blueName: "Блок 1",
                useForGrouping: false,
                order: 1));

            checkdb.Disciplines.Add(CreateDiscipline(
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

            checkdb.AcademicPlanRecords.Add(CreateAcademicPlanRecord(
                id: 200,
                academicPlanId: 100,
                disciplineId: 300,
                index: "Б1.О.01",
                name: "Математика",
                semester: 1,
                zet: 3,
                academicHours: 108,
                exam: 1,
                pass: 0,
                gradedPass: 0,
                courseWork: 0,
                courseProject: 0,
                rgr: 0,
                lectures: 36,
                laboratoryHours: 18,
                practicalHours: 18));

            checkdb.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetAcademicPlansAsync())
            .ReturnsAsync(new List<AcademicPlanOneCDto>());

        await _logic.SyncAcademicPlansAsync();

        using var db = new DepartmentDatabase();

        Assert.That(db.AcademicPlanRecords.Count(), Is.EqualTo(0));
        Assert.That(db.AcademicPlans.Count(), Is.EqualTo(0));

        // Эти сущности текущая логика НЕ удаляет
        Assert.That(db.DisciplineBlocks.Count(), Is.EqualTo(1));
        Assert.That(db.Disciplines.Count(), Is.EqualTo(1));
    }

    [Test]
    public void SyncAcademicPlansAsync_ShouldThrow_WhenDisciplineIdIsNull()
    {
        _oneCApiServiceMock
            .Setup(x => x.GetAcademicPlansAsync())
            .ReturnsAsync(new List<AcademicPlanOneCDto>
            {
                new AcademicPlanOneCDto
                {
                    Id = 100,
                    EducationDirectionId = null,
                    AcademicCourses = AcademicCourse.Course_1,
                    EducationForm = EducationForm.Очная,
                    Year = "2024",
                    AcademicPlanRecords = new List<AcademicPlanRecordOneCDto>
                    {
                        new AcademicPlanRecordOneCDto
                        {
                            Id = 200,
                            AcademicPlanId = 100,
                            DisciplineId = null,
                            DisciplineBlockId = 400,
                            DisciplineBlockTitle = "Блок 1",
                            DisciplineBlockBlueAsteriskName = "Блок 1",
                            DisciplineBlockUseForGrouping = false,
                            DisciplineBlockOrder = 1,

                            Index = "Б1.О.01",
                            Name = "Математика",
                            Semester = 1,
                            Zet = 3,
                            AcademicHours = 108,

                            HasExam = true,
                            HasCredit = false,
                            HasCourseWork = false,
                            HasCourseProject = false
                        }
                    }
                }
            });

        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _logic.SyncAcademicPlansAsync());

        Assert.That(ex!.Message, Does.Contain("отсутствует DisciplineId"));
    }
}