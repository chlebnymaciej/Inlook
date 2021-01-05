using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Inlook_API.Extensions
{
    public static class BaseControllerExtension
    {
        public static Guid GetUserId(this ControllerBase controllerBase)
        {
            if (controllerBase.User == null) 
                return new Guid("2884a694-6a60-4e87-9477-6bd589106ab2");
            var userId = controllerBase.User.Claims.First(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            
            return Guid.Parse(userId);
        }
    }
}
