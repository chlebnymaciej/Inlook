using Inlook_API.Extensions;
using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            this.attachmentService = attachmentService;
        }

        [HttpGet("GetAttachment")]
        public IActionResult GetAttachment(Guid id)
        {
            var file = attachmentService.GetFile(id);

            return new JsonResult(file);
        }


        [HttpPost("UploadAttachment")]
        public async Task<IActionResult> UploadAttachment(IFormFile file)
        {
            var fileInfo = await attachmentService.UploadFile(file.OpenReadStream(), file.FileName);
            return new JsonResult(fileInfo);
        }

        [HttpPost("DeleteAttachment")]
        public async Task<IActionResult> DeleteAttachment(Guid id)
        {
            await attachmentService.DeleteAttachment(id);
            return NoContent();
        }
    }
}

