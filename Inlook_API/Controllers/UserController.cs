using Inlook_API.Extensions;
using Inlook_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetGroups()
        {
            var userId = this.GetUserId();
            
            
            List<User> tmp = new List<User>();
            tmp.Add(new User
            {
                Mail = "palpatine@sith.pis"   
            });
            tmp.Add(new User
            {
                Mail = "mariusz@pudzian.pl"  
            });
            tmp.Add(new User
            {
                Mail = "general.grivous@sith.pis" 
            });
            tmp.Add(new User
            {
                Mail = "adam_malysz102m@wp.pl" 
            });
            tmp.Add(new User
            {
                Mail = "andrzej@duda.pis" 
            });

            tmp.Add(new User
            {
                Mail = "ziobro@ty.ku"
            });
            return new JsonResult(tmp);
        }

       
    }
}

