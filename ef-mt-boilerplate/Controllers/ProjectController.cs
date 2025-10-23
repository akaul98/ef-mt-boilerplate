using ef_mt_boilerplate.Data;
using ef_mt_boilerplate.Entities;
using ef_mt_boilerplate.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ef_mt_boilerplate.Controllers
{
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITenantService _tenantService;

        public ProjectController(ApplicationDbContext dbContext, ITenantService tenantService)
        {
            _dbContext = dbContext;
            _tenantService = tenantService;
        }

        [HttpGet("api/projects")]
        public IActionResult GetProjects()
        {
            // This will return only projects for the current tenant due to global query filter
            var projects = _dbContext.Projects.ToList();

            // If you want to access the tenant id explicitly:
            var tenantId = _tenantService.GetCurrentTenantId();

            return Ok(new { tenantId, projects });
        }

        // Admin-only endpoint to create a project. OwnerUserId is taken from the caller's user id claim.
        [HttpPost("api/projects")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateProject([FromBody] CreateProjectRequest request)
        {
            if (request == null)
                return BadRequest("Request body is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var principal = HttpContext.User;
            if (principal?.Identity?.IsAuthenticated != true)
                return Unauthorized();

            // Try common claim locations for user id
            var userIdClaim =
                principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                ?? principal.FindFirst("UserId")?.Value;

            if (!int.TryParse(userIdClaim, out var ownerUserId) || ownerUserId == 0)
                return BadRequest("UserId claim missing or invalid.");

            // Create project and set OwnerUserId from claim.
            var project = new Project
            {
                Title = request.Title,
                OwnerUserId = ownerUserId
            };

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            // Build minimal response (adjust to your CreateProjectResponse model)
            var response = new CreateProjectResponse
            {
                ProjectId = project.ProjectId,
                Title = project.Title,
                OwnerUserId = project.OwnerUserId,
                TenantId = project.TenantId,
                Status="Success",
                Message="Project Added Successfully"
            };

            // Return 201 with location to the collection (you can change to CreatedAtAction for a single resource endpoint)
            return CreatedAtAction(nameof(GetProjects), new { id = project.ProjectId }, response);
        }
    }
}





