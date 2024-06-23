using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MomAndBabyContext _context;

        public MessageRepository(MomAndBabyContext context)
        {
            _context = context;
        }

        public async Task AddMessage(Message message)
        {
            await _context.Messages.AddAsync(message);

        }

        public async Task<IEnumerable<Message>> GetMessageCommunication(Guid userId)
        {
            return await _context.Messages.Where(x => x.SenderId.Equals(userId) || x.ReceiverId.Equals(userId)).ToListAsync();
		}


	}
}
