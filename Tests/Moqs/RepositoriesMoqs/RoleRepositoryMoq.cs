using Core.Models;
using Core.Repositories.RoleRepository;
using Moq;

namespace Tests.Moqs.RepositoriesMoqs;

public static class RoleRepositoryMoq
{
    public static List<Role> roles = new List<Role>(){
        new Role() { Id = new Guid("0cd70aac-7aa1-4f13-98de-3717e22dca1e"), Name = "Admin" },
        new Role() { Id = new Guid("92fa11b5-5cff-4cfd-ae99-fe975aaf2452"), Name = "Customer" }
    };

    public static Mock<IRoleRepository> GetRoleRepositoryMoq(){
        var mock = new Mock<IRoleRepository>();

        mock.Setup(r => r.GetAllRolesAsync())
            .Returns(Task.FromResult(roles.AsQueryable()));
        mock.Setup(r => r.CreateRoleAsync(It.IsAny<Role>()))
            .Returns<Role>((Role) => {
                if(roles.Where(r => r.Name.Equals(Role.Name)).Count() != 0)
                    throw new Exception();
                else return Task.FromResult(Role);
            });     
        mock.Setup(r => r.GetRoleAsync(It.IsAny<Guid>()))
            .Returns<Guid>((Id) => {
                if(roles.Where(r => r.Id == Id).Count() == 0)
                    throw new Exception();
                else return Task.FromResult(roles.SingleOrDefault(r => r.Id == Id));
            });
        mock.Setup(r => r.UpdateRoleAsync(It.IsAny<Role>()))
            .Returns<Role>(Role => Task.FromResult(Role));        
        mock.Setup(r => r.DeleteRoleAsync(It.IsAny<Role>()))
            .Returns<Role>((Role) => {
                if(!roles.Contains(Role))
                    throw new Exception();
                else return Task.CompletedTask;
            });
                    
        return mock;
    }

}
