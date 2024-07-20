using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Repository.Repository
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAll();
        Task AddNotification(Notification notification);
    }
}
