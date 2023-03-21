using Core.Models;
using Core.Repositories.RoleRepository;
using Core.Repositories.UserRepository;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Infrastructure.Services.RoleService;

public class AdminService : IRoleService
{
    private readonly IRoleRepository _repository;
    private readonly IUserRepository _userRepository;

    public AdminService(IRoleRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<IActionResult> CreateRoleAsync(string name)
    {
        Role role = new Role(name);
        try
        {
            role = await _repository.CreateRoleAsync(role);
            return new OkObjectResult(role);
        }
        catch (NpgsqlException e)
        {
            return new ObjectResult(e.Message);
        }
    }

    public async Task<IActionResult> UpdateRoleAsync(Guid id, string name)
    {
        try
        {
            Role role = await _repository.GetRoleAsync(id);
            role.Name = name;
            role = await _repository.UpdateRoleAsync(role);
            return new OkObjectResult(role);
        }
        catch (NpgsqlException e)
        {
            return new ObjectResult(e.Message);
        }
    }

    public async Task<IActionResult> DeleteRoleAsync(Guid id)
    {
        try
        {
            Role role = await _repository.GetRoleAsync(id);
            await _repository.DeleteRoleAsync(role);
            return new OkResult();
        }
        catch (NpgsqlException e)
        {
            return new ObjectResult(e.Message);
        }
    }
    
    public async Task<IActionResult> AddUserToRoleAsync(Guid userId, Guid roleId)
    {
        try
        {
            User user = await _userRepository.GetUserAsync(userId);
            Role role = await _repository.GetRoleAsync(roleId);
            
            user.Roles.Add(role);
            user = await _userRepository.UpdateUserAsync(user);

            return new OkObjectResult(user);
        }
        catch (NpgsqlException e)
        {
            return new ObjectResult(e.Message);
        }
    }
}