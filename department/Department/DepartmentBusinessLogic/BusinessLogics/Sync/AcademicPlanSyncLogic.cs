using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.BusinessLogicsContracts.Sync;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;

namespace DepartmentBusinessLogic.BusinessLogics.Sync
{
    public class AcademicPlanSyncLogic : IAcademicPlanSyncLogic
    {
        private readonly IOneCApiService _oneCApiService;
        private readonly IAcademicPlanStorage _academicPlanStorage;
        private readonly IDisciplineStorage _disciplineStorage;
        private readonly IAcademicPlanRecordStorage _academicPlanRecordStorage;
        private readonly IDisciplineBlockStorage _disciplineBlockStorage;

        public AcademicPlanSyncLogic(
            IOneCApiService oneCApiService,
            IAcademicPlanStorage academicPlanStorage,
            IDisciplineStorage disciplineStorage,
            IAcademicPlanRecordStorage academicPlanRecordStorage,
            IDisciplineBlockStorage disciplineBlockStorage)
        {
            _oneCApiService = oneCApiService;
            _academicPlanStorage = academicPlanStorage;
            _disciplineStorage = disciplineStorage;
            _academicPlanRecordStorage = academicPlanRecordStorage;
            _disciplineBlockStorage = disciplineBlockStorage;
        }

        public async Task SyncAcademicPlansAsync()
        {
            var oneCAcademicPlans = await _oneCApiService.GetAcademicPlansAsync();

            var currentAcademicPlans = _academicPlanStorage.GetFullList() ?? new List<AcademicPlanViewModel>();
            var currentDisciplines = _disciplineStorage.GetFullList() ?? new List<DisciplineViewModel>();
            var currentAcademicPlanRecords = _academicPlanRecordStorage.GetFullList() ?? new List<AcademicPlanRecordViewModel>();
            var currentDisciplineBlocks = _disciplineBlockStorage.GetFullList() ?? new List<DisciplineBlockViewModel>();

            foreach (var oneCPlan in oneCAcademicPlans)
            {
                SyncAcademicPlan(oneCPlan, currentAcademicPlans);

                if (oneCPlan.AcademicPlanRecords == null || oneCPlan.AcademicPlanRecords.Count == 0)
                {
                    continue;
                }

                foreach (var oneCRecord in oneCPlan.AcademicPlanRecords)
                {
                    SyncDisciplineBlock(oneCRecord, currentDisciplineBlocks);
                    SyncDiscipline(oneCRecord, currentDisciplines, currentDisciplineBlocks);
                    SyncAcademicPlanRecord(oneCRecord, currentAcademicPlanRecords);
                }
            }

            DeleteRemovedAcademicPlanRecords(oneCAcademicPlans, currentAcademicPlanRecords);
            DeleteRemovedAcademicPlans(oneCAcademicPlans, currentAcademicPlans);
        }

        private void DeleteRemovedAcademicPlanRecords(
            List<DepartmentContracts.Dtos.OneC.AcademicPlanOneCDto> oneCAcademicPlans,
            List<AcademicPlanRecordViewModel> currentAcademicPlanRecords)
        {
            var oneCRecordIds = oneCAcademicPlans
                .SelectMany(x => x.AcademicPlanRecords ?? new List<DepartmentContracts.Dtos.OneC.AcademicPlanRecordOneCDto>())
                .Select(x => x.Id)
                .ToHashSet();

            var recordsToDelete = currentAcademicPlanRecords
                .Where(x => !oneCRecordIds.Contains(x.Id))
                .ToList();

            foreach (var record in recordsToDelete)
            {
                _academicPlanRecordStorage.Delete(new AcademicPlanRecordBindingModel
                {
                    Id = record.Id
                });

                currentAcademicPlanRecords.Remove(record);
            }
        }

        private void DeleteRemovedAcademicPlans(
            List<DepartmentContracts.Dtos.OneC.AcademicPlanOneCDto> oneCAcademicPlans,
            List<AcademicPlanViewModel> currentAcademicPlans)
        {
            var oneCPlanIds = oneCAcademicPlans
                .Select(x => x.Id)
                .ToHashSet();

            var plansToDelete = currentAcademicPlans
                .Where(x => !oneCPlanIds.Contains(x.Id))
                .ToList();

            foreach (var plan in plansToDelete)
            {
                _academicPlanStorage.Delete(new AcademicPlanBindingModel
                {
                    Id = plan.Id
                });

                currentAcademicPlans.Remove(plan);
            }
        }

        private void SyncAcademicPlan(
            DepartmentContracts.Dtos.OneC.AcademicPlanOneCDto oneCPlan,
            List<AcademicPlanViewModel> currentAcademicPlans)
        {
            var existingPlan = currentAcademicPlans.FirstOrDefault(x => x.Id == oneCPlan.Id);

            var planModel = new AcademicPlanBindingModel
            {
                Id = oneCPlan.Id,
                EducationDirectionId = oneCPlan.EducationDirectionId,
                AcademicCourses = oneCPlan.AcademicCourses,
                Year = oneCPlan.Year
            };

            if (existingPlan == null)
            {
                _academicPlanStorage.Insert(planModel);

                currentAcademicPlans.Add(new AcademicPlanViewModel
                {
                    Id = planModel.Id,
                    EducationDirectionId = planModel.EducationDirectionId,
                    AcademicCourses = planModel.AcademicCourses,
                    Year = planModel.Year
                });
            }
            else
            {
                var needUpdate =
                    existingPlan.EducationDirectionId != oneCPlan.EducationDirectionId ||
                    existingPlan.AcademicCourses != oneCPlan.AcademicCourses ||
                    existingPlan.Year != oneCPlan.Year;

                if (needUpdate)
                {
                    _academicPlanStorage.Update(planModel);

                    existingPlan.EducationDirectionId = planModel.EducationDirectionId;
                    existingPlan.AcademicCourses = planModel.AcademicCourses;
                    existingPlan.Year = planModel.Year;
                }
            }
        }

        private void SyncDisciplineBlock(
            DepartmentContracts.Dtos.OneC.AcademicPlanRecordOneCDto oneCRecord,
            List<DisciplineBlockViewModel> currentDisciplineBlocks)
        {
            var existingBlock = currentDisciplineBlocks.FirstOrDefault(x => x.Id == oneCRecord.DisciplineBlockId);

            var blockModel = new DisciplineBlockBindingModel
            {
                Id = oneCRecord.DisciplineBlockId,
                Title = oneCRecord.DisciplineBlockTitle,
                DisciplineBlockBlueAsteriskName = oneCRecord.DisciplineBlockBlueAsteriskName,
                DisciplineBlockUseForGrouping = oneCRecord.DisciplineBlockUseForGrouping,
                DisciplineBlockOrder = oneCRecord.DisciplineBlockOrder
            };

            if (existingBlock == null)
            {
                _disciplineBlockStorage.Insert(blockModel);

                currentDisciplineBlocks.Add(new DisciplineBlockViewModel
                {
                    Id = blockModel.Id,
                    Title = blockModel.Title,
                    DisciplineBlockBlueAsteriskName = blockModel.DisciplineBlockBlueAsteriskName,
                    DisciplineBlockUseForGrouping = blockModel.DisciplineBlockUseForGrouping,
                    DisciplineBlockOrder = blockModel.DisciplineBlockOrder
                });
            }
            else
            {
                var needUpdate =
                    existingBlock.Title != blockModel.Title ||
                    existingBlock.DisciplineBlockBlueAsteriskName != blockModel.DisciplineBlockBlueAsteriskName ||
                    existingBlock.DisciplineBlockUseForGrouping != blockModel.DisciplineBlockUseForGrouping ||
                    existingBlock.DisciplineBlockOrder != blockModel.DisciplineBlockOrder;

                if (needUpdate)
                {
                    _disciplineBlockStorage.Update(blockModel);

                    existingBlock.Title = blockModel.Title;
                    existingBlock.DisciplineBlockBlueAsteriskName = blockModel.DisciplineBlockBlueAsteriskName;
                    existingBlock.DisciplineBlockUseForGrouping = blockModel.DisciplineBlockUseForGrouping;
                    existingBlock.DisciplineBlockOrder = blockModel.DisciplineBlockOrder;
                }
            }
        }

        private void SyncDiscipline(
            DepartmentContracts.Dtos.OneC.AcademicPlanRecordOneCDto oneCRecord,
            List<DisciplineViewModel> currentDisciplines,
            List<DisciplineBlockViewModel> currentDisciplineBlocks)
        {
            var existingBlock = currentDisciplineBlocks.FirstOrDefault(x => x.Id == oneCRecord.DisciplineBlockId);
            if (existingBlock == null)
            {
                throw new InvalidOperationException(
                    $"Не найден блок дисциплин с Id = {oneCRecord.DisciplineBlockId} для дисциплины '{oneCRecord.Name}'.");
            }

            if (!oneCRecord.DisciplineId.HasValue)
            {
                throw new InvalidOperationException(
                    $"У записи учебного плана Id = {oneCRecord.Id} отсутствует DisciplineId.");
            }

            var existingDiscipline = currentDisciplines.FirstOrDefault(x => x.Id == oneCRecord.DisciplineId.Value);

            var disciplineModel = new DisciplineBindingModel
            {
                Id = oneCRecord.DisciplineId.Value,
                DisciplineBlockId = oneCRecord.DisciplineBlockId,

                // ГЛАВНОЕ ПРАВИЛО:
                DisciplineName = oneCRecord.Name,

                DisciplineShortName = string.IsNullOrWhiteSpace(oneCRecord.DisciplineShortName)
                    ? oneCRecord.Name
                    : oneCRecord.DisciplineShortName,

                DisciplineDescription = string.IsNullOrWhiteSpace(oneCRecord.DisciplineDescription)
                    ? oneCRecord.Name
                    : oneCRecord.DisciplineDescription,

                DisciplineBlockBlueAsteriskName = oneCRecord.DisciplineBlockBlueAsteriskName,

                HasExam = oneCRecord.HasExam,
                HasCredit = oneCRecord.HasCredit,
                HasCourseWork = oneCRecord.HasCourseWork,
                HasCourseProject = oneCRecord.HasCourseProject
            };

            if (existingDiscipline == null)
            {
                _disciplineStorage.Insert(disciplineModel);

                currentDisciplines.Add(new DisciplineViewModel
                {
                    Id = disciplineModel.Id,
                    DisciplineBlockId = disciplineModel.DisciplineBlockId,
                    DisciplineName = disciplineModel.DisciplineName,
                    DisciplineShortName = disciplineModel.DisciplineShortName,
                    DisciplineDescription = disciplineModel.DisciplineDescription,
                    DisciplineBlockBlueAsteriskName = disciplineModel.DisciplineBlockBlueAsteriskName,
                    HasExam = disciplineModel.HasExam,
                    HasCredit = disciplineModel.HasCredit,
                    HasCourseWork = disciplineModel.HasCourseWork,
                    HasCourseProject = disciplineModel.HasCourseProject
                });
            }
            else
            {
                var needUpdate =
                    existingDiscipline.DisciplineBlockId != disciplineModel.DisciplineBlockId ||
                    existingDiscipline.DisciplineName != disciplineModel.DisciplineName ||
                    existingDiscipline.DisciplineShortName != disciplineModel.DisciplineShortName ||
                    existingDiscipline.DisciplineDescription != disciplineModel.DisciplineDescription ||
                    existingDiscipline.DisciplineBlockBlueAsteriskName != disciplineModel.DisciplineBlockBlueAsteriskName ||
                    existingDiscipline.HasExam != disciplineModel.HasExam ||
                    existingDiscipline.HasCredit != disciplineModel.HasCredit ||
                    existingDiscipline.HasCourseWork != disciplineModel.HasCourseWork ||
                    existingDiscipline.HasCourseProject != disciplineModel.HasCourseProject;

                if (needUpdate)
                {
                    _disciplineStorage.Update(disciplineModel);

                    existingDiscipline.DisciplineBlockId = disciplineModel.DisciplineBlockId;
                    existingDiscipline.DisciplineName = disciplineModel.DisciplineName;
                    existingDiscipline.DisciplineShortName = disciplineModel.DisciplineShortName;
                    existingDiscipline.DisciplineDescription = disciplineModel.DisciplineDescription;
                    existingDiscipline.DisciplineBlockBlueAsteriskName = disciplineModel.DisciplineBlockBlueAsteriskName;
                    existingDiscipline.HasExam = disciplineModel.HasExam;
                    existingDiscipline.HasCredit = disciplineModel.HasCredit;
                    existingDiscipline.HasCourseWork = disciplineModel.HasCourseWork;
                    existingDiscipline.HasCourseProject = disciplineModel.HasCourseProject;
                }
            }
        }

        private void SyncAcademicPlanRecord(
            DepartmentContracts.Dtos.OneC.AcademicPlanRecordOneCDto oneCRecord,
            List<AcademicPlanRecordViewModel> currentAcademicPlanRecords)
        {
            var existingRecord = currentAcademicPlanRecords.FirstOrDefault(x => x.Id == oneCRecord.Id);

            var recordModel = new AcademicPlanRecordBindingModel
            {
                Id = oneCRecord.Id,
                AcademicPlanId = oneCRecord.AcademicPlanId,
                DisciplineId = oneCRecord.DisciplineId,
                Index = oneCRecord.Index,
                Name = oneCRecord.Name,
                Semester = oneCRecord.Semester,
                Zet = oneCRecord.Zet,
                AcademicHours = oneCRecord.AcademicHours,
                Exam = oneCRecord.Exam,
                Pass = oneCRecord.Pass,
                GradedPass = oneCRecord.GradedPass,
                CourseWork = oneCRecord.CourseWork,
                CourseProject = oneCRecord.CourseProject,
                Rgr = oneCRecord.Rgr,
                Lectures = oneCRecord.Lectures,
                LaboratoryHours = oneCRecord.LaboratoryHours,
                PracticalHours = oneCRecord.PracticalHours
            };

            if (existingRecord == null)
            {
                _academicPlanRecordStorage.Insert(recordModel);

                currentAcademicPlanRecords.Add(new AcademicPlanRecordViewModel
                {
                    Id = recordModel.Id,
                    AcademicPlanId = recordModel.AcademicPlanId,
                    DisciplineId = recordModel.DisciplineId,
                    Index = recordModel.Index,
                    Name = recordModel.Name,
                    Semester = recordModel.Semester,
                    Zet = recordModel.Zet,
                    AcademicHours = recordModel.AcademicHours,
                    Exam = recordModel.Exam,
                    Pass = recordModel.Pass,
                    GradedPass = recordModel.GradedPass,
                    CourseWork = recordModel.CourseWork,
                    CourseProject = recordModel.CourseProject,
                    Rgr = recordModel.Rgr,
                    Lectures = recordModel.Lectures,
                    LaboratoryHours = recordModel.LaboratoryHours,
                    PracticalHours = recordModel.PracticalHours
                });
            }
            else
            {
                var needUpdate =
                    existingRecord.AcademicPlanId != recordModel.AcademicPlanId ||
                    existingRecord.DisciplineId != recordModel.DisciplineId ||
                    existingRecord.Index != recordModel.Index ||
                    existingRecord.Name != recordModel.Name ||
                    existingRecord.Semester != recordModel.Semester ||
                    existingRecord.Zet != recordModel.Zet ||
                    existingRecord.AcademicHours != recordModel.AcademicHours ||
                    existingRecord.Exam != recordModel.Exam ||
                    existingRecord.Pass != recordModel.Pass ||
                    existingRecord.GradedPass != recordModel.GradedPass ||
                    existingRecord.CourseWork != recordModel.CourseWork ||
                    existingRecord.CourseProject != recordModel.CourseProject ||
                    existingRecord.Rgr != recordModel.Rgr ||
                    existingRecord.Lectures != recordModel.Lectures ||
                    existingRecord.LaboratoryHours != recordModel.LaboratoryHours ||
                    existingRecord.PracticalHours != recordModel.PracticalHours;

                if (needUpdate)
                {
                    _academicPlanRecordStorage.Update(recordModel);

                    existingRecord.AcademicPlanId = recordModel.AcademicPlanId;
                    existingRecord.DisciplineId = recordModel.DisciplineId;
                    existingRecord.Index = recordModel.Index;
                    existingRecord.Name = recordModel.Name;
                    existingRecord.Semester = recordModel.Semester;
                    existingRecord.Zet = recordModel.Zet;
                    existingRecord.AcademicHours = recordModel.AcademicHours;
                    existingRecord.Exam = recordModel.Exam;
                    existingRecord.Pass = recordModel.Pass;
                    existingRecord.GradedPass = recordModel.GradedPass;
                    existingRecord.CourseWork = recordModel.CourseWork;
                    existingRecord.CourseProject = recordModel.CourseProject;
                    existingRecord.Rgr = recordModel.Rgr;
                    existingRecord.Lectures = recordModel.Lectures;
                    existingRecord.LaboratoryHours = recordModel.LaboratoryHours;
                    existingRecord.PracticalHours = recordModel.PracticalHours;
                }
            }
        }
    }
}