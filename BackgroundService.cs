
using Microsoft.AspNetCore.SignalR;
using NCrontab;
using Serilog;
using Serilog.Core;
using System.Data;

namespace SignalR
{
    public class AppBackgroundService : BackgroundService
    {
        private readonly IHubContext<Publisher> _hubContext;

        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly string _cronExpression = "*/1 * * * *"; // Every day at midnight
        public AppBackgroundService(IHubContext<Publisher> hubContext)
        {
            _hubContext = hubContext;
            _schedule = CrontabSchedule.Parse(_cronExpression);
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Start");
            Log.Information("Background service is starting.");
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;

                if (now >= _nextRun)
                {
                    Console.WriteLine("update");
                    var result =await UpdateResource();
                    await _hubContext.Clients.All.SendAsync("UpdateTime", result);
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                Log.Information("Background service is starting.");

                await Task.Delay(1000, stoppingToken);
            }
        }
        private async Task<string> UpdateResource()
        {

            await Task.Delay(50); // Simulating the update task's delay
            return DateTime.Now.ToString();  
        }
        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Information("Background service is stopping.");
            await base.StopAsync(cancellationToken);
        }
    }
}
