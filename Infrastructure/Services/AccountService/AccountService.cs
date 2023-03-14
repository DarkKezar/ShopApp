using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Core.Models;
using Core.Repositories.ProductRepository;
using Core.Repositories.UserRepository;
using FluentValidation.Results;
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
    private readonly IMapper _mapper;

    public AccountService(IUserRepository repository, IProductRepository productRepository, IMapper mapper)
    {
        _repository = repository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> RegisterUserAsync(CreateAccountTO model)
    {
        ValidationResult result = await (new CreateAccountTOValidator()).ValidateAsync(model);
        if (result.IsValid)
        {
            User user = new User(model.Name, model.Login);
            try
            {
                user = await _repository.CreateUserAsync(user, model.Password);
            }
            catch (NpgsqlException e)
            {
                return new ObjectResult(e);
            }
            return new ObjectResult(user);
        }
        else
        {
            return new ObjectResult(result.Errors);
        }
    }

    public async Task<IActionResult> AuthorizeUserAsync(AuthorizeAccountTO model, JWTConfig config)
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
                
                return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
            }
            else
            {
                return new ObjectResult("Error: incorrect login or password");
            }
        }
        else
        {
            return new BadRequestObjectResult(result.Errors);
        }
    }

    public async Task<IActionResult> AddToCartAsync(Guid userId, List<Guid> productsId)
    {
        try
        {
            User user = await _repository.GetUserAsync(userId);
            List<Product> products = (await _productRepository.GetAllProductsAsync())
                .Where(p => productsId.Contains(p.Id)).ToList();
            user.ShoppingCart.Products.AddRange(products);
            user = await _repository.UpdateUserAsync(user);

            return new OkObjectResult(user);
        }
        catch (NpgsqlException e)
        {
            return new ObjectResult(e.Message);
        }
    }
}