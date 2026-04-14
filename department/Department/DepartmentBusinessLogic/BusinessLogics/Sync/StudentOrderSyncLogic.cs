using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.BusinessLogicsContracts.Sync;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;

namespace DepartmentBusinessLogic.BusinessLogics.Sync
{
    public class StudentOrderSyncLogic : IStudentOrderSyncLogic
    {
        private readonly IOneCApiService _oneCService;
        private readonly IStudentOrderStorage _orderStorage;
        private readonly IStudentOrderBlockStorage _blockStorage;
        private readonly IStudentOrderBlockStudentStorage _blockStudentStorage;

        public StudentOrderSyncLogic(
            IOneCApiService oneCService,
            IStudentOrderStorage orderStorage,
            IStudentOrderBlockStorage blockStorage,
            IStudentOrderBlockStudentStorage blockStudentStorage)
        {
            _oneCService = oneCService;
            _orderStorage = orderStorage;
            _blockStorage = blockStorage;
            _blockStudentStorage = blockStudentStorage;
        }

        public async Task SyncStudentOrdersAsync()
        {
            var oneCOrders = await _oneCService.GetStudentOrdersAsync();

            var currentOrders = _orderStorage.GetFullList() ?? new List<StudentOrderViewModel>();
            var currentBlocks = _blockStorage.GetFullList() ?? new List<StudentOrderBlockViewModel>();
            var currentBlockStudents = _blockStudentStorage.GetFullList() ?? new List<StudentOrderBlockStudentViewModel>();

            foreach (var oneCOrder in oneCOrders)
            {
                SyncStudentOrder(oneCOrder, currentOrders);

                if (oneCOrder.Blocks == null || oneCOrder.Blocks.Count == 0)
                {
                    continue;
                }

                foreach (var oneCBlock in oneCOrder.Blocks)
                {
                    SyncStudentOrderBlock(oneCOrder, oneCBlock, currentBlocks);

                    if (oneCBlock.Students == null || oneCBlock.Students.Count == 0)
                    {
                        continue;
                    }

                    foreach (var oneCBlockStudent in oneCBlock.Students)
                    {
                        SyncStudentOrderBlockStudent(oneCBlock, oneCBlockStudent, currentBlockStudents);
                    }
                }
            }

            DeleteRemovedStudentOrderBlockStudents(oneCOrders, currentBlockStudents);
            DeleteRemovedStudentOrderBlocks(oneCOrders, currentBlocks);
            DeleteRemovedStudentOrders(oneCOrders, currentOrders);
        }

        private void DeleteRemovedStudentOrderBlockStudents(
            List<DepartmentContracts.Dtos.OneC.StudentOrderOneCDto> oneCOrders,
            List<StudentOrderBlockStudentViewModel> currentBlockStudents)
        {
            var oneCBlockStudentIds = oneCOrders
                .SelectMany(x => x.Blocks ?? new List<DepartmentContracts.Dtos.OneC.StudentOrderBlockOneCDto>())
                .SelectMany(x => x.Students ?? new List<DepartmentContracts.Dtos.OneC.StudentOrderBlockStudentOneCDto>())
                .Select(x => x.Id)
                .ToHashSet();

            var blockStudentsToDelete = currentBlockStudents
                .Where(x => !oneCBlockStudentIds.Contains(x.Id))
                .ToList();

            foreach (var blockStudent in blockStudentsToDelete)
            {
                _blockStudentStorage.Delete(new StudentOrderBlockStudentBindingModel
                {
                    Id = blockStudent.Id
                });

                currentBlockStudents.Remove(blockStudent);
            }
        }

        private void DeleteRemovedStudentOrderBlocks(
            List<DepartmentContracts.Dtos.OneC.StudentOrderOneCDto> oneCOrders,
            List<StudentOrderBlockViewModel> currentBlocks)
        {
            var oneCBlockIds = oneCOrders
                .SelectMany(x => x.Blocks ?? new List<DepartmentContracts.Dtos.OneC.StudentOrderBlockOneCDto>())
                .Select(x => x.Id)
                .ToHashSet();

            var blocksToDelete = currentBlocks
                .Where(x => !oneCBlockIds.Contains(x.Id))
                .ToList();

            foreach (var block in blocksToDelete)
            {
                _blockStorage.Delete(new StudentOrderBlockBindingModel
                {
                    Id = block.Id
                });

                currentBlocks.Remove(block);
            }
        }

        private void DeleteRemovedStudentOrders(
            List<DepartmentContracts.Dtos.OneC.StudentOrderOneCDto> oneCOrders,
            List<StudentOrderViewModel> currentOrders)
        {
            var oneCOrderIds = oneCOrders
                .Select(x => x.Id)
                .ToHashSet();

            var ordersToDelete = currentOrders
                .Where(x => !oneCOrderIds.Contains(x.Id))
                .ToList();

            foreach (var order in ordersToDelete)
            {
                _orderStorage.Delete(new StudentOrderBindingModel
                {
                    Id = order.Id
                });

                currentOrders.Remove(order);
            }
        }

        private void SyncStudentOrder(
            DepartmentContracts.Dtos.OneC.StudentOrderOneCDto oneCOrder,
            List<StudentOrderViewModel> currentOrders)
        {
            var existingOrder = currentOrders.FirstOrDefault(x => x.Id == oneCOrder.Id);

            var orderModel = new StudentOrderBindingModel
            {
                Id = oneCOrder.Id,
                OrderNumber = oneCOrder.OrderNumber,
                StudentOrderType = oneCOrder.StudentOrderType
            };

            if (existingOrder == null)
            {
                _orderStorage.Insert(orderModel);

                currentOrders.Add(new StudentOrderViewModel
                {
                    Id = orderModel.Id,
                    OrderNumber = orderModel.OrderNumber,
                    StudentOrderType = orderModel.StudentOrderType
                });
            }
            else
            {
                var needUpdate =
                    existingOrder.OrderNumber != oneCOrder.OrderNumber ||
                    existingOrder.StudentOrderType != oneCOrder.StudentOrderType;

                if (needUpdate)
                {
                    _orderStorage.Update(orderModel);

                    existingOrder.OrderNumber = orderModel.OrderNumber;
                    existingOrder.StudentOrderType = orderModel.StudentOrderType;
                }
            }
        }

        private void SyncStudentOrderBlock(
            DepartmentContracts.Dtos.OneC.StudentOrderOneCDto oneCOrder,
            DepartmentContracts.Dtos.OneC.StudentOrderBlockOneCDto oneCBlock,
            List<StudentOrderBlockViewModel> currentBlocks)
        {
            var existingBlock = currentBlocks.FirstOrDefault(x => x.Id == oneCBlock.Id);

            var blockModel = new StudentOrderBlockBindingModel
            {
                Id = oneCBlock.Id,
                StudentOrderId = oneCOrder.Id,
                EducationDirectionId = oneCBlock.EducationDirectionId,
                StudentOrderType = oneCBlock.StudentOrderType
            };

            if (existingBlock == null)
            {
                _blockStorage.Insert(blockModel);

                currentBlocks.Add(new StudentOrderBlockViewModel
                {
                    Id = blockModel.Id,
                    StudentOrderId = blockModel.StudentOrderId,
                    EducationDirectionId = blockModel.EducationDirectionId,
                    StudentOrderType = blockModel.StudentOrderType
                });
            }
            else
            {
                var needUpdate =
                    existingBlock.StudentOrderId != oneCOrder.Id ||
                    existingBlock.EducationDirectionId != oneCBlock.EducationDirectionId ||
                    existingBlock.StudentOrderType != oneCBlock.StudentOrderType;

                if (needUpdate)
                {
                    _blockStorage.Update(blockModel);

                    existingBlock.StudentOrderId = blockModel.StudentOrderId;
                    existingBlock.EducationDirectionId = blockModel.EducationDirectionId;
                    existingBlock.StudentOrderType = blockModel.StudentOrderType;
                }
            }
        }

        private void SyncStudentOrderBlockStudent(
            DepartmentContracts.Dtos.OneC.StudentOrderBlockOneCDto oneCBlock,
            DepartmentContracts.Dtos.OneC.StudentOrderBlockStudentOneCDto oneCBlockStudent,
            List<StudentOrderBlockStudentViewModel> currentBlockStudents)
        {
            var existingBlockStudent = currentBlockStudents.FirstOrDefault(x => x.Id == oneCBlockStudent.Id);

            var blockStudentModel = new StudentOrderBlockStudentBindingModel
            {
                Id = oneCBlockStudent.Id,
                StudentOrderBlockId = oneCBlock.Id,
                StudentId = oneCBlockStudent.StudentId,
                StudentGroupFromId = oneCBlockStudent.StudentGroupFromId,
                StudentGroupToId = oneCBlockStudent.StudentGroupToId
            };

            if (existingBlockStudent == null)
            {
                _blockStudentStorage.Insert(blockStudentModel);

                currentBlockStudents.Add(new StudentOrderBlockStudentViewModel
                {
                    Id = blockStudentModel.Id,
                    StudentOrderBlockId = blockStudentModel.StudentOrderBlockId,
                    StudentId = blockStudentModel.StudentId,
                    StudentGroupFromId = blockStudentModel.StudentGroupFromId,
                    StudentGroupToId = blockStudentModel.StudentGroupToId
                });
            }
            else
            {
                var needUpdate =
                    existingBlockStudent.StudentOrderBlockId != oneCBlock.Id ||
                    existingBlockStudent.StudentId != oneCBlockStudent.StudentId ||
                    existingBlockStudent.StudentGroupFromId != oneCBlockStudent.StudentGroupFromId ||
                    existingBlockStudent.StudentGroupToId != oneCBlockStudent.StudentGroupToId;

                if (needUpdate)
                {
                    _blockStudentStorage.Update(blockStudentModel);

                    existingBlockStudent.StudentOrderBlockId = blockStudentModel.StudentOrderBlockId;
                    existingBlockStudent.StudentId = blockStudentModel.StudentId;
                    existingBlockStudent.StudentGroupFromId = blockStudentModel.StudentGroupFromId;
                    existingBlockStudent.StudentGroupToId = blockStudentModel.StudentGroupToId;
                }
            }
        }
    }
}