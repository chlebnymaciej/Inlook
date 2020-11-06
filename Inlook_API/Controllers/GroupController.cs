using Inlook_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class GroupController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public GroupController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetGroups()
        {
            List<Group> tmp = new List<Group>();
            tmp.Add(new Group() { Name = "Sithowie" });
            tmp.Add(new Group() { Name = "Rycerze jedi" });
            tmp.Add(new Group() { Name = "Potężni polacy" });
            return new JsonResult(tmp);
        }


        [HttpPost]
        public IActionResult PostGroup([FromBody] CreateGroupModel createGroupModel)
        {
            // service
            return NoContent();
        }
    }
}

