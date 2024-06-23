using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IMessageRepository
    {
		Task AddMessage(Message message);
		Task<IEnumerable<Message>> GetMessageCommunication(Guid userId);

	}
}
