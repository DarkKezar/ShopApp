using Core.Models;
using Core.Repositories.UserRepository;
using Moq;

namespace Tests.Moqs.RepositoriesMoqs;

public static class UserRepositoryMoq
{
    public static List<User> users = new List<User>(){
        new User("login1", "pass") { 
            Id = new Guid("1cd70aac-7aa1-4f13-98de-3717e22dca2e"), 
            Name = "User 1", 
            Roles = new List<Role>()
        },
    };

    
    public static Mock<IUserRepository> GetUserRepositoryMoq(){
        var mock = new Mock<IUserRepository>();

        mock.Setup(r => r.GetAllUsersAsync()).Returns(Task.FromResult(users.AsQueryable()));
        mock.Setup(r => r.GetUserAsync(It.IsAny<Guid>())).Returns<Guid>((Id) => {
                return Task.FromResult(users.SingleOrDefault(u => u.Id == Id));
            });
        mock.Setup(r => r.UpdateUserAsync(It.IsAny<User>())).Returns<User>(u => Task.FromResult(u));    

        return mock;
    }
}
