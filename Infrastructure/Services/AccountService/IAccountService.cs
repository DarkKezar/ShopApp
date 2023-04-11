using Infrastructure.CustomResults;
using Infrastructure.DTO;
using Infrastructure.DTO.AccountTO;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.AccountService;

public interface IAccountService
{
    public Task<ApiResult> RegisterUserAsync(CreateAccountTO model);
    public Task<ApiResult> AuthorizeUserAsync(AuthorizeAccountTO model, JWTConfig config);

    public Task<ApiResult> AddToCartAsync(Guid userId, List<Guid> productsId);
    /*
     public Task<IActionResult> UpdateUserAsync();
     public Task<IActionResult> UpdatePasswordAsync();
     public Task<IActionResult> DeleteUserAsync();
     */
}