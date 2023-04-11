using Infrastructure.CustomResults;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.RoleService;

public interface IRoleService
{
    public Task<ApiResult> CreateRoleAsync(string name);
    public Task<ApiResult> UpdateRoleAsync(Guid id, string name);
    public Task<ApiResult> DeleteRoleAsync(Guid id);
    public Task<ApiResult> AddUserToRoleAsync(Guid userId, Guid roleId);
}