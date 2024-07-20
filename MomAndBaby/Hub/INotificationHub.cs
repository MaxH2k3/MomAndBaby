using MomAndBaby.BusinessObject.Models.MessageModel;

namespace MomAndBaby
{
    public interface INotificationHub
    {
        Task ReceivedNotificaiton(IEnumerable<NotificationDTO> notifications);
    }
}
