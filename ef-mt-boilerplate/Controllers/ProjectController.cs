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
        public IActionResult GetProjects()
        {
            // This will return only projects for the current tenant due to global query filter
            var projects = _dbContext.Projects.IgnoreQueryFilters().ToList();

            // If you want to access the tenant id explicitly:
            var tenantId = _tenantService.GetCurrentTenantId();

            return Ok(new { tenantId, projects });
        }
    }
}


