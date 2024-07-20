using Microsoft.Extensions.DependencyInjection;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.Service.BackgroundTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Worker
{
    public class NotificationWorker
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NotificationWorker(IBackgroundTaskQueue taskQueue, IServiceScopeFactory serviceScopeFactory)
        {
            _taskQueue = taskQueue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void DoWork(Guid userId, string tableName, NotificationType type)
        {
            _taskQueue.QueueBackgroundWorkItem(async token =>
            {
                await SendNotification(userId, tableName, type);
            });
        }

        public async Task SendNotification(Guid userId, string tableName, NotificationType type)
        {
            var userService = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IUserService>();

            var user = await userService.getUserById(userId);

            var userName = user.Username == null ? user.FullName : user.Username;

            var notificationService = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<INotificationService>();

            await notificationService.AddNotification(userId, userName, tableName, type);

        }

    }
}
