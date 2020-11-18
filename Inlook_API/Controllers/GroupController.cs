using Inlook_API.Extensions;
using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet("GetMyGroups")]
        public IActionResult GetGroups()
        {
            var userId = this.GetUserId();

            var groups = groupService.GetAllGroups(userId);

            List<GetUserGroupModel> resultsList = new List<GetUserGroupModel>();
            foreach (Group item in groups)
            {
                List<GetUserWithIdModel> listusers = new List<GetUserWithIdModel>();
                foreach (UserGroup user in item.UserGroups)
                {
                    listusers.Add(new GetUserWithIdModel()
                    {
                        Name = user.User.Name,
                        Email = user.User.Email,
                        Id = user.User.Id
                    });
                }
                resultsList.Add(new GetUserGroupModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Users = listusers.ToArray()
                });
            }

            return new JsonResult(resultsList);
        }


        [HttpPost("PostGroup")]
        public IActionResult PostGroup([FromBody] PostGroupModel createGroupModel)
        {
            var userId = this.GetUserId();
            this.groupService.AddGroup(createGroupModel, userId);
            return NoContent();
        }

        [HttpDelete("DeleteGroup")]
        public IActionResult DeleteGroup([FromBody] string id)
        {
            var userId = this.GetUserId();
            Guid group = Guid.Parse(id);
            this.groupService.Delete(group);
            return NoContent();
        }

        [HttpPut("UpdateGroup")]
        public IActionResult UpdateGroup([FromBody] UpdateGroupModel model)
        {
            var userId = this.GetUserId();
            this.groupService.UpdateGroup(model, userId);
            return NoContent();
        }
    }
}

