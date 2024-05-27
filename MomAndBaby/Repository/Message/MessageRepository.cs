using MomAndBaby.Entity;

namespace MomAndBaby.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private MomAndBabyContext _context;

        public MessageRepository(MomAndBabyContext context)
        {
            _context = context;
        }

        
    }
}
