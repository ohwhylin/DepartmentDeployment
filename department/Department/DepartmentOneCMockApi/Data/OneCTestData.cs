using DepartmentDataModels.Enums;
using DepartmentOneCMockApi.Models;
using System.Linq;
using System.Xml.Linq;

namespace DepartmentOneCMockApi.Data
{
    public static class OneCTestData
    {
        public static List<StudentGroupMockModel> StudentGroups => new()
        {
            new()
            {
                Id = 1,
                EducationDirectionId = 1,
                CuratorId = 1,
                GroupName = "ПИбд-11",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 2,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-12",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 3,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-13",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 4,
                EducationDirectionId = 1,
                CuratorId = 3,
                GroupName = "ПИбд-14",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 5,
                EducationDirectionId = 1,
                CuratorId = 1,
                GroupName = "ПИбд-21",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 6,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-22",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 7,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-23",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 8,
                EducationDirectionId = 1,
                CuratorId = 3,
                GroupName = "ПИбд-24",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 9,
                EducationDirectionId = 1,
                CuratorId = 1,
                GroupName = "ПИбд-31",
                Course = AcademicCourse.Course_3
            },
            new()
            {
                Id = 10,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-32",
                Course = AcademicCourse.Course_3
            },
            new()
            {
                Id = 11,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-33",
                Course = AcademicCourse.Course_3
            },
            new()
            {
                Id = 12,
                EducationDirectionId = 1,
                CuratorId = 1,
                GroupName = "ПИбд-41",
                Course = AcademicCourse.Course_4
            },
            new()
            {
                Id = 13,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-42",
                Course = AcademicCourse.Course_4
            },
            new()
            {
                Id = 14,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-43",
                Course = AcademicCourse.Course_4
            },
            new()
            {
                Id = 15,
                EducationDirectionId = 2,
                CuratorId = 2,
                GroupName = "ИСЭбд-11",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 16,
                EducationDirectionId = 2,
                CuratorId = 1,
                GroupName = "ИСЭбд-12",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 17,
                EducationDirectionId = 2,
                CuratorId = 2,
                GroupName = "ИСЭбд-21",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 18,
                EducationDirectionId = 2,
                CuratorId = 1,
                GroupName = "ИСЭбд-22",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 19,
                EducationDirectionId = 2,
                CuratorId = 2,
                GroupName = "ИСЭбд-31",
                Course = AcademicCourse.Course_3
            },
            new()
            {
                Id = 20,
                EducationDirectionId = 2,
                CuratorId = 1,
                GroupName = "ИСЭбд-41",
                Course = AcademicCourse.Course_4
            },
            new()
            {
                Id = 21,
                EducationDirectionId = 3,
                CuratorId = 2,
                GroupName = "БИмд-11",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 22,
                EducationDirectionId = 3,
                CuratorId = 2,
                GroupName = "БИмд-21",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 23,
                EducationDirectionId = 4,
                CuratorId = 2,
                GroupName = "ИИПАмд-11",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 24,
                EducationDirectionId = 4,
                CuratorId = 2,
                GroupName = "ИИПАмд-21",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 25,
                EducationDirectionId = 5,
                CuratorId = 2,
                GroupName = "ИИБАмд-11",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 26,
                EducationDirectionId = 5,
                CuratorId = 2,
                GroupName = "ИИБАмд-21",
                Course = AcademicCourse.Course_2
            },
        };

        private static readonly Dictionary<int, int> StudentCountByGroupId = new()
        {
            { 1, 30 },
            { 2, 29 },
            { 3, 28 },
            { 4, 19 },
            { 5, 30 },
            { 6, 30 },
            { 7, 29 },
            { 8, 19 },
            { 9, 32 },
            { 10, 32 },
            { 11, 31 },
            { 12, 32 },
            { 13, 31 },
            { 14, 29 },
            { 15, 27 },
            { 16, 25 },
            { 17, 31 },
            { 18, 28 },
            { 19, 32 },
            { 20, 25 },
            { 21, 4 },
            { 22, 4 },
            { 23, 24 },
            { 24, 29 },
            { 25, 15 },
            { 26, 27 }
        };

        public static List<StudentMockModel> Students => GenerateStudents();

        private static readonly string[] FemaleFirstNames =
{
    "Алина", "Мария", "Екатерина", "Анна",
    "Дарья", "Виктория", "Полина", "София"
};

        private static readonly string[] MaleFirstNames =
        {
    "Илья", "Даниил", "Артем", "Кирилл",
    "Степан", "Максим", "Павел", "Егор"
};

        private static readonly string[] MaleLastNames =
{
    "Иванов", "Петров", "Сидоров", "Кузнецов",
    "Смирнов", "Орлов", "Волков", "Попов",
    "Васильев"
};

        private static readonly string[] FemaleLastNames =
        {
    "Иванова", "Петрова", "Сидорова", "Кузнецова",
    "Смирнова", "Орлова", "Волкова", "Попова",
    "Васильева"
};
        private static readonly string[] FemalePatronymics =
        {
    "Ивановна", "Петровна", "Сергеевна", "Андреевна",
    "Игоревна", "Павловна", "Дмитриевна", "Олеговна"
};

        private static readonly string[] MalePatronymics =
        {
    "Иванович", "Петрович", "Сергеевич", "Андреевич",
    "Игоревич", "Павлович", "Дмитриевич", "Олегович"
};

        private static List<StudentMockModel> GenerateStudents()
        {
            var students = new List<StudentMockModel>();
            var studentId = 1;
            var femaleIndex = 0;
            var maleIndex = 0;

            foreach (var group in StudentGroups.OrderBy(x => x.Id))
            {
                var studentCount = StudentCountByGroupId.TryGetValue(group.Id, out var count)
                    ? count
                    : 4;

                for (var i = 0; i < studentCount; i++)
                {
                    var isFemale = i % 2 == 0;

                    string firstName;
                    string lastName;
                    string patronymic;

                    if (isFemale)
                    {
                        firstName = FemaleFirstNames[femaleIndex % FemaleFirstNames.Length];
                        lastName = FemaleLastNames[femaleIndex % FemaleLastNames.Length];
                        patronymic = FemalePatronymics[femaleIndex % FemalePatronymics.Length];
                        femaleIndex++;
                    }
                    else
                    {
                        firstName = MaleFirstNames[maleIndex % MaleFirstNames.Length];
                        lastName = MaleLastNames[maleIndex % MaleLastNames.Length];
                        patronymic = MalePatronymics[maleIndex % MalePatronymics.Length];
                        maleIndex++;
                    }

                    students.Add(new StudentMockModel
                    {
                        Id = studentId,
                        StudentGroupId = group.Id,
                        NumberOfBook = $"22{studentId:000}",
                        FirstName = firstName,
                        LastName = lastName,
                        Patronymic = patronymic,
                        Email = $"student{studentId}@university.local",
                        StudentState = StudentState.Учится,
                        Description = string.Empty,
                        IsSteward = i == 0
                    });

                    studentId++;
                }
            }

            return students;
        }


        private static (int blockId, string blockTitle, string blueAsteriskName, bool useForGrouping, int blockOrder) GetBlockInfo(PlanSeed seed)
        {
            if (seed.Index.StartsWith("Б3"))
            {
                return (3, "Государственная итоговая аттестация", "", false, 3);
            }

            if (seed.Index.StartsWith("Б2"))
            {
                return (2, "Практики", "", false, 2);
            }

            return (1, "Дисциплины (модули)", "", true, 1);
        }

        private sealed class PlanSeed
        {
            public string Index { get; set; } = "";
            public string Name { get; set; } = "";
            public int Semester { get; set; }
            public int Zet { get; set; }
            public int AcademicHours { get; set; }

            public int? Exam { get; set; }
            public int? Pass { get; set; }
            public int? GradedPass { get; set; }
            public int? CourseWork { get; set; }
            public int? CourseProject { get; set; }
            public int? Rgr { get; set; }

            public int Lectures { get; set; }
            public int LaboratoryHours { get; set; }
            public int PracticalHours { get; set; }

            public bool IsSpecialBlock { get; set; }

            public PlanSeed Clone()
            {
                return new PlanSeed
                {
                    Index = Index,
                    Name = Name,
                    Semester = Semester,
                    Zet = Zet,
                    AcademicHours = AcademicHours,
                    Exam = Exam,
                    Pass = Pass,
                    GradedPass = GradedPass,
                    CourseWork = CourseWork,
                    CourseProject = CourseProject,
                    Rgr = Rgr,
                    Lectures = Lectures,
                    LaboratoryHours = LaboratoryHours,
                    PracticalHours = PracticalHours,
                    IsSpecialBlock = IsSpecialBlock
                };
            }
        }

        private static int _recordId = 1;
        private static int _disciplineId = 1;

        private static PlanSeed D(
            string index,
            string name,
            int semester,
            int zet,
            int lectures,
            int laboratoryHours,
            int practicalHours,
            int? exam = null,
            int? pass = null,
            int? gradedPass = null,
            int? courseWork = null,
            int? courseProject = null,
            int? rgr = null)
        {
            return new PlanSeed
            {
                Index = index,
                Name = name,
                Semester = semester,
                Zet = zet,
                AcademicHours = lectures + laboratoryHours + practicalHours,
                Exam = exam,
                Pass = pass,
                GradedPass = gradedPass,
                CourseWork = courseWork,
                CourseProject = courseProject,
                Rgr = rgr,
                Lectures = lectures,
                LaboratoryHours = laboratoryHours,
                PracticalHours = practicalHours,
                IsSpecialBlock = false
            };
        }

        private static PlanSeed P(
            string index,
            string name,
            int semester,
            int zet = 3,
            int academicHours = 108)
        {
            return new PlanSeed
            {
                Index = index,
                Name = name,
                Semester = semester,
                Zet = zet,
                AcademicHours = academicHours,
                IsSpecialBlock = true
            };
        }

        private static List<PlanSeed> Copy(List<PlanSeed> source)
        {
            return source.Select(x => x.Clone()).ToList();
        }

        private static void Move(List<PlanSeed> plan, string index, int newSemester)
        {
            var item = plan.First(x => x.Index == index);
            item.Semester = newSemester;
        }

        private static void SetHours(List<PlanSeed> plan, string index, int lectures, int laboratoryHours, int practicalHours)
        {
            var item = plan.First(x => x.Index == index);
            item.Lectures = lectures;
            item.LaboratoryHours = laboratoryHours;
            item.PracticalHours = practicalHours;
            item.AcademicHours = lectures + laboratoryHours + practicalHours;
        }

        private static void Remove(List<PlanSeed> plan, string index)
        {
            var item = plan.FirstOrDefault(x => x.Index == index);
            if (item != null)
                plan.Remove(item);
        }

        private static void Add(List<PlanSeed> plan, PlanSeed item)
        {
            plan.Add(item);
        }

        private static AcademicPlanMockModel BuildPlan(
    int planId,
    int educationDirectionId,
    string year,
    List<PlanSeed> seeds)
        {
            var records = new List<AcademicPlanRecordMockModel>();

            foreach (var seed in seeds)
            {
                var block = GetBlockInfo(seed);

                records.Add(new AcademicPlanRecordMockModel
                {
                    Id = _recordId++,
                    AcademicPlanId = planId,
                    DisciplineId = GetOrCreateDisciplineId(seed.Name),

                    DisciplineBlockId = block.blockId,
                    DisciplineBlockTitle = block.blockTitle,
                    DisciplineBlockBlueAsteriskName = block.blueAsteriskName,
                    DisciplineBlockUseForGrouping = block.useForGrouping,
                    DisciplineBlockOrder = block.blockOrder,

                    DisciplineShortName = seed.Name,
                    DisciplineDescription = seed.Name,

                    HasExam = seed.Exam.HasValue,
                    HasCredit = seed.Pass.HasValue || seed.GradedPass.HasValue,
                    HasCourseWork = seed.CourseWork.HasValue,
                    HasCourseProject = seed.CourseProject.HasValue,

                    Index = seed.Index,
                    Name = seed.Name,
                    Semester = seed.Semester,
                    Zet = seed.Zet,
                    AcademicHours = seed.AcademicHours,

                    Exam = seed.Exam,
                    Pass = seed.Pass,
                    GradedPass = seed.GradedPass,
                    CourseWork = seed.CourseWork,
                    CourseProject = seed.CourseProject,
                    Rgr = seed.Rgr,

                    Lectures = seed.IsSpecialBlock ? 0 : seed.Lectures,
                    LaboratoryHours = seed.IsSpecialBlock ? 0 : seed.LaboratoryHours,
                    PracticalHours = seed.IsSpecialBlock ? 0 : seed.PracticalHours
                });
            }

            return new AcademicPlanMockModel
            {
                Id = planId,
                EducationDirectionId = educationDirectionId,
                EducationForm = EducationForm.Очная,
                AcademicCourses = AcademicCourse.Course_1,
                Year = year,
                AcademicPlanRecords = records
            };
        }

        //
        // 09.03.04 — EducationDirectionId = 1
        //

        private static List<PlanSeed> Base090304()
        {
            return new List<PlanSeed>
            {
                D("Б1.О.13", "Введение в программную инженерию", 1, 2, 16, 0, 16, pass: 1),
                D("Б1.О.22", "Информационные системы и технологии", 1, 2, 16, 32, 0, exam: 1),
                D("Б1.О.23", "Теоретические основы информатики", 1, 2, 16, 0, 16, pass: 1, rgr: 1),

                D("Б1.О.14", "Организация ЭВМ и системы", 2, 2, 16, 32, 0, exam: 1),
                P("Б2.О.01(У)", "Ознакомительная практика", 2, 3, 108),

                D("Б1.О.15", "Базы данных", 3, 2, 16, 32, 0, exam: 1, courseProject: 1),
                D("Б1.О.16", "Системы управления базами данных", 3, 2, 16, 32, 0, pass: 1),

                D("Б1.О.20", "Методы моделирования", 4, 2, 16, 0, 16, pass: 1),
                D("Б1.О.21", "Технологии программирования", 4, 3, 16, 64, 0, exam: 1, courseWork: 1),

                D("Б1.О.33", "Алгоритмы и структуры данных", 5, 2, 16, 32, 0, exam: 1),
                D("Б1.В.01", "Проектирование и архитектура программных систем", 5, 2, 16, 32, 0, pass: 1, courseProject: 1),
                D("Б1.В.03", "Методы искусственного интеллекта", 5, 2, 16, 32, 0, pass: 1),

                D("Б1.В.05", "Интернет-программирование", 6, 2, 16, 32, 0, exam: 1),
                D("Б1.В.07", "Программирование на Java", 6, 2, 16, 32, 0, pass: 1),

                D("Б1.В.12", "Тестирование программного обеспечения", 7, 2, 16, 32, 0, exam: 1),
                D("Б1.В.13", "Конструирование программного обеспечения", 7, 2, 16, 32, 0, exam: 1, courseWork: 1),
                P("Б2.О.02(П)", "Научно-исследовательская работа", 7, 3, 108),

                P("Б1.О.24", "Проектный практикум", 8, 3, 108),
                P("Б2.О.03(П)", "Технологическая (проектно-технологическая) практика", 8, 3, 108),
                P("Б2.В.01(П)", "Преддипломная практика", 8, 3, 108),
                P("Б3.01", "Подготовка к сдаче и сдача государственного экзамена", 8, 3, 108),
                P("Б3.02", "Выполнение и защита выпускной квалификационной работы", 8, 3, 108)
            };
        }

        private static List<PlanSeed> Plan090304_2022_2026()
        {
            return Copy(Base090304());
        }

        private static List<PlanSeed> Plan090304_2023_2027()
        {
            var plan = Copy(Base090304());

            Move(plan, "Б1.В.05", 5);
            Move(plan, "Б1.В.03", 6);

            SetHours(plan, "Б1.О.22", 16, 64, 0);
            SetHours(plan, "Б1.О.21", 16, 32, 0);
            SetHours(plan, "Б1.В.07", 16, 64, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090304_2024_2028()
        {
            var plan = Copy(Base090304());

            Move(plan, "Б1.О.20", 3);
            Move(plan, "Б1.О.16", 4);
            Move(plan, "Б1.В.12", 6);
            Move(plan, "Б1.В.07", 7);

            SetHours(plan, "Б1.О.15", 16, 64, 0);
            SetHours(plan, "Б1.В.12", 16, 32, 0);
            SetHours(plan, "Б1.В.13", 16, 64, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090304_2025_2029()
        {
            var plan = Copy(Base090304());

            Remove(plan, "Б1.В.07");
            Add(plan, D("Б1.В.14", "Разработка веб-приложений", 6, 2, 16, 32, 0, exam: 1));

            Move(plan, "Б1.В.03", 6);
            Move(plan, "Б1.В.05", 5);

            SetHours(plan, "Б1.О.14", 16, 64, 0);
            SetHours(plan, "Б1.В.01", 16, 64, 0);
            SetHours(plan, "Б1.В.03", 16, 32, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090304_2026_2030()
        {
            var plan = Copy(Base090304());

            Move(plan, "Б1.В.01", 4);
            Move(plan, "Б1.О.33", 6);
            Move(plan, "Б1.В.05", 5);
            Move(plan, "Б1.В.03", 6);

            SetHours(plan, "Б1.О.33", 16, 64, 0);
            SetHours(plan, "Б1.О.20", 16, 0, 16);
            SetHours(plan, "Б1.В.13", 16, 64, 0);

            return plan;
        }

        private static List<AcademicPlanMockModel> Build090304Plans()
        {
            return new List<AcademicPlanMockModel>
            {
                BuildPlan(1, 1, "2022-2026", Plan090304_2022_2026()),
                BuildPlan(2, 1, "2023-2027", Plan090304_2023_2027()),
                BuildPlan(3, 1, "2024-2028", Plan090304_2024_2028()),
                BuildPlan(4, 1, "2025-2029", Plan090304_2025_2029()),
                BuildPlan(5, 1, "2026-2030", Plan090304_2026_2030())
            };
        }

        //
        // 09.03.03 — EducationDirectionId = 2
        //

        private static List<PlanSeed> Base090303()
        {
            return new List<PlanSeed>
            {
                D("Б1.О.01", "Программирование", 1, 2, 16, 32, 0, exam: 1),
                D("Б1.О.02", "Основы алгоритмизации и программирования", 1, 2, 16, 0, 16, pass: 1),
                D("Б1.О.03", "Основы информационных технологий", 1, 2, 16, 32, 0, pass: 1),

                D("Б1.О.16", "Основы прикладной информатики", 2, 2, 16, 0, 16, pass: 1, rgr: 1),
                D("Б1.О.17", "Организация вычислительных машин и систем", 2, 2, 16, 32, 0, exam: 1),
                P("Б2.О.01(У)", "Ознакомительная практика", 2, 3, 108),

                D("Б1.О.18", "Базы данных", 3, 2, 16, 32, 0, exam: 1, courseProject: 1),
                D("Б1.О.19", "Системы управления базами данных", 3, 2, 16, 32, 0, pass: 1),
                D("Б1.О.28", "Алгоритмы и структуры данных", 3, 2, 16, 32, 0, exam: 1),

                D("Б1.О.22", "Методы моделирования", 4, 2, 16, 0, 16, pass: 1, courseWork: 1),
                D("Б1.О.23", "Операционные системы", 4, 2, 16, 32, 0, exam: 1),
                D("Б1.О.24", "Проектирование информационных систем", 4, 2, 16, 32, 0, pass: 1),

                D("Б1.В.01", "Интернет-программирование", 5, 2, 16, 32, 0, exam: 1),
                D("Б1.В.02", "Построение информационных систем", 5, 2, 16, 32, 0, pass: 1),
                D("Б1.В.03", "Методы искусственного интеллекта", 5, 2, 16, 32, 0, pass: 1),

                D("Б1.В.07", "Экспертные системы", 6, 2, 16, 32, 0, exam: 1),
                D("Б1.В.08", "Сетевые технологии в экономике", 6, 2, 16, 32, 0, pass: 1),

                D("Б1.В.ДВ.03.01", "Теория и практика экономических информационных систем", 7, 2, 16, 32, 0, pass: 1),
                D("Б1.В.ДВ.03.02", "Сервис-ориентированное программирование", 7, 2, 16, 32, 0, exam: 1),
                P("Б2.О.02(П)", "Научно-исследовательская работа", 7, 3, 108),

                P("Б2.О.03(П)", "Технологическая (проектно-технологическая) практика", 8, 3, 108),
                P("Б2.В.01(П)", "Преддипломная практика", 8, 3, 108),
                P("Б3.01", "Подготовка к сдаче и сдача государственного экзамена", 8, 3, 108),
                P("Б3.02", "Выполнение и защита выпускной квалификационной работы", 8, 3, 108)
            };
        }

        private static List<PlanSeed> Plan090303_2022_2026()
        {
            return Copy(Base090303());
        }

        private static List<PlanSeed> Plan090303_2023_2027()
        {
            var plan = Copy(Base090303());

            Move(plan, "Б1.В.03", 6);
            Move(plan, "Б1.В.08", 5);

            SetHours(plan, "Б1.О.17", 16, 64, 0);
            SetHours(plan, "Б1.В.01", 16, 64, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090303_2024_2028()
        {
            var plan = Copy(Base090303());

            Move(plan, "Б1.О.22", 3);
            Move(plan, "Б1.О.19", 4);

            SetHours(plan, "Б1.О.18", 16, 64, 0);
            SetHours(plan, "Б1.В.07", 16, 64, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090303_2025_2029()
        {
            var plan = Copy(Base090303());

            Move(plan, "Б1.В.01", 6);
            Move(plan, "Б1.В.07", 5);

            SetHours(plan, "Б1.О.24", 16, 64, 0);
            SetHours(plan, "Б1.В.03", 16, 64, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090303_2026_2030()
        {
            var plan = Copy(Base090303());

            Move(plan, "Б1.О.28", 4);
            Move(plan, "Б1.О.22", 5);

            SetHours(plan, "Б1.О.16", 16, 0, 16);
            SetHours(plan, "Б1.В.ДВ.03.02", 16, 64, 0);

            return plan;
        }

        private static List<AcademicPlanMockModel> Build090303Plans()
        {
            return new List<AcademicPlanMockModel>
            {
                BuildPlan(6, 2, "2022-2026", Plan090303_2022_2026()),
                BuildPlan(7, 2, "2023-2027", Plan090303_2023_2027()),
                BuildPlan(8, 2, "2024-2028", Plan090303_2024_2028()),
                BuildPlan(9, 2, "2025-2029", Plan090303_2025_2029()),
                BuildPlan(10, 2, "2026-2030", Plan090303_2026_2030())
            };
        }

        //
        // 09.04.04 — EducationDirectionId = 4
        //

        private static List<PlanSeed> Base090404()
        {
            return new List<PlanSeed>
            {
                D("Б1.О.04", "Управление проектами в области искусственного интеллекта", 1, 2, 16, 32, 0, exam: 1),
                D("Б1.О.05", "Методы анализа данных в предиктивной аналитике", 1, 2, 16, 32, 0, pass: 1),

                D("Б1.О.08", "Методы интеллектуального анализа естественного языка", 2, 2, 16, 32, 0, exam: 1),
                D("Б1.О.09", "Методы глубокого обучения и трансформеры", 2, 2, 16, 64, 0, pass: 1),
                P("Б2.О.01(У)", "Ознакомительная практика", 2, 3, 108),

                D("Б1.В.01", "Технологии обработки и анализа больших данных в предиктивной аналитике", 3, 2, 16, 64, 0, exam: 1),
                D("Б1.В.03", "Методы искусственного интеллекта в предиктивной аналитике", 3, 2, 16, 32, 0, pass: 1),

                D("Б1.В.ДВ.01.01", "Проектирование интеллектуальных систем", 4, 2, 16, 64, 0, pass: 1),
                P("Б2.О.02(П)", "Научно-исследовательская работа", 4, 3, 108),
                P("Б2.В.01(П)", "Преддипломная практика", 4, 3, 108),
                P("Б3.02", "Выполнение и защита выпускной квалификационной работы", 4, 3, 108)
            };
        }

        private static List<PlanSeed> Plan090404_2022_2024()
        {
            return Copy(Base090404());
        }

        private static List<PlanSeed> Plan090404_2023_2025()
        {
            var plan = Copy(Base090404());

            Move(plan, "Б1.О.05", 2);
            Move(plan, "Б1.О.08", 1);

            SetHours(plan, "Б1.О.09", 16, 32, 0);
            SetHours(plan, "Б1.В.01", 16, 32, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090404_2024_2026()
        {
            var plan = Copy(Base090404());

            Move(plan, "Б1.В.03", 4);
            Remove(plan, "Б1.В.ДВ.01.01");
            Add(plan, D("Б1.В.02", "Анализ многомерных данных", 3, 2, 16, 32, 0, pass: 1));
            Add(plan, D("Б1.В.ДВ.02.01", "Интеллектуальные САПР", 4, 2, 16, 64, 0, pass: 1));

            return plan;
        }

        private static List<PlanSeed> Plan090404_2025_2027()
        {
            var plan = Copy(Base090404());

            Remove(plan, "Б1.В.ДВ.01.01");
            Add(plan, D("Б1.В.ДВ.02.02", "Математическое моделирование информационных систем", 4, 2, 16, 64, 0, pass: 1));

            SetHours(plan, "Б1.О.05", 16, 64, 0);
            SetHours(plan, "Б1.В.03", 16, 64, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090404_2026_2028()
        {
            var plan = Copy(Base090404());

            Move(plan, "Б1.В.01", 2);
            Move(plan, "Б1.О.09", 3);

            SetHours(plan, "Б1.В.01", 16, 64, 0);
            SetHours(plan, "Б1.О.08", 16, 32, 0);

            return plan;
        }

        private static List<AcademicPlanMockModel> Build090404Plans()
        {
            return new List<AcademicPlanMockModel>
            {
                BuildPlan(11, 4, "2022-2024", Plan090404_2022_2024()),
                BuildPlan(12, 4, "2023-2025", Plan090404_2023_2025()),
                BuildPlan(13, 4, "2024-2026", Plan090404_2024_2026()),
                BuildPlan(14, 4, "2025-2027", Plan090404_2025_2027()),
                BuildPlan(15, 4, "2026-2028", Plan090404_2026_2028())
            };
        }

        //
        // 09.04.03 — EducationDirectionId = 5
        //

        private static List<PlanSeed> Base090403()
        {
            return new List<PlanSeed>
            {
                D("Б1.О.05", "Методы анализа данных в бизнес-аналитике", 1, 2, 16, 32, 0, exam: 1),
                D("Б1.О.08", "Методы интеллектуального анализа естественного языка", 1, 2, 16, 32, 0, pass: 1),

                D("Б1.О.09", "Методы глубокого обучения в бизнес-аналитике", 2, 2, 16, 64, 0, exam: 1),
                P("Б2.О.01(У)", "Ознакомительная практика", 2, 3, 108),

                D("Б1.В.01", "Системы и технологии Web-аналитики", 3, 2, 16, 64, 0, pass: 1),
                D("Б1.В.03", "Методы искусственного интеллекта в бизнес-аналитике", 3, 2, 16, 32, 0, exam: 1),

                D("Б1.В.ДВ.01.01", "Интеллектуальные информационно-аналитические системы", 4, 2, 16, 64, 0, pass: 1),
                P("Б2.О.02(П)", "Научно-исследовательская работа", 4, 3, 108),
                P("Б2.В.01(П)", "Преддипломная практика", 4, 3, 108),
                P("Б3.02", "Выполнение и защита выпускной квалификационной работы", 4, 3, 108)
            };
        }

        private static List<PlanSeed> Plan090403_2022_2024()
        {
            return Copy(Base090403());
        }

        private static List<PlanSeed> Plan090403_2023_2025()
        {
            var plan = Copy(Base090403());

            Add(plan, D("Б1.О.04", "Управление проектами в области искусственного интеллекта", 1, 2, 16, 32, 0, pass: 1));
            Move(plan, "Б1.О.08", 2);

            return plan;
        }

        private static List<PlanSeed> Plan090403_2024_2026()
        {
            var plan = Copy(Base090403());

            Remove(plan, "Б1.В.ДВ.01.01");
            Add(plan, D("Б1.В.ДВ.01.02", "Обработка больших данных в бизнес-аналитике", 4, 2, 16, 64, 0, pass: 1));

            SetHours(plan, "Б1.О.09", 16, 32, 0);
            SetHours(plan, "Б1.В.01", 16, 32, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090403_2025_2027()
        {
            var plan = Copy(Base090403());

            Move(plan, "Б1.В.03", 4);
            Add(plan, D("Б1.В.ДВ.02.02", "Интеллектуальные информационные системы на основе хранилищ данных", 3, 2, 16, 64, 0, pass: 1));

            return plan;
        }

        private static List<PlanSeed> Plan090403_2026_2028()
        {
            var plan = Copy(Base090403());

            Move(plan, "Б1.О.05", 2);
            Remove(plan, "Б1.В.ДВ.01.01");
            Add(plan, D("Б1.В.ДВ.02.01", "Интеллектуальные информационные системы", 4, 2, 16, 64, 0, pass: 1));

            SetHours(plan, "Б1.О.05", 16, 64, 0);
            SetHours(plan, "Б1.В.03", 16, 64, 0);

            return plan;
        }

        private static List<AcademicPlanMockModel> Build090403Plans()
        {
            return new List<AcademicPlanMockModel>
            {
                BuildPlan(16, 5, "2022-2024", Plan090403_2022_2024()),
                BuildPlan(17, 5, "2023-2025", Plan090403_2023_2025()),
                BuildPlan(18, 5, "2024-2026", Plan090403_2024_2026()),
                BuildPlan(19, 5, "2025-2027", Plan090403_2025_2027()),
                BuildPlan(20, 5, "2026-2028", Plan090403_2026_2028())
            };
        }

        private static List<AcademicPlanMockModel> BuildAllAcademicPlans()
        {
            _recordId = 1;
            _disciplineId = 1;
            _disciplineIds.Clear();

            var result = new List<AcademicPlanMockModel>();
            result.AddRange(Build090304Plans());
            result.AddRange(Build090303Plans());
            result.AddRange(Build090404Plans());
            result.AddRange(Build090403Plans());

            return result;
        }

        private static readonly Dictionary<string, int> _disciplineIds =
    new(StringComparer.OrdinalIgnoreCase);

        private static string NormalizeDisciplineKey(string value)
        {
            return string.Join(
                " ",
                value.Trim()
                     .Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .ToLowerInvariant();
        }

        private static int GetOrCreateDisciplineId(string disciplineName)
        {
            var key = NormalizeDisciplineKey(disciplineName);

            if (_disciplineIds.TryGetValue(key, out var existingId))
            {
                return existingId;
            }

            var newId = _disciplineId++;
            _disciplineIds[key] = newId;
            return newId;
        }

        public static List<AcademicPlanMockModel> AcademicPlans => BuildAllAcademicPlans();
        private static DateTime? GetDemoMarkDate(StudentMockModel student, int studySemesterNumber)
        {
            if (!student.StudentGroupId.HasValue)
                return null;

            var group = StudentGroups.First(x => x.Id == student.StudentGroupId.Value);
            var enrollmentDate = GetEnrollmentDateByCourse((int)group.Course);

            var semesterStart = enrollmentDate.AddMonths((studySemesterNumber - 1) * 6);
            var markDate = semesterStart.AddMonths(4).AddDays((student.Id % 20) + 1);

            return DateTime.SpecifyKind(markDate, DateTimeKind.Utc);
        }

        public static List<DisciplineStudentRecordMockModel> DisciplineStudentRecords => GenerateDisciplineStudentRecords();

        private static List<DisciplineStudentRecordMockModel> GenerateDisciplineStudentRecords()
        {
            var result = new List<DisciplineStudentRecordMockModel>();
            var recordByDisciplineId = AcademicPlans
            .SelectMany(x => x.AcademicPlanRecords)
            .Where(x => x.DisciplineId.HasValue)
            .GroupBy(x => x.DisciplineId!.Value)
            .ToDictionary(
                g => g.Key,
                g => g.OrderBy(x => x.AcademicPlanId)
                      .ThenBy(x => x.Semester)
                      .First());

            var groupById = StudentGroups.ToDictionary(x => x.Id, x => x);

            int id = 1;

            foreach (var student in Students)
            {
                if (!student.StudentGroupId.HasValue || !groupById.TryGetValue(student.StudentGroupId.Value, out var group))
                    continue;

                var disciplineIds = group.Course switch
                {
                    AcademicCourse.Course_1 => Enumerable.Range(1, 3),
                    AcademicCourse.Course_2 => Enumerable.Range(1, 6),
                    AcademicCourse.Course_3 => Enumerable.Range(1, 9),
                    AcademicCourse.Course_4 => Enumerable.Range(1, 12),
                    _ => Enumerable.Empty<int>()
                };

                foreach (var disciplineId in disciplineIds)
                {
                    var planRecord = recordByDisciplineId[disciplineId];

                    var variant =
                        planRecord.Exam == 1 ? "Экзамен" :
                        planRecord.GradedPass == 1 ? "Дифф. зачет" :
                        planRecord.Pass == 1 ? "Зачет" :
                        "Аттестация";

                    var semester = (Semesters)planRecord.Semester;

                    var mark = GetDemoMark(student, disciplineId, semester);
                    var markDate = GetDemoMarkDate(student, planRecord.Semester);

                    result.Add(new DisciplineStudentRecordMockModel
                    {
                        Id = id++,
                        DisciplineId = disciplineId,
                        StudentId = student.Id,
                        Semester = semester,
                        Variant = variant,
                        SubGroup = ((student.Id - 1) % 2) + 1,
                        MarkType = mark,
                        MarkDate = markDate
                    });
                }
            }

            return result;
        }

        private static readonly Lazy<HashSet<int>> HighRiskDebtStudentIds = new(() =>
    Students
        .Where(student =>
        {
            var group = StudentGroups.First(x => x.Id == student.StudentGroupId);
            return group.Course == AcademicCourse.Course_4;
        })
        .Take(1)
        .Select(x => x.Id)
        .ToHashSet());

        private static readonly Lazy<HashSet<int>> RegularDebtStudentIds = new(() =>
            Students
                .Where(student =>
                {
                    var group = StudentGroups.First(x => x.Id == student.StudentGroupId);
                    return group.Course == AcademicCourse.Course_3;
                })
                .Take(1)
                .Select(x => x.Id)
                .ToHashSet());

        private static MarkType GetDemoMark(StudentMockModel student, int disciplineId, Semesters semester)
        {
            if (student.StudentState == StudentState.Академ && disciplineId >= 5)
                return MarkType.Неявка;

            if (HighRiskDebtStudentIds.Value.Contains(student.Id) && semester == Semesters.Пятый)
                return MarkType.Неудовлетворительно;

            if (RegularDebtStudentIds.Value.Contains(student.Id) && semester == Semesters.Четвертый)
                return MarkType.Неудовлетворительно;

            var marks = new[]
            {
        MarkType.Отлично,
        MarkType.Хорошо,
        MarkType.Удовлетворительно,
        MarkType.Хорошо,
        MarkType.Отлично
    };

            return marks[(student.Id + disciplineId) % marks.Length];
        }

        private sealed class StudentOrderEvent
        {
            public int StudentId { get; init; }
            public StudentOrderType Type { get; init; }
            public DateTime Date { get; init; }
            public int EducationDirectionId { get; init; }
            public int? GroupFromId { get; init; }
            public int? GroupToId { get; init; }
            public string DocumentKey { get; init; } = string.Empty;
        }

        private static DateTime GetEnrollmentDateByCourse(int course)
        {
            return course switch
            {
                1 => new DateTime(2025, 9, 1),
                2 => new DateTime(2024, 9, 1),
                3 => new DateTime(2023, 9, 1),
                4 => new DateTime(2022, 9, 1),
                _ => new DateTime(2025, 9, 1)
            };
        }

        private static int FindTransferTargetGroupId(StudentGroupMockModel currentGroup)
        {
            var candidate = StudentGroups
                .Where(x =>
                    x.Id != currentGroup.Id &&
                    x.Course == currentGroup.Course &&
                    x.EducationDirectionId == currentGroup.EducationDirectionId)
                .OrderBy(x => x.Id)
                .FirstOrDefault();

            return candidate?.Id ?? currentGroup.Id;
        }

        private static List<StudentOrderEvent> BuildStudentTimeline(
            StudentMockModel student,
            StudentGroupMockModel currentGroup)
        {
            var enrollmentDate = GetEnrollmentDateByCourse((int)currentGroup.Course);

            var events = new List<StudentOrderEvent>
    {
        new StudentOrderEvent
        {
            StudentId = student.Id,
            Type = StudentOrderType.Зачисление,
            Date = enrollmentDate,
            EducationDirectionId = currentGroup.EducationDirectionId,
            GroupToId = currentGroup.Id,
            DocumentKey = $"ENROLL-{currentGroup.Id}-{enrollmentDate:yyyyMMdd}"
        }
    };

            if (student.Id % 9 == 0)
            {
                var targetGroupId = FindTransferTargetGroupId(currentGroup);

                if (targetGroupId != currentGroup.Id)
                {
                    var transferDate = enrollmentDate.AddYears(1).AddMonths(5);

                    events.Add(new StudentOrderEvent
                    {
                        StudentId = student.Id,
                        Type = StudentOrderType.ПереводВГруппу,
                        Date = transferDate,
                        EducationDirectionId = currentGroup.EducationDirectionId,
                        GroupFromId = currentGroup.Id,
                        GroupToId = targetGroupId,
                        DocumentKey = $"MOVE-{transferDate:yyyyMMdd}"
                    });
                }
            }

            if (student.StudentState == StudentState.Академ)
            {
                var academicDate = enrollmentDate.AddYears(1).AddMonths(5);

                events.Add(new StudentOrderEvent
                {
                    StudentId = student.Id,
                    Type = StudentOrderType.ВАкадем,
                    Date = academicDate,
                    EducationDirectionId = currentGroup.EducationDirectionId,
                    GroupFromId = currentGroup.Id,
                    GroupToId = null,
                    DocumentKey = $"MOVE-{academicDate:yyyyMMdd}"
                });
            }

            if (student.StudentState == StudentState.Отчислен)
            {
                var dismissalDate = enrollmentDate.AddYears(2).AddMonths(6);

                events.Add(new StudentOrderEvent
                {
                    StudentId = student.Id,
                    Type = StudentOrderType.ОтчислитьЗаНеуспевамость,
                    Date = dismissalDate,
                    EducationDirectionId = currentGroup.EducationDirectionId,
                    GroupFromId = currentGroup.Id,
                    GroupToId = null,
                    DocumentKey = $"MOVE-{dismissalDate:yyyyMMdd}"
                });
            }

            return events
                .OrderBy(x => x.Date)
                .ToList();
        }

        private static StudentOrderType ResolveDocumentType(List<StudentOrderEvent> events)
        {
            var types = events
                .Select(x => x.Type)
                .Distinct()
                .ToList();

            return types.Count == 1
                ? types[0]
                : StudentOrderType.Движение;
        }

        private static string BuildOrderNumber(
    StudentOrderType type,
    DateTime date,
    Dictionary<int, int> yearCounters)
        {
            if (!yearCounters.ContainsKey(date.Year))
            {
                yearCounters[date.Year] = 0;
            }

            yearCounters[date.Year]++;

            var suffix = type switch
            {
                StudentOrderType.Зачисление => "к",
                StudentOrderType.ПереводВГруппу => "п",
                StudentOrderType.ВАкадем => "а",
                StudentOrderType.ИзАкадема => "лс",
                StudentOrderType.Восстановить => "в",
                StudentOrderType.ОтчислитьЗаНеуспевамость => "лс",
                StudentOrderType.ОтчислитьПоСобственному => "лс",
                StudentOrderType.Движение => "комб",
                _ => "лс"
            };

            return $"{yearCounters[date.Year]:000}-{suffix}";
        }

        private static (
    List<StudentOrderMockModel> Orders,
    List<StudentOrderBlockMockModel> Blocks,
    List<StudentOrderBlockStudentMockModel> BlockStudents)
GenerateStudentOrdersData()
        {
            var allEvents = new List<StudentOrderEvent>();

            foreach (var student in Students.OrderBy(x => x.Id))
            {
                var group = StudentGroups.First(x => x.Id == student.StudentGroupId);
                allEvents.AddRange(BuildStudentTimeline(student, group));
            }

            var orders = new List<StudentOrderMockModel>();
            var blocks = new List<StudentOrderBlockMockModel>();
            var blockStudents = new List<StudentOrderBlockStudentMockModel>();

            var orderId = 1;
            var blockId = 1;
            var blockStudentId = 1;
            var yearCounters = new Dictionary<int, int>();

            var documentGroups = allEvents
                .GroupBy(x => x.DocumentKey)
                .OrderBy(x => x.Min(e => e.Date));

            foreach (var documentGroup in documentGroups)
            {
                var eventList = documentGroup
                    .OrderBy(x => x.Date)
                    .ThenBy(x => x.StudentId)
                    .ToList();

                var orderDate = eventList.Min(x => x.Date);
                var documentType = ResolveDocumentType(eventList);

                var order = new StudentOrderMockModel
                {
                    Id = orderId++,
                    OrderNumber = BuildOrderNumber(documentType, orderDate, yearCounters),
                    StudentOrderType = documentType,
                    OrderDate = orderDate,
                    Blocks = new List<StudentOrderBlockMockModel>()
                };

                var blockGroups = eventList
                    .GroupBy(x => new
                    {
                        x.Type,
                        x.EducationDirectionId
                    })
                    .OrderBy(x => x.Key.Type)
                    .ThenBy(x => x.Key.EducationDirectionId);

                foreach (var blockGroup in blockGroups)
                {
                    var block = new StudentOrderBlockMockModel
                    {
                        Id = blockId,
                        StudentOrderId = order.Id,
                        EducationDirectionId = blockGroup.Key.EducationDirectionId,
                        StudentOrderType = blockGroup.Key.Type,
                        Students = new List<StudentOrderBlockStudentMockModel>()
                    };

                    foreach (var evt in blockGroup.OrderBy(x => x.StudentId))
                    {
                        var blockStudent = new StudentOrderBlockStudentMockModel
                        {
                            Id = blockStudentId++,
                            StudentOrderBlockId = blockId,
                            StudentId = evt.StudentId,
                            StudentGroupFromId = evt.GroupFromId,
                            StudentGroupToId = evt.GroupToId
                        };

                        block.Students.Add(blockStudent);
                        blockStudents.Add(blockStudent);
                    }

                    order.Blocks.Add(block);
                    blocks.Add(block);
                    blockId++;
                }

                orders.Add(order);
            }

            return (orders, blocks, blockStudents);
        }

        private static readonly Lazy<(
    List<StudentOrderMockModel> Orders,
    List<StudentOrderBlockMockModel> Blocks,
    List<StudentOrderBlockStudentMockModel> BlockStudents)> GeneratedOrders
    = new(GenerateStudentOrdersData);

        public static List<StudentOrderMockModel> StudentOrders => GeneratedOrders.Value.Orders;
        public static List<StudentOrderBlockMockModel> StudentOrderBlocks => GeneratedOrders.Value.Blocks;
        public static List<StudentOrderBlockStudentMockModel> StudentOrderBlockStudents => GeneratedOrders.Value.BlockStudents;
    }
}