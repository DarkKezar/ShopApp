using Infrastructure.DTO;
using Infrastructure.DTO.AccountTO;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.AccountService;

public interface IAccountService
{
    public Task<IActionResult> RegisterUserAsync(CreateAccountTO model);
    public Task<IActionResult> AuthorizeUserAsync(AuthorizeAccountTO model, JWTConfig config);

    public Task<IActionResult> AddToCartAsync(Guid userId, List<Guid> productsId);
    /*
     public Task<IActionResult> UpdateUserAsync();
     public Task<IActionResult> UpdatePasswordAsync();
     public Task<IActionResult> DeleteUserAsync();
     */
}