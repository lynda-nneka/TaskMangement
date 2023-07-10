using System;
using TaskManagement.Models.Dtos.Requests;
using TaskManagement.Models.Dtos.Responses;

namespace TaskManagement.Services.Interfaces
{
    public interface ITaskService
    {
        Task<ItemResponse> CreateTask(ItemRequest request);
        Task<RetrieveItemResponse> GetTask(string Id);
        Task<ItemResponse> UpdateTask(string Id, UpdatetaskRequest request);
        Task<ItemResponse> DeleteTask(string Id);
    }
}

