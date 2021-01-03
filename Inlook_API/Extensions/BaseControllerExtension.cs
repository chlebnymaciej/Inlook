﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Inlook_API.Extensions
{
    public static class BaseControllerExtension
    {
        public static Guid GetUserId(this ControllerBase controllerBase)
        {
            var userId = controllerBase.User.Claims.First(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            return Guid.Parse(userId);
        }
    }
}
