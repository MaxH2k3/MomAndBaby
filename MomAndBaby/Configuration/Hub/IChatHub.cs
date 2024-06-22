using MomAndBaby.Entity;
using MomAndBaby.Models.MessageModel;

namespace MomAndBaby.Configuration.Hub
{
    public interface IChatHub
    {
        /// <summary>
        /// Send message to user
        /// </summary>
        /// <param name="user">User recieved</param>
        /// <param name="message">Content of message</param>
        /// <returns></returns>
        Task SendMessageToUser(Message message);

        /// <summary>
        /// Load message from database to frontend
        /// </summary>
        /// <param name="messageDTOs"></param>
        /// <returns></returns>
        Task LoadMessage(IEnumerable<MessageDTO> messageDTOs);
    }
}
