using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Inlook_API.Extensions
{
    public static class BaseControllerExtension
    {
        public static Guid GetUserId(this ControllerBase controllerBase)
        {
            var userId = controllerBase.User.Claims.First(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            return Guid.Parse(userId);
        }

        public static bool IsModelValid(this ControllerBase controllerBase, object model, out string errorMessage)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(model, context, validationResults, true);
            if (!isValid)
            {
                var errorMessages = validationResults.Select(vr => vr.ErrorMessage);
                errorMessage = string.Join(". ", errorMessages);
                return false;
            }
            errorMessage = "";
            return true;
        }
    }
}
