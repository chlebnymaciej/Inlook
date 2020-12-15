using System;
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
        public async System.Threading.Tasks.Task<GetNotificationModel> getNotificationAsync(Guid userID)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://mini-notification-service.azurewebsites.net/notifications/16"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");
                    request.Headers.TryAddWithoutValidation("x-api-key", "db5bddca-c759-419e-bc0d-65d8dd2cad42");

                    var response = await httpClient.SendAsync(request);
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(await response.Content.ReadAsStringAsync());
                    }
                    return new GetNotificationModel();
                }
            }
        }

        public async System.Threading.Tasks.Task sendNotificationAsync(PostNotificationModel postNotificationModel)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://mini-notification-service.azurewebsites.net/notifications"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");
                    request.Headers.TryAddWithoutValidation("x-api-key", "db5bddca-c759-419e-bc0d-65d8dd2cad42");

                    request.Content = new StringContent("{\"content\":\"string\",\"contentType\":\"string\",\"recipientsList\":[\"string\"],\"withAttachments\":true}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }
        
    }
}