using AutoMapper;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models.MessageModel;
using MomAndBaby.Repository.Uow;
using MomAndBaby.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationDTO>> GetNotifications()
        {
            return _mapper.Map<IEnumerable<NotificationDTO>>(await _unitOfWork.NotificationRepository.GetAll());
        }

        public async Task AddNotification(Guid userId, string userName, string tableName, NotificationType type)
        {
            var notification = new Notification
            {
                UserId = userId,
                TypeMessage = type.ToString(),
                Title = $"{userName} have been {type.ToString()}",
                Message = $"{userName} have been {type.ToString()} in {tableName}",
                IsRead = false,
                CreatedAt = TimeHelper.GetCurrentInVietName()
            };

            await _unitOfWork.NotificationRepository.AddNotification(notification);
        }

    }
}
