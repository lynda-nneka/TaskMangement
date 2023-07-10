using System;
using TaskManagement.Models.Dtos.Requests;
using TaskManagement.Models.Dtos.Responses;

namespace TaskManagement.Services.Interfaces
{
    public interface ITaskService
    {
        Task<ItemResponse> CreateTask(ItemRequest request);
        Task<IEnumerable<ItemResponse>> GetTask(string Id);
        Task<ItemResponse> UpdateTask(string Id);
        Task DeleteTask(string Id);
    }
}

