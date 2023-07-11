using System;
using TaskManagement.Models.Dtos.Requests;
using TaskManagement.Models.Dtos.Responses;

namespace TaskManagement.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<Response<RoleResponse>> AddUserToRole(string userId, string roleName);
        public Task<RoleResponse> CreateRole(RoleRequest request);
        public Task<Response<RoleResponse>> DeleteRole(string name);
        public Task<Response<RoleResponse>> EditRole(string id, string name);
        public Task<Response<IEnumerable<string>>> GetAllRoles();
    }
}

