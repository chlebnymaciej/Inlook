using Inlook_API.Extensions;
using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class GroupController : BaseController
    {
        private readonly IGroupService groupService;
        public GroupController(ILogger<GroupController> logger, TelemetryClient telemetryClient, IGroupService groupService) : base(logger, telemetryClient)
        {
            this.groupService = groupService;
        }

        /// <summary>
        /// Gets groups owned by calling user
        /// </summary>
        /// <response code="200">List of groups that user owns.Groups contains info about members</response>
        [ProducesResponseType(typeof(List<GetUserGroupModel>), 200)]
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

        /// <summary>
        /// Creates a new group, with caller as owner
        /// </summary>
        /// <param name="createGroupModel"></param>
        ///  <response code="204">Success indicator</response>
        [HttpPost("PostGroup")]
        public IActionResult PostGroup([FromBody] PostGroupModel createGroupModel)
        {
            var userId = this.GetUserId();
            this.groupService.AddGroup(createGroupModel, userId);
            return NoContent();
        }

        /// <summary>
        /// Deletes group of given Id
        /// </summary>
        /// <param name="id">Id of group to delete</param>
        ///  <response code="204">Success indicator</response>
        [HttpDelete("DeleteGroup")]
        public IActionResult DeleteGroup([FromBody] string id)
        {
            var userId = this.GetUserId();
            Guid group = Guid.Parse(id);
            this.groupService.Delete(group);
            return NoContent();
        }

        /// <summary>
        /// Updates data about group
        /// </summary>
        /// <param name="model">Group update model</param>
        ///  <response code="204">Success indicator</response>
        [HttpPut("UpdateGroup")]
        public IActionResult UpdateGroup([FromBody] UpdateGroupModel model)
        {
            var userId = this.GetUserId();
            this.groupService.UpdateGroup(model, userId);
            return NoContent();
        }
    }
}

