
using Infrastructure.Services.RoleService;
using Infrastructure.CustomResults;
using Core.Models;
using Core.Repositories.RoleRepository;
using Core.Repositories.UserRepository;
using Moq;
using System.Net;

namespace Tests;

public class RoleServiceTests
{
    private List<Role> roles = new List<Role>(){
        new Role() { Id = new Guid("0cd70aac-7aa1-4f13-98de-3717e22dca1e"), Name = "Admin" },
        new Role() { Id = new Guid("92fa11b5-5cff-4cfd-ae99-fe975aaf2452"), Name = "Customer" }
    };

    private List<User> users = new List<User>(){
        new User("login1", "pass") { 
            Id = new Guid("1cd70aac-7aa1-4f13-98de-3717e22dca2e"), 
            Name = "User 1", 
            Roles = new List<Role>()
        },
    };

    private Mock<IRoleRepository> GetRoleRepositoryMock(){
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
    private Mock<IUserRepository> GetUserRepositoryMock(){
        var mock = new Mock<IUserRepository>();

        mock.Setup(r => r.GetAllUsersAsync()).Returns(Task.FromResult(users.AsQueryable()));
        mock.Setup(r => r.GetUserAsync(It.IsAny<Guid>())).Returns<Guid>((Id) => {
                return Task.FromResult(users.SingleOrDefault(u => u.Id == Id));
            });
        mock.Setup(r => r.UpdateUserAsync(It.IsAny<User>())).Returns<User>(u => Task.FromResult(u));    

        return mock;
    }

    [Fact]
    public async void CreateTest1(){
        // Arrange
        var service = new RoleService(
            GetRoleRepositoryMock().Object, 
            GetUserRepositoryMock().Object);
        string RoleName = "TestRole 1";    

        // Act
        ApiResult result = await service.CreateRoleAsync(RoleName);

        // Assert
        Assert.Equal((HttpStatusCode)201, result.HttpStatusCode);
    }

    [Fact]
    public async void CreateTest2(){
        // Arrange
        var service = new RoleService(
            GetRoleRepositoryMock().Object, 
            GetUserRepositoryMock().Object);
        string RoleName = "Admin";    

        // Act
        ApiResult result = await service.CreateRoleAsync(RoleName);

        // Assert
        Assert.Equal((HttpStatusCode)500, result.HttpStatusCode);
    }

    [Fact]
    public async void UpdateTest1(){
        // Arrange
        var service = new RoleService(
            GetRoleRepositoryMock().Object, 
            GetUserRepositoryMock().Object);

        // Act
        ApiResult result = await service.UpdateRoleAsync(new Guid("0cd70aac-7aa1-4f13-98de-3717e22dca1e"), "Admin1");

        // Assert
        Assert.Equal(
           new { Code = (HttpStatusCode)204,
                 Name = "Admin1" }, 
           new { Code = result.HttpStatusCode,
                 Name = ((Role)result.ObjectResult).Name }
        );
    }

    [Fact]
    public async void UpdateTest2(){
        // Arrange
        var service = new RoleService(
            GetRoleRepositoryMock().Object, 
            GetUserRepositoryMock().Object);

        // Act
        ApiResult result = await service.UpdateRoleAsync(new Guid("1cd70aac-7aa1-4f13-98de-3717e22dca1e"), "Admin1");

        // Assert
        Assert.Equal((HttpStatusCode)500, result.HttpStatusCode);
    }

    [Fact]
    public async void DeleteTest1(){
        // Arrange
        var service = new RoleService(
            GetRoleRepositoryMock().Object, 
            GetUserRepositoryMock().Object);

        // Act
        ApiResult result = await service.DeleteRoleAsync(new Guid("92fa11b5-5cff-4cfd-ae99-fe975aaf2452"));

        // Assert
        Assert.Equal((HttpStatusCode)204, result.HttpStatusCode);
    }

    [Fact]
    public async void DeleteTest2(){
        // Arrange
        var service = new RoleService(
            GetRoleRepositoryMock().Object, 
            GetUserRepositoryMock().Object);

        // Act
        ApiResult result = await service.DeleteRoleAsync(new Guid("0ad70aac-7aa1-4f13-98de-3717e22dca1e"));

        // Assert
        Assert.Equal((HttpStatusCode)500, result.HttpStatusCode);
    }

    [Fact]
    public async void AddUserToRoleTest1(){
        // Arrange
        var service = new RoleService(
            GetRoleRepositoryMock().Object, 
            GetUserRepositoryMock().Object);

        // Act
        ApiResult result = await service.AddUserToRoleAsync(
            new Guid("1cd70aac-7aa1-4f13-98de-3717e22dca2e"),
            new Guid("0cd70aac-7aa1-4f13-98de-3717e22dca1e")
        );
        User user = (User)(result.ObjectResult);

        // Assert
        Assert.Equal(true, user.Roles.Contains(roles[0]));
    }
    
    [Fact]
    public async void AddUserToRoleTest2(){
        // Arrange
        var service = new RoleService(
            GetRoleRepositoryMock().Object, 
            GetUserRepositoryMock().Object);

        // Act
        ApiResult result = await service.AddUserToRoleAsync(
            new Guid("5cd70aac-7aa1-4f13-98de-3717e22dca2e"),
            new Guid("0cd70aac-7aa1-4f13-98de-3717e22dca1e")
        );

        // Assert
        Assert.Equal((HttpStatusCode)500, result.HttpStatusCode);
    }

    [Fact]
    public async void AddUserToRoleTest3(){
        // Arrange
        var service = new RoleService(
            GetRoleRepositoryMock().Object, 
            GetUserRepositoryMock().Object);

        // Act
        ApiResult result = await service.AddUserToRoleAsync(
            new Guid("1cd70aac-7aa1-4f13-98de-3717e22dca2e"),
            new Guid("1cd70aac-7aa1-4f13-98de-3717e22dca1e")
        );

        // Assert
        Assert.Equal((HttpStatusCode)500, result.HttpStatusCode);
    }

    [Fact]
    public async void AddUserToRoleTest4(){
        // Arrange
        var service = new RoleService(
            GetRoleRepositoryMock().Object, 
            GetUserRepositoryMock().Object);

        // Act
        ApiResult result = await service.AddUserToRoleAsync(
            new Guid("5cd70aac-7aa1-4f13-98de-3717e22dca2e"),
            new Guid("8cd70aac-7aa1-4f13-98de-3717e22dca1e")
        );

        // Assert
        Assert.Equal((HttpStatusCode)500, result.HttpStatusCode);
    }
}
