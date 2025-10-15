using Microsoft.AspNetCore.Mvc;
using ef_mt_boilerplate.Services;
using ef_mt_boilerplate.Data;
using ef_mt_boilerplate.Entities;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _dbContext;

    public AuthController(ITokenService tokenService, ApplicationDbContext dbContext)
    {
        _tokenService = tokenService;
        _dbContext = dbContext;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Validate user credentials (this is just an example)
        try
        {
            var user = _dbContext.Users.IgnoreQueryFilters()
                .FirstOrDefault(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized();

            var token = _tokenService.GenerateToken(user, user.TenantId);
            return Ok(new { token });
        }

        catch (Exception e)
        {
            return null;
        }
    }
}
