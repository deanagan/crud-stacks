using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Models;


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
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var result = await _rolesService.GetAllRoles();
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
        public IActionResult CreateRole(Role role)
        {
            if (role != null)
            {
                try
                {
                    var newRole = _rolesService.CreateRole(role);
                    return CreatedAtAction(nameof(GetRoles), new { Id = newRole.Id }, newRole);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("Role creation failed.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(Guid guid, Role role)
        {
            if (role != null)
            {
                try
                {
                    var updatedRole = _rolesService.UpdateRole(guid, role);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(Guid guid)
        {
            try
            {
                if (_rolesService.DeleteRole(guid))
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
