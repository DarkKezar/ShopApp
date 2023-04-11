using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Core.Models;
using Core.Repositories.ProductRepository;
using Core.Repositories.RoleRepository;
using Core.Repositories.UserRepository;
using FluentValidation.Results;
using Infrastructure.CustomResults;
using Infrastructure.DTO;
using Infrastructure.DTO.AccountTO;
using Infrastructure.Validators.AccountValidators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Npgsql;


namespace Infrastructure.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IUserRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public AccountService(IUserRepository repository, IProductRepository productRepository, IRoleRepository roleRepository, IMapper mapper)
    {
        _repository = repository;
        _productRepository = productRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<ApiResult> RegisterUserAsync(CreateAccountTO model)
    {
        ValidationResult result = await (new CreateAccountTOValidator()).ValidateAsync(model);
        if (result.IsValid)
        {
            User user = new User(model.Name, model.Login);
            user.Roles.Add(await _roleRepository.GetRoleAsync(new Guid("92fa11b5-5cff-4cfd-ae99-fe975aaf2452")));
            try
            {
                user = await _repository.CreateUserAsync(user, model.Password);
            }
            catch (Exception e)
            {
                return new ApiResult(e.Message, (HttpStatusCode)500);
            }
            return new ApiResult("Ok", HttpStatusCode.Created, user);
        }
        else
        {
            return new ApiResult("Model has errors", HttpStatusCode.BadRequest, result.Errors);
        }
    }

    public async Task<ApiResult> AuthorizeUserAsync(AuthorizeAccountTO model, JWTConfig config)
    {
        ValidationResult result = await (new AuthorizeAccountTOValidator()).ValidateAsync(model);
        if (result.IsValid)
        {
            User user = await _repository.AuthorizeUserAsync(model.Login, model.Password);
            if (user != null)
            {
                List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Sid, user.Id.ToString()) };
                claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name)));
                
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: config.issuer,
                    audience: config.audience,
                    notBefore: DateTime.UtcNow,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.key)), 
                        SecurityAlgorithms.HmacSha256));
                
                return new ApiResult("Ok", HttpStatusCode.OK, new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
            }
            else
            {
                return new ApiResult("Incorrect login or password", HttpStatusCode.BadRequest);
            }
        }
        else
        {
            return new ApiResult("Data has errors", HttpStatusCode.BadRequest, result.Errors);
        }
    }

    public async Task<ApiResult> AddToCartAsync(Guid userId, List<Guid> productsId)
    {
        try
        {
            User user = await _repository.GetUserAsync(userId);
            List<Product> products = (await _productRepository.GetAllProductsAsync())
                .Where(p => productsId.Contains(p.Id)).ToList();
            user.ShoppingCart.Products.AddRange(products);
            user = await _repository.UpdateUserAsync(user);

            return new ApiResult("Products have been added to cart", (HttpStatusCode)204, user);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, (HttpStatusCode)500);
        }
    }
}