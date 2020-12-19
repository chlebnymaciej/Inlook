using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
namespace Inlook_Infrastructure.Services
{
    public class NotificationService : INotificationService
    { 

        public Guid _userID;
        bool started = false;
        public async System.Threading.Tasks.Task<GetNotificationModel> getNotificationAsync(Guid userID)
        {
            return new GetNotificationModel();       
        }

        public async System.Threading.Tasks.Task sendNotificationAsync(PostNotificationModel postNotificationModel)
        {
            foreach(var item in postNotificationModel.RecipientsList)
            {
                await sendOneNotificationAsync(item);
            }
        }
        private async System.Threading.Tasks.Task sendOneNotificationAsync(string s)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://mini-notification-service.azurewebsites.net/notifications"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");
                    request.Headers.TryAddWithoutValidation("x-api-key", "db5bddca-c759-419e-bc0d-65d8dd2cad42");

                    request.Content = new StringContent($"{{\"content\":\"string\",\"contentType\":\"string\",\"recipientsList\":[\"{s}\"] ,\"withAttachments\":true}}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }
        
    }
}