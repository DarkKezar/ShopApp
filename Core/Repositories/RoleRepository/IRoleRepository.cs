using Core.Models;

namespace Core.Repositories.RoleRepository;

public interface IRoleRepository
{
    public Task<IEnumerable<Role>> GetAllRolesAsync(int count, int page);
    public Task<Role> GetRoleAsync(Guid id);
    public Task<Role> CreateRoleAsync(Role role);
    public Task<Role> UpdateRoleAsync(Role role);
    public Task DeleteRoleAsync(Role role);
}