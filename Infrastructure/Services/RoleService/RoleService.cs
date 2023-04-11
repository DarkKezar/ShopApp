using System.Net;
using Core.Models;
using Core.Repositories.RoleRepository;
using Core.Repositories.UserRepository;
using Infrastructure.CustomResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Infrastructure.Services.RoleService;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;
    private readonly IUserRepository _userRepository;

    public RoleService(IRoleRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<ApiResult> CreateRoleAsync(string name)
    {
        Role role = new Role(name);
        try
        {
            role = await _repository.CreateRoleAsync(role);
            return new ApiResult("Role has been create", HttpStatusCode.Created ,role);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ApiResult> UpdateRoleAsync(Guid id, string name)
    {
        try
        {
            Role role = await _repository.GetRoleAsync(id);
            role.Name = name;
            role = await _repository.UpdateRoleAsync(role);
            return new ApiResult("Role has been updated", (HttpStatusCode)204, id);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ApiResult> DeleteRoleAsync(Guid id)
    {
        try
        {
            Role role = await _repository.GetRoleAsync(id);
            await _repository.DeleteRoleAsync(role);
            return new ApiResult("Role has been deleted", (HttpStatusCode)204, id);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, HttpStatusCode.InternalServerError);
        }
    }
    
    public async Task<ApiResult> AddUserToRoleAsync(Guid userId, Guid roleId)
    {
        try
        {
            User user = await _userRepository.GetUserAsync(userId);
            Role role = await _repository.GetRoleAsync(roleId);
            
            user.Roles.Add(role);
            user = await _userRepository.UpdateUserAsync(user);

            return new ApiResult("", (HttpStatusCode)204, user);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, HttpStatusCode.InternalServerError);
        }
    }
}