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
                        StudentState = ResolveGeneratedStudentState(group, i),
                        Description = string.Empty,
                        IsSteward = i == 0
                    });

                    studentId++;
                }
            }

            return students;
        }

        private static string BuildDisciplineShortName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Дисциплина";
            }

            var normalized = name.Trim();

            var predefined = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["Введение в программную инженерию"] = "Введение в ПИ",
                ["Информационные системы и технологии"] = "ИСиТ",
                ["Теоретические основы информатики"] = "Теор. основы информатики",
                ["Программирование на Java"] = "Java",
                ["Алгоритмы и структуры данных"] = "Алгоритмы и СД",
                ["Организация ЭВМ и системы"] = "Организация ЭВМ",
                ["Организация вычислительных машин и систем"] = "ОВМиС",
                ["Базы данных"] = "БД",
                ["Системы управления базами данных"] = "СУБД",
                ["Технологии программирования"] = "Технологии программ.",
                ["Основы технологии программирования"] = "Осн. техн. программ.",
                ["Интернет-программирование"] = "Интернет-прогр.",
                ["Операционные системы"] = "ОС",
                ["Методы моделирования"] = "Методы моделир.",
                ["Методы искусственного интеллекта"] = "Методы ИИ",
                ["Системы искусственного интеллекта"] = "Системы ИИ",
                ["Системный анализ"] = "Сист. анализ",
                ["Вычислительная математика"] = "Выч. математика",
                ["Проектирование и архитектура программных систем"] = "Архитектура ПС",
                ["Тестирование программного обеспечения"] = "Тестирование ПО",
                ["Конструирование программного обеспечения"] = "Конструирование ПО",
                ["Информационная безопасность"] = "Инф. безопасность",
                ["Управление проектом"] = "Упр. проектом",
                ["Проектный практикум"] = "Проектный практикум",
                ["Ознакомительная практика"] = "Ознакомит. практика",
                ["Научно-исследовательская работа"] = "НИР",
                ["Технологическая (проектно-технологическая) практика"] = "Техн. практика",
                ["Преддипломная практика"] = "Преддипл. практика",
                ["Подготовка к сдаче и сдача государственного экзамена"] = "Госэкзамен",
                ["Выполнение и защита выпускной квалификационной работы"] = "ВКР",

                ["Программирование"] = "Программирование",
                ["Основы алгоритмизации и программирования"] = "Алгоритмизация",
                ["Основы информационных технологий"] = "Основы ИТ",
                ["Основы прикладной информатики"] = "Осн. прикл. информатики",
                ["Разработка профессиональных приложений"] = "Проф. приложения",
                ["Основы компьютерной графики"] = "Комп. графика",
                ["Экспертные системы"] = "Экспертные системы",
                ["Право интеллектуальной собственности"] = "Интелл. собственность",
                ["Исследование операций и методы оптимизации"] = "Исследование операций",
                ["Прикладные программные решения"] = "Прикладные решения",

                ["Управление проектами в области искусственного интеллекта"] = "Упр. проектами ИИ",
                ["Методы анализа данных в предиктивной аналитике"] = "Анализ данных",
                ["Методы интеллектуального анализа естественного языка"] = "NLP",
                ["Методы глубокого обучения и трансформеры"] = "Глубокое обучение",
                ["Технологии обработки и анализа больших данных в предиктивной аналитике"] = "Big Data",
                ["Методы искусственного интеллекта в предиктивной аналитике"] = "ИИ в аналитике",
                ["Проектирование интеллектуальных систем"] = "Интеллектуальные системы",

                ["Методы анализа данных в бизнес-аналитике"] = "Анализ данных",
                ["Методы глубокого обучения в бизнес-аналитике"] = "Глубокое обучение",
                ["Системы и технологии Web-аналитики"] = "Web-аналитика",
                ["Методы искусственного интеллекта в бизнес-аналитике"] = "ИИ в бизнес-аналитике",
                ["Интеллектуальные информационно-аналитические системы"] = "ИИАС"
            };

            if (predefined.TryGetValue(normalized, out var shortName))
            {
                return shortName;
            }

            if (normalized.Length <= 48)
            {
                return normalized;
            }

            return normalized.Substring(0, 45).TrimEnd() + "...";
        }

        private static string BuildDisciplineDescription(string name, string index, int semester)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Учебная дисциплина кафедрального портала.";
            }

            var normalized = name.Trim();

            if (index.StartsWith("Б3"))
            {
                return $"Элемент государственной итоговой аттестации «{normalized}», изучается в {semester} семестре.";
            }

            if (index.StartsWith("Б2"))
            {
                return $"Практика «{normalized}», предусмотренная учебным планом и проводимая в {semester} семестре.";
            }

            return $"Учебная дисциплина «{normalized}», предусмотренная учебным планом и изучаемая в {semester} семестре.";
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
                    DisciplineId = _disciplineId++,

                    DisciplineBlockId = block.blockId,
                    DisciplineBlockTitle = block.blockTitle,
                    DisciplineBlockBlueAsteriskName = block.blueAsteriskName,
                    DisciplineBlockUseForGrouping = block.useForGrouping,
                    DisciplineBlockOrder = block.blockOrder,

                    DisciplineShortName = BuildDisciplineShortName(seed.Name),
                    DisciplineDescription = BuildDisciplineDescription(seed.Name, seed.Index, seed.Semester),

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
                D("Б1.В.07", "Программирование на Java", 1, 2, 16, 32, 0, exam: 1),

                P("Б2.О.01(У)", "Ознакомительная практика", 2, 3, 108),
                D("Б1.О.33", "Алгоритмы и структуры данных", 2, 2, 16, 32, 0, exam: 1),
                D("Б1.О.12", "Дискретная математика", 2, 2, 16, 32, 0, exam: 1),

                D("Б1.О.14", "Организация ЭВМ и системы", 3, 2, 16, 32, 0, exam: 1),
                D("Б1.О.15", "Базы данных", 3, 2, 16, 32, 0, exam: 1, courseProject: 1),
                D("Б1.О.21", "Технологии программирования", 3, 3, 16, 64, 0, exam: 1, courseWork: 1),
                D("Б1.В.05", "Интернет-программирование", 3, 2, 16, 32, 0, exam: 1),

                D("Б1.О.16", "Системы управления базами данных", 4, 2, 16, 32, 0, pass: 1),
                D("Б1.О.21", "Технологии программирования", 4, 3, 16, 64, 0, exam: 1),
                D("Б1.В.05", "Интернет-программирование", 4, 2, 16, 32, 0, exam: 1),
                D("Б1.В.05", "Операционные системы", 4, 2, 16, 32, 0, pass: 1),

                D("Б1.О.20", "Методы моделирования", 5, 2, 16, 0, 16, pass: 1),
                D("Б1.В.03", "Методы искусственного интеллекта", 5, 2, 16, 32, 0, pass: 1),
                D("Б1.В.19", "Системный анализ", 5, 2, 16, 32, 0, exam: 1),
                D("Б1.В.25", "Вычислительная математика", 5, 2, 16, 32, 0, exam: 1),

                D("Б1.В.01", "Проектирование и архитектура программных систем", 6, 2, 16, 32, 0, pass: 1, courseProject: 1),
                D("Б1.В.03", "Методы искусственного интеллекта", 6, 2, 16, 32, 0, pass: 1),
                D("Б1.В.12", "Тестирование программного обеспечения", 6, 2, 16, 32, 0, exam: 1),
                D("Б1.В.25", "Вычислительная математика", 5, 2, 16, 32, 0, exam: 1),

                D("Б1.В.13", "Конструирование программного обеспечения", 7, 2, 16, 32, 0, exam: 1, courseWork: 1),
                D("Б1.В.35", "Системы искусственного интеллекта", 7, 2, 16, 32, 0, exam: 1, courseProject: 1),
                D("Б1.В.17", "Информационная безопасность", 7, 2, 16, 32, 0, exam: 1),
                P("Б2.О.02(П)", "Научно-исследовательская работа", 7, 3, 108),

                P("Б1.О.24", "Проектный практикум", 8, 3, 108),
                D("Б1.О.18", "Управление проектом", 8, 2, 8, 16, 0, pass: 1, rgr: 1),
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
            return Copy(Base090304());
        }

        private static List<PlanSeed> Plan090304_2024_2028()
        {
            return Copy(Base090304());
        }

        private static List<PlanSeed> Plan090304_2025_2029()
        {
            return Copy(Base090304());
        }

        private static List<PlanSeed> Plan090304_2026_2030()
        {
            return Copy(Base090304());
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
                D("Б1.О.23", "Теоретические основы информатики", 1, 2, 16, 0, 16, pass: 1, rgr: 1),

                D("Б1.О.16", "Основы прикладной информатики", 2, 2, 16, 0, 16, pass: 1, rgr: 1),
                D("Б1.О.33", "Алгоритмы и структуры данных", 2, 2, 16, 32, 0, exam: 1),
                P("Б2.О.01(У)", "Ознакомительная практика", 2, 3, 108),

                D("Б1.О.15", "Базы данных", 3, 2, 16, 32, 0, exam: 1, courseProject: 1),
                D("Б1.О.17", "Организация вычислительных машин и систем", 2, 2, 16, 32, 0, exam: 1),
                D("Б1.О.27", "Основы технологии программирования", 2, 2, 16, 32, 0, exam: 1),
                D("Б1.О.23", "Операционные системы", 3, 2, 16, 32, 0, exam: 1),

                D("Б1.О.22", "Методы моделирования", 4, 2, 16, 0, 16, pass: 1),
                D("Б1.О.16", "Системы управления базами данных", 4, 2, 16, 32, 0, pass: 1),
                D("Б1.О.23", "Разработка профессиональных приложений", 4, 2, 16, 32, 0, exam: 1, courseWork: 1),
                D("Б1.О.29", "Основы компьютерной графики", 4, 2, 16, 32, 0, pass: 1),

                D("Б1.В.03", "Методы искусственного интеллекта", 5, 2, 16, 32, 0, pass: 1),
                D("Б1.В.19", "Системный анализ", 5, 2, 16, 32, 0, exam: 1),
                D("Б1.В.01", "Проектирование и архитектура программных систем", 6, 2, 16, 32, 0, pass: 1, courseProject: 1),

                D("Б1.В.07", "Экспертные системы", 6, 2, 16, 32, 0, exam: 1),
                D("Б1.В.08", "Право интеллектуальной собственности", 6, 2, 16, 32, 0, pass: 1),
                D("Б1.В.24", "Исследование операций и методы оптимизации", 6, 2, 16, 32, 0, pass: 1),

                D("Б1.В.35", "Системы искусственного интеллекта", 7, 2, 16, 32, 0, exam: 1, courseProject: 1),
                D("Б1.В.17", "Информационная безопасность", 7, 2, 16, 32, 0, exam: 1),
                D("Б1.В.36", "Прикладные программные решения", 7, 2, 16, 32, 0, exam: 1),
                P("Б2.О.02(П)", "Научно-исследовательская работа", 7, 3, 108),

                P("Б1.О.24", "Проектный практикум", 8, 3, 108),
                D("Б1.О.18", "Управление проектом", 8, 2, 8, 16, 0, pass: 1, rgr: 1),
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
            return Copy(Base090303());
        }

        private static List<PlanSeed> Plan090303_2024_2028()
        {
            return Copy(Base090303());
        }

        private static List<PlanSeed> Plan090303_2025_2029()
        {
            return Copy(Base090303());
        }

        private static List<PlanSeed> Plan090303_2026_2030()
        {
            return Copy(Base090303());
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
            return Copy(Base090404());
        }

        private static List<PlanSeed> Plan090404_2024_2026()
        {
            return Copy(Base090404());
        }

        private static List<PlanSeed> Plan090404_2025_2027()
        {
            return Copy(Base090404());
        }

        private static List<PlanSeed> Plan090404_2026_2028()
        {
            return Copy(Base090404());
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
            return Copy(Base090403());
        }

        private static List<PlanSeed> Plan090403_2024_2026()
        {
            return Copy(Base090403());
        }

        private static List<PlanSeed> Plan090403_2025_2027()
        {
            return Copy(Base090403());
        }

        private static List<PlanSeed> Plan090403_2026_2028()
        {
            return Copy(Base090403());
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

            var result = new List<AcademicPlanMockModel>();
            result.AddRange(Build090304Plans());
            result.AddRange(Build090303Plans());
            result.AddRange(Build090404Plans());
            result.AddRange(Build090403Plans());

            return result;
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

            int id = 1;

            foreach (var group in StudentGroups.OrderBy(x => x.Id))
            {
                var groupStudents = Students
                    .Where(x => x.StudentGroupId == group.Id)
                    .OrderBy(x => x.Id)
                    .ToList();

                if (groupStudents.Count == 0)
                {
                    continue;
                }

                var planDirectionId = GetPlanDirectionIdForGroup(group);

                var academicPlan = AcademicPlans
                    .Where(x => x.EducationDirectionId == planDirectionId)
                    .OrderBy(x => x.Id)
                    .FirstOrDefault();

                if (academicPlan == null || academicPlan.AcademicPlanRecords == null)
                {
                    continue;
                }

                var maxSemester = GetMaxSemester(group.Course);

                var planRecords = academicPlan.AcademicPlanRecords
                    .Where(x => x.DisciplineId.HasValue)
                    .Where(x => x.Semester >= 1 && x.Semester <= maxSemester)
                    .OrderBy(x => x.Semester)
                    .ThenBy(x => x.Index)
                    .ToList();

                foreach (var student in groupStudents)
                {
                    var debtPlanRecordIds = GetRegularDebtPlanRecordIds(
                        student,
                        groupStudents,
                        planRecords,
                        group.Course);

                    foreach (var planRecord in planRecords)
                    {
                        var variant =
                            planRecord.Exam == 1 ? "Экзамен" :
                            planRecord.GradedPass == 1 ? "Дифф. зачет" :
                            planRecord.Pass == 1 ? "Зачет" :
                            "Аттестация";

                        var isRegularDebt = debtPlanRecordIds.Contains(planRecord.Id);

                        var mark = isRegularDebt
                            ? MarkType.Неудовлетворительно
                            : GetRegularDemoMark(student, planRecord);

                        result.Add(new DisciplineStudentRecordMockModel
                        {
                            Id = id++,
                            DisciplineId = planRecord.DisciplineId!.Value,
                            StudentId = student.Id,
                            Semester = (Semesters)planRecord.Semester,
                            Variant = variant,
                            SubGroup = ((student.Id - 1) % 2) + 1,
                            MarkType = mark,
                            MarkDate = GetDemoMarkDate(
                                group.Course,
                                planRecord.Semester,
                                student.Id,
                                planRecord.DisciplineId!.Value)
                        });
                    }
                }
            }

            return result;
        }

        private static (int FromSemester, int ToSemester) GetSemesterRange(AcademicCourse course)
        {
            return course switch
            {
                AcademicCourse.Course_1 => (1, 2),
                AcademicCourse.Course_2 => (3, 4),
                AcademicCourse.Course_3 => (5, 6),
                AcademicCourse.Course_4 => (7, 8),
                _ => (1, 8)
            };
        }

        private static int GetMaxSemester(AcademicCourse course) => course switch
        {
            AcademicCourse.Course_1 => 2,
            AcademicCourse.Course_2 => 4,
            AcademicCourse.Course_3 => 6,
            AcademicCourse.Course_4 => 8,
            _ => 0
        };

        private static (int FromSemester, int ToSemester) GetCurrentCourseSemesterRange(AcademicCourse course) => course switch
        {
            AcademicCourse.Course_1 => (1, 2),
            AcademicCourse.Course_2 => (3, 4),
            AcademicCourse.Course_3 => (5, 6),
            AcademicCourse.Course_4 => (7, 8),
            _ => (1, 2)
        };

        private const int DirectionId090403 = 5; // сюда поставьте реальный Id направления 09.04.03

        private static int GetPlanDirectionIdForGroup(StudentGroupMockModel group)
        {
            if (!string.IsNullOrWhiteSpace(group.GroupName) &&
                group.GroupName.StartsWith("БИмд", StringComparison.OrdinalIgnoreCase))
            {
                return DirectionId090403;
            }

            return group.EducationDirectionId;
        }

        private static int GetRegularDebtCountForStudent(
            StudentMockModel student,
            List<StudentMockModel> groupStudents)
        {
            var ordered = groupStudents
                .OrderBy(x => x.Id)
                .ToList();

            var index = ordered.FindIndex(x => x.Id == student.Id);

            return index switch
            {
                1 => 2,   // второй студент в группе
                7 => 3,   // восьмой студент
                12 => 2,  // тринадцатый студент
                _ => 0
            };
        }

        private static HashSet<int> GetRegularDebtPlanRecordIds(
            StudentMockModel student,
            List<StudentMockModel> groupStudents,
            List<AcademicPlanRecordMockModel> planRecords,
            AcademicCourse course)
        {
            var debtCount = GetRegularDebtCountForStudent(student, groupStudents);

            if (debtCount == 0)
            {
                return new HashSet<int>();
            }

            var (fromSemester, toSemester) = GetCurrentCourseSemesterRange(course);

            return planRecords
                .Where(x => x.DisciplineId.HasValue)
                .Where(x => x.Semester >= fromSemester && x.Semester <= toSemester)
                .OrderByDescending(x => x.Semester)
                .ThenBy(x => x.Index)
                .Take(debtCount)
                .Select(x => x.Id)
                .ToHashSet();
        }

        private static MarkType GetRegularDemoMark(
            StudentMockModel student,
            AcademicPlanRecordMockModel planRecord)
        {
            // немного неявок оставляем
            if (student.Id % 41 == 0 && planRecord.Semester % 2 == 0)
            {
                return MarkType.Неявка;
            }

            var disciplineId = planRecord.DisciplineId ?? 0;

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

        private static int GetStudyStartYear(AcademicCourse course)
        {
            var today = DateTime.Today;
            var currentAcademicYearStart = today.Month >= 9 ? today.Year : today.Year - 1;

            return course switch
            {
                AcademicCourse.Course_1 => currentAcademicYearStart,
                AcademicCourse.Course_2 => currentAcademicYearStart - 1,
                AcademicCourse.Course_3 => currentAcademicYearStart - 2,
                AcademicCourse.Course_4 => currentAcademicYearStart - 3,
                _ => currentAcademicYearStart
            };
        }

        private static DateTime GetDemoMarkDate(
            AcademicCourse currentCourse,
            int semester,
            int studentId,
            int disciplineId)
        {
            var studyStartYear = GetStudyStartYear(currentCourse);

            // 1-2 семестр = 1 год обучения, 3-4 = 2 год и т.д.
            var studyYearIndex = (semester - 1) / 2;
            var academicYearStart = studyStartYear + studyYearIndex;

            DateTime rangeStart;
            DateTime rangeEnd;

            if (semester == 1 || semester == 3 || semester == 5 || semester == 7)
            {
                // 29 декабря — 21 января
                rangeStart = new DateTime(academicYearStart, 12, 29, 12, 0, 0, DateTimeKind.Utc);
                rangeEnd = new DateTime(academicYearStart + 1, 1, 21, 12, 0, 0, DateTimeKind.Utc);
            }
            else
            {
                // 1 июня — 21 июня
                rangeStart = new DateTime(academicYearStart + 1, 6, 1, 12, 0, 0, DateTimeKind.Utc);
                rangeEnd = new DateTime(academicYearStart + 1, 6, 21, 12, 0, 0, DateTimeKind.Utc);
            }

            var days = (rangeEnd - rangeStart).Days;
            var offset = Math.Abs(HashCode.Combine(studentId, disciplineId, semester)) % (days + 1);

            return rangeStart.AddDays(offset);
        }

        private static MarkType GetDemoMark(StudentMockModel student, AcademicPlanRecordMockModel planRecord)
        {
            var semester = planRecord.Semester;
            var disciplineId = planRecord.DisciplineId ?? 0;

            // немного старых долгов
            if (student.Id % 29 == 0 && semester <= 2)
                return MarkType.Неудовлетворительно;

            // немного текущих долгов
            if (student.Id % 37 == 0 && semester >= 3 && semester <= 4)
                return MarkType.Неудовлетворительно;

            // немного неявок
            if (student.Id % 41 == 0 && semester == 2)
                return MarkType.Неявка;

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
        private enum StudentOrderScenario
        {
            None,
            TransferGroup,
            AcademicLeaveOnly,
            AcademicLeaveAndReturn,
            DismissedForBadPerformance,
            DismissedByOwnWish,
            DismissedAndRestored,
            Completed
        }

        private static StudentOrderScenario GetOrderScenario(StudentGroupMockModel group, int studentIndexInGroup)
        {
            return (group.Id, studentIndexInGroup) switch
            {
                (6, 3) => StudentOrderScenario.TransferGroup,              // ПИбд-22
                (9, 4) => StudentOrderScenario.TransferGroup,              // ПИбд-31

                (17, 1) => StudentOrderScenario.AcademicLeaveOnly,          // ИСЭбд-21
                (19, 2) => StudentOrderScenario.AcademicLeaveAndReturn,     // ИСЭбд-31

                (10, 2) => StudentOrderScenario.DismissedForBadPerformance, // ПИбд-32
                (18, 0) => StudentOrderScenario.DismissedByOwnWish,         // ИСЭбд-22

                (13, 1) => StudentOrderScenario.DismissedAndRestored,       // ПИбд-42
                (20, 1) => StudentOrderScenario.Completed,                  // ИСЭбд-41

                _ => StudentOrderScenario.None
            };
        }

        private static int GetStudentIndexInGroup(StudentMockModel student)
        {
            return Students
                .Where(x => x.StudentGroupId == student.StudentGroupId)
                .OrderBy(x => x.Id)
                .Select((x, index) => new { x.Id, Index = index })
                .First(x => x.Id == student.Id)
                .Index;
        }

        private static StudentState ResolveGeneratedStudentState(StudentGroupMockModel group, int studentIndexInGroup)
        {
            var scenario = GetOrderScenario(group, studentIndexInGroup);

            return scenario switch
            {
                StudentOrderScenario.AcademicLeaveOnly => StudentState.Академ,
                StudentOrderScenario.DismissedForBadPerformance => StudentState.Отчислен,
                StudentOrderScenario.DismissedByOwnWish => StudentState.Отчислен,
                StudentOrderScenario.Completed => StudentState.Завершил,
                _ => StudentState.Учится
            };
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
            var studentIndexInGroup = GetStudentIndexInGroup(student);
            var scenario = GetOrderScenario(currentGroup, studentIndexInGroup);

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

            switch (scenario)
            {
                case StudentOrderScenario.TransferGroup:
                    {
                        var targetGroupId = FindTransferTargetGroupId(currentGroup);
                        if (targetGroupId != currentGroup.Id)
                        {
                            var transferDate = enrollmentDate.AddMonths(17).AddDays(student.Id % 3);

                            events.Add(new StudentOrderEvent
                            {
                                StudentId = student.Id,
                                Type = StudentOrderType.ПереводВГруппу,
                                Date = transferDate,
                                EducationDirectionId = currentGroup.EducationDirectionId,
                                GroupFromId = currentGroup.Id,
                                GroupToId = targetGroupId,
                                DocumentKey = $"TRANSFER-GROUP-{transferDate:yyyyMMdd}"
                            });
                        }

                        break;
                    }

                case StudentOrderScenario.AcademicLeaveOnly:
                    {
                        var academicDate = enrollmentDate.AddMonths(17).AddDays(student.Id % 3);

                        events.Add(new StudentOrderEvent
                        {
                            StudentId = student.Id,
                            Type = StudentOrderType.ВАкадем,
                            Date = academicDate,
                            EducationDirectionId = currentGroup.EducationDirectionId,
                            GroupFromId = currentGroup.Id,
                            GroupToId = null,
                            DocumentKey = $"ACADEMIC-LEAVE-{academicDate:yyyyMMdd}"
                        });

                        break;
                    }

                case StudentOrderScenario.AcademicLeaveAndReturn:
                    {
                        var academicDate = enrollmentDate.AddMonths(17).AddDays(student.Id % 3);
                        var returnDate = academicDate.AddMonths(10).AddDays(2);

                        events.Add(new StudentOrderEvent
                        {
                            StudentId = student.Id,
                            Type = StudentOrderType.ВАкадем,
                            Date = academicDate,
                            EducationDirectionId = currentGroup.EducationDirectionId,
                            GroupFromId = currentGroup.Id,
                            GroupToId = null,
                            DocumentKey = $"ACADEMIC-LEAVE-{academicDate:yyyyMMdd}"
                        });

                        events.Add(new StudentOrderEvent
                        {
                            StudentId = student.Id,
                            Type = StudentOrderType.ИзАкадема,
                            Date = returnDate,
                            EducationDirectionId = currentGroup.EducationDirectionId,
                            GroupFromId = null,
                            GroupToId = currentGroup.Id,
                            DocumentKey = $"ACADEMIC-RETURN-{returnDate:yyyyMMdd}"
                        });

                        break;
                    }

                case StudentOrderScenario.DismissedForBadPerformance:
                    {
                        var dismissalDate = enrollmentDate.AddMonths(28).AddDays(student.Id % 4);

                        events.Add(new StudentOrderEvent
                        {
                            StudentId = student.Id,
                            Type = StudentOrderType.ОтчислитьЗаНеуспевамость,
                            Date = dismissalDate,
                            EducationDirectionId = currentGroup.EducationDirectionId,
                            GroupFromId = currentGroup.Id,
                            GroupToId = null,
                            DocumentKey = $"DISMISS-BAD-{dismissalDate:yyyyMMdd}"
                        });

                        break;
                    }

                case StudentOrderScenario.DismissedByOwnWish:
                    {
                        var dismissalDate = enrollmentDate.AddMonths(20).AddDays(student.Id % 4);

                        events.Add(new StudentOrderEvent
                        {
                            StudentId = student.Id,
                            Type = StudentOrderType.ОтчислитьПоСобственному,
                            Date = dismissalDate,
                            EducationDirectionId = currentGroup.EducationDirectionId,
                            GroupFromId = currentGroup.Id,
                            GroupToId = null,
                            DocumentKey = $"DISMISS-OWN-{dismissalDate:yyyyMMdd}"
                        });

                        break;
                    }

                case StudentOrderScenario.DismissedAndRestored:
                    {
                        var dismissalDate = enrollmentDate.AddMonths(19).AddDays(student.Id % 4);
                        var restoreDate = dismissalDate.AddMonths(7).AddDays(3);

                        events.Add(new StudentOrderEvent
                        {
                            StudentId = student.Id,
                            Type = StudentOrderType.ОтчислитьЗаНеуспевамость,
                            Date = dismissalDate,
                            EducationDirectionId = currentGroup.EducationDirectionId,
                            GroupFromId = currentGroup.Id,
                            GroupToId = null,
                            DocumentKey = $"DISMISS-BAD-{dismissalDate:yyyyMMdd}"
                        });

                        events.Add(new StudentOrderEvent
                        {
                            StudentId = student.Id,
                            Type = StudentOrderType.Восстановить,
                            Date = restoreDate,
                            EducationDirectionId = currentGroup.EducationDirectionId,
                            GroupFromId = null,
                            GroupToId = currentGroup.Id,
                            DocumentKey = $"RESTORE-{restoreDate:yyyyMMdd}"
                        });

                        break;
                    }

                case StudentOrderScenario.Completed:
                    {
                        if (currentGroup.Course == AcademicCourse.Course_4)
                        {
                            var completionDate = new DateTime(enrollmentDate.Year + 4, 6, 30)
                                .AddDays(student.Id % 3);

                            events.Add(new StudentOrderEvent
                            {
                                StudentId = student.Id,
                                Type = StudentOrderType.ОтчислитьПоЗавершению,
                                Date = completionDate,
                                EducationDirectionId = currentGroup.EducationDirectionId,
                                GroupFromId = currentGroup.Id,
                                GroupToId = null,
                                DocumentKey = $"COMPLETE-{completionDate:yyyyMMdd}"
                            });
                        }

                        break;
                    }
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
                StudentOrderType.ИзАкадема => "иа",
                StudentOrderType.Восстановить => "в",
                StudentOrderType.ОтчислитьЗаНеуспевамость => "ну",
                StudentOrderType.ОтчислитьПоСобственному => "сж",
                StudentOrderType.ОтчислитьПоЗавершению => "ок",
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