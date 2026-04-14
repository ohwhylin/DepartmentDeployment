using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.BusinessLogicsContracts.Sync;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;

namespace DepartmentBusinessLogic.BusinessLogics.Sync
{
    public class StudentGroupSyncLogic : IStudentGroupSyncLogic
    {
        private readonly IOneCApiService _oneCApiService;
        private readonly IStudentGroupStorage _studentGroupStorage;

        public StudentGroupSyncLogic(
            IOneCApiService oneCApiService,
            IStudentGroupStorage studentGroupStorage)
        {
            _oneCApiService = oneCApiService;
            _studentGroupStorage = studentGroupStorage;
        }

        public async Task SyncStudentGroupsAsync()
        {
            var oneCGroups = await _oneCApiService.GetStudentGroupsAsync();
            var currentGroups = _studentGroupStorage.GetFullList() ?? new List<StudentGroupViewModel>();

            foreach (var oneCGroup in oneCGroups)
            {
                var existing = currentGroups.FirstOrDefault(x => x.Id == oneCGroup.Id);

                var model = new StudentGroupBindingModel
                {
                    Id = oneCGroup.Id,
                    EducationDirectionId = oneCGroup.EducationDirectionId,
                    CuratorId = oneCGroup.CuratorId,
                    GroupName = oneCGroup.GroupName,
                    Course = oneCGroup.Course
                };

                if (existing == null)
                {
                    _studentGroupStorage.Insert(model);
                }
                else
                {
                    var needUpdate =
                        existing.GroupName != oneCGroup.GroupName ||
                        existing.Course != oneCGroup.Course ||
                        existing.CuratorId != oneCGroup.CuratorId ||
                        existing.EducationDirectionId != oneCGroup.EducationDirectionId;

                    if (needUpdate)
                    {
                        _studentGroupStorage.Update(model);
                    }
                }
            }

            DeleteRemovedStudentGroups(oneCGroups, currentGroups);
        }

        private void DeleteRemovedStudentGroups(
        List<DepartmentContracts.Dtos.OneC.StudentGroupOneCDto> oneCGroups,
        List<StudentGroupViewModel> currentGroups)
            {
                var oneCGroupIds = oneCGroups
                    .Select(x => x.Id)
                    .ToHashSet();

                var groupsToDelete = currentGroups
                    .Where(x => !oneCGroupIds.Contains(x.Id))
                    .ToList();

                foreach (var group in groupsToDelete)
                {
                    _studentGroupStorage.Delete(new StudentGroupBindingModel
                    {
                        Id = group.Id
                    });

                    currentGroups.Remove(group);
                }
            }
    }
}