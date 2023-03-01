using Core.Context;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.RoleRepository;

public class RoleRepository : IRoleRepository
{
    private readonly ShopContext _context;

    public RoleRepository(ShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync(int count, int page)
    {
        return _context.Roles.Skip(page * count).Take(count);
    }

    public async Task<Role> GetRoleAsync(Guid id)
    {
        return await _context.Roles.SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Role> CreateRoleAsync(Role role)
    {
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<Role> UpdateRoleAsync(Role role)
    {
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task DeleteRoleAsync(Role role)
    {
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
    }
}