using AutoMapper;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Repository.Uow;

namespace MomAndBaby.Service.MessageCommunication
{
    public class MessageService : IMessageService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public MessageService(IMapper mapper)
		{
			_mapper = mapper;
			_unitOfWork = new UnitOfWork();
		}

		public async Task<Message> SendMessage(Guid senderId, Guid receiverId, string content)
		{
			Message message = new()
			{
				ReceiverId = receiverId,
				Content = content,
				SenderId = senderId,
				IsSystem = await _unitOfWork.UserRepository.IsStaff(senderId),
				CreatedAt = DateTime.Now
			};

			await _unitOfWork.MessageRepository.AddMessage(message);
			
			if(await _unitOfWork.SaveChangesAsync())
			{
				return message;
			}

			return null!;

		}

		public IEnumerable<MessageDTO> GetMessages(Guid userId)
		{
			var messages = _unitOfWork.MessageRepository.GetMessageCommunication(userId);

			return _mapper.Map<IEnumerable<MessageDTO>>(messages);
		}

	}
}
