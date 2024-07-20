﻿using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.MessageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDTO>> GetNotifications();
    }
}
