using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Repository.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly MomAndBabyContext _context;

        public NotificationRepository(MomAndBabyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetAll()
        {
            return await _context.Notifications
                .Include(n => n.User)
                .ToListAsync();
        }
    }
}
