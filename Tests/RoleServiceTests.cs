
using Infrastructure.Services.RoleService;
using Infrastructure.CustomResults;
using Core.Models;
using System.Net;
using Tests.Moqs.RepositoriesMoqs;

namespace Tests;

public class RoleServiceTests
{
       
    [Fact]
    public async void CreateTest1(){
        // Arrange
        var service = new RoleService(
            RoleRepositoryMoq.GetRoleRepositoryMoq().Object, 
            UserRepositoryMoq.GetUserRepositoryMoq().Object);
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
            RoleRepositoryMoq.GetRoleRepositoryMoq().Object, 
            UserRepositoryMoq.GetUserRepositoryMoq().Object);
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
            RoleRepositoryMoq.GetRoleRepositoryMoq().Object, 
            UserRepositoryMoq.GetUserRepositoryMoq().Object);

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
            RoleRepositoryMoq.GetRoleRepositoryMoq().Object, 
            UserRepositoryMoq.GetUserRepositoryMoq().Object);

        // Act
        ApiResult result = await service.UpdateRoleAsync(new Guid("1cd70aac-7aa1-4f13-98de-3717e22dca1e"), "Admin1");

        // Assert
        Assert.Equal((HttpStatusCode)500, result.HttpStatusCode);
    }

    [Fact]
    public async void DeleteTest1(){
        // Arrange
        var service = new RoleService(
            RoleRepositoryMoq.GetRoleRepositoryMoq().Object, 
            UserRepositoryMoq.GetUserRepositoryMoq().Object);

        // Act
        ApiResult result = await service.DeleteRoleAsync(new Guid("92fa11b5-5cff-4cfd-ae99-fe975aaf2452"));

        // Assert
        Assert.Equal((HttpStatusCode)204, result.HttpStatusCode);
    }

    [Fact]
    public async void DeleteTest2(){
        // Arrange
        var service = new RoleService(
            RoleRepositoryMoq.GetRoleRepositoryMoq().Object, 
            UserRepositoryMoq.GetUserRepositoryMoq().Object);

        // Act
        ApiResult result = await service.DeleteRoleAsync(new Guid("0ad70aac-7aa1-4f13-98de-3717e22dca1e"));

        // Assert
        Assert.Equal((HttpStatusCode)500, result.HttpStatusCode);
    }

    [Fact]
    public async void AddUserToRoleTest1(){
        // Arrange
        var service = new RoleService(
            RoleRepositoryMoq.GetRoleRepositoryMoq().Object, 
            UserRepositoryMoq.GetUserRepositoryMoq().Object);

        // Act
        ApiResult result = await service.AddUserToRoleAsync(
            new Guid("1cd70aac-7aa1-4f13-98de-3717e22dca2e"),
            new Guid("0cd70aac-7aa1-4f13-98de-3717e22dca1e")
        );
        User user = (User)(result.ObjectResult);

        // Assert
        Assert.Equal(true, user.Roles.Contains(RoleRepositoryMoq.roles[0]));
    }
    
    [Fact]
    public async void AddUserToRoleTest2(){
        // Arrange
        var service = new RoleService(
            RoleRepositoryMoq.GetRoleRepositoryMoq().Object, 
            UserRepositoryMoq.GetUserRepositoryMoq().Object);

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
            RoleRepositoryMoq.GetRoleRepositoryMoq().Object, 
            UserRepositoryMoq.GetUserRepositoryMoq().Object);

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
            RoleRepositoryMoq.GetRoleRepositoryMoq().Object, 
            UserRepositoryMoq.GetUserRepositoryMoq().Object);

        // Act
        ApiResult result = await service.AddUserToRoleAsync(
            new Guid("5cd70aac-7aa1-4f13-98de-3717e22dca2e"),
            new Guid("8cd70aac-7aa1-4f13-98de-3717e22dca1e")
        );

        // Assert
        Assert.Equal((HttpStatusCode)500, result.HttpStatusCode);
    }
}
