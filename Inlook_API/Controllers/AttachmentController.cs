using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models.Attachments;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class AttachmentController : BaseController
    {
        private readonly IAttachmentService attachmentService;


        public AttachmentController(ILogger<AttachmentController> logger, TelemetryClient telemetryClient, IAttachmentService attachmentService) : base(logger, telemetryClient)
        {
            this.attachmentService = attachmentService;
        }

        /// <summary>
        /// Gets file by its Id
        /// </summary>
        /// <param name="id">Id of file</param>
        /// <response code="200">File of given Id</response>
        [ProducesResponseType(typeof(FileStreamResult), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [HttpGet("GetFile")]
        public async Task<IActionResult> GetFile(Guid id)
        {
            var file = await attachmentService.GetFile(id);

            if (file == null) return NotFound();

            return File(file.FileStream, file.ContentType);
        }

        /// <summary>
        /// Uploads attachment to database
        /// </summary>
        /// <param name="file">Attachment file</param>
        [SwaggerResponse((int)HttpStatusCode.OK, "Attachment model", typeof(IEnumerable<GetAttachmentModel>))]
        [Produces("application/json")]
        [HttpPost("UploadAttachment")]
        public async Task<IActionResult> UploadAttachment(IFormFile file)
        {
            var attachmentModel = await attachmentService.UploadFile(file.OpenReadStream(), file.FileName);
            return new JsonResult(attachmentModel);
        }

        /// <summary>
        /// Deletes attachment of given Id
        /// </summary>
        /// <param name="id">Id of attachment to be deleted</param>
        ///  <response code="204">Success indicator</response>
        [HttpDelete("DeleteAttachment")]
        public async Task<IActionResult> DeleteAttachment(Guid id)
        {
            await attachmentService.DeleteAttachment(id);
            return NoContent();
        }
    }
}

