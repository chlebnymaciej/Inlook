using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inlook_Core.Models;

namespace Inlook_Core.Interfaces.Services
{
    public interface INotificationService
    {
        public Task SendNotificationAsync(PostNotificationModel postNotificationModel);

        public GetNotificationModel GetNotification(Guid userID);
    }
}
