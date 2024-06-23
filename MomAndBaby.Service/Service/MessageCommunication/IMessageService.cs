using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;

namespace MomAndBaby.Service.MessageCommunication
{
    public interface IMessageService
	{
		Task<IEnumerable<MessageDTO>> GetMessages(Guid userId);
		Task<Message> SendMessage(Guid senderId, Guid receiverId, string content);
	}
}
