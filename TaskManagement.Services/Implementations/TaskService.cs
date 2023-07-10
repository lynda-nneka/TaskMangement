using System;
using TaskManagement.Data.Interfaces;
using TaskManagement.Models.Dtos.Requests;
using TaskManagement.Models.Dtos.Responses;
using TaskManagement.Models.Entities;
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

        public Task<ItemResponse> CreateTask(ItemRequest request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTask(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemResponse>> GetTask(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemResponse> UpdateTask(string Id)
        {
            throw new NotImplementedException();
        }
    }
}

