using ef_mt_boilerplate.Data;
using ef_mt_boilerplate.Services;
using ef_mt_boilerplate.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using System.Text;
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
        public async Task<IActionResult> GetProjects()
        {
            // This will return only projects for the current tenant due to global query
            
            var tenantId = _tenantService.GetCurrentTenantId();
            var projects = await _dbContext.Projects.ToListAsync();

            // If you want to access the tenant id explicitly:

            return Ok(new { tenantId, projects });
        }
    }
}



