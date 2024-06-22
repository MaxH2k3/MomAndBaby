using Microsoft.AspNetCore.SignalR;
using MomAndBaby.Configuration.Uow;
using MomAndBaby.Entity;
using MomAndBaby.Service.MessageCommunication;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace MomAndBaby.Configuration.Hub
{
    public class ChatHub : Hub<IChatHub>
    {
        private IEnumerable<Claim> Claims => Context.User!.Claims;

        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
			_messageService = messageService;
		}

        public async Task ChatAsysnc(Guid receiverId, string content)
        {
			// Get userId from cookie
			string userId = Claims.ElementAt(0).Value;

			// Add message to database
			var tempMessage = await _messageService.SendMessage(Guid.Parse(userId), receiverId, content);

			// Send message
			await Clients.Group(userId).SendMessageToUser(tempMessage);

		}

		public async Task GetMessageAsync(Guid userId)
		{
			var messages = await _messageService.GetMessages(userId);

			await Clients.Group(userId.ToString()).LoadMessage(messages);
		}


    }
}
