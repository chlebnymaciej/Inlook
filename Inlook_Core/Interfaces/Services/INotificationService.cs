using Inlook_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlook_Core.Interfaces.Services
{
    public interface INotificationService
    {
        public  System.Threading.Tasks.Task sendNotificationAsync(PostNotificationModel postNotificationModel);

        public  System.Threading.Tasks.Task<GetNotificationModel> getNotificationAsync(Guid userID);
    }
}
