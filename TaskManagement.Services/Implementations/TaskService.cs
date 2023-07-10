using System;
using System.Threading.Tasks;
using Azure.Core;
using TaskManagement.Data.Interfaces;
using TaskManagement.Models.Dtos.Requests;
using TaskManagement.Models.Dtos.Responses;
using TaskManagement.Models.Entities;
using TaskManagement.Models.Enums;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceFactory _serviceFactory;
        private readonly IRepository<TaskItem> _taskItemManager;
        private readonly IRepository<ApplicationUser> _userManager;

        public TaskService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
            _taskItemManager = _unitOfWork.GetRepository<TaskItem>();
            _userManager = _unitOfWork.GetRepository<ApplicationUser>();
        }

        public async Task<ItemResponse> CreateTask(ItemRequest request)
        {
            ApplicationUser user = await _userManager.GetSingleByAsync(user => user.Id == request.AssigneeId && user.Id == request.ReporterId);

            if (user == null)
                throw new InvalidOperationException($"user does not exist");

            TaskItem taskItem = new()
            {

                
                Title = request.Title,
                Description = request.Description,
                TaskStatus = request.TaskStatus,
                AssigneeId = request.AssigneeId,
                DueTime = request.DueTime,
                ReporterId = request.ReporterId,
                Priority = Priority.Mid,
                CreatedAt = DateTime.Now
                

            };

            var result = await _taskItemManager.AddAsync(taskItem);
            if (result == null)
            {
                throw new InvalidOperationException("task creation failed");
            }
            return new ItemResponse
            {
                
                Success = true,
                Message = "task created successfully"
            };
        }
        public async Task<ItemResponse> DeleteTask(string Id)
        {
            TaskItem task = await _taskItemManager.GetSingleByAsync(c => c.Id == Id);

            if (task == null)
                throw new InvalidOperationException("Item doesnt exist");

            await _taskItemManager.DeleteAsync(task);

            return new ItemResponse
            {
                Success = true,
                Message = "Successfully Deleted"
            };
        }

        public async Task<RetrieveItemResponse> GetTask(string Id)
        {
            TaskItem task = await _taskItemManager.GetSingleByAsync(x => x.Id == Id);

            if (task == null)
                throw new InvalidOperationException("no task with that Id");

            return new RetrieveItemResponse
            {
                Title = task.Title,
                Description = task.Description,
                TaskStatus = task.TaskStatus,
                AssigneeId = task.AssigneeId,
                DueTime = task.DueTime,
                ReporterId = task.ReporterId,
                Priority = Priority.Mid,
                CreatedAt = DateTime.Now

            };
        }

        public async Task<ItemResponse> UpdateTask(string Id, UpdatetaskRequest request)
        {
            TaskItem item = await _taskItemManager.GetByIdAsync(Id);
            if (item == null)
                throw new InvalidOperationException("item not found");

            item.Title = request.Title;
            item.Description = request.Description;
            item.TaskStatus = request.TaskStatus;
            item.AssigneeId = request.AssigneeId;
            item.DueTime = request.DueTime;
            item.ReporterId = request.ReporterId;
            item.Priority = Priority.Mid;
            item.UpdatedAt = DateTime.Now;

            await _taskItemManager.UpdateAsync(item);
            return new ItemResponse
            {
               
                Success = true,
                Message = "Update Successful"

            };
        }
    }
}

