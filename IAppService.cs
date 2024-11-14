namespace SignalR
{
    public interface IAppService
    {
        public Task Notify(string userId,string message);
        public Task AddEvent();
        public Task RemoveEvent(string eventId);

    }
}
