using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.ViewModels;


namespace TodoBackend.Api.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class RolesController : ControllerBase
    {
        private IRolesService _rolesService;
        private readonly ILogger<RolesController> _logger;

        public RolesController(ILogger<RolesController> logger, IRolesService rolesService)
        {
            _logger = logger;
            _rolesService = rolesService;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            try
            {
                var result = _rolesService.GetAllRoles();
                if (result == null)
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetRole(Guid guid)
        {
            try
            {
                var result = await _rolesService.GetRoleByGuid(guid);
                if (result == null)
                {
                    return NoContent();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel roleView)
        {
            if (roleView != null)
            {
                try
                {
                    var newRole = await _rolesService.CreateRole(roleView);
                    return CreatedAtAction(nameof(GetRoles), new { UniqueId = newRole.UniqueId }, newRole);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("Role creation failed.");
        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateRole(Guid guid, RoleViewModel roleView)
        {
            if (roleView != null)
            {
                try
                {
                    var updatedRole = await _rolesService.UpdateRole(guid, roleView);
                    if (updatedRole != null)
                    {
                        return Ok(updatedRole);
                    }
                    else
                    {
                        return BadRequest($"Role with {guid} not found.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("Role is null");
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteRole(Guid guid)
        {
            try
            {
                if (await _rolesService.DeleteRole(guid))
                {
                    return NoContent();
                }
                else
                {
                    // 401 if not authorised, else 404. 404 for now knowing we will
                    // have authentication eventually.
                    return NotFound("id does not exist");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
