using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography.X509Certificates;

namespace SignalR
{
    public class Publisher:Hub
    {
        private readonly IAppService _appService;
        public Publisher(IAppService appService)
        {
            _appService = appService;
        }
        public async Task Notify(string userId,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", userId, message);
        }
    }
}
