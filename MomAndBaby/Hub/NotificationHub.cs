using Microsoft.AspNetCore.SignalR;
using MomAndBaby.Service;

namespace MomAndBaby
{
    public class NotificationHub : Hub<INotificationHub>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NotificationHub(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task SendNotification()
        {
            var _notificationService = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<INotificationService>();
            var notifs = await _notificationService.GetNotifications();
            await Clients.All.ReceivedNotificaiton(notifs);
        }
    }
}
