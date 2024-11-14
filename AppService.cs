
using Microsoft.AspNetCore.SignalR;

namespace SignalR
{
    public class AppService : IAppService
    {
        public AppService()
        {
        }

        public Task AddEvent()
        {
            throw new NotImplementedException();
        }

        public Task Notify(string userId, string message)
        {
            throw new NotImplementedException();

        }

        public Task RemoveEvent(string eventId)
        {
            throw new NotImplementedException();
        }

    }
}
